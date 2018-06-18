namespace DataTemplates
{
    using DisplayData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Klient")]
    public partial class DBKlient : Klient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBKlient()
        {
            Zamowienia = new HashSet<DBZamowienia>();
        }
        
        public int? Class_Imie { get; set; }

        public int? Class_Nazwisko { get; set; }

        public int? Class_Adres { get; set; }


        public int? Class { get; set; }

        public virtual DBKonto Konto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBZamowienia> Zamowienia { get; set; }
    }
}
