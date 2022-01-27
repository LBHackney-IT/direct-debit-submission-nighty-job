using System;
using System.Collections.Generic;
using System.Text;

namespace DirectDebitSubmissionNightyJob.AppConstants
{
    public static class HackneyFileInfoConstants
    {
        public static string HackneySortCode = Environment.GetEnvironmentVariable("HackneySortCode");
        public const string HackneyAccountNumber = "00641877";
        public const string HackneyAccountName = "LB Hackney";
    }
}
