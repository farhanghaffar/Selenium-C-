using System;
using System.Globalization;

namespace MRO.ROI.Automation.Utility
{
    public static class UiHelper
    {
        public static string ReturnMonthName()
        {
            int month = new Random().Next(1, 12);
            DateTime date = new DateTime(2020, month, 1);
            return date.ToString("MMMM");
        }
    }
}
