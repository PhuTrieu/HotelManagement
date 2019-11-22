using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CKhachHang
    {
        private string m_Hoten;
        private int m_CMND;
        private bool m_Gioitinh;
        private int m_Tuoi;
        private string m_Quoctich;
        private int m_sdt;

        public string Hoten { get => m_Hoten; set => m_Hoten = value; }
        public int CMND { get => m_CMND; set => m_CMND = value; }
        public bool Gioitinh { get => m_Gioitinh; set => m_Gioitinh = value; }
        public int Tuoi { get => m_Tuoi; set => m_Tuoi = value; }
        public string Quoctich { get => m_Quoctich; set => m_Quoctich = value; }
        public int Sdt { get => m_sdt; set => m_sdt = value; }

        public CKhachHang()
        {
            m_Hoten = "";
            m_CMND = 0;
            m_Gioitinh = false;
            m_Tuoi = 0;
            m_Quoctich = "";
            m_sdt = 0;
        }
    }
}
