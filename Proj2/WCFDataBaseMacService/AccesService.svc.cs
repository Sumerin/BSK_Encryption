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

        #region Interface
        public bool Login(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return false;
            }

            this.konto = (from ctxKonto in ctx.Konto
                          where ctxKonto.Login.Equals(username) 
                                && md5Hash.VerifyMd5Hash(password, ctxKonto.Salt, ctxKonto.Haslo)
                          select (ctxKonto)).FirstOrDefault();

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

        public bool Register(string username, string password)
        {
            throw new NotImplementedException();
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
            int? clear = this.konto.Clear ;
            var resultClient = new Klient();

            if (IsRead(client.Class,clear))
            {
                resultClient.ID = client.ID;
                resultClient.Imie = IsRead(client.Class_Imie, clear) ? client.Imie : null;
                resultClient.Nazwisko = IsRead(client.Class_Nazwisko, clear) ? client.Nazwisko : null;
                resultClient.Adres = IsRead(client.Class_Adres, clear) ? client.Adres : null;

                return resultClient;
            }
            return null;
        }

        private Konto RestrictReadKonto(DBKonto account)
        {
            throw new NotImplementedException();
        }

        private Pracownik RestrictReadPracownik(DBPracownik worker)
        {
            throw new NotImplementedException();
        }

        private Produkt RestrictReadProdukt(DBProdukt product)
        {
            throw new NotImplementedException();
        }

        private zam_prod RestrictReadZam_pod(DBzam_prod orderProduct)
        {
            throw new NotImplementedException();
        }

        private Zamowienia RestrictReadZamowienia(DBZamowienia order)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region WriteUpAccess
        #endregion
        #endregion
    }
}
