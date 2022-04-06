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
        private List<ExerciseDAL> Exercises = new();

        public TrainingProgramBuilder(GeneralContext context)
        {
            _context = context;
        }

        public List<string> Calculate(int[] idList, int intensity)
        {              
            foreach(int id in idList)
            {
                MuscleListToWork.Add(_context.Muscles.FirstOrDefault(m => (int)m.MuscleType == id));
            }


            switch (intensity)
            {
                case 1:
                    Exercises = FullBodySplit();
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
            foreach (var ex in Exercises)
            {
                result.Add(ex.Name);
            }
            return result;
            
        }

        private List<ExerciseDAL> FullBodySplit()
        {
            return GetShortCompoundList();
        }

        private void UpperLowerSplit()
        {

        }

        private void PushPullLegSplit()
        {

        }

        private List<ExerciseDAL> GetShortCompoundList()
        {
            List<ExerciseDAL> compoundExercises = new();
            List<MuscleDAL> musclesUnprocessed = MuscleListToWork.ToList();
            List<MuscleDAL> musclesToRemove = new();
            List<ExerciseDAL> exercisesUnprocessed = _context.Exercises.Include(e => e.PrimaryMuscleList).Include(e => e.SecondaryMuscleList).ToList();

            MuscleDAL heart = _context.Muscles.FirstOrDefault(m => m.MuscleType == MuscleType.Heart);
            if (!MuscleListToWork.Contains(heart))
            {
                exercisesUnprocessed.RemoveAll(e => e.PrimaryMuscleList.Contains(heart));
            }

            MuscleDAL loopedMuscle;
            List<ExerciseDAL> foundExercises = new();

            List<MuscleDAL> unionList = new();
            List<MuscleDAL> intersectionList = new();


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

            musclesToRemove.Distinct();
            musclesUnprocessed.RemoveAll(m => musclesToRemove.Contains(m));

            for (int i = 0; i < musclesUnprocessed.Count; i++)
            {
                loopedMuscle = musclesUnprocessed[i];
                foundExercises = exercisesUnprocessed.Where(e => e.PrimaryMuscleList.Contains(loopedMuscle) || e.SecondaryMuscleList.Contains(loopedMuscle)).ToList();

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
                        i = 0;
                        break;
                    }
                }
            }
            return compoundExercises;
        }

}
}