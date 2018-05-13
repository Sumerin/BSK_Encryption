using DataTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new DataBaseModel())
            {
                var klin = new DBKlient();
                klin.Imie = "Adam";
                klin.Nazwisko = "Niezgodka";
                klin.Adres = "tam";

                ctx.Klient.Add(klin);
                ctx.SaveChanges();

            }

            using (var ctx = new DataBaseModel())
            {

                foreach (var item in ctx.Klient)
                {
                    Console.WriteLine(item.ID + item.Imie);
                }

            }
        }
    }
}
