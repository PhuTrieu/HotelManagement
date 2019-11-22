using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CDatPhong 
    {
        private CKhachHang kh;
        private CPhong phong;
        private DateTime m_Ngayden;
        private DateTime m_Ngaydi;

        public DateTime Ngayden { get => m_Ngayden; set => m_Ngayden = value; }
        public DateTime Ngaydi { get => m_Ngaydi; set => m_Ngaydi = value; }
        internal CKhachHang Kh { get => kh; set => kh = value; }
        internal CPhong Phong { get => phong; set => phong = value; }

        public CDatPhong() : this(new CKhachHang(),new CPhong(), DateTime.Now, DateTime.Now)
        {
        }

        public CDatPhong(CKhachHang kh, CPhong phong, DateTime m_Ngayden, DateTime m_Ngaydi)
        {
            this.kh = kh;
            this.phong = phong;
            this.m_Ngayden = m_Ngayden;
            this.m_Ngaydi = m_Ngaydi;
        }

        public int SoNgayO()
        {
            TimeSpan s = m_Ngaydi - m_Ngayden;
            int kq = s.Days + 1;
            return kq;
        }
        public float ThanhTien()
        {
            float s;
            if (string.Compare("Đơn", phong.Loaiphong) == 0)
            {
                s = (float)(100000 * SoNgayO());
            }
            else if (string.Compare("Đôi", phong.Loaiphong) == 0)
            {
                s = (float)(300000 * SoNgayO());
            }
            else s = (float)(600000 * SoNgayO());
            return s;
        }
    }
}
