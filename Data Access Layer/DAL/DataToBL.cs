using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.BL.Models;

namespace Gym.DAL
{
    public class DataToBL
    {
        private GeneralContext _context;
        public DataToBL(GeneralContext context)
        {
            _context = context;
        }

        public List<MuscleBL> GetMusclesWithExercises()
        {

        }

    }
}
