using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetGames
{
    public class ShiftParticipation
    {
        public int shiftParticipationId { get; set; }
        public int shiftId { get; set; }
        public int employeeId { get; set; }
        public int availabilityStatusId { get; set; }
        public int assignmentStatusId { get; set; }
        public DateTime submittedAt { get; set; }

        public DateTime? assignedAt { get; set; }

        public string availability => ((AvailabilityStatus)availabilityStatusId).ToString();
        public string aassignment => ((AssignmentStatus)assignmentStatusId).ToString();

        

        public ShiftParticipation(int shiftParticipationId, int shiftId, int employeeId, int availabilityStatusId, int assignmentStatusId, DateTime submittedAt, DateTime? assignedAt, int formId)
        {
            this.shiftParticipationId = shiftParticipationId;
            this.shiftId = shiftId;
            this.employeeId = employeeId;
            this.availabilityStatusId = availabilityStatusId;
            this.assignmentStatusId = assignmentStatusId;
            this.submittedAt = submittedAt;
            this.assignedAt = assignedAt;
        }
    }
}
