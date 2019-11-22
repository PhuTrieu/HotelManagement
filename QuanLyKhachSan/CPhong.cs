using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CPhong
    {
        private int m_Sophong;
        private string m_Loaiphong;
        private string m_Trangthai;
        private string m_Gia;

        public int Sophong { get => m_Sophong; set => m_Sophong = value; }
        public string Loaiphong { get => m_Loaiphong; set => m_Loaiphong = value; }
        public string Trangthai { get => m_Trangthai; set => m_Trangthai = value; }
        public string Gia { get => m_Gia; set => m_Gia = value; }

        public CPhong()
        {
            m_Sophong = 0;
            m_Loaiphong = "";
            m_Trangthai = "";
        }
    }
}
