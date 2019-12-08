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
    public partial class HoaDonThanhToan : Form
    {
        FormManager frmmng = new FormManager();
        private string sTenKH;
        private int iCMND;

        public void showDataTenKH()
        {
            cbxTTKH.Items.Clear();
            if (frmmng.Data.ArrKH.Count>0)
            {
                foreach (CKhachHang kh in frmmng.Data.ArrKH)
                {
                    cbxTTKH.Items.Add(kh.Hoten + " (" + kh.CMND + ")");
                }
            }
        }

        public HoaDonThanhToan()
        {
            InitializeComponent();
            frmmng.Data.OpenKH("dskh.txt");
            frmmng.Data.OpenDP("dsdp.txt");
            frmmng.Data.OpenDSDDV("dsddv.txt");
            frmmng.Data.OpenDSBill("dsbill.txt");
            frmmng.Data.OpenLSKH("dslskh.txt");
            dtpNgayTT.Value = DateTime.Now;
            if (frmmng.Data.ArrBill.Count>0)
            {
                showDataTenKH();
                cbxTTKH.Text = "";
                cbxTTKH.Select();
            }
        }

        public HoaDonThanhToan(string kh)
        {
            InitializeComponent();
            frmmng.Data.OpenKH("dskh.txt");
            frmmng.Data.OpenDP("dsdp.txt");
            frmmng.Data.OpenDSDDV("dsddv.txt");
            frmmng.Data.OpenDSBill("dsbill.txt");
            frmmng.Data.OpenLSKH("dslskh.txt");
            dtpNgayTT.Value = DateTime.Now;
            if (frmmng.Data.ArrBill.Count > 0)
            {
                showDataTenKH();
                cbxTTKH.Text = kh;
            }
        }

        public void layTenKHvaCMND(string c)
        {
            string temp = c;
            int charfrom = temp.IndexOf("(", 0) + 1;
            int charto = temp.IndexOf(")", charfrom) - 1;
            int charlength = charto - charfrom + 1;
            sTenKH = temp.Substring(0, charfrom - 3 + 1);
            iCMND = int.Parse(temp.Substring(charfrom, charlength));
        }

        private void hienthiDSPhong(CDatPhong dp)
        {
            lvwPhong.Items.Clear();
            if (frmmng.Data.ArrBill.Count>0)
            {
                foreach (CPhong phong in dp.Phong)
                {
                    ListViewItem li = lvwPhong.Items.Add(phong.Sophong.ToString());
                    li.SubItems.Add(phong.Loaiphong);
                    li.SubItems.Add(phong.);
                }
            }
        }

        private void hienthiDSDDV(CDatDichVu ddv)
        {
            lvwPhong.Items.Clear();
            if (frmmng.Data.ArrBill.Count > 0)
            {
                foreach (CDichVu item in ddv.Arrdv)
                {
                    ListViewItem li = lvwDichVu.Items.Add(item.Smadv);
                    li.SubItems.Add(item.Stendv);
                    li.SubItems.Add(item.Giatien.ToString());
                }
            }
        }

        private CBill timHoaDonKH(int cmnd)
        {
            if (frmmng.Data.ArrBill.Count>0)
            {
                foreach (CBill item in frmmng.Data.ArrBill)
                {
                    if (item.Kh.CMND==cmnd)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        private void HoaDonThanhToan_Load(object sender, EventArgs e)
        {

        }

        private void cbxTTKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTTKH.Text=="")
            {
                MessageBox.Show("Chưa chọn khách hàng");
                return;
            }
            layTenKHvaCMND(cbxTTKH.Text);
            CBill hoadonKH = timHoaDonKH(iCMND);
            if (hoadonKH.Dp!=null)
            {
            }
        }
    }
}
