using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapTuan2
{
    public partial class SinhVien : Form
    {
        public SinhVien()
        {
            InitializeComponent();
        }

        //biến toàn cục
        BaiTapTuan2DataContext db = new BaiTapTuan2DataContext();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SinhVien_Load(object sender, EventArgs e)
        {
            LoadFullSV();           
        }
        private void LoadFullSV()
        {

            IQueryable sinhVien = from sv in db.SINHVIENs
                                  select sv;
            dgvSinhVien.DataSource = sinhVien;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "" || txtHoTen.Text == "" || txtDiaChi.Text == "" || txtEmail.Text == "" || txtLopHoc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Cập nhật");
            }
            else
            {
                var update = db.SINHVIENs.Single(sv => sv.MaSV == txtMaSV.Text);
                update.HoTen = txtHoTen.Text;
                update.DiaChi = txtDiaChi.Text;
                update.Email = txtEmail.Text;
                update.LopHoc = txtLopHoc.Text;
                db.SubmitChanges();
                MessageBox.Show("Cập nhật thành công", "Cập nhật");
            }
            
        }

        private void dgvSinhVien_Click(object sender, EventArgs e)
        {
            int dong = dgvSinhVien.CurrentCell.RowIndex;

            txtMaSV.Text = dgvSinhVien.Rows[dong].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[dong].Cells[1].Value.ToString();
            txtDiaChi.Text = dgvSinhVien.Rows[dong].Cells[2].Value.ToString();
            txtEmail.Text = dgvSinhVien.Rows[dong].Cells[3].Value.ToString();
            txtLopHoc.Text = dgvSinhVien.Rows[dong].Cells[4].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtMaSV.Text == "" || txtHoTen.Text == "" || txtDiaChi.Text == ""|| txtEmail.Text == ""|| txtLopHoc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Cập nhật");
            }
            else
            {
                try
                {
                    SINHVIEN sv = new SINHVIEN
                    {
                        MaSV = txtMaSV.Text,
                        HoTen = txtHoTen.Text,
                        DiaChi = txtDiaChi.Text,
                        Email = txtEmail.Text,
                        LopHoc = txtLopHoc.Text
                    };

                    db.SINHVIENs.InsertOnSubmit(sv);
                }catch(Exception ex)
                {
                    MessageBox.Show("Lỗi không thể thêm sinh viên !", "Thông báo !");
                }
                finally
                {
                    db.SubmitChanges();
                    LoadFullSV();
                    MessageBox.Show("Thêm thành công !", "Thêm");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn xóa sinh viên ?","Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(kq == DialogResult.Yes)
            {
                try
                {
                    var xoa = from sv in db.SINHVIENs
                              where sv.MaSV == txtMaSV.Text
                              select sv;

                    foreach(var item in xoa)
                    {
                        db.SINHVIENs.DeleteOnSubmit(item);
                        db.SubmitChanges();
                    }

                    MessageBox.Show("Xóa thành công !", "Xóa");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi không thể xóa sinh viên !", "Thông báo !");
                }
                finally
                {
                    LoadFullSV();
                }
            }
        }

        private void SinhVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn thoát ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
