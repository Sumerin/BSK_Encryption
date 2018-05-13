using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplates.DisplayData
{
    [DataContract]
    public class Zamowienia
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int? Status { get; set; }

        [DataMember]
        public DateTime? Data_zlozenia { get; set; }
    }
}
