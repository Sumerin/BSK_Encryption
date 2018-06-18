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
        bool Register(string username, string password,int clear);

        [OperationContract]
        List<Klient> GetKlienty();

        [OperationContract]
        List<Konto> GetKonta();

        [OperationContract]
        List<Pracownik> GetPracownicy();

        [OperationContract]
        List<Produkt> GetProdkuty();

        [OperationContract]
        List<zam_prod> GetZam_prody();

        [OperationContract]
        List<Zamowienia> GetZamowienia();

        [OperationContract]
        int MyKlientId();

        [OperationContract]
        int? GetClear();

        [OperationContract]
        bool SetZam(Zamowienia input, int[] classes);

        [OperationContract]
        bool SetZam_prody(zam_prod input, int[] classes);

        [OperationContract]
        bool SetProdukt(Produkt input, int[] classes);

        [OperationContract]
        bool SetKonto(Konto input, int[] classes,string pass);

        [OperationContract]
        bool SetKlient(Klient input, int[] classes);

        [OperationContract]
        bool SetPracownik(Pracownik input, int[] classes);
    }
}
