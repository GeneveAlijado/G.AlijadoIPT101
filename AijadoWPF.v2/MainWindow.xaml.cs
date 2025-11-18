using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using EntityFramework.v2;

namespace AlijadoWPF.v2
{
    public partial class MainWindow : Window
    {
        private readonly IPartRepository _repo;
        private List<Part> _parts;

        // Parameterless constructor for WPF XAML
        public MainWindow()
        {
            InitializeComponent();

            // Create DbContext using factory (pass empty array)
            var dbContext = new PartsDbContextFactory().CreateDbContext(new string[0]);

            // Create repository manually
            _repo = new PartRepository(dbContext);

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                _parts = await _repo.GetAllAsync();
                dgParts.ItemsSource = _parts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPartName.Text)) return;

            var part = new Part
            {
                Part_Name = txtPartName.Text,
                Part_Number = txtPartNumber.Text,
                Descriptions = txtDescription.Text,
                Price = decimal.TryParse(txtPrice.Text, out var p) ? p : 0
            };

            try
            {
                await _repo.AddAsync(part);
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding part: " + ex.Message);
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPartID.Text)) return;

            try
            {
                var part = await _repo.GetByIdAsync(int.Parse(txtPartID.Text));
                if (part == null) return;

                part.Part_Name = txtPartName.Text;
                part.Part_Number = txtPartNumber.Text;
                part.Descriptions = txtDescription.Text;
                part.Price = decimal.TryParse(txtPrice.Text, out var p) ? p : part.Price;

                await _repo.UpdateAsync(part);
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing part: " + ex.Message);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPartID.Text))
            {
                try
                {
                    await _repo.DeleteAsync(int.Parse(txtPartID.Text));
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting part: " + ex.Message);
                }
            }
        }

        private void dgParts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgParts.SelectedItem is Part p)
            {
                txtPartID.Text = p.Part_ID.ToString();
                txtPartName.Text = p.Part_Name;
                txtPartNumber.Text = p.Part_Number;
                txtDescription.Text = p.Descriptions;
                txtPrice.Text = p.Price.ToString();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_parts == null) return;

            string keyword = txtSearch.Text.ToLower();
            dgParts.ItemsSource = _parts
                .Where(p =>
                    (p.Part_Name?.ToLower().Contains(keyword) ?? false) ||
                    (p.Part_Number?.ToLower().Contains(keyword) ?? false) ||
                    (p.Descriptions?.ToLower().Contains(keyword) ?? false))
                .ToList();
        }

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
