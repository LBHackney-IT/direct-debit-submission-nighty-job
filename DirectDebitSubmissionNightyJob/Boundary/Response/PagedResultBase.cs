using System;

namespace DirectDebitSubmissionNightyJob.Boundary.Response
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

    }
}
