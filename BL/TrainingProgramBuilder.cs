using AutoMapper;
using Gym.BL.Mapping;
using Gym.BL.Models;
using Gym.BL.Models.ModelsView;
using Gym.Common.Enum;
using Gym.DAL;
using Gym.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gym.BL
{
    public class TrainingProgramBuilder 
    {
        private const int maxExerciseCount = 10;
        private List<MuscleBL> MuscleListToWork;

        private List<ExerciseBL> DbExercises;
        private List<MuscleBL> DbMuscles;
        private List<EquipBL> DbEquipment;

        private List<MuscleDAL> DbMusclesDAL;
        private List<ExerciseDAL> DbExercisesDAL;

        private readonly IMapper _mapper;

        public TrainingProgramBuilder(GetDataFromDAL dataDAL)//mapper
        {
            DbExercises = dataDAL.GetExercises();
            DbMuscles = dataDAL.GetMuscles();
            DbEquipment = dataDAL.GetEquipment();
            DbMusclesDAL = dataDAL.GetMusclesDAL();
            DbExercisesDAL = dataDAL.GetExercisesDAL();
            MuscleListToWork = new();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileBL>());
            _mapper = mapperConfig.CreateMapper();
        }

        public TrainingProgramDisplayPR Calculate(MuscleViewModel muscleModel, out bool success)
        {
            MuscleBL? muscleBL;
            foreach (int id in muscleModel.IdList)
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

            switch (muscleModel.Intensity)
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

        public TrainingProgramDAL GetProgramDAL(TrainingProgramViewModel model) //check it really well
        {
            TrainingProgramDisplayPR programPR = Calculate(model.MuscleModel, out bool success);
            List<MuscleDAL> muscleListDAL = new();
            MuscleListToWork.ForEach(m => muscleListDAL.Add(DbMusclesDAL.FirstOrDefault(dbm => dbm.Name == m.Name)));

            List<ExercisePR> exerciseListPR = new();
            programPR.trainingDayList.ForEach(x => exerciseListPR.AddRange(x.Exercises));
            exerciseListPR.Distinct();
            List<ExerciseDAL> exerciseListDAL = new();
            exerciseListPR.ForEach(ex => exerciseListDAL.Add(DbExercisesDAL.FirstOrDefault(dbex => dbex.Name == ex.Name))); //eh perhaps do something

            TrainingProgramDAL programDAL = new();
            programDAL.MuscleList = muscleListDAL;
            programDAL.ExerciseList = exerciseListDAL;
            programDAL.Intensity = model.MuscleModel.Intensity;
            return programDAL;

        }

        private TrainingProgramDisplayPR FullBodySplit()
        {    
            var dayOneExercises = GetMinimumCompoundList(MuscleListToWork, exercisesToChooseFrom: DbExercises); //adds minimum compound exercises           
            dayOneExercises = GetExercisesForMusclesWithoutPrimary(MuscleListToWork, currentExercises: dayOneExercises, exercisesToChooseFrom: DbExercises); //adds primary exercises to muscles that don't have them
            List<ExerciseBL> otherExercises = DbExercises.Except(dayOneExercises).ToList();
            var dayTwoExercises = GetMinimumCompoundList(MuscleListToWork, exercisesToChooseFrom: otherExercises);
            dayTwoExercises = GetExercisesForMusclesWithoutPrimary(MuscleListToWork, currentExercises: dayTwoExercises, exercisesToChooseFrom: otherExercises);

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

            TrainingDayPR dayOne;
            TrainingDayPR dayTwo;
            List<TrainingDayPR> upperLowerTrainingDayList = new();
            TrainingProgramDisplayPR upperLowerSplitProgram;

            if (upperMuscles.Count == 0 || lowerMuscles.Count == 0)
            {
                List<MuscleBL> musclesToSend = upperMuscles.Count == 0 ? lowerMuscles : upperMuscles;
                List<ExerciseBL> dayVariationOne = GetMinimumCompoundList(musclesToSend, DbExercises);
                dayVariationOne = GetExercisesForMusclesWithoutPrimary(musclesToSend, dayVariationOne, DbExercises);
                if (dayVariationOne.Count < maxExerciseCount)
                {
                    dayVariationOne = GetIsolated( musclesToSend, dayVariationOne, DbExercises);
                }

                List<ExerciseBL> otherExercises = DbExercises.Except(dayVariationOne).ToList();
                List<ExerciseBL> dayVariationTwo = GetMinimumCompoundList(musclesToSend, otherExercises);             
                dayVariationTwo = GetExercisesForMusclesWithoutPrimary( musclesToSend, dayVariationTwo, otherExercises);
                if (dayVariationTwo.Count < maxExerciseCount)
                {
                    dayVariationTwo = GetIsolated(musclesToSend, dayVariationTwo, otherExercises);
                }
                List<ExercisePR> mappedVariationOne = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(dayVariationOne);
                List<ExercisePR> mappedVariationTwo = _mapper.Map<List<ExerciseBL>, List<ExercisePR>>(dayVariationTwo);

                dayOne = new() { DayName = upperMuscles.Count == 0 ? "Lower Day" : "Upper Day", Exercises = mappedVariationOne }; //string to const
                dayTwo = new() { DayName = upperMuscles.Count == 0 ? "Lower Day" : "Upper Day", Exercises = mappedVariationTwo };
                upperLowerTrainingDayList.Add(dayOne);
                upperLowerTrainingDayList.Add(dayTwo);
                upperLowerTrainingDayList.Add(dayOne);
            }
            else
            {
                List<ExerciseBL> upperExercises = GetMinimumCompoundList(upperMuscles, DbExercises);
                List<ExerciseBL> lowerExercises = GetMinimumCompoundList(lowerMuscles, DbExercises);        
                upperExercises = GetExercisesForMusclesWithoutPrimary(upperMuscles, upperExercises, DbExercises);
                lowerExercises = GetExercisesForMusclesWithoutPrimary(lowerMuscles, lowerExercises, DbExercises);
                if (upperExercises.Count < maxExerciseCount)
                {
                    upperExercises = GetIsolated(upperMuscles, upperExercises, DbExercises);
                }
                if (lowerExercises.Count < maxExerciseCount)
                {
                    lowerExercises = GetIsolated(lowerMuscles, lowerExercises, DbExercises);
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
            List<MuscleBL> pushMuscles = MuscleListToWork.FindAll(m => m.MovementType == MovementType.Push);
            List<MuscleBL> pullMuscles = MuscleListToWork.FindAll(m => m.MovementType == MovementType.Pull);
            List<MuscleBL> legMuscles = MuscleListToWork.FindAll(m => m.MovementType == MovementType.Leg);
            MuscleBL? heart = DbMuscles.FirstOrDefault(m => m.MuscleType == MuscleType.Heart);
            if (heart != null && MuscleListToWork.Contains(heart))
            {
                legMuscles.Add(heart); //leg because whatever works heart usually works legs very intense too
            }

            List<ExerciseBL> dayOne = new();
            List<ExerciseBL> dayTwo = new();
            List<ExerciseBL> dayThree = new();

            if (pushMuscles.Count > 0)
            {
                dayOne = GetMinimumCompoundList(pushMuscles, DbExercises);
                dayOne = GetExercisesForMusclesWithoutPrimary(pushMuscles, dayOne, DbExercises);
                dayOne = GetIsolated(pushMuscles, dayOne, DbExercises);
            }

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

        private List<ExerciseBL> GetMinimumCompoundList(List<MuscleBL> musclesToWork, List<ExerciseBL> exercisesToChooseFrom)//this function is supposed to cover all the selected muscles with mostly compound exercises
        {          
            List<ExerciseBL> compoundExercises = new();
            List<MuscleBL> musclesToRemove = new();
            var musclesUnprocessed = musclesToWork.ToList();
            var exercisesUnprocessed = exercisesToChooseFrom.ToList();

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
                    if (compoundExercises.Count >= maxExerciseCount)
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
                        if (compoundExercises.Count >= maxExerciseCount)
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

        private List<ExerciseBL> GetExercisesForMusclesWithoutPrimary(List<MuscleBL> musclesToWork, List<ExerciseBL> currentExercises, List<ExerciseBL> exercisesToChooseFrom) //this function looks for primary exercises for muscles that don't have primary yet
        {
            int amountOfExercisesToAdd = maxExerciseCount - currentExercises.Count;
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

        private List<ExerciseBL> GetIsolated(List<MuscleBL> musclesToWork, List<ExerciseBL> currentExercises, List<ExerciseBL> exercisesToChooseFrom)
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
                if (currentExercises.Count >= maxExerciseCount)
                {
                    break;
                }
            }
            return currentExercises;
        }

        //add GetWhatever function
}
}