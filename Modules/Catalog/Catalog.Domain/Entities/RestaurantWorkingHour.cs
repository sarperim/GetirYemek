using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class RestaurantWorkingHour
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public int DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, ..., 6 = Saturday
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


    }
}
