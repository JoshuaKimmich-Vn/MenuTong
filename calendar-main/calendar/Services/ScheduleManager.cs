using carlender.Models;

namespace carlender.Services
{
    public class ScheduleManager
    {
        private List<ScheduleEvent> events;

        public ScheduleManager()
        {
            events = new List<ScheduleEvent>();
        }

        public List<ScheduleEvent> GetAllEvents()
        {
            return events;
        }

        public void SetEvents(List<ScheduleEvent> eventList)
        {
            events = eventList;
        }

        public void AddEvent(ScheduleEvent scheduleEvent)
        {
            if (scheduleEvent.StartTime >= scheduleEvent.EndTime)
            {
                throw new Exception("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.");
            }

            events.Add(scheduleEvent);
        }

        public void DeleteEvent(string eventId)
        {
            ScheduleEvent? scheduleEvent = events.FirstOrDefault(e => e.Id == eventId);

            if (scheduleEvent != null)
            {
                events.Remove(scheduleEvent);
            }
        }

        public void UpdateEvent(ScheduleEvent updatedEvent)
        {
            ScheduleEvent? oldEvent = events.FirstOrDefault(e => e.Id == updatedEvent.Id);

            if (oldEvent == null)
            {
                throw new Exception("Không tìm thấy sự kiện cần sửa.");
            }

            if (updatedEvent.StartTime >= updatedEvent.EndTime)
            {
                throw new Exception("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.");
            }

            oldEvent.Title = updatedEvent.Title;
            oldEvent.StartTime = updatedEvent.StartTime;
            oldEvent.EndTime = updatedEvent.EndTime;
            oldEvent.Description = updatedEvent.Description;
            oldEvent.Category = updatedEvent.Category;
        }

        public List<ScheduleEvent> GetEventsByDate(DateTime date)
        {
            return events
                .Where(e => e.StartTime.Date == date.Date)
                .OrderBy(e => e.StartTime)
                .ToList();
        }
    }
}