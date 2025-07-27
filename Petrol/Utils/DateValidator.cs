using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Petrol.Utils
{
    public static class DateValidator
    {
        private static readonly string DatePattern = @"^(0?[1-9]|[12]\d|3[01])\/(0?[1-9]|1[0-2])\/\d{4}$";
        private static readonly  string[] formats = { "dd/MM/yyyy","d/MM/yyyy","d/M/yyyy","dd/M/yyyy" };
        
        /// <summary>
        /// Validates if the input string is a valid date in dd/MM/yyyy format
        /// </summary>
        /// <param name="dateString">Date string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return false;

            // Check if the string matches the dd/MM/yyyy pattern
            if (!Regex.IsMatch(dateString, DatePattern))
                return false;
            // Try to parse the date - this will validate if the date actually exists
            // For example: 31/02/2023 will fail because February doesn't have 31 days
            return DateTime.TryParseExact(dateString, formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        /// Converts a valid dd/MM/yyyy string to DateTime
        /// </summary>
        /// <param name="dateString">Date string in dd/MM/yyyy format</param>
        /// <returns>DateTime object if valid, null otherwise</returns>
        public static DateTime? ParseDate(string dateString)
        {
            if (!IsValidDate(dateString))
                return null;

            if (DateTime.TryParseExact(dateString, formats, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Formats a DateTime to dd/MM/yyyy string
        /// </summary>
        /// <param name="date">DateTime to format</param>
        /// <returns>Formatted date string</returns>
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Gets the placeholder text for date input
        /// </summary>
        /// <returns>Placeholder text</returns>
        public static string GetPlaceholderText()
        {
            return "dd/MM/yyyy";
        }

        /// <summary>
        /// Validates if start date is before or equal to end date
        /// </summary>
        /// <param name="startDate">Start date string</param>
        /// <param name="endDate">End date string</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidDateRange(string startDate, string endDate)
        {
            var start = ParseDate(startDate);
            var end = ParseDate(endDate);

            if (!start.HasValue || !end.HasValue)
                return false;

            return start.Value.Date <= end.Value.Date;
        }

       
          }
} 