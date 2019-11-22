using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    class CNhanVien
    {
        private string m_MaNV;
        private string m_HoTen;
        private bool m_GioiTinh;
        private DateTime m_NgaySinh;
        private int m_Sdt;
        private string m_QueQuan;
        private string m_ChucVu;
        private string m_Luong;

        public string MaNV { get => m_MaNV; set => m_MaNV = value; }
        public string HoTen { get => m_HoTen; set => m_HoTen = value; }
        public bool GioiTinh { get => m_GioiTinh; set => m_GioiTinh = value; }
        public DateTime NgaySinh { get => m_NgaySinh; set => m_NgaySinh = value; }
        public int Sdt { get => m_Sdt; set => m_Sdt = value; }
        public string QueQuan { get => m_QueQuan; set => m_QueQuan = value; }
        public string ChucVu { get => m_ChucVu; set => m_ChucVu = value; }
        public string Luong { get => m_Luong; set => m_Luong = value; }

        public CNhanVien(string m_MaNV, string m_HoTen, bool m_GioiTinh, DateTime m_NgaySinh, int m_Sdt, string m_QueQuan, string m_ChucVu, string m_Luong)
        {
            this.m_MaNV = m_MaNV;
            this.m_HoTen = m_HoTen;
            this.m_GioiTinh = m_GioiTinh;
            this.m_NgaySinh = m_NgaySinh;
            this.m_Sdt = m_Sdt;
            this.m_QueQuan = m_QueQuan;
            this.m_ChucVu = m_ChucVu;
            this.m_Luong = m_Luong;
        }

        public CNhanVien()
        {
            m_MaNV = "";
            m_HoTen = "";
            m_GioiTinh = false;
            m_NgaySinh = DateTime.Now;
            m_Sdt = 0;
            m_QueQuan = "";
            m_ChucVu = " ";
            m_Luong = " ";
        }
    }
}
