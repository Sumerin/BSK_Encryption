namespace DataTemplates
{
    using DisplayData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Konto")]
    public partial class DBKonto : Konto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBKonto()
        {
            Klient = new HashSet<DBKlient>();
            Pracownik = new HashSet<DBPracownik>();
        }
      
        public int? Class_Login { get; set; }

        [StringLength(32)]
        public string Haslo { get; set; }

        public int? Class_Haslo { get; set; }

        [StringLength(5)]
        public string Salt { get; set; }

        public int? Class_salt { get; set; }

        public int? Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBKlient> Klient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBPracownik> Pracownik { get; set; }
    }
}
