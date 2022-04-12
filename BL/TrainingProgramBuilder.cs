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
    public class TrainingProgramBuilder //mapper, method args, similar functions into one maybe with local sub-functions inside?
    {
        private List<MuscleBL> MuscleListToWork;

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
            MuscleListToWork = new();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileBL>()); // ASK: is it legit
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
                    return new TrainingProgramDisplayPR(); 
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
            List<ExerciseBL> dayOneExercises = new();
            List<ExerciseBL> dayTwoExercises = new();
            
            dayOneExercises = GetMinimumCompoundList(exerciseCount, MuscleListToWork, exercisesToChooseFrom: DbExercises); //adds minimum compound exercises           
            dayOneExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, MuscleListToWork, currentExercises: dayOneExercises, exercisesToChooseFrom: DbExercises); //adds primary exercises to muscles that don't have them
            List<ExerciseBL> otherExercises = DbExercises.Except(dayOneExercises).ToList();
            dayTwoExercises = GetMinimumCompoundList(exerciseCount, MuscleListToWork, exercisesToChooseFrom: otherExercises);
            dayTwoExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, MuscleListToWork, currentExercises: dayTwoExercises, exercisesToChooseFrom: otherExercises);

            List<ExercisePR> mappedDayOne = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(dayOneExercises);
            List<ExercisePR> mappedDayTwo = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(dayTwoExercises);
            TrainingDayPR trainingDayOne = new() { DayName = "Full Body Day", Exercises = mappedDayOne };
            TrainingDayPR trainingDayTwo = new() { DayName = "Full Body Day", Exercises = mappedDayTwo };

            List<TrainingDayPR> trainingDayList = new();
            trainingDayList.Add(trainingDayOne);
            trainingDayList.Add(trainingDayTwo);
            trainingDayList.Add(trainingDayOne);

            TrainingProgramDisplayPR fullBodyProgram = new() { trainingDayList = trainingDayList, DaysBetweenMessage = "2-4 days in-between" };
            return fullBodyProgram; 
        }

        private TrainingProgramDisplayPR UpperLowerSplit()
        {           
            List<MuscleBL> upperMuscles = MuscleListToWork.FindAll(m => m.BodySectionType == BodySectionType.Upper);
            List<MuscleBL> lowerMuscles = MuscleListToWork.FindAll(m => m.BodySectionType == BodySectionType.Lower);
            int exerciseCount = 10; 

            TrainingDayPR dayOne;
            TrainingDayPR dayTwo;
            List<TrainingDayPR> upperLowerTrainingDayList = new();
            TrainingProgramDisplayPR upperLowerSplitProgram;

            if (upperMuscles.Count == 0 || lowerMuscles.Count == 0)
            {
                List<MuscleBL> musclesToSend = upperMuscles.Count == 0 ? lowerMuscles : upperMuscles;
                List<ExerciseBL> dayVariationOne = GetMinimumCompoundList(exerciseCount, musclesToSend, DbExercises);
                dayVariationOne = GetExercisesForMusclesWithoutPrimary(exerciseCount, musclesToSend, dayVariationOne, DbExercises);
                if (dayVariationOne.Count < exerciseCount)
                {
                    dayVariationOne = GetIsolated(exerciseCount, musclesToSend, dayVariationOne, DbExercises);
                }

                List<ExerciseBL> otherExercises = DbExercises.Except(dayVariationOne).ToList();
                List<ExerciseBL> dayVariationTwo = GetMinimumCompoundList(exerciseCount, musclesToSend, otherExercises);             
                dayVariationTwo = GetExercisesForMusclesWithoutPrimary(exerciseCount, musclesToSend, dayVariationTwo, otherExercises);
                if (dayVariationTwo.Count < exerciseCount)
                {
                    dayVariationTwo = GetIsolated(exerciseCount, musclesToSend, dayVariationTwo, otherExercises);
                }
                List<ExercisePR> mappedVariationOne = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(dayVariationOne);
                List<ExercisePR> mappedVariationTwo = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(dayVariationTwo);

                dayOne = new() { DayName = upperMuscles.Count == 0 ? "Lower Day" : "Upper Day", Exercises = mappedVariationOne };
                dayTwo = new() { DayName = upperMuscles.Count == 0 ? "Lower Day" : "Upper Day", Exercises = mappedVariationTwo };
                upperLowerTrainingDayList.Add(dayOne);
                upperLowerTrainingDayList.Add(dayTwo);
                upperLowerTrainingDayList.Add(dayOne);
            }
            else
            {
                List<ExerciseBL> upperExercises = GetMinimumCompoundList(exerciseCount, upperMuscles, DbExercises);
                List<ExerciseBL> lowerExercises = GetMinimumCompoundList(exerciseCount, lowerMuscles, DbExercises);        
                upperExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, upperMuscles, upperExercises, DbExercises);
                lowerExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, lowerMuscles, lowerExercises, DbExercises);
                if (upperExercises.Count < exerciseCount)
                {
                    upperExercises = GetIsolated(exerciseCount, upperMuscles, upperExercises, DbExercises);
                }
                if (lowerExercises.Count < exerciseCount)
                {
                    lowerExercises = GetIsolated(exerciseCount, lowerMuscles, lowerExercises, DbExercises);
                }

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
            }
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

        private List<ExerciseBL> GetMinimumCompoundList(int maxExerciseAmount, List<MuscleBL> musclesToWork, List<ExerciseBL> exercisesToChooseFrom)//this function is supposed to cover all the selected muscles with mostly compound exercises
        {          
            List<ExerciseBL> compoundExercises = new();
            List<MuscleBL> musclesToRemove = new();
            List<MuscleBL> musclesUnprocessed = new();
            musclesUnprocessed.AddRange(musclesToWork);
            List<ExerciseBL> exercisesUnprocessed = new();
            exercisesUnprocessed.AddRange(exercisesToChooseFrom); 

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

        private List<ExerciseBL> GetExercisesForMusclesWithoutPrimary(int maxExerciseAmount, List<MuscleBL> musclesToWork, List<ExerciseBL> currentExercises, List<ExerciseBL> exercisesToChooseFrom) //this function looks for primary exercises for muscles that don't have primary yet
        {
            int amountOfExercisesToAdd = maxExerciseAmount - currentExercises.Count;
            if (amountOfExercisesToAdd == 0)
            {
                return currentExercises;
            }
            List<ExerciseBL> exercisesUnprocessed = exercisesToChooseFrom.Except(currentExercises).ToList();
            List<MuscleBL> musclesWithoutPrimary = new();
            List<MuscleBL> primaryMusclesFromChosenExercises = new();
            List<ExerciseBL> resultExerciseList = new();
            List<MuscleBL> muscleList = new();
            muscleList.AddRange(musclesToWork);

            ExerciseBL? foundExercise; 
            MuscleBL loopedMuscle;
            List<MuscleBL> intersectionList = new();

            foreach (var exercise in currentExercises) 
            {
                primaryMusclesFromChosenExercises.AddRange(exercise.PrimaryMuscleList);
            }
            primaryMusclesFromChosenExercises.Distinct();
            musclesWithoutPrimary = muscleList.Except(primaryMusclesFromChosenExercises).ToList();

            exercisesUnprocessed = HeartCheck(exercisesUnprocessed, musclesWithoutPrimary);

            for (int i = 0; i < musclesWithoutPrimary.Count; i++)
            {
                loopedMuscle = musclesWithoutPrimary[i];
                foundExercise = exercisesUnprocessed.FirstOrDefault(e => e.PrimaryMuscleList.Contains(loopedMuscle)); 
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
            currentExercises.AddRange(resultExerciseList);
            return currentExercises;
        }

        private List<ExerciseBL> GetIsolated(int maxExerciseAmount, List<MuscleBL> musclesToWork, List<ExerciseBL> currentExercises, List<ExerciseBL> exercisesToChooseFrom)
        {
            List<ExerciseBL> exercisesUnprocessed = exercisesToChooseFrom.Except(currentExercises).ToList();
            List<MuscleBL> muscleList = new();
            muscleList.AddRange(musclesToWork);

            ExerciseBL? foundExercise;
            MuscleBL loopedMuscle;

            for (int i = 0; i < muscleList.Count; i++)
            {
                loopedMuscle = muscleList[i];
                foundExercise = exercisesUnprocessed.FirstOrDefault(e => e.PrimaryMuscleList.Contains(loopedMuscle) && !e.IsCompound);
                if (foundExercise is not null)
                {
                    currentExercises.Add(foundExercise);
                    muscleList.Remove(loopedMuscle);
                    exercisesUnprocessed.Remove(foundExercise);
                    i--;
                }
                if (currentExercises.Count >= maxExerciseAmount)
                {
                    break;
                }
            }
            return currentExercises;
        }

        //add GetWhatever function
}
}