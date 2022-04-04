using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public enum EquipType
    {
        Machine = 1, //inclued racks and etc, everything that is used as a one set of equip for some certain goal
        StationaryItem = 2, //stuff like benches, mats and etc
        ActiveItem = 3 //stuff like barbells, balls
    }
}
