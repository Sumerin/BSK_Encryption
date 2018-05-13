using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataTemplates.DisplayData;
using DataTemplates;

namespace WCFDataBaseMacService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AccessService : IAccessService
    {
        #region field
        private DBKonto konto = null;
        #endregion

        #region Interface
        public Klient[] GetKlienty()
        {
            throw new NotImplementedException();
        }

        public Konto[] GetKonta()
        {
            throw new NotImplementedException();
        }

        public Pracownik[] GetPracownicy()
        {
            throw new NotImplementedException();
        }

        public Produkt[] GetProdkuty()
        {
            throw new NotImplementedException();
        }

        public Zamowienia[] GetZamowienia()
        {
            throw new NotImplementedException();
        }

        public zam_prod[] GetZam_prody()
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region methods
        #endregion
    }
}
