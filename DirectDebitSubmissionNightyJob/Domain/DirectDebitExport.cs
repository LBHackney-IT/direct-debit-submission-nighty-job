namespace DirectDebitSubmissionNightyJob.Domain
{
    public class DirectDebitExport
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public int Due { get; set; }

        public string PRN { get; set; }
        public string AccountNumber { get; set; }
        public string Fund { get; set; }
        public string Type { get; set; }
        public int Acc { get; set; }
        public int Trans { get; set; }
        public decimal Amount { get; set; }
    }
}
