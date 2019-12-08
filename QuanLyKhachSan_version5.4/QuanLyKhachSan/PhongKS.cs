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
        private int i = -1;

        int gpdon, gpdoi, gpcc;
        public PhongKS()
        {
            InitializeComponent();
        }

        public void hienthi()
        {
            lvwPKS.Items.Clear();
            foreach (CPhong phong in frmmng.Data.ArrPKS)
            {
                ListViewItem li = lvwPKS.Items.Add(phong.Sophong.ToString());
                li.SubItems.Add(phong.Loaiphong);
                li.SubItems.Add(phong.Trangthai);
                li.SubItems.Add(phong.Gia.ToString());
            }
        }

        public void hienthiPKS(int j)
        {
            CPhong phong = frmmng.Data.ArrPKS[j];
            txtSoPhong.Text = phong.Sophong.ToString();
            cbxLoaiphong.Text = phong.Loaiphong;
            cbxTrangthai.Text = phong.Trangthai;
            txtGia.Text = phong.Gia.ToString();
        }

        private CPhong TimpDon()
        {
            CPhong pdon = null;
            if (frmmng.Data.ArrPKS.Count>0)
            {
                foreach (CPhong p  in frmmng.Data.ArrPKS)
                {
                    if (string.Compare(p.Loaiphong,"Đơn")==0)
                    {
                        pdon = p;
                        break;
                    }
                }
            }
            return pdon;
        }

        private CPhong TimpDoi()
        {
            CPhong pdoi = null;
            if (frmmng.Data.ArrPKS.Count>0)
            {
                foreach (CPhong p  in frmmng.Data.ArrPKS)
                {
                    if (string.Compare(p.Loaiphong,"Đôi")==0)
                    {
                        pdoi = p;
                        break;
                    }
                }
            }
            return pdoi;
        }

        private CPhong TimpCC()
        {
            CPhong pCC = null;
            if (frmmng.Data.ArrPKS.Count > 0)
            {
                foreach (CPhong p in frmmng.Data.ArrPKS)
                {
                    if (string.Compare(p.Loaiphong, "Cao cấp") == 0)
                    {
                        pCC = p;
                        break;
                    }
                }
            }
            return pCC;
        }

        public void setupGiaPhong(string loaiphong,int giaphong)
        {
            if (string.Compare(loaiphong,"Đơn")==0 && giaphong!=gpdon)
                gpdon = giaphong;
            if (string.Compare(loaiphong, "Đôi") == 0 && giaphong != gpdoi)
                gpdoi = giaphong;
            if (string.Compare(loaiphong, "Cao cấp") == 0 && giaphong != gpcc)
                gpcc = giaphong;
        }

        public void syncGiaPhong(string loaiphong)
        {
            if (frmmng.Data.ArrPKS.Count>0)
            {
                foreach (CPhong p  in frmmng.Data.ArrPKS)
                {
                    if (string.Compare(p.Loaiphong,loaiphong)==0)
                    {
                        if (string.Compare(loaiphong, "Đơn") == 0 && gpdon!=p.Gia)
                        {
                            p.Gia = gpdon;
                        }
                        else if (string.Compare(loaiphong, "Đôi") == 0 && gpdoi != p.Gia)
                        {
                            p.Gia = gpdoi;
                        }
                        else if (string.Compare(loaiphong,"Cao cấp")==0 && gpcc != p.Gia)
                        {
                            p.Gia = gpcc;
                        }
                    }
                }
            }
        }

        private void PhongKS_Load(object sender, EventArgs e)
        {
            frmmng.Data.OpenP("dsp.txt");
            if (frmmng.Data.ArrPKS.Count > 0)
            {
                hienthi();
                CPhong pdon = TimpDon();
                if (pdon != null) setupGiaPhong(pdon.Loaiphong, pdon.Gia);
                CPhong pdoi = TimpDoi();
                if (pdoi != null) setupGiaPhong(pdoi.Loaiphong, pdoi.Gia);
                CPhong pcc = TimpCC();
                if (pcc != null) setupGiaPhong(pcc.Loaiphong, pcc.Gia);
            }
        }

        private void cbxLoaiphong_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxLoaiphong.SelectedIndex == 0)
            {
                try
                {
                    txtGia.Text = gpdon.ToString();
                }
                catch (Exception)
                {
                    txtGia.Text = "0";
                }
            }
            else if (cbxLoaiphong.SelectedIndex == 1)
            {
                try
                {
                    txtGia.Text = gpdoi.ToString();
                }
                catch (Exception)
                {
                    txtGia.Text = "0";
                }
            }
            else
            {
                try
                {
                    txtGia.Text = gpcc.ToString();
                }
                catch (Exception)
                {
                    txtGia.Text = "0";
                }
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmmng.Data.SaveP("dsp.txt");
            this.Hide();
            frmmng.ShowDialog();
            this.Close();
        }

        private void PhongKS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public bool checkSoPhong(int sp)
        {
            bool kq = false;
            if (frmmng.Data.ArrPKS.Count>0)
            {
                foreach (CPhong p  in frmmng.Data.ArrPKS)
                {
                    if (sp==p.Sophong)
                    {
                        kq = true;
                        break;
                    }
                }
            }
            return kq;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CPhong phong = new CPhong();
            if (checkSoPhong(Convert.ToInt32(txtSoPhong.Text)))
            {
                return;
            }
            phong.Sophong = Convert.ToInt32(txtSoPhong.Text);
            phong.Loaiphong = cbxLoaiphong.Text;
            phong.Trangthai = cbxTrangthai.Text;
            phong.Gia = int.Parse(txtGia.Text) ;
            frmmng.Data.ArrPKS.Add(phong);
            i++;
            setupGiaPhong(phong.Loaiphong, phong.Gia);
            syncGiaPhong(phong.Loaiphong); 
            hienthi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (frmmng.Data.ArrPKS.Count > 0)
            {
                frmmng.Data.ArrPKS.RemoveAt(i);
                i--;
                if (i < 0 && frmmng.Data.ArrPKS.Count > 0)
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
            CPhong phong = (CPhong)frmmng.Data.ArrPKS[i];
            phong.Sophong = Convert.ToInt32(txtSoPhong.Text);
            phong.Loaiphong = cbxLoaiphong.Text;
            phong.Trangthai = cbxTrangthai.Text;
            phong.Gia = int.Parse(txtGia.Text);
            setupGiaPhong(phong.Loaiphong, phong.Gia);
            syncGiaPhong(phong.Loaiphong);
            hienthi();
        }
    }
}
