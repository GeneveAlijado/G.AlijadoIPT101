using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MOTORPARTS
{
    public partial class MainWindow : Window
    {
        // Connection string for LocalDB
        private string connectionString =
            @"Data Source=(localdb)\ProjectModels;Initial Catalog=AlijadoDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        // LOAD DATA INTO DATAGRID
        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM MotorParts", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgParts.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // ADD PART
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO MotorParts 
                                    (Part_Name, Part_Number, Descriptions, Price)
                                    VALUES (@Name, @Number, @Desc, @Price)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", txtPartName.Text);
                    cmd.Parameters.AddWithValue("@Number", txtPartNumber.Text);
                    cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding part: " + ex.Message);
            }
        }

        // EDIT PART
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartID.Text))
            {
                MessageBox.Show("Select an item to edit.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE MotorParts SET
                                    Part_Name=@Name,
                                    Part_Number=@Number,
                                    Descriptions=@Desc,
                                    Price=@Price
                                    WHERE Part_ID=@ID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", txtPartID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtPartName.Text);
                    cmd.Parameters.AddWithValue("@Number", txtPartNumber.Text);
                    cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing part: " + ex.Message);
            }
        }

        // DELETE PART
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartID.Text))
            {
                MessageBox.Show("Select a row to delete.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM MotorParts WHERE Part_ID=@ID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", txtPartID.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting part: " + ex.Message);
            }
        }

        // FILL INPUT FIELDS WHEN SELECTING A ROW
        private void dgParts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = dgParts.SelectedItem as DataRowView;
                if (row == null) return;

                txtPartID.Text = row["Part_ID"].ToString();
                txtPartName.Text = row["Part_Name"].ToString().Trim();
                txtPartNumber.Text = row["Part_Number"].ToString().Trim();
                txtDescription.Text = row["Descriptions"].ToString().Trim();
                txtPrice.Text = row["Price"].ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting row: " + ex.Message);
            }
        }

        // SEARCH PARTS
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT * FROM MotorParts
                                     WHERE Part_Name LIKE @kw
                                        OR Part_Number LIKE @kw
                                        OR Descriptions LIKE @kw
                                        OR Price LIKE @kw";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgParts.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message);
            }
        }

        // CLEAR INPUT FIELDS
        private void ClearFields()
        {
            txtPartID.Text = "";
            txtPartName.Text = "";
            txtPartNumber.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
        }
    }
}
