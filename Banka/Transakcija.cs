using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka
{
    public class Transakcija
    {
        public Racun Izvor { get; set; }
        public Racun Odrediste { get; set; }
        public double Iznos { get; set; }
        public double Naplaceno { get; set; }
        public double PreostaloNaplatiti { get; set; }
    }
}
