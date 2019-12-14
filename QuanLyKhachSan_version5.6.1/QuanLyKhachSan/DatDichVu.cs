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
    public partial class DatDichVu : Form
    {
        #region Attributes
        FormManager frmmng = new FormManager();

        private string sTenKH;
        private int iCMND;
        #endregion
        #region Method
        public void hienthiDSDV()
        {
            lvwDSDV.Items.Clear();
            if (frmmng.Data.ArrDV.Count > 0)
            {
                foreach (CDichVu item in frmmng.Data.ArrDV)
                {
                    ListViewItem li = lvwDSDV.Items.Add(item.Smadv);
                    li.SubItems.Add(item.Stendv);
                    li.SubItems.Add(item.Giatien.ToString());
                }
            }
        }
        public void hienthiChooseDSDV(int cmnd)
        {
            lvwChooseDSDV.Items.Clear();
            if (frmmng.Data.ArrDDV.Count > 0)
            {
                foreach (CDatDichVu ddv in frmmng.Data.ArrDDV)
                {
                    if (ddv.Kh.CMND == cmnd)
                    {
                        foreach (CDichVu item in ddv.Arrdv)
                        {
                            ListViewItem li = lvwChooseDSDV.Items.Add(item.Smadv);
                            li.SubItems.Add(item.Stendv);
                            li.SubItems.Add(item.Giatien.ToString());
                        }
                        break;
                    }
                }
            }
        }
        private bool checkSameCMND(int cmnd,List<CKhachHang> arrHoTenKH)
        {
            bool kq = false;
            if (arrHoTenKH.Count > 0)
            {
                foreach (CKhachHang item in arrHoTenKH)
                {
                    if (item.CMND == cmnd)
                    {
                        kq = true;
                        break;
                    }
                }
            }
            return kq;
        }
        private List<CKhachHang> getDataNoSameHoTenKH()
        {
            List<CKhachHang> arrHoTenKH = new List<CKhachHang>();
            if (frmmng.Data.ArrKH.Count > 0)
            {
                foreach (CKhachHang kh in frmmng.Data.ArrKH)
                {
                    if (!checkSameCMND(kh.CMND, arrHoTenKH))
                        arrHoTenKH.Add(kh);
                }
            }
            return arrHoTenKH;
        }
        public void ShowDataTenKH()
        {
            cbxHoten.Items.Clear();
            List<CKhachHang> arrHoTenKH = getDataNoSameHoTenKH();
            if (arrHoTenKH.Count > 0)
            {
                foreach (CKhachHang kh in arrHoTenKH)
                {
                    cbxHoten.Items.Add(kh.Hoten + " (" + kh.CMND + ")");
                }
            }
        }
        public void layTenKHvaCMND(string c)
        {
            string temp = c;
            int charfrom = temp.IndexOf('(', 0) + 1;
            int charto = temp.IndexOf(')', 0) - 1;
            int charlength = charto - charfrom + 1;
            sTenKH = temp.Substring(0, charfrom - 3 + 1);
            iCMND = int.Parse(temp.Substring(charfrom, charlength));
        }
        private CDatDichVu timKHOld()
        {
            CDatDichVu ddv_old = null;
            if(frmmng.Data.ArrDDV.Count>0)
            {
                foreach (CDatDichVu item in frmmng.Data.ArrDDV)
                {
                    if (item.Kh.CMND == iCMND)
                    {
                        ddv_old = item;
                        break;
                    }
                }
            }
            return ddv_old;
        }
        private CDichVu timDV(string madv)
        {
            CDichVu dv = null;
            if(frmmng.Data.ArrDV.Count>0)
            {
                foreach (CDichVu item in frmmng.Data.ArrDV)
                {
                    if(string.Equals(madv,item.Smadv))
                    {
                        dv = item;
                        break;
                    }
                }
            }
            return dv;
        }
        #endregion
        public DatDichVu()
        {
            InitializeComponent();
            frmmng.Data.OpenDSDDV("dsddv.txt");
            frmmng.Data.OpenDSDV("dsdv.txt");
            frmmng.Data.OpenKH("dskh.txt");
            frmmng.Data.OpenDSBill("dsbill.txt");
            if (frmmng.Data.ArrDV.Count > 0)
                hienthiDSDV();
            if (frmmng.Data.ArrKH.Count > 0)
                ShowDataTenKH();
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            if (cbxHoten.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn KH", "Error");
                return;
            }
            CDatDichVu a = timKHOld();
            if(a != null)
            {
                foreach (int j in lvwDSDV.SelectedIndices)
                {
                    CDichVu dv = new CDichVu();
                    dv.Smadv = lvwDSDV.Items[j].Text;
                    dv.Stendv = lvwDSDV.Items[j].SubItems[1].Text;
                    dv.Giatien = Convert.ToInt32(lvwDSDV.Items[j].SubItems[2].Text);

                    a.Arrdv.Add(dv);
                }
            }
            else
            {
                a = new CDatDichVu();
                foreach (CKhachHang item in frmmng.Data.ArrKH)
                {
                    if (item.CMND == iCMND)
                    {
                        a.Kh = item;
                        break;
                    }
                }
                foreach (int j in lvwDSDV.SelectedIndices)
                {
                    CDichVu dv = new CDichVu();
                    dv.Smadv = lvwDSDV.Items[j].Text;
                    dv.Stendv = lvwDSDV.Items[j].SubItems[1].Text;
                    dv.Giatien = Convert.ToInt32(lvwDSDV.Items[j].SubItems[2].Text);

                    a.Arrdv.Add(dv);
                }
                frmmng.Data.ArrDDV.Add(a);
            }
            hienthiChooseDSDV(iCMND);
            txtTongGiaTien.Text = a.tinhTongGiaTien().ToString();
            if (frmmng.Data.ArrDV.Count > 0)
                hienthiDSDV();

            foreach (CBill item in frmmng.Data.ArrBill)
            {
                if (item.Kh.CMND == iCMND)
                {
                    item.Ddv = a;
                    break;
                }
            }
        }

        private void cbxHoten_SelectedIndexChanged(object sender, EventArgs e)
        {
            layTenKHvaCMND(cbxHoten.Text);
            hienthiChooseDSDV(iCMND);
            if (frmmng.Data.ArrDV.Count > 0)
                hienthiDSDV();
        }

        private void btnHuyDV_Click(object sender, EventArgs e)
        {
            CDatDichVu dv_kh_old = timKHOld();
            foreach (int j in lvwChooseDSDV.SelectedIndices)
            {
                int vitrixoa = 0;
                foreach (CDichVu dv in dv_kh_old.Arrdv)
                {
                    if (string.Equals(dv.Smadv, lvwChooseDSDV.Items[j].Text))
                        break;
                    vitrixoa++;
                }
                dv_kh_old.Arrdv.RemoveAt(vitrixoa);
                hienthiChooseDSDV(iCMND);
                txtTongGiaTien.Text = dv_kh_old.tinhTongGiaTien().ToString();
                if (frmmng.Data.ArrDV.Count > 0)
                    hienthiDSDV();
                break;
            }
            foreach (CBill item in frmmng.Data.ArrBill)
            {
                if (item.Kh.CMND == iCMND)
                {
                    item.Ddv = dv_kh_old;
                    break;
                }
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmmng.Data.SaveDSDDV("dsddv.txt");
            frmmng.Data.SaveDSBill("dsbill.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        private void DatDichVu_Load(object sender, EventArgs e)
        {

        }
    }
}
