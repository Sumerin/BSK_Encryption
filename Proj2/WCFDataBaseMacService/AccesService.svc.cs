using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataTemplates.DisplayData;
using DataTemplates;
using System.Runtime.CompilerServices;

namespace WCFDataBaseMacService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class AccessService : IAccessService
    {
        #region field
        private DataBaseModel ctx = null;
        private MD5Hash md5Hash = null;
        private DBKonto konto = null;
        #endregion

        public AccessService()
        {
            ctx = new DataBaseModel();
            md5Hash = new MD5Hash();
        }

        public AccessService(DataBaseModel ctx, MD5Hash md5Hash)
        {
            this.ctx = ctx;
            this.md5Hash = md5Hash;
        }

        private int GetID()
        {
            return konto.ID;
        }

        public int? GetClear()
        {
            return konto.Clear;
        }

        public int MyKlientId()
        {
            int id = GetID();
            var person = ctx.Klient.Where(x => x.ID_Konto == id).First();
            return person.ID;
        }

        #region Interface
        public bool Login(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return false;
            }

            //this.konto = (from ctxKonto in ctx.Konto
            //              where ctxKonto.Login.Equals(username)
            //                    && md5Hash.VerifyMd5Hash(password, ctxKonto.Salt, ctxKonto.Haslo)
            //              select (ctxKonto)).FirstOrDefault();

            foreach (var ctxKonto in ctx.Konto)
            {
                if (ctxKonto.Login.Equals(username)
                            && md5Hash.VerifyMd5Hash(password, ctxKonto.Salt, ctxKonto.Haslo))
                {
                    this.konto = ctxKonto;
                }
            }

            return this.konto != null;
        }

        public List<Klient> GetKlienty()
        {
            if (this.konto == null)
            {
                return null;
            }

            return Restricter<DBKlient, Klient>
                .Restrict(ctx.Klient, RestrictReadKlient);
        }

        public List<Konto> GetKonta()
        {
            if (this.konto == null)
            {
                return null;
            }

            return Restricter<DBKonto, Konto>
                .Restrict(ctx.Konto, RestrictReadKonto);
        }

        public List<Pracownik> GetPracownicy()
        {
            if (this.konto == null)
            {
                return null;
            }

            return Restricter<DBPracownik, Pracownik>
                .Restrict(ctx.Pracownik, RestrictReadPracownik);
        }

        public List<Produkt> GetProdkuty()
        {
            if (this.konto == null)
            {
                return null;
            }

            return Restricter<DBProdukt, Produkt>
                .Restrict(ctx.Produkt, RestrictReadProdukt);
        }

        public List<Zamowienia> GetZamowienia()
        {
            if (this.konto == null)
            {
                return null;
            }

            return Restricter<DBZamowienia, Zamowienia>
                .Restrict(ctx.Zamowienia, RestrictReadZamowienia);
        }

        public List<zam_prod> GetZam_prody()
        {
            if (this.konto == null)
            {
                return null;
            }

            return Restricter<DBzam_prod, zam_prod>
                .Restrict(ctx.zam_prod, RestrictReadZam_pod);
        }

        public bool Register(string username, string password, int clear)
        {
            string hash = password + "a6s8d";
            var konto = new DBKonto() { Login = username, Haslo = md5Hash.GetMD5Hash(hash), Salt = "a6s8d", Clear = clear, Class_Haslo = 1, Class = 1, Class_Login = 1, Class_salt = 1 };
            ctx.Konto.Add(konto);
            ctx.SaveChanges();
            int? id = ctx.Konto.Where(c => c.Login.Equals(username)).First().ID;
            var kl = new DBKlient() { Imie = username, Nazwisko = username, Adres = username + " 11", Class_Adres = 1, Class_Imie = 1, Class_Nazwisko = 1, Class = 1, ID_Konto = id };
            ctx.Klient.Add(kl);
            DBPracownik p;
            if (clear > 1)
            {
                p = new DBPracownik() { Imie = username, Nazwisko = username, Class_Imie = 1, Class_Nazwisko = 1, Class = 1, ID_Konto = id, Data_zaczecia = DateTime.Now, Class_Data_zaczecia = 1, Stanowisko = "Test", Class_Stanowisko = 1 };
                ctx.Pracownik.Add(p);
            }

            ctx.SaveChanges();
            return true;
        }

        public bool SetZam(Zamowienia input, int[] classes)
        {
            if (this.konto == null)
            {
                return false;
            }
            DBZamowienia newOrder = new DBZamowienia()
            {
                ID_Klienta = input.ID_Klienta,
                Data_zlozenia = input.Data_zlozenia,
                Class_Data_zlozenia = classes[1],
                Status = input.Status,
                Class_Status = classes[0],
                Class = classes[2]
            };

            if (Locker<DBZamowienia>.Lock(newOrder, LockWriteZamowienia))
            {
                ctx.Zamowienia.Add(newOrder);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetZam_prody(zam_prod input, int[] classes)
        {
            if (this.konto == null)
            {
                return false;
            }
            DBzam_prod newProdOrd = new DBzam_prod()
            {
                ID_Produkt = input.ID_Produkt,
                ID_Zamowienia = input.ID_Zamowienia,
                Ilosc = input.Ilosc,
                Class_Ilosc = classes[0],
                Class = classes[1]
            };

            if (Locker<DBzam_prod>.Lock(newProdOrd, LockWriteZam_prod))
            {
                ctx.zam_prod.Add(newProdOrd);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetProdukt(Produkt input, int[] classes)
        {
            if (this.konto == null)
            {
                return false;
            }
            DBProdukt newProd = new DBProdukt()
            {
                Nazwa = input.Nazwa,
                Class_Nazwa = classes[0],
                Cena = input.Cena,
                Class_Cena = classes[1],
                Dostepnosc = input.Dostepnosc,
                Class_Dostepnosc = classes[2],
                Class = classes[3]
            };

            if (Locker<DBProdukt>.Lock(newProd, LockWriteProdukt))
            {
                ctx.Produkt.Add(newProd);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetKonto(Konto input, int[] classes, string pass)
        {
            if (this.konto == null)
            {
                return false;
            }
            string hash = pass + "a6s8d";
            DBKonto newKon = new DBKonto() { Login = input.Login, Haslo = md5Hash.GetMD5Hash(hash), Salt = "a6s8d", Clear = input.Clear, Class_Haslo = classes[1], Class = classes[3], Class_Login = classes[0], Class_salt = classes[2] };

            if (Locker<DBKonto>.Lock(newKon, LockWriteKonta))
            {
                ctx.Konto.Add(newKon);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }


        public bool SetKlient(Klient input, int[] classes)
        {
            if (this.konto == null)
            {
                return false;
            }
            DBKlient newKli = new DBKlient()
            {
                Imie = input.Imie,
                Class_Imie = classes[0],
                Nazwisko = input.Nazwisko,
                Class_Nazwisko = classes[1],
                Adres = input.Adres,
                Class_Adres = classes[2],
                ID_Konto=input.ID_Konto,
                Class = classes[3]
            };

            if (Locker<DBKlient>.Lock(newKli, LockWriteKlient))
            {
                ctx.Klient.Add(newKli);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetPracownik(Pracownik input, int[] classes)
        {
            if (this.konto == null)
            {
                return false;
            }
            DBPracownik newPra = new DBPracownik()
            {
                Imie = input.Imie,
                Class_Imie = classes[0],
                Nazwisko = input.Nazwisko,
                Class_Nazwisko = classes[1],
                Data_zaczecia = input.Data_zaczecia,
                Class_Data_zaczecia = classes[2],
                Stanowisko=input.Stanowisko,
                Class_Stanowisko=classes[3],
                ID_Konto = input.ID_Konto,
                Class = classes[4]
            };

            if (Locker<DBPracownik>.Lock(newPra, LockWritePracownik))
            {
                ctx.Pracownik.Add(newPra);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region methods
        #region ReadDownAccess
        /// <summary>
        /// Indicates read permission.
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="clear"></param>
        /// <returns>
        /// <c>True</c> on access
        /// <c>False</c> on denied
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IsRead(int? Class, int? clear)
        {
            int cla = Class != null ? (int)Class : 0;
            int cle = clear != null ? (int)clear : 0;
            return cla <= cle;
        }

        private Klient RestrictReadKlient(DBKlient client)
        {
            int? clear = this.konto.Clear;
            var resultClient = new Klient();

            if (IsRead(client.Class, clear))
            {
                resultClient.ID = client.ID;
                resultClient.Imie = IsRead(client.Class_Imie, clear) ? client.Imie : null;
                resultClient.Nazwisko = IsRead(client.Class_Nazwisko, clear) ? client.Nazwisko : null;
                resultClient.Adres = IsRead(client.Class_Adres, clear) ? client.Adres : null;
                resultClient.ID_Konto = client.ID_Konto;

                return resultClient;
            }
            return null;
        }

        private Konto RestrictReadKonto(DBKonto account)
        {
            int? clear = this.konto.Clear;
            var resultKonto = new Konto();

            if (IsRead(account.Class, clear))
            {
                resultKonto.ID = account.ID;
                resultKonto.Login = IsRead(account.Class_Login, clear) ? account.Login : null;
                resultKonto.Clear = account.Clear;

                return resultKonto;
            }
            return null;
        }

        private Pracownik RestrictReadPracownik(DBPracownik worker)
        {
            int? clear = this.konto.Clear;
            var resultPracownik = new Pracownik();

            if (IsRead(worker.Class, clear))
            {
                resultPracownik.ID = worker.ID;
                resultPracownik.Imie = IsRead(worker.Class_Imie, clear) ? worker.Imie : null;
                resultPracownik.Nazwisko = IsRead(worker.Class_Nazwisko, clear) ? worker.Nazwisko : null;
                resultPracownik.Stanowisko = IsRead(worker.Class_Stanowisko, clear) ? worker.Stanowisko : null;
                resultPracownik.Data_zaczecia = IsRead(worker.Class_Data_zaczecia, clear) ? worker.Data_zaczecia : null;
                resultPracownik.ID_Konto = worker.ID_Konto;


                return resultPracownik;
            }
            return null;
        }

        private Produkt RestrictReadProdukt(DBProdukt product)
        {
            int? clear = this.konto.Clear;
            var resultProdukt = new Produkt();

            if (IsRead(product.Class, clear))
            {
                resultProdukt.ID = product.ID;
                resultProdukt.Nazwa = IsRead(product.Class_Nazwa, clear) ? product.Nazwa : null;
                resultProdukt.Cena = IsRead(product.Class_Cena, clear) ? product.Cena : null;
                resultProdukt.Dostepnosc = IsRead(product.Class_Dostepnosc, clear) ? product.Dostepnosc : null;

                return resultProdukt;
            }
            return null;
        }

        private zam_prod RestrictReadZam_pod(DBzam_prod orderProduct)
        {
            int? clear = this.konto.Clear;
            var resultZam_prod = new zam_prod();

            if (IsRead(orderProduct.Class, clear))
            {
                resultZam_prod.ID_Produkt = orderProduct.ID_Produkt;
                resultZam_prod.ID_Zamowienia = orderProduct.ID_Zamowienia;
                resultZam_prod.Ilosc = IsRead(orderProduct.Class_Ilosc, clear) ? orderProduct.Ilosc : null;

                return resultZam_prod;
            }
            return null;
        }

        private Zamowienia RestrictReadZamowienia(DBZamowienia order)
        {
            int? clear = this.konto.Clear;
            var resultZamowienia = new Zamowienia();

            if (IsRead(order.Class, clear))
            {
                resultZamowienia.ID = order.ID;
                resultZamowienia.Status = IsRead(order.Class_Status, clear) ? order.Status : null;
                resultZamowienia.Data_zlozenia = IsRead(order.Class_Data_zlozenia, clear) ? order.Data_zlozenia : null;
                resultZamowienia.ID_Klienta = order.ID_Klienta;
                return resultZamowienia;
            }
            return null;
        }
        #endregion
        #region WriteUpAccess
        bool IsWrite(int? Class, int? clear)
        {
            int cla = Class != null ? (int)Class : 0;
            int cle = clear != null ? (int)clear : 0;
            return cla >= cle;
        }

        private bool LockWriteKonta(DBKonto input)
        {
            int? clear = this.konto.Clear;
            if (IsWrite(input.Class, clear))
            {
                if (!IsWrite(input.Class_Haslo, clear))
                    return false;
                if (!IsWrite(input.Class_Login, clear))
                    return false;
                if (!IsWrite(input.Class_salt, clear))
                    return false;
                return true;

            }

            return false;
        }

        private bool LockWriteKlient(DBKlient input)
        {
            int? clear = this.konto.Clear;
            if (IsWrite(input.Class, clear))
            {
                if (!IsWrite(input.Class_Adres, clear))
                    return false;
                if (!IsWrite(input.Class_Imie, clear))
                    return false;
                if (!IsWrite(input.Class_Nazwisko, clear))
                    return false;
                return true;

            }

            return false;
        }

        private bool LockWritePracownik(DBPracownik input)
        {
            int? clear = this.konto.Clear;
            if (IsWrite(input.Class, clear))
            {
                if (!IsWrite(input.Class_Data_zaczecia, clear))
                    return false;
                if (!IsWrite(input.Class_Imie, clear))
                    return false;
                if (!IsWrite(input.Class_Nazwisko, clear))
                    return false;
                if (!IsWrite(input.Class_Stanowisko, clear))
                    return false;
                return true;

            }

            return false;
        }

        private bool LockWriteProdukt(DBProdukt input)
        {
            int? clear = this.konto.Clear;
            if (IsWrite(input.Class, clear))
            {
                if (!IsWrite(input.Class_Cena, clear))
                    return false;
                if (!IsWrite(input.Class_Dostepnosc, clear))
                    return false;
                if (!IsWrite(input.Class_Nazwa, clear))
                    return false;
                return true;

            }

            return false;
        }

        private bool LockWriteZamowienia(DBZamowienia input)
        {
            int? clear = this.konto.Clear;
            if (IsWrite(input.Class, clear))
            {
                if (!IsWrite(input.Class_Status, clear))
                    return false;
                if (!IsWrite(input.Class_Data_zlozenia, clear))
                    return false;
                return true;

            }

            return false;
        }

        private bool LockWriteZam_prod(DBzam_prod input)
        {
            int? clear = this.konto.Clear;
            if (IsWrite(input.Class, clear))
            {
                if (!IsWrite(input.Class_Ilosc, clear))
                    return false;
                return true;

            }

            return false;
        }
        #endregion
        #endregion
    }
}
