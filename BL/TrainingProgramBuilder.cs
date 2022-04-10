using AutoMapper;
using Gym.BL.Mapping;
using Gym.BL.Models;
using Gym.Common.Enum;
using Gym.DAL;
using Gym.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gym.BL
{
    public class TrainingProgramBuilder
    {
        private List<MuscleBL> MuscleListToWork;
        //private List<ExerciseBL> ResultingExercises;

        private List<ExerciseBL> DbExercises;
        private List<MuscleBL> DbMuscles;
        private List<EquipBL> DbEquipment;

        private readonly IMapper _mapper;

        public TrainingProgramBuilder()
        {
            GetDataFromDAL dataDAL = new();
            DbExercises = dataDAL.GetExercises();
            DbMuscles = dataDAL.GetMuscles();
            DbEquipment = dataDAL.GetEquipment();
            MuscleListToWork = new();//questionable
            //ResultingExercises = new();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileBL>()); //is it legit?
            _mapper = new Mapper(mapperConfig);
        }

        public TrainingProgramDisplayPR Calculate(int[] idList, int intensity, out bool success)
        {
            MuscleBL? muscleBL;
            foreach (int id in idList)
            {
                muscleBL = DbMuscles.FirstOrDefault(m => (int)m.MuscleType == id);
                if (muscleBL is null)
                {
                    success = false;
                    return new TrainingProgramDisplayPR(); //change to return resulting exercises later
                }
                else
                {
                    MuscleListToWork.Add(muscleBL);
                }
            }
            TrainingProgramDisplayPR trainingProgram = new();

            switch (intensity)
            {
                case 1:
                    trainingProgram = FullBodySplit();
                    break;
                case 2:
                    trainingProgram = UpperLowerSplit();
                    break;
                case 3:
                    PushPullLegSplit();
                    break;
                default:
                    break;
            };

            success = true;
            return trainingProgram;
            
        }

        private TrainingProgramDisplayPR FullBodySplit()
        {
            int exerciseCount = 10;
            List<ExerciseBL> fullBodySplitExercises = new();
            
            fullBodySplitExercises = GetMinimumCompoundList(exerciseCount, MuscleListToWork); //adds minimum compound exercises           
            fullBodySplitExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, fullBodySplitExercises, MuscleListToWork); //adds primary exercises to muscles that don't have them

            List<ExercisePR> mappedFullBodyExercises = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(fullBodySplitExercises);
            TrainingDayPR fullBodyDay = new() { DayName = "Full Body Day", Exercises = mappedFullBodyExercises };
            List<TrainingDayPR> trainingDayList = new();
            for (int i = 0; i < 3; i++)
            {
                trainingDayList.Add(fullBodyDay);
            }
            TrainingProgramDisplayPR fullBodyProgram = new() { trainingDayList = trainingDayList, DaysBetweenMessage = "2-4 days in-between" };
            return fullBodyProgram;
        }

        private TrainingProgramDisplayPR UpperLowerSplit()
        {           
            List<MuscleBL> upperMuscles = MuscleListToWork.FindAll(m => m.BodySectionType == BodySectionType.Upper);
            List<MuscleBL> lowerMuscles = MuscleListToWork.FindAll(m => m.BodySectionType == BodySectionType.Lower);
            int exerciseCount = upperMuscles.Count == 0 || lowerMuscles.Count == 0 ? 20 : 10;

            TrainingDayPR dayOne;
            TrainingDayPR dayTwo;
            List<TrainingDayPR> upperLowerTrainingDayList = new();
            TrainingProgramDisplayPR upperLowerSplitProgram;

            if (upperMuscles.Count == 0 || lowerMuscles.Count == 0)
            {
                List<MuscleBL> musclesToSend = upperMuscles.Count == 0 ? lowerMuscles : upperMuscles;
                List<ExerciseBL> upperLowerSplitExercises = GetMinimumCompoundList(exerciseCount, musclesToSend);
                upperLowerSplitExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, upperLowerSplitExercises, musclesToSend);
                List <ExercisePR> mappedUpperLowerSplitExercises = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(upperLowerSplitExercises);

                dayOne = new() { DayName = upperMuscles.Count == 0 ? "Lower Day" : "Upper Day", Exercises = mappedUpperLowerSplitExercises };
                for (int i = 0; i < 3; i++)
                {
                    upperLowerTrainingDayList.Add(dayOne);
                }              
            }
            else
            {
                List<ExerciseBL> upperExercises = GetMinimumCompoundList(exerciseCount, upperMuscles);
                List<ExerciseBL> lowerExercises = GetMinimumCompoundList(exerciseCount, lowerMuscles);        
                upperExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, upperExercises, upperMuscles);
                lowerExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, lowerExercises, lowerMuscles);
                List<ExercisePR> mappedUpperExercises = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(upperExercises);
                List<ExercisePR> mappedLowerExercises = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(lowerExercises);

                if (upperExercises.Count >= lowerMuscles.Count)
                {
                    dayOne = new() { DayName = "Upper Day", Exercises = mappedUpperExercises };
                    dayTwo = new() { DayName = "Lower Day", Exercises = mappedLowerExercises };
                }
                else
                {
                    dayOne = new() { DayName = "Lower Day", Exercises = mappedLowerExercises };
                    dayTwo = new() { DayName = "Upper Day", Exercises = mappedUpperExercises };
                    
                }
                upperLowerTrainingDayList.Add(dayOne);
                upperLowerTrainingDayList.Add(dayTwo);
                upperLowerTrainingDayList.Add(dayOne);
            } // TODO: add more exercises if count is less than max
            upperLowerSplitProgram = new() { trainingDayList = upperLowerTrainingDayList, DaysBetweenMessage = "1-2 days in-between" };
            return upperLowerSplitProgram;
        }

        private void PushPullLegSplit()
        {

        }

        private List<ExerciseBL> HeartCheck(List<ExerciseBL> exercises, List<MuscleBL> musclesToCheck) //this excludes cardio exercises from selection list if heart is not selected as a muscle to train
        {
            MuscleBL? heart = DbMuscles.FirstOrDefault(m => m.MuscleType == MuscleType.Heart);
            if (heart != null && !musclesToCheck.Contains(heart))
            {
               exercises.RemoveAll(e => e.PrimaryMuscleList.Contains(heart));
            }
            return exercises;
        }

        private List<ExerciseBL> GetMinimumCompoundList(int maxExerciseAmount, List<MuscleBL> musclesToWork)//this function is supposed to cover all the selected muscles with mostly compound exercises
        {          
            List<ExerciseBL> compoundExercises = new();
            List<MuscleBL> musclesToRemove = new();
            List<MuscleBL> musclesUnprocessed = new();
            musclesUnprocessed.AddRange(musclesToWork);
            List<ExerciseBL> exercisesUnprocessed = new();
            exercisesUnprocessed.AddRange(DbExercises); 

            //cardio check
            exercisesUnprocessed = HeartCheck(exercisesUnprocessed, musclesUnprocessed);

            MuscleBL loopedMuscle;
            List<ExerciseBL> foundExercises = new();
            List<MuscleBL> unionList = new();
            List<MuscleBL> intersectionList = new();


            //this loop looks for the fattest essential compounds
            for (int i = 0; i < musclesUnprocessed.Count; i++)
            {
                loopedMuscle = musclesUnprocessed[i];
                if (musclesToRemove.Contains(loopedMuscle))
                {
                    continue;
                }
                foundExercises = exercisesUnprocessed.Where(e => e.PrimaryMuscleList.Contains(loopedMuscle)).ToList();

                foreach (var foundExercise in foundExercises)
                {
                    if (foundExercise is not null)
                    {
                        intersectionList = musclesUnprocessed.Intersect(foundExercise.PrimaryMuscleList).ToList();
                        if (intersectionList.Count > 1 && foundExercise.IsEssential)
                        {
                            compoundExercises.Add(foundExercise);
                            musclesToRemove.AddRange(foundExercise.PrimaryMuscleList);
                            musclesToRemove.AddRange(foundExercise.SecondaryMuscleList);
                            exercisesUnprocessed.Remove(foundExercise);
                            break;
                        }
                    }
                    if (compoundExercises.Count >= maxExerciseAmount)
                    {
                        return compoundExercises;
                    }
                    continue;      
                }   
            }
            musclesToRemove.Distinct(); //removes all muscles that were already processed in the first loop
            musclesUnprocessed.RemoveAll(m => musclesToRemove.Contains(m));



            //this looks for any exercises for the rest of the muscles that weren't processed in the first loop
            for (int i = 0; i < musclesUnprocessed.Count; i++)
            {
                loopedMuscle = musclesUnprocessed[i];
                foundExercises = exercisesUnprocessed.Where(e => e.PrimaryMuscleList.Contains(loopedMuscle)).ToList();

                foreach (var foundExercise in foundExercises)
                {
                    unionList = foundExercise.PrimaryMuscleList.Union(foundExercise.SecondaryMuscleList).ToList();
                    intersectionList = musclesUnprocessed.Intersect(unionList).ToList();
                    if (intersectionList.Count > 0)
                    {
                        compoundExercises.Add(foundExercise);
                        if (compoundExercises.Count >= maxExerciseAmount)
                        {
                            return compoundExercises;
                        }
                        musclesUnprocessed.RemoveAll(m => foundExercise.PrimaryMuscleList.Contains(m));
                        musclesUnprocessed.RemoveAll(m => foundExercise.SecondaryMuscleList.Contains(m));
                        exercisesUnprocessed.Remove(foundExercise);
                        i = -1;
                        break;
                    }
                }
            }
            return compoundExercises;
        }

        private List<ExerciseBL> GetExercisesForMusclesWithoutPrimary(int maxExerciseAmount, List<ExerciseBL> preparedExerciseList, List<MuscleBL> musclesToWork) //this function looks for primary exercises for muscles that don't have primary yet
        {
            int amountOfExercisesToAdd = maxExerciseAmount - preparedExerciseList.Count;
            if (amountOfExercisesToAdd == 0)
            {
                return preparedExerciseList;
            }
            List<ExerciseBL> exercisesUnprocessed = DbExercises.Except(preparedExerciseList).ToList();
            List<MuscleBL> musclesWithoutPrimary = new();
            List<MuscleBL> primaryMusclesFromChosenExercises = new();
            List<ExerciseBL> resultExerciseList = new();
            List<MuscleBL> muscleList = new();
            muscleList.AddRange(musclesToWork);

            ExerciseBL? foundExercise; 
            MuscleBL loopedMuscle;
            List<MuscleBL> intersectionList = new();

            foreach (var exercise in preparedExerciseList) 
            {
                primaryMusclesFromChosenExercises.AddRange(exercise.PrimaryMuscleList);
            }
            primaryMusclesFromChosenExercises.Distinct();
            musclesWithoutPrimary = muscleList.Except(primaryMusclesFromChosenExercises).ToList();

            exercisesUnprocessed = HeartCheck(exercisesUnprocessed, musclesWithoutPrimary);

            for (int i = 0; i < musclesWithoutPrimary.Count; i++)
            {
                loopedMuscle = musclesWithoutPrimary[i];
                foundExercise = exercisesUnprocessed.FirstOrDefault(e => e.PrimaryMuscleList.Contains(loopedMuscle)); //nullcheck
                if (foundExercise is not null)
                {
                    intersectionList = foundExercise.PrimaryMuscleList.Intersect(musclesWithoutPrimary).ToList();
                    resultExerciseList.Add(foundExercise);
                    musclesWithoutPrimary.RemoveAll(m => intersectionList.Contains(m));
                    exercisesUnprocessed.Remove(foundExercise);
                    i -= intersectionList.Count;
                }
                if (resultExerciseList.Count >= amountOfExercisesToAdd)
                {
                    break;
                }
            }
            preparedExerciseList.AddRange(resultExerciseList);
            return preparedExerciseList;
        }


}
}