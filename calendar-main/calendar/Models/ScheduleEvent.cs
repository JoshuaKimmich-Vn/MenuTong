using System;
using System.Collections.Generic;
using System.Text;

namespace carlender.Models
{
    public class ScheduleEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public ScheduleEvent()
        {
            Id = Guid.NewGuid().ToString();
            Title = "";
            Description = "";
            Category = "";
        }

        public ScheduleEvent(string title, DateTime startTime, DateTime endTime, string description, string category)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            Category = category;
        }

        public override string ToString()
        {
            return $"⏰ {StartTime:HH:mm} - {EndTime:HH:mm}   {Title}   [{Category}]";
        }
    }
}