namespace DataTemplates
{
    using DisplayData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pracownik")]
    public partial class DBPracownik : Pracownik
    {
        public int? Class_Imie { get; set; }
        
        public int? Class_Nazwisko { get; set; }

        public int? Class_Data_zaczecia { get; set; }

        public int? Class_Stanowisko { get; set; }

        public int? ID_Konto { get; set; }

        public int? Class { get; set; }

        public virtual DBKonto Konto { get; set; }
    }
}
