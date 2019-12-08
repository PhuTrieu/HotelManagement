using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CDichVu
    {
        private string smadv;
        private string stendv;
        private int giatien;

        public CDichVu(string smadv, string stendv, int giatien)
        {
            this.smadv = smadv;
            this.stendv = stendv;
            this.giatien = giatien;
        }

        public CDichVu(): this("","",0)
        {
        }

        public string Smadv { get => smadv; set => smadv = value; }
        public string Stendv { get => stendv; set => stendv = value; }
        public int Giatien { get => giatien; set => giatien = value; }
    }
}
