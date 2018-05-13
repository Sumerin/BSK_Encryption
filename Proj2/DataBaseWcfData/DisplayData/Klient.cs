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
    public class Klient
    {
        [Key]
        [DataMember]
        public int ID { get; set; }

        [StringLength(255)]
        [DataMember]
        public string Imie { get; set; }

        [StringLength(255)]
        [DataMember]
        public string Nazwisko { get; set; }

        [StringLength(255)]
        [DataMember]
        public string Adres { get; set; }

    }
}
