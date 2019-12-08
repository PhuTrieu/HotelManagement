using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CBill
    {
        private CKhachHang m_kh;
        private CDatPhong m_dp;
        private CDatDichVu m_ddv;
        private DateTime m_ngaythanhtoan;

        public CBill(CKhachHang kh, CDatPhong dp, CDatDichVu ddv, DateTime ngaythanhtoan)
        {
            m_kh = kh;
            m_dp = dp;
            m_ddv = ddv;
            m_ngaythanhtoan = ngaythanhtoan;
        }

        public CBill(): this(new CKhachHang(),new CDatPhong(),new CDatDichVu(),DateTime.Now)
        {
        }

        public DateTime Ngaythanhtoan { get => m_ngaythanhtoan; set => m_ngaythanhtoan = value; }
        internal CKhachHang Kh { get => m_kh; set => m_kh = value; }
        internal CDatPhong Dp { get => m_dp; set => m_dp = value; }
        internal CDatDichVu Ddv { get => m_ddv; set => m_ddv = value; }

        //public float tinhTongThanhTien()
        //{
        //    if (m_dp == null && m_ddv == null) return 0;
        //    else if (m_dp == null) return m_ddv.tinhTongGiaTien();
        //    else if (m_ddv == null) return m_dp.ThanhTien();
        //    return m_dp.ThanhTien() + m_ddv.tinhTongGiaTien();
        //}
    }
}
