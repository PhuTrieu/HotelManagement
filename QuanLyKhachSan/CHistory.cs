using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    class CHistory
    {
        private CKhachHang kh;
        private CDatPhong dp;

        internal CKhachHang Kh { get => kh; set => kh = value; }
        internal CDatPhong Dp { get => dp; set => dp = value; }

        public CHistory(CKhachHang kh, CDatPhong dp)
        {
            this.kh = kh;
            this.dp = dp;
        }

        public CHistory():this(new CKhachHang(),new CDatPhong())
        {
        }
    }
}
