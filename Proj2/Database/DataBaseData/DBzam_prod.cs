namespace DataBaseWcfData
{
    using DisplayData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DBzam_prod : zam_prod
    {
        public int? Class_Ilosc { get; set; }

        public int? Class { get; set; }

        public virtual DBProdukt Produkt { get; set; }

        public virtual DBZamowienia Zamowienia { get; set; }
    }
}
