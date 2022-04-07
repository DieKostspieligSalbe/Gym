using BL.Models;
using DAL.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;

namespace BL
{
    public class TrainingProgramBuilder
    {
        private readonly GeneralContext _context;
        private List<MuscleDAL> MuscleListToWork = new();
        private List<ExerciseDAL> DbExercises = new();
        private List<ExerciseDAL> ResultingExercises = new();

        public TrainingProgramBuilder(GeneralContext context)
        {
            _context = context;
        }

        public List<string> Calculate(int[] idList, int intensity)
        { 
            DbExercises = _context.Exercises.Include(e => e.PrimaryMuscleList).Include(e => e.SecondaryMuscleList).ToList();

            foreach (int id in idList)
            {
                MuscleListToWork.Add(_context.Muscles.FirstOrDefault(m => (int)m.MuscleType == id)); //how to deal with it better
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
            return result;
            
        }

        private List<ExerciseDAL> FullBodySplit()
        {
            int exerciseCount = 10;
            List<ExerciseDAL> fullBodySplitExercises = new();
            
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

        private List<ExerciseDAL> GetMinimumCompoundList()//this function is supposed to cover all the selected muscles with mostly compound exercises
        {          
            List<ExerciseDAL> compoundExercises = new();
            List<MuscleDAL> musclesUnprocessed = MuscleListToWork.ToList();
            List<MuscleDAL> musclesToRemove = new();
            List<ExerciseDAL> exercisesUnprocessed = DbExercises.ToList(); //is it ref and if to list helps

            //this excludes cardio exercises from selection list if heart is not selected as a muscle to train
            MuscleDAL heart = _context.Muscles.FirstOrDefault(m => m.MuscleType == MuscleType.Heart);
            if (!MuscleListToWork.Contains(heart))
            {
                exercisesUnprocessed.RemoveAll(e => e.PrimaryMuscleList.Contains(heart));
            }

            MuscleDAL loopedMuscle;
            List<ExerciseDAL> foundExercises = new();

            List<MuscleDAL> unionList = new();
            List<MuscleDAL> intersectionList = new();

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

        private List<ExerciseDAL> GetExercisesForMusclesWithoutPrimary(int maxExerciseAmount, List<ExerciseDAL> preparedExerciseList) //this function looks for primary exercises for muscles that don't have primary yet
        {
            int amountOfExercisesToAdd = maxExerciseAmount - preparedExerciseList.Count;
            if (amountOfExercisesToAdd == 0)
            {
                return preparedExerciseList;
            }
            List<ExerciseDAL> exercisesUnprocessed = DbExercises.Except(preparedExerciseList).ToList();
            List<MuscleDAL> muscleList = MuscleListToWork.ToList();
            List<MuscleDAL> musclesWithoutPrimary = new();
            List<MuscleDAL> primaryMusclesFromChosenExercises = new();
            List<ExerciseDAL> resultExerciseList = new();

            ExerciseDAL foundExercise = null; //is it bad?
            MuscleDAL loopedMuscle;
            List<MuscleDAL> intersectionList = new();

            foreach (var exercise in preparedExerciseList) 
            {
                primaryMusclesFromChosenExercises.AddRange(exercise.PrimaryMuscleList);
            }
            primaryMusclesFromChosenExercises.Distinct();
            musclesWithoutPrimary = muscleList.Except(primaryMusclesFromChosenExercises).ToList(); 

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
            preparedExerciseList.AddRange(resultExerciseList);
            return preparedExerciseList;
        }


}
}