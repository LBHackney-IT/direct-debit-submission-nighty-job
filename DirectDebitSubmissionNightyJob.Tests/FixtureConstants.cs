using System;
using System.Collections.Generic;
using System.Text;

namespace DirectDebitSubmissionNightyJob.Tests
{
    public static class FixtureConstants
    {
        public static string TenureApiRoute => "http://localhost:4558/api/v1/";
        public static string TenureApiToken => "1235";
        public static string TransactionsApiRoute => "http://localhost:5001/api/v1/";
        public static string TransactionsApiToken => "123";
        public static string PtxApiRoute => "http://localhost:5001/";
        public static string PtxApiToken => "1234";
        public static string RegisterPTXEmail => "asda";
        public static string PTXPassword => "123443";
        public static string PTXProfile => "test";
        public static string HackneySortCode => "1234";
        public static string HackneyAccountNumber => "1234232";
        public static string HackneyAccountName => "Hackney";
    }
}
