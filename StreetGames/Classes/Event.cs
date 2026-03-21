using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetGames
{
    public class Event
    {
        private int? askedViaFormId1;

        public int eventId { get; set; }
        public int customerId { get; set; }
        public int? responsibleEmployeeId { get; set; }
        public DateTime eventDate { get; set; }
        public TimeSpan startTime { get; set; }
        public double duration { get; set; }
        public int participantsCount { get; set; }
        public int statusId { get; set; }
        public string notes { get; set; }
        public int askedViaFormId { get; set; }

        public string status => ((EventStatus)statusId).ToString();

        public Event() { }

        public Event(int eventId, int customer, int? responsibleEmployeeId, DateTime eventDate, TimeSpan startTime, double duration, int participantsCount, int statusId, string notes, int askedViaFormId)
        {
            this.eventId = eventId;
            this.customerId = customer;
            this.responsibleEmployeeId = responsibleEmployeeId;
            this.eventDate = eventDate;
            this.startTime = startTime;
            this.duration = duration;
            this.participantsCount = participantsCount;
            this.statusId = statusId;
            this.notes = notes;
            this.askedViaFormId = askedViaFormId;
        }

        public Event(int eventId, int customerId, int? responsibleEmployeeId, DateTime eventDate, TimeSpan startTime, double duration, int participantsCount, int statusId, string notes, int? askedViaFormId1)
        {
            this.eventId = eventId;
            this.customerId = customerId;
            this.responsibleEmployeeId = responsibleEmployeeId;
            this.eventDate = eventDate;
            this.startTime = startTime;
            this.duration = duration;
            this.participantsCount = participantsCount;
            this.statusId = statusId;
            this.notes = notes;
            this.askedViaFormId1 = askedViaFormId1;
        }
    }
}
