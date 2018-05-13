using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseWcfData.DisplayData
{
    public class Pracownik
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public string Imie { get; set; }

        [StringLength(255)]
        public string Nazwisko { get; set; }
        
        public DateTime? Data_zaczecia { get; set; }

        [StringLength(255)]
        public string Stanowisko { get; set; }

    }
}
