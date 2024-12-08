using System;
using System.Collections.Generic;

namespace EventPlannerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EventPlanner eventPlanner = new EventPlanner();
            Console.WriteLine("Welcome to the Dynamic Event Planner!");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Create Event");
                Console.WriteLine("2. Display Event Times in Different Time Zones");
                Console.WriteLine("3. Check for Event Conflicts");
                Console.WriteLine("4. Show Countdown to Event");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateEvent(eventPlanner);
                        break;
                    case "2":
                        DisplayEventTimes(eventPlanner);
                        break;
                    case "3":
                        CheckEventConflicts(eventPlanner);
                        break;
                    case "4":
                        ShowEventCountdown(eventPlanner);
                        break;
                    case "5":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateEvent(EventPlanner eventPlanner)
        {
            Console.Write("Enter event name: ");
            string name = Console.ReadLine();

            Console.Write("Enter event date and time (yyyy-mm-dd hh:mm): ");
            DateTime localDateTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter event duration (hours:minutes): ");
            TimeSpan duration = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter time zone (e.g., Eastern Standard Time or UTC offset like +05:30): ");
            string timeZoneId = Console.ReadLine();

            eventPlanner.CreateEvent(name, localDateTime, duration, timeZoneId);
        }

        static void DisplayEventTimes(EventPlanner eventPlanner)
        {
            Console.Write("Enter event name to display times: ");
            string eventName = Console.ReadLine();

            Event ev = eventPlanner.GetEventByName(eventName);
            if (ev == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }

            Console.WriteLine("Available time zones:");
            List<string> availableTimeZones = TimeZoneHelper.ListAvailableTimeZones();
            foreach (var timeZone in availableTimeZones)
            {
                Console.WriteLine(timeZone);
            }

            Console.Write("Enter time zones to display (comma-separated): ");
            string[] timeZoneIds = Console.ReadLine().Split(',');

            eventPlanner.DisplayEventTimesInTimeZones(ev, new List<string>(timeZoneIds));
        }

        static void CheckEventConflicts(EventPlanner eventPlanner)
        {
            Console.Write("Enter first event name: ");
            string firstEventName = Console.ReadLine();
            Event firstEvent = eventPlanner.GetEventByName(firstEventName);
            if (firstEvent == null)
            {
                Console.WriteLine("First event not found.");
                return;
            }

            Console.Write("Enter second event name: ");
            string secondEventName = Console.ReadLine();
            Event secondEvent = eventPlanner.GetEventByName(secondEventName);
            if (secondEvent == null)
            {
                Console.WriteLine("Second event not found.");
                return;
            }

            eventPlanner.CheckForConflicts(firstEvent, secondEvent);
        }

        static void ShowEventCountdown(EventPlanner eventPlanner)
        {
            Console.Write("Enter event name to show countdown: ");
            string eventName = Console.ReadLine();

            Event ev = eventPlanner.GetEventByName(eventName);
            if (ev == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }

            eventPlanner.ShowCountdownToEvent(ev);
        }
    }
}