using System;
using System.Collections.Generic;
using System.Text;

using carlender.Models;
using Newtonsoft.Json;

namespace carlender.Services
{
    public class FileStorage
    {
        private readonly string filePath;

        public FileStorage(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveEvents(List<ScheduleEvent> events)
        {
            string json = JsonConvert.SerializeObject(events, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<ScheduleEvent> LoadEvents()
        {
            if (!File.Exists(filePath))
            {
                return new List<ScheduleEvent>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<ScheduleEvent>();
            }

            List<ScheduleEvent>? events = JsonConvert.DeserializeObject<List<ScheduleEvent>>(json);

            if (events == null)
            {
                return new List<ScheduleEvent>();
            }

            return events;
        }
    }
}