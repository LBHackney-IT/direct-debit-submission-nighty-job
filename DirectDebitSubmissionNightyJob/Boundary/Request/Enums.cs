using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public enum TargetType
    {
        Tenure
    }

    public enum TransactionType
    {
        [Description("Direct Debit")]
        DirectDebit,
    }
}
