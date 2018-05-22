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
    public class Zamowienia
    {
        [Key]
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int? Status { get; set; }

        [DataMember]
        public DateTime? Data_zlozenia { get; set; }
        
        [DataMember]
        public int? ID_Klienta { get; set; }
    }
}
