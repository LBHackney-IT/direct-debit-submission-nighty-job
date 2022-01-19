using System.Collections.Generic;

namespace DirectDebitApi.V1.Boundary.Response
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
