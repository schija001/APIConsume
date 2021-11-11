using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class PageWrapper
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }
        public string nextPage { get; set; }
        public object previousPage { get; set; }
        public List<People> data { get; set; } 
        public bool succeeded { get; set; }
        public object errors { get; set; }
        public object message { get; set; }
    }
}
