namespace DataBaseWcfData
{
    using DisplayData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Produkt")]
    public partial class DBProdukt : Produkt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBProdukt()
        {
            zam_prod = new HashSet<DBzam_prod>();
        }

        public int? Class_Nazwa { get; set; }

        public int? Class_Cena { get; set; }

        public int? Class_Dostepnosc { get; set; }

        public int? Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBzam_prod> zam_prod { get; set; }
    }
}
