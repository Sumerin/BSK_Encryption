using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseWcfData.DisplayData
{
    public class Zamowienia
    {
        public int ID { get; set; }

        public int? Status { get; set; }

        public DateTime? Data_zlozenia { get; set; }
    }
}
