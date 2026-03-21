using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetGames
{
    public enum EventStatus
    {
        requested = 1,
        approved = 2,
        rejected = 3,
        cancelled = 4,
        completed = 5,
        underReview = 6,
        archived = 7,
        settled = 8,
        awaitingSettlement = 9,
        scheduled = 10,
    }
}
