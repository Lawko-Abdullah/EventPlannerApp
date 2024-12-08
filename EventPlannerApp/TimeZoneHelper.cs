using System;
using System.Collections.Generic;

namespace EventPlannerApp
{
    public static class TimeZoneHelper
    {
        // Method to list all available time zones
        public static List<string> ListAvailableTimeZones()
        {
            List<string> timeZoneIds = new List<string>();
            foreach (TimeZoneInfo timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZoneIds.Add(timeZone.Id);
            }
            return timeZoneIds;
        }

        // Method to convert time between time zones
        public static DateTime ConvertToTimeZone(DateTime dateTime, string sourceTimeZoneId, string targetTimeZoneId)
        {
            TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId);
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(targetTimeZoneId);
            DateTime sourceTime = TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone);
            DateTime targetTime = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZone, targetTimeZone);
            return targetTime;
        }

        // Method to calculate time difference between two DateTime objects
        public static TimeSpan CalculateTimeDifference(DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime2 - dateTime1;
        }
    }
}