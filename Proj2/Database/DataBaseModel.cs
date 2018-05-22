namespace DataTemplates
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataBaseModel : DbContext
    {
        public DataBaseModel()
            : base("name=DataBaseModel")
        {
        }

        public virtual DbSet<DBKlient> Klient { get; set; }
        public virtual DbSet<DBKonto> Konto { get; set; }
        public virtual DbSet<DBPracownik> Pracownik { get; set; }
        public virtual DbSet<DBProdukt> Produkt { get; set; }
        public virtual DbSet<DBzam_prod> zam_prod { get; set; }
        public virtual DbSet<DBZamowienia> Zamowienia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBKlient>()
                .Property(e => e.Imie)
                .IsUnicode(false);

            modelBuilder.Entity<DBKlient>()
                .Property(e => e.Nazwisko)
                .IsUnicode(false);

            modelBuilder.Entity<DBKlient>()
                .Property(e => e.Adres)
                .IsUnicode(false);

            modelBuilder.Entity<DBKlient>()
                .HasMany(e => e.Zamowienia)
                .WithOptional(e => e.Klient)
                .HasForeignKey(e => e.ID_Klienta);

            modelBuilder.Entity<DBKonto>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<DBKonto>()
                .Property(e => e.Haslo)
                .IsUnicode(false);

            modelBuilder.Entity<DBKonto>()
                .Property(e => e.Salt)
                .IsUnicode(false);

            modelBuilder.Entity<DBKonto>()
                .HasMany(e => e.Klient)
                .WithOptional(e => e.Konto)
                .HasForeignKey(e => e.ID_Konto);

            modelBuilder.Entity<DBKonto>()
                .HasMany(e => e.Pracownik)
                .WithOptional(e => e.Konto)
                .HasForeignKey(e => e.ID_Konto);

            modelBuilder.Entity<DBPracownik>()
                .Property(e => e.Imie)
                .IsUnicode(false);

            modelBuilder.Entity<DBPracownik>()
                .Property(e => e.Nazwisko)
                .IsUnicode(false);

            modelBuilder.Entity<DBPracownik>()
                .Property(e => e.Stanowisko)
                .IsUnicode(false);

            modelBuilder.Entity<DBProdukt>()
                .Property(e => e.Nazwa)
                .IsUnicode(false);

            modelBuilder.Entity<DBProdukt>()
                .Property(e => e.Cena)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DBProdukt>()
                .HasMany(e => e.zam_prod)
                .WithRequired(e => e.Produkt)
                .HasForeignKey(e => e.ID_Produkt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DBZamowienia>()
                .HasMany(e => e.zam_prod)
                .WithRequired(e => e.Zamowienia)
                .HasForeignKey(e => e.ID_Zamowienia)
                .WillCascadeOnDelete(false);
        }
    }
}
