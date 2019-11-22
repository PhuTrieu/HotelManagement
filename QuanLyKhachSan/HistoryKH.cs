using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class HistoryKH : Form
    {
        FormManager frmmng = new FormManager();
        private List<HistoryKH> arrLS;

        private List<CKhachHang> arrKH;
        private List<CDatPhong> arrDP;

        private int i = -1;
        public HistoryKH()
        {
            InitializeComponent();
        }

        private void HistoryKH_Load(object sender, EventArgs e)
        {
            arrLS = new List<HistoryKH>();
        }

        public void hienthi()
        {
            lvwLS.Items.Clear();
            foreach (CHistory ls  in arrLS)
            {
                ListViewItem li = lvwLS.Items.Add(ls.Dp.Kh.Hoten);
                li.SubItems.Add(ls.Dp.Kh.CMND.ToString());
                if (ls.Kh.Gioitinh == true)
                {
                    li.SubItems.Add("Nam");
                }
                else li.SubItems.Add("Nu");
                li.SubItems.Add(ls.Kh.Tuoi.ToString());
                li.SubItems.Add(ls.Kh.Quoctich);
                li.SubItems.Add(ls.Kh.Sdt.ToString());
                li.SubItems.Add(ls.Dp.Phong.Loaiphong);
                li.SubItems.Add(ls.Dp.Phong.Sophong.ToString());
                li.SubItems.Add(ls.Dp.Ngayden.ToShortDateString());
                li.SubItems.Add(ls.Dp.Ngaydi.ToShortDateString());
                li.SubItems.Add(ls.Dp.SoNgayO().ToString());
                li.SubItems.Add(ls.Dp.ThanhTien().ToString());
            }
        }
    }
}
