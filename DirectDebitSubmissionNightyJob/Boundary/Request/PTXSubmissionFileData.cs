namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class PTXSubmissionFileData
    {

        public string Name { get; set; }
        public string Ref { get; set; }
        public string Sort { get; set; }
        public string Number { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }
        public string Type { get; set; } = "17";

    }
}
