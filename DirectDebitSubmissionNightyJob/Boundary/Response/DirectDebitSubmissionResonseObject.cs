using System;
using System.Collections.Generic;

namespace DirectDebitApi.V1.Boundary.Response
{
    public class DirectDebitSubmissionResonseObject
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public List<DirectDebitResponseObject> DirectDebits { get; set; }
        public DateTime DateCreated { get; set; }
        public int DateOfCollection { get; set; }
        public string Status { get; set; }

    }
}
