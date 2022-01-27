using System;
using System.Collections.Generic;
using System.Text;

namespace DirectDebitSubmissionNightyJob.Exceptions.Models
{
    public class ModelStateError
    {
        public ModelStateError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
