using System;
namespace FFSAPI.Models
{
    public class EtikettData
    {
        public string Filmnamn { get; set; }
        public string Ort { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;

    }
}
