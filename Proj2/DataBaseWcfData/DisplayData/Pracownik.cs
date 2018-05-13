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
    public class Pracownik
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

        [DataMember]
        public DateTime? Data_zaczecia { get; set; }

        [DataMember]
        [StringLength(255)]
        public string Stanowisko { get; set; }

    }
}
