using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Notices
    {
        public int nNo { get; set; }
        public string nTitle { get; set; }
        public string nContents { get; set; }
        public string delYn { get; set; }
    }
}
