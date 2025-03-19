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
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Bien toan cuc
        BaiTapTuan2DataContext db = new BaiTapTuan2DataContext();
        private void DangKy_Load(object sender, EventArgs e)
        {

            LoadMonHoc();
            LoadSinhVien();

            LoadDangKyFull();
        }
        private void LoadMonHoc()
        {
            IQueryable monHoc = from mh in db.MONHOCs
                                  select mh;
            cobMaMH.DataSource = monHoc;
            cobMaMH.DisplayMember = "TenHM";
            cobMaMH.ValueMember = "MaMH";
        }
        private void LoadSinhVien()
        {
            IQueryable sinhVien = from sv in db.SINHVIENs
                                select sv;
            cobMaSV.DataSource = sinhVien;
            cobMaSV.DisplayMember = "HoTen";
            cobMaSV.ValueMember = "MaSV";
        }

        private void LoadDangKyFull()
        {
            IQueryable dangKy = from dk in db.DANGKies
                                select new
                                {
                                    dk.MaSV,
                                    dk.MaMH,
                                    dk.DiemGK,
                                    dk.DiemCK,
                                    dk.DiemDT,
                                    dk.GhiChu
                                };
            dgvDangKy.DataSource = dangKy;
        }

        private void dgvDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dgvDangKy.Rows[dgvDangKy.CurrentCell.RowIndex].Cells[1].Value.ToString());
            if (dgvDangKy.Rows[dgvDangKy.CurrentCell.RowIndex].Cells[0] != null)
            {
                int dong = dgvDangKy.CurrentCell.RowIndex;
                cobMaSV.Text = LayTenSinhVien(dgvDangKy.Rows[dong].Cells[0].Value.ToString());
                cobMaMH.Text = LayTenMonHoc(dgvDangKy.Rows[dong].Cells[1].Value.ToString());
                txtDiemGK.Text = dgvDangKy.Rows[dong].Cells[2].Value.ToString();
                txtDiemCK.Text = dgvDangKy.Rows[dong].Cells[3].Value.ToString();
                txtDiemDT.Text = dgvDangKy.Rows[dong].Cells[4].Value.ToString();
                txtGhiChu.Text = dgvDangKy.Rows[dong].Cells[5].Value.ToString();
            }
        }
        private string LayTenSinhVien(string ma)
        {
            try
            {
                var ten = db.SINHVIENs.Single(sv => sv.MaSV == ma);

                return ten.HoTen.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không thể lấy tên sinh viên !", "Lỗi");
                return null;
            }
        }

        private string LayTenMonHoc(string ma)
        {
            try
            {
                var ten = db.MONHOCs.Single(mh => mh.MaMH == ma);
                return ten.TenHM.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi không thể lấy tên môn học !", "Lỗi");
                return null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtDiemGK.Text == "" || txtDiemCK.Text == "" || txtDiemDT.Text == "" || txtGhiChu.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo");
            }
            else
            {
                try
                {
                    DANGKY monHocAdd = new DANGKY
                    {
                        MaSV = cobMaSV.SelectedValue.ToString(),
                        MaMH = cobMaMH.SelectedValue.ToString(),
                        DiemGK = double.Parse(txtDiemGK.Text),
                        DiemCK = double.Parse(txtDiemCK.Text),
                        DiemDT = double.Parse(txtDiemDT.Text),
                        GhiChu = txtGhiChu.Text
                    };

                    db.DANGKies.InsertOnSubmit(monHocAdd);
                    MessageBox.Show("Thêm đăng ký thành công !", "Thông Báo");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi không thể thêm đăng ký", "Lỗi");
                }
                finally
                {
                    db.SubmitChanges();
                    LoadDangKyFull();
                    
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Bạn có muốn xóa đăng ký ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(question == DialogResult.Yes)
            {
                try
                {
                    var xoa = db.DANGKies.First(dk => dk.MaSV == cobMaSV.SelectedValue.ToString() && dk.MaMH == cobMaMH.SelectedValue.ToString());

                    db.DANGKies.DeleteOnSubmit(xoa);
                    db.SubmitChanges();

                    MessageBox.Show("Xóa thành công !", "Thông báo");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi không thể xóa đăng ký", "Lỗi");
                }
                finally
                {
                    LoadDangKyFull();
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (txtDiemGK.Text == "" || txtDiemCK.Text == "" || txtDiemDT.Text == "" || txtGhiChu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo");
            }
            else
            {
                try
                {
                    DANGKY dangKyUpdate = db.DANGKies.Single(dk => dk.MaMH == cobMaMH.SelectedValue.ToString() && dk.MaSV == cobMaSV.SelectedValue.ToString());
                    dangKyUpdate.DiemGK = double.Parse(txtDiemGK.Text);
                    dangKyUpdate.DiemCK = double.Parse(txtDiemCK.Text);
                    dangKyUpdate.DiemDT = double.Parse(txtDiemDT.Text);
                    dangKyUpdate.GhiChu = txtGhiChu.Text;

                    db.SubmitChanges();
                    MessageBox.Show("Sửa đăng ký thành công !", "Thông Báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không thể sửa đăng ký", "Lỗi");
                }
                finally
                {
                    db.SubmitChanges();
                    LoadDangKyFull();

                }
            }
        }

        private void DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult question = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (question == DialogResult.Yes)
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
