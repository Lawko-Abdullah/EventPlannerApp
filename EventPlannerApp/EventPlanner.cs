using System;
using System.Collections.Generic;

namespace EventPlannerApp
{
    public class EventPlanner
    {
        private List<Event> events;

        public EventPlanner()
        {
            events = new List<Event>();
        }

        public void CreateEvent(string name, DateTime localDateTime, TimeSpan duration, string timeZoneId)
        {
            Event newEvent = new Event(name, localDateTime, duration, timeZoneId);
            events.Add(newEvent);
            Console.WriteLine($"Event '{name}' created successfully.");
        }

        public void DisplayEventTimesInTimeZones(Event ev, List<string> timeZoneIds)
        {
            Console.WriteLine($"Event: {ev.Name}");
            foreach (var timeZoneId in timeZoneIds)
            {
                DateTime convertedTime = TimeZoneHelper.ConvertToTimeZone(ev.LocalDateTime, ev.TimeZoneId, timeZoneId);
                Console.WriteLine($"Time in {timeZoneId}: {convertedTime}");
            }
        }

        public bool CheckForConflicts(Event event1, Event event2)
        {
            DateTime event1End = event1.LocalDateTime.Add(event1.Duration);
            DateTime event2End = event2.LocalDateTime.Add(event2.Duration);

            if (event1.LocalDateTime < event2End && event2.LocalDateTime < event1End)
            {
                Console.WriteLine("Events conflict with each other.");
                return true;
            }
            else
            {
                Console.WriteLine("No conflict between events.");
                return false;
            }
        }

        public void ShowCountdownToEvent(Event ev)
        {
            TimeSpan timeUntilEvent = ev.LocalDateTime - DateTime.Now;
            if (timeUntilEvent.TotalSeconds > 0)
            {
                Console.WriteLine($"Time until event '{ev.Name}': {timeUntilEvent.Days} days, {timeUntilEvent.Hours} hours, {timeUntilEvent.Minutes} minutes, {timeUntilEvent.Seconds} seconds.");
            }
            else
            {
                Console.WriteLine($"Event '{ev.Name}' has already started or passed.");
            }
        }
    }
}