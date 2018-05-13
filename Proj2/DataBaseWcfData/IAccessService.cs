using DataTemplates.DisplayData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DataTemplates
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAccessService
    {
        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        Klient[] GetKlienty();

        [OperationContract]
        Konto[] GetKonta();

        [OperationContract]
        Pracownik[] GetPracownicy();

        [OperationContract]
        Produkt[] GetProdkuty();

        [OperationContract]
        zam_prod[] GetZam_prody();

        [OperationContract]
        Zamowienia[] GetZamowienia();
    }
}
