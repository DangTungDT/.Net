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
    public partial class MonHoc : Form
    {
        public MonHoc()
        {
            InitializeComponent();
        }
        BaiTapTuan2DataContext db = new BaiTapTuan2DataContext();
        private void MonHoc_Load(object sender, EventArgs e)
        {
            LoadFullMonHoc();
        }
        private void LoadFullMonHoc()
        {
            IQueryable monHoc = from mh in db.MONHOCs
                                select mh;

            dgvMonHoc.DataSource = monHoc;

        }

        private void dgvMonHoc_Click(object sender, EventArgs e)
        {
            //if (dgvMonHoc.CurrentCell != null &&
            //    dgvMonHoc.CurrentCell.RowIndex >= 0 &&
            //    dgvMonHoc.Rows[dgvMonHoc.CurrentCell.RowIndex].Cells[0].Value != null)
            //{
            //    int dong = dgvMonHoc.CurrentCell.RowIndex;


            //    txtMaMH.Text = dgvMonHoc.Rows[dong].Cells[0].Value.ToString();
            //    txtTenMH.Text = dgvMonHoc.Rows[dong].Cells[1].Value.ToString();
            //    txtSoTC.Text = dgvMonHoc.Rows[dong].Cells[2].Value.ToString();
            //    txtHocKy.Text = dgvMonHoc.Rows[dong].Cells[3].Value.ToString();
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaMH.Text == "" || txtTenMH.Text == "" || txtSoTC.Text == "" || txtHocKy.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo !");
            }
            else
            {
                try
                {
                    MONHOC monHoc = new MONHOC()
                    {
                        MaMH = txtMaMH.Text,
                        TenHM = txtTenMH.Text,
                        SoTC = int.Parse(txtSoTC.Text),
                        HocKy = int.Parse(txtHocKy.Text)
                    };

                    db.MONHOCs.InsertOnSubmit(monHoc);
                    MessageBox.Show("Thêm thành công !", "Thêm !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm thất bại !", "Lỗi !");
                }
                finally
                {
                    db.SubmitChanges();
                    LoadFullMonHoc();
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn xóa môn học không ?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                var xoa = from monHoc in db.MONHOCs
                          where monHoc.MaMH == txtMaMH.Text
                          select monHoc;
                foreach (var item in xoa)
                {
                    db.MONHOCs.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
                MessageBox.Show("Xóa thành công !", "Xóa");
                LoadFullMonHoc();
            }
        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1 && e != null)
            //{
            //    int dong = dgvMonHoc.CurrentCell.RowIndex;

            //    txtMaMH.Text = dgvMonHoc.Rows[dong].Cells[0].Value.ToString();
            //    txtTenMH.Text = dgvMonHoc.Rows[dong].Cells[1].Value.ToString();
            //    txtSoTC.Text = dgvMonHoc.Rows[dong].Cells[2].Value.ToString();
            //    txtHocKy.Text = dgvMonHoc.Rows[dong].Cells[3].Value.ToString();
            //}
        }

        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;

            if (dgvMonHoc.Rows[dong].Cells[0].Value != null)
            {
                txtMaMH.Text = dgvMonHoc.Rows[dong].Cells[0].Value.ToString();
                txtTenMH.Text = dgvMonHoc.Rows[dong].Cells[1].Value.ToString();
                txtSoTC.Text = dgvMonHoc.Rows[dong].Cells[2].Value.ToString();
                txtHocKy.Text = dgvMonHoc.Rows[dong].Cells[3].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtMaMH.Text == "" || txtTenMH.Text == "" || txtSoTC.Text == "" || txtHocKy.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo !");
            }
            else
            {
                try
                {
                    var update = db.MONHOCs.Single(mh => mh.MaMH == txtMaMH.Text);

                    update.MaMH = txtMaMH.Text;
                    update.TenHM = txtTenMH.Text;
                    update.SoTC = int.Parse(txtSoTC.Text);
                    update.HocKy = int.Parse(txtHocKy.Text);

                    db.SubmitChanges();
                    MessageBox.Show("Cập nhật thành công !", "Cập Nhật");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Cập nhật thất bại !", "Lỗi");
                }
                finally
                {
                    LoadFullMonHoc();
                }
            }

        }

        private void MonHoc_FormClosing(object sender, FormClosingEventArgs e)
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
