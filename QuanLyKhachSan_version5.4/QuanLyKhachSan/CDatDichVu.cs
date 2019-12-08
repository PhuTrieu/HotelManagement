using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CDatDichVu
    {
        private CKhachHang kh;
        private List<CDichVu> arrdv;

        internal CKhachHang Kh { get => kh; set => kh = value; }
        internal List<CDichVu> Arrdv { get => arrdv; set => arrdv = value; }

        public CDatDichVu(CKhachHang kh, List<CDichVu> arrdv)
        {
            this.Kh = kh;
            this.Arrdv = arrdv;
        }
        public CDatDichVu() : this(new CKhachHang(), new List<CDichVu>())
        {
        }

        public int tinhTongGiaTien()
        {
            int sum = 0;
            if (Arrdv.Count > 0)
            {
                foreach (CDichVu dv in Arrdv)
                {
                    sum += dv.Giatien;
                }
            }
            return sum;
        }
    }
}
