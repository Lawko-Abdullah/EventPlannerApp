using System;
using System.Collections.Generic;
using NUnit.Framework;
using EventPlannerApp;

namespace EventPlannerApp.Tests
{
    [TestFixture]
    public class EventPlannerTests
    {
        private EventPlanner eventPlanner;

        [SetUp]
        public void Setup()
        {
            eventPlanner = new EventPlanner();
        }

        [Test]
        public void TestCreateEvent()
        {
            string eventName = "Test Event";
            DateTime eventDateTime = new DateTime(2023, 12, 25, 10, 0, 0);
            TimeSpan duration = new TimeSpan(2, 0, 0);
            string timeZoneId = "Eastern Standard Time";

            eventPlanner.CreateEvent(eventName, eventDateTime, duration, timeZoneId);
            Event createdEvent = eventPlanner.GetEventByName(eventName);

            // Fixed NUnit assertion for IsNotNull and other assertions
            NUnit.Framework.Assert.IsNotNull(createdEvent, "Event should be created successfully.");
            NUnit.Framework.Assert.AreEqual(eventName, createdEvent.Name, "Event name does not match.");
            NUnit.Framework.Assert.AreEqual(eventDateTime, createdEvent.LocalDateTime, "Event date and time do not match.");
            NUnit.Framework.Assert.AreEqual(duration, createdEvent.Duration, "Event duration does not match.");
            NUnit.Framework.Assert.AreEqual(timeZoneId, createdEvent.TimeZoneId, "Event time zone ID does not match.");
        }

        [Test]
        public void TestDisplayEventTimesInTimeZones()
        {
            string eventName = "Time Zone Test Event";
            DateTime eventDateTime = new DateTime(2023, 12, 25, 10, 0, 0);
            TimeSpan duration = new TimeSpan(2, 0, 0);
            string timeZoneId = "Eastern Standard Time";

            eventPlanner.CreateEvent(eventName, eventDateTime, duration, timeZoneId);
            Event createdEvent = eventPlanner.GetEventByName(eventName);

            List<string> timeZoneIds = new List<string> { "Pacific Standard Time", "UTC", "Central European Standard Time" };

            foreach (var tzId in timeZoneIds)
            {
                DateTime convertedTime = createdEvent.ConvertToTimeZone(tzId);

                NUnit.Framework.Assert.AreNotEqual(eventDateTime, convertedTime, $"Event time should change when converted to {tzId}");
            }
        }

        [Test]
        public void TestCheckForConflicts()
        {
            string firstEventName = "First Event";
            DateTime firstEventDateTime = new DateTime(2023, 12, 25, 10, 0, 0);
            TimeSpan firstEventDuration = new TimeSpan(2, 0, 0);
            string firstEventTimeZoneId = "Eastern Standard Time";

            string secondEventName = "Second Event";
            DateTime secondEventDateTime = new DateTime(2023, 12, 25, 11, 0, 0);
            TimeSpan secondEventDuration = new TimeSpan(2, 0, 0);
            string secondEventTimeZoneId = "Eastern Standard Time";

            eventPlanner.CreateEvent(firstEventName, firstEventDateTime, firstEventDuration, firstEventTimeZoneId);
            eventPlanner.CreateEvent(secondEventName, secondEventDateTime, secondEventDuration, secondEventTimeZoneId);

            Event firstEvent = eventPlanner.GetEventByName(firstEventName);
            Event secondEvent = eventPlanner.GetEventByName(secondEventName);

            bool conflict = eventPlanner.CheckForConflicts(firstEvent, secondEvent);

            NUnit.Framework.Assert.IsTrue(conflict, "Events should conflict based on their timings.");
        }

        [Test]
        public void TestShowCountdownToEvent()
        {
            string eventName = "Countdown Event";
            DateTime eventDateTime = DateTime.UtcNow.AddHours(1); // Use UtcNow for precision
            TimeSpan duration = new TimeSpan(2, 0, 0);
            string timeZoneId = "Eastern Standard Time";

            eventPlanner.CreateEvent(eventName, eventDateTime, duration, timeZoneId);
            Event createdEvent = eventPlanner.GetEventByName(eventName);

            TimeSpan timeUntilEvent = createdEvent.LocalDateTime - DateTime.UtcNow;
            NUnit.Framework.Assert.IsTrue(timeUntilEvent.TotalSeconds > 0, "Countdown should show positive time until the event starts.");
        }
    }
}
