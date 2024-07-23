using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageModels
{
    public class GetResponseProduct
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
        public string? ProductDesign { get; set; }
        public string? ProductSize { get; set; }
    }
}
