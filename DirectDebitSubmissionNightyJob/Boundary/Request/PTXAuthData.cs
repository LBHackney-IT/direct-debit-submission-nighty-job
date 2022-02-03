namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class PTXAuthData
    {
        public string XCsrf { get; set; }
        public string Authtoken { get; set; }
        public string JsessionId { get; set; }
    }
}
