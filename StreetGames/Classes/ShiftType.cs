using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetGames
{
    public class ShiftType
    {
        public int shiftTypeId { get; set; }
        public string name { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public int requiredEmployees { get; set; }
        public string description { get; set; }

        public ActivationStatus status { get; set; }



        public ShiftType(int shiftTypeId, string name, TimeSpan startTime, TimeSpan endTime, int requiredEmployees, string description)
        {
            this.shiftTypeId = shiftTypeId;
            this.name = name;
            this.startTime = startTime;
            this.endTime = endTime;
            this.requiredEmployees = requiredEmployees;
            this.description = description;
        }
    }
}
