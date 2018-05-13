using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplates.DisplayData
{
    [DataContract]
    public class zam_prod
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public int ID_Zamowienia { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public int ID_Produkt { get; set; }

        [DataMember]
        public int? Ilosc { get; set; }

    }
}
