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
    public partial class PhongKS : Form
    {
        FormManager frmmng = new FormManager();
        private List<CPhong> arrPKS;
        private int i = -1;
        public PhongKS()
        {
            InitializeComponent();
        }

        public void hienthi()
        {
            lvwPKS.Items.Clear();
            foreach (CPhong phong in arrPKS)
            {
                ListViewItem li = lvwPKS.Items.Add(phong.Sophong.ToString());
                li.SubItems.Add(phong.Loaiphong);
                li.SubItems.Add(phong.Trangthai);
                li.SubItems.Add(phong.Gia);
            }
        }

        public void hienthiPKS(int j)
        {
            CPhong phong = arrPKS[j];
            txtSoPhong.Text = phong.Sophong.ToString();
            cbxLoaiphong.Text = phong.Loaiphong;
            cbxTrangthai.Text = phong.Trangthai;
            txtGia.Text = phong.Gia;
        }

        public void OpenP(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                arrPKS = (List<CPhong>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("Không có dữ liệu", "Error");
            }
        }

        private void PhongKS_Load(object sender, EventArgs e)
        {
            arrPKS = new List<CPhong>();
            OpenP("dsp.txt");
            if (arrPKS.Count > 0)
                hienthi();
        }

        private void cbxLoaiphong_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxLoaiphong.SelectedIndex == 0)
            {
                txtGia.Text = "100.000 VND/ngày";
            }
            else if (cbxLoaiphong.SelectedIndex == 1)
            {
                txtGia.Text = "300.000 VND/ngày";
            }
            else
            {
                txtGia.Text = "600.000 VND/ngày";
            }
        }

        private void lvwPKS_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwPKS.SelectedIndices)
            {
                i = j;
                hienthiPKS(j);
                break;
            }
        }

        public void SaveP(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, arrPKS);
                fs.Close();
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("Lưu không được", "Error");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            SaveP("dsp.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        private void PhongKS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CPhong phong = new CPhong();
            phong.Sophong = Convert.ToInt32(txtSoPhong.Text);
            phong.Loaiphong = cbxLoaiphong.Text;
            phong.Trangthai = cbxTrangthai.Text;
            phong.Gia = txtGia.Text;
            arrPKS.Add(phong);
            i++;
            hienthi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (arrPKS.Count > 0)
            {
                arrPKS.RemoveAt(i);
                i--;
                if (i < 0 && arrPKS.Count > 0)
                {
                    i = 0;
                }
                if (i >= 0)
                {
                    hienthiPKS(i);
                }
                hienthi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            CPhong phong = (CPhong)arrPKS[i];
            phong.Sophong = Convert.ToInt32(txtSoPhong.Text);
            phong.Loaiphong = cbxLoaiphong.Text;
            phong.Trangthai = cbxTrangthai.Text;
            phong.Gia = txtGia.Text;
            hienthi();
        }
    }
}
