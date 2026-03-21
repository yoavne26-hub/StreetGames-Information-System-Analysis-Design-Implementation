using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetGames
{
    public class Shift
    {
        public int shiftId { get; set; }
        public int weekScheduleId { get; set; }
        public int shiftTypeId { get; set; }
        public DateTime shiftDate { get; set; }

        public Shift(int shiftId, int weekScheduleId, int shiftTypeId, DateTime shiftDate)
        {
            this.shiftId = shiftId;
            this.weekScheduleId = weekScheduleId;
            this.shiftTypeId = shiftTypeId;
            this.shiftDate = shiftDate;
        }
    }
}
