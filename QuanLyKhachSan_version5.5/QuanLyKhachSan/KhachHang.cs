using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class KhachHang : Form
    {
        #region Attributes
        FormManager frmmng = new FormManager();
        private int i = -1;
        #endregion
        
        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            frmmng.Data.ArrKH = new List<CKhachHang>();
            frmmng.Data.OpenKH("dskh.txt");
            frmmng.Data.OpenDSBill("dsbill.txt");
            if (frmmng.Data.ArrKH.Count > 0)
                hienthi();
            chkGioitinh.Checked = true;
        }

        private void chkGioitinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGioitinh.Checked == true)
            {
                chkGioitinh.Text = "Nam";
            }
            else chkGioitinh.Text = "Nu";
        }

        public void hienthi()
        {
            lvwKH.Items.Clear();
            foreach (CKhachHang kh  in frmmng.Data.ArrKH)
            {
                ListViewItem li = lvwKH.Items.Add(kh.Hoten);
                li.SubItems.Add(kh.CMND.ToString());
                if (kh.Gioitinh == true)
                {
                    li.SubItems.Add("Nam");
                }
                else li.SubItems.Add("Nu");
                li.SubItems.Add(kh.Tuoi.ToString());
                li.SubItems.Add(kh.Quoctich);
                li.SubItems.Add(kh.Sdt.ToString());
            }
        }

        public void hienthiKH(int j)
        {
            CKhachHang kh = (CKhachHang)frmmng.Data.ArrKH[j];
            txtTenKH.Text = kh.Hoten;
            txtCM.Text = kh.CMND.ToString();
            chkGioitinh.Checked = kh.Gioitinh;
            txtTuoi.Text = kh.Tuoi.ToString();
            txtQuoctich.Text = kh.Quoctich;
            txtSDT.Text = kh.Sdt.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CKhachHang kh = new CKhachHang();
            kh.Hoten = txtTenKH.Text;
            kh.CMND = Convert.ToInt32(txtCM.Text);
            kh.Gioitinh = chkGioitinh.Checked;
            kh.Tuoi = Convert.ToInt32(txtTuoi.Text);
            kh.Quoctich = txtQuoctich.Text;
            kh.Sdt = Convert.ToInt32(txtSDT.Text);
            frmmng.Data.ArrKH.Add(kh);
            i++;
            hienthi();

            CBill ctmbill = new CBill(kh, null, null, DateTime.Now);
            frmmng.Data.ArrBill.Add(ctmbill);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (frmmng.Data.ArrKH.Count > 0)
            {
                if(frmmng.Data.ArrBill.Count>0)
                {
                    frmmng.Data.ArrBill.RemoveAt(i);
                }

                frmmng.Data.ArrKH.RemoveAt(i);
                i--;
                if (i < 0 && frmmng.Data.ArrKH.Count > 0)
                {
                    i = 0;
                }
                if (i >= 0)
                {
                    hienthiKH(i);
                }
                hienthi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            CKhachHang kh = (CKhachHang)frmmng.Data.ArrKH[i];
            kh.Hoten = txtTenKH.Text;
            kh.CMND = Convert.ToInt32(txtCM.Text);
            kh.Gioitinh = chkGioitinh.Checked;
            kh.Tuoi = Convert.ToInt32(txtTuoi.Text);
            kh.Quoctich = txtQuoctich.Text;
            kh.Sdt = Convert.ToInt32(txtSDT.Text);
            hienthi();

            CBill ctmbill = (CBill)frmmng.Data.ArrBill[i];
            ctmbill.Kh = kh;
        }

        private void lvwKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwKH.SelectedIndices)
            {
                i = j;
                hienthiKH(j);
                break;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmmng.Data.SaveKH("dskh.txt");
            frmmng.Data.SaveDSBill("dsbill.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        private void KhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
