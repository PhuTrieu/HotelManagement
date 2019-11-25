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
        FormManager frmmng = new FormManager();
        private List<CKhachHang> arrKH;
        private int i = -1;
        
        public KhachHang()
        {
            InitializeComponent();
        }

        public void OpenKH(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrKH = (List<CKhachHang>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("No Data KH", "Error");
            }
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            arrKH = new List<CKhachHang>();
            OpenKH("dskh.txt");
            if (arrKH.Count > 0)
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
            foreach (CKhachHang kh  in arrKH)
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
            CKhachHang kh = (CKhachHang)arrKH[j];
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
            arrKH.Add(kh);
            i++;
            hienthi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (arrKH.Count > 0)
            {
                arrKH.RemoveAt(i);
                i--;
                if (i < 0 && arrKH.Count > 0)
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
            CKhachHang kh = (CKhachHang)arrKH[i];
            kh.Hoten = txtTenKH.Text;
            kh.CMND = Convert.ToInt32(txtCM.Text);
            kh.Gioitinh = chkGioitinh.Checked;
            kh.Tuoi = Convert.ToInt32(txtTuoi.Text);
            kh.Quoctich = txtQuoctich.Text;
            kh.Sdt = Convert.ToInt32(txtSDT.Text);
            hienthi();
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

        public void SaveKH(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrKH);
                fs.Close();
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("Save Ko OK", "Error");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            SaveKH("dskh.txt");
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
