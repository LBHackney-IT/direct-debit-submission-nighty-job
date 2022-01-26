using System.Collections.Generic;

namespace DirectDebitSubmissionNightyJob.Boundary.Response
{
    public class PagedResult<T> : PagedResultBase
    {
        public IList<T> Results { get; set; }
        public int Residents { get; set; }
        public decimal TotalAmount { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
