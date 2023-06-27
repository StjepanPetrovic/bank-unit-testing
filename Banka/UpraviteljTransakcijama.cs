using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka
{
    public class InvalidBankAccountException : ApplicationException
    {
        public InvalidBankAccountException()
        {

        }
    }

    public class InvalidAmountException : ApplicationException
    {
        public InvalidAmountException()
        {

        }
    }

    public class UpraviteljTransakcijama
    {
        private List<Racun> Racuni { get; set; }

        public UpraviteljTransakcijama()
        {
            Racuni = new List<Racun>();
            Racuni.Add(new Racun { IBAN = "HR11", Stanje = 100000 });
            Racuni.Add(new Racun { IBAN = "HR22", Stanje = 50000 });
            Racuni.Add(new Racun { IBAN = "HR33", Stanje = 12000 });
            Racuni.Add(new Racun { IBAN = "HR44", Stanje = 36000 });
            Racuni.Add(new Racun { IBAN = "HR55", Stanje = 8000 });
            Racuni.Add(new Racun { IBAN = "HR66", Stanje = 2000 });
        }

        public Transakcija PrebaciSredstva(string izvorIBAN, string odredisteIBAN, double iznos)
        {
            Racun izvor = Racuni.FirstOrDefault(r => r.IBAN == izvorIBAN);
            Racun odrediste = Racuni.FirstOrDefault(r => r.IBAN == odredisteIBAN);

            if (izvor == null || odrediste == null)
            {
                throw new InvalidBankAccountException();
            }

            if (iznos <= 0)
            {
                throw new InvalidAmountException();
            }

            Transakcija t = new Transakcija();
            t.Izvor = izvor;
            t.Odrediste = odrediste;
            t.Iznos = iznos;

            if (izvor.Stanje >= iznos)
            {
                t.Naplaceno = iznos;
            }
            else
            {
                t.Naplaceno = izvor.Stanje;
                
            }
            t.PreostaloNaplatiti = t.Iznos - t.Naplaceno;

            izvor.Stanje = izvor.Stanje - t.Naplaceno;
            odrediste.Stanje = odrediste.Stanje + t.Naplaceno;

            return t;
        }
    }
}
