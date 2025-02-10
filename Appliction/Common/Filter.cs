using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Common
{
    public class Filter
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string[] OrderBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string? SearchText { get; set; }
    }
}
