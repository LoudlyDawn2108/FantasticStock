using FantasticStock.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views
{
    public partial class SystemConfigurationView : UserControl
    {
        private SystemConfigurationViewModel _viewModel;

        public SystemConfigurationView()
        {
            InitializeComponent();

            // Initialize view model
            _viewModel = new SystemConfigurationViewModel();

            // Set up data bindings
            SetupBindings();
        }

        private void SetupBindings()
        {
            // Bind company info fields
            txtCompanyName.DataBindings.Add("Text", _viewModel, "CompanyInfo.CompanyName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtAddress.DataBindings.Add("Text", _viewModel, "CompanyInfo.Address", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCity.DataBindings.Add("Text", _viewModel, "CompanyInfo.City", true, DataSourceUpdateMode.OnPropertyChanged);
            txtState.DataBindings.Add("Text", _viewModel, "CompanyInfo.State", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPostalCode.DataBindings.Add("Text", _viewModel, "CompanyInfo.PostalCode", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCountry.DataBindings.Add("Text", _viewModel, "CompanyInfo.Country", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPhone.DataBindings.Add("Text", _viewModel, "CompanyInfo.Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            txtEmail.DataBindings.Add("Text", _viewModel, "CompanyInfo.Email", true, DataSourceUpdateMode.OnPropertyChanged);
            txtWebsite.DataBindings.Add("Text", _viewModel, "CompanyInfo.Website", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTaxID.DataBindings.Add("Text", _viewModel, "CompanyInfo.TaxID", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpEstablishedDate.DataBindings.Add("Value", _viewModel, "CompanyInfo.EstablishedDate", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind logo image
            if (_viewModel.LogoImageData != null)
            {
                try
                {
                    using (var ms = new System.IO.MemoryStream(_viewModel.LogoImageData))
                    {
                        pictureBoxLogo.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                catch
                {
                    // If image loading fails, we'll keep the default image (or no image)
                }
            }

            // Bind settings data grids
            dgvGeneralSettings.DataSource = _viewModel.GeneralSettings;
            dgvInventorySettings.DataSource = _viewModel.InventorySettings;
            dgvSalesSettings.DataSource = _viewModel.SalesSettings;
            dgvFinancialSettings.DataSource = _viewModel.FinancialSettings;

            // Configure grids
            ConfigureSettingsGrid(dgvGeneralSettings);
            ConfigureSettingsGrid(dgvInventorySettings);
            ConfigureSettingsGrid(dgvSalesSettings);
            ConfigureSettingsGrid(dgvFinancialSettings);

            // Bind commands to buttons
            btnSaveCompanyInfo.Click += (s, e) => _viewModel.SaveCompanyInfoCommand.Execute(null);
            btnBrowseLogo.Click += (s, e) => _viewModel.BrowseLogoCommand.Execute(null);
            btnApplySettings.Click += (s, e) => _viewModel.ApplySettingsCommand.Execute(null);
            btnResetSettings.Click += (s, e) => _viewModel.ResetSettingsCommand.Execute(null);
        }

        private void ConfigureSettingsGrid(DataGridView grid)
        {
            // Configure common properties for all settings grids
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Clear any existing columns
            grid.Columns.Clear();

            // Create columns
            var nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SettingName",
                HeaderText = "Setting",
                Name = "colSettingName",
                ReadOnly = true
            };

            var valueColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SettingValue",
                HeaderText = "Value",
                Name = "colSettingValue",
                ReadOnly = false
            };

            var descriptionColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                Name = "colDescription",
                ReadOnly = true
            };

            // Add columns to grid
            grid.Columns.Add(nameColumn);
            grid.Columns.Add(valueColumn);
            grid.Columns.Add(descriptionColumn);
        }
    }
}
