using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace ThangLong_COFFEE
{
    internal class TaiKhoanDAO
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; pwd=123; database=Cafe_Management");

        //Ngọc: Code chức năng đăng nhập.
        public void dangNhapFunc(TextBox temptenTKTxtBox, PasswordBox tempmatKhauPswBox)
        {
            TaiKhoan taiKhoan = new TaiKhoan();

            if (temptenTKTxtBox.Text == "")
            {
                MessageBox.Show("Vui lòng điền tài khoản.");
            }
            else if (tempmatKhauPswBox.Password == "")
            {
                MessageBox.Show("Vui lòng điền mật khẩu.");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from Tai_khoan where ten_tai_khoan = @tenTK and mat_khau = @matKhau", con);
                    cmd.Parameters.AddWithValue("tenTK", temptenTKTxtBox.Text);
                    cmd.Parameters.AddWithValue("matKhau", tempmatKhauPswBox.Password);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        Home home = new Home();
                        home.Show();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại.");
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taiKhoan.taikhoan_id = (int)reader[0];
                            taiKhoan.nhanvien_id = (int)reader[1];
                        }
                    }
                }

                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
