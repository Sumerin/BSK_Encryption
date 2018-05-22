namespace DataTemplates
{
    using DisplayData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zamowienia")]
    public partial class DBZamowienia : Zamowienia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBZamowienia()
        {
            zam_prod = new HashSet<DBzam_prod>();
        }
        
        public int? Class_Status { get; set; }

        public int? Class_Data_zlozenia { get; set; }

        public int? Class { get; set; }

        public virtual DBKlient Klient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBzam_prod> zam_prod { get; set; }
    }
}
