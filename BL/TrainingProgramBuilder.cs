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
        private List<ExerciseBL> ResultingExercises;

        private List<ExerciseBL> DbExercises;
        private List<MuscleBL> DbMuscles;
        private List<EquipBL> DbEquipment;

        public TrainingProgramBuilder()
        {
            GetDataFromDAL dataDAL = new();
            DbExercises = dataDAL.GetExercises();
            DbMuscles = dataDAL.GetMuscles();
            DbEquipment = dataDAL.GetEquipment();
            MuscleListToWork = new();//questionable
            ResultingExercises = new();
        }

        public List<string> Calculate(int[] idList, int intensity, out bool success)
        {
            MuscleBL? muscleBL;
            foreach (int id in idList)
            {
                muscleBL = DbMuscles.FirstOrDefault(m => (int)m.MuscleType == id);
                if (muscleBL is null)
                {
                    success = false;
                    return new List<string>(); //change to return resulting exercises later
                }
                else
                {
                    MuscleListToWork.Add(muscleBL);
                }
            }

            switch (intensity)
            {
                case 1:
                    ResultingExercises = FullBodySplit();
                    break;
                case 2:
                    UpperLowerSplit();
                    break;
                case 3:
                    PushPullLegSplit();
                    break;
                default:
                    break;
            }

            List<string> result = new();
            foreach (var ex in ResultingExercises)
            {
                result.Add(ex.Name);
            }
            success = true;
            return result;
            
        }

        private List<ExerciseBL> FullBodySplit()
        {
            int exerciseCount = 10;
            List<ExerciseBL> fullBodySplitExercises = new();
            
            fullBodySplitExercises = GetMinimumCompoundList(); //adds minimum compound exercises           
            fullBodySplitExercises = GetExercisesForMusclesWithoutPrimary(exerciseCount, fullBodySplitExercises); //adds primary exercises to muscles that don't have them

            return fullBodySplitExercises;
        }

        private void UpperLowerSplit()
        {

        }

        private void PushPullLegSplit()
        {

        }

        private List<ExerciseBL> GetMinimumCompoundList()//this function is supposed to cover all the selected muscles with mostly compound exercises
        {          
            List<ExerciseBL> compoundExercises = new();
            List<MuscleBL> musclesToRemove = new();
            List<MuscleBL> musclesUnprocessed = new();
            musclesUnprocessed.AddRange(MuscleListToWork);
            List<ExerciseBL> exercisesUnprocessed = new();
            exercisesUnprocessed.AddRange(DbExercises); 

            //this excludes cardio exercises from selection list if heart is not selected as a muscle to train
            MuscleBL? heart = DbMuscles.FirstOrDefault(m => m.MuscleType == MuscleType.Heart);
            if (heart != null && !MuscleListToWork.Contains(heart))
            {
                exercisesUnprocessed.RemoveAll(e => e.PrimaryMuscleList.Contains(heart));
            }


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

        private List<ExerciseBL> GetExercisesForMusclesWithoutPrimary(int maxExerciseAmount, List<ExerciseBL> preparedExerciseList) //this function looks for primary exercises for muscles that don't have primary yet
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
            muscleList.AddRange(MuscleListToWork);

            ExerciseBL? foundExercise; 
            MuscleBL loopedMuscle;
            List<MuscleBL> intersectionList = new();

            foreach (var exercise in preparedExerciseList) 
            {
                primaryMusclesFromChosenExercises.AddRange(exercise.PrimaryMuscleList);
            }
            primaryMusclesFromChosenExercises.Distinct();
            musclesWithoutPrimary = muscleList.Except(primaryMusclesFromChosenExercises).ToList(); 

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