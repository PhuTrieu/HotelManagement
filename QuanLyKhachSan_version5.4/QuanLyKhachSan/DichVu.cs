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
    public partial class DichVu : Form
    {
        #region Attributes
        FormManager frmmng = new FormManager();
        private int i = -1;
        #endregion
        #region Method
        public void hienThiDSDV()
        {
            lvwDV.Items.Clear();
            foreach (CDichVu item in frmmng.Data.ArrDV)
            {
                ListViewItem li = lvwDV.Items.Add(item.Smadv);
                li.SubItems.Add(item.Stendv);
                li.SubItems.Add(item.Giatien.ToString());
            }
        }
        public void hienThiDV(int j)
        {
            CDichVu dv = frmmng.Data.ArrDV[j];
            txtMaDV.Text = dv.Smadv;
            txtTenDV.Text = dv.Stendv;
            txtGiaTien.Text = dv.Giatien.ToString();
        }
        private CDichVu timDV(string ma)
        {
            CDichVu dv = null;
            if (frmmng.Data.ArrDV.Count > 0)
            {
                foreach (CDichVu item in frmmng.Data.ArrDV)
                {
                    if (string.Equals(ma, item.Smadv))
                    {
                        dv = item;
                        break;
                    }
                }
            }
            return dv;
        }
        #endregion

        public DichVu()
        {
            InitializeComponent();
            frmmng.Data.OpenDSDV("dsdv.txt");
            if (frmmng.Data.ArrDV.Count > 0)
                hienThiDSDV();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CDichVu dv = new CDichVu();
            dv.Smadv = txtMaDV.Text;
            dv.Stendv = txtTenDV.Text;
            dv.Giatien = Convert.ToInt32(txtGiaTien.Text);

            CDichVu a = timDV(dv.Smadv);
            if (a == null)
                frmmng.Data.ArrDV.Add(dv);
            else
                MessageBox.Show("Trùng mã DV", "Error");

            i++;
            hienThiDSDV();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            CDichVu dv = new CDichVu();
            dv.Smadv = txtMaDV.Text;
            dv.Stendv = txtTenDV.Text;
            dv.Giatien = Convert.ToInt32(txtGiaTien.Text);

            CDichVu a = timDV(dv.Smadv);
            if (a != null)
            {
                a.Stendv = dv.Stendv;
                a.Giatien = dv.Giatien;
            }
            else
                MessageBox.Show("Trùng mã DV or Không có Data", "Error");

            hienThiDSDV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            frmmng.Data.ArrDV.RemoveAt(i);
            i--;
            if (i < 0 && frmmng.Data.ArrDV.Count > 0) i = 0;
            if (i >= 0)
                hienThiDV(i);
            hienThiDSDV();
        }

        private void lvwDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int j in lvwDV.SelectedIndices)
            {
                i = j;
                hienThiDV(i);
                break;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmmng.Data.SaveDSDV("dsdv.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }
    }
}
