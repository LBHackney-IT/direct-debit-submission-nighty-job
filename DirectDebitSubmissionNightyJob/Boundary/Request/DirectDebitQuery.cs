using DirectDebitSubmissionNightyJob.Domain.Enums;
using System;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class DirectDebitQuery
    {
        public TargetTypeEnum? TargetType { get; set; }
        public Guid? TargetId { get; set; }
        public string Status { get; set; }
        public int? CollectionDate { get; set; }
        public string SearchKeyword { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
