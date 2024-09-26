using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kampanya_Gönderme_Programı
{
    public class Kampanya
    {
        public int KampanyaId { get; set; }
        public string KampanyaIcerik { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}
