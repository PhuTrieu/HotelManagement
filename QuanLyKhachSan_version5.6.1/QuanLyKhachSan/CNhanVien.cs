using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CNhanVien
    {
        private string m_MaNV;
        private string m_HoTen;
        private bool m_GioiTinh;
        private DateTime m_NgaySinh;
        private int m_Sdt;
        private string m_QueQuan;
        private string m_ChucVu;
        private int m_Luongcb;
        private int m_songaylam;

        public string MaNV { get => m_MaNV; set => m_MaNV = value; }
        public string HoTen { get => m_HoTen; set => m_HoTen = value; }
        public bool GioiTinh { get => m_GioiTinh; set => m_GioiTinh = value; }
        public DateTime NgaySinh { get => m_NgaySinh; set => m_NgaySinh = value; }
        public int Sdt { get => m_Sdt; set => m_Sdt = value; }
        public string QueQuan { get => m_QueQuan; set => m_QueQuan = value; }
        public string ChucVu { get => m_ChucVu; set => m_ChucVu = value; }
        public int Luongcb { get => m_Luongcb; set => m_Luongcb = value; }
        public int Songaylam { get => m_songaylam; set => m_songaylam = value; }

        public CNhanVien()
        {
            m_MaNV = " ";
            m_HoTen = "";
            m_GioiTinh = false;
            m_NgaySinh = DateTime.Now;
            m_Sdt = 0;
            m_QueQuan = "";
            m_ChucVu = "";
            m_songaylam = 0;
        }

        public double TongLuong()
        {
            return (double)(m_Luongcb * m_songaylam);
        }
    }
}
