using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplates.DisplayData
{
    [DataContract]
    public class Produkt
    {
        [Key]
        [DataMember]
        public int ID { get; set; }

        [StringLength(255)]
        [DataMember]
        public string Nazwa { get; set; }

        [DataMember]
        public decimal? Cena { get; set; }

        [DataMember]
        public int? Dostepnosc { get; set; }
    }
}
