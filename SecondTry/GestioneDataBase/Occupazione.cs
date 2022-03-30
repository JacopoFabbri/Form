using System;
using System.Collections.Generic;

namespace SecondTry
{
    public partial class Occupazione
    {
        public int Id { get; set; }
        public string Indirizzo { get; set; } = null!;
        public string Commessa { get; set; } = null!;
        public DateTime DataInserimento { get; set; }
        public string Cartella_Destinazione { get; set; } = null!;
    }
}
