using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseWcfData.DisplayData
{
    public class Konto
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public string Login { get; set; }

        public int? Clear { get; set; }

    }
}
