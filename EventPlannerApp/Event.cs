using System;

namespace EventPlannerApp
{
    public class Event
    {
        public string Name { get; private set; }
        public DateTime LocalDateTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string TimeZoneId { get; private set; }

        public Event(string name, DateTime localDateTime, TimeSpan duration, string timeZoneId)
        {
            Name = name;
            LocalDateTime = localDateTime;
            Duration = duration;
            TimeZoneId = timeZoneId;
        }

        public DateTime ConvertToTimeZone(string targetTimeZoneId)
        {
            TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(targetTimeZoneId);
            DateTime sourceTime = TimeZoneInfo.ConvertTime(LocalDateTime, sourceTimeZone);
            DateTime targetTime = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZone, targetTimeZone);
            return targetTime;
        }

        public bool ConflictsWith(Event otherEvent)
        {
            DateTime thisEventEnd = LocalDateTime.Add(Duration);
            DateTime otherEventStartInThisTimeZone = TimeZoneInfo.ConvertTime(otherEvent.LocalDateTime, TimeZoneInfo.FindSystemTimeZoneById(otherEvent.TimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId));
            DateTime otherEventEndInThisTimeZone = otherEventStartInThisTimeZone.Add(otherEvent.Duration);

            return LocalDateTime < otherEventEndInThisTimeZone && otherEventStartInThisTimeZone < thisEventEnd;
        }
    }
}