using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseWcfData.DisplayData
{
    public class Produkt
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public string Nazwa { get; set; }
        
        public decimal? Cena { get; set; }
        
        public int? Dostepnosc { get; set; }
    }
}
