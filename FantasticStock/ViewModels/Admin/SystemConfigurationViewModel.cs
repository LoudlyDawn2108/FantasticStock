using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Services.Admin;

namespace FantasticStock.ViewModels
{
    public class SystemConfigurationViewModel : ViewModelBase
    {
        private readonly IConfigService _configService;
        private readonly IAuditService _auditService;

        private CompanyInformation _companyInfo;
        private BindingList<SystemSetting> _generalSettings;
        private BindingList<SystemSetting> _inventorySettings;
        private BindingList<SystemSetting> _salesSettings;
        private BindingList<SystemSetting> _financialSettings;
        private byte[] _logoImageData;
        private bool _isDirty;

        public SystemConfigurationViewModel()
        {
            _configService = ServiceLocator.GetService<IConfigService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
            
            // Initialize commands
            SaveCompanyInfoCommand = new RelayCommand(SaveCompanyInfo, CanSaveCompanyInfo);
            BrowseLogoCommand = new RelayCommand(BrowseLogo);
            ApplySettingsCommand = new RelayCommand(ApplySettings, CanApplySettings);
            ResetSettingsCommand = new RelayCommand(ResetSettings, CanResetSettings);
            
            // Initialize data
            LoadData();
        }

        #region Properties

        public CompanyInformation CompanyInfo
        {
            get => _companyInfo;
            set
            {
                if (SetProperty(ref _companyInfo, value))
                {
                    _isDirty = true;
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public BindingList<SystemSetting> GeneralSettings
        {
            get => _generalSettings;
            set => SetProperty(ref _generalSettings, value);
        }

        public BindingList<SystemSetting> InventorySettings
        {
            get => _inventorySettings;
            set => SetProperty(ref _inventorySettings, value);
        }

        public BindingList<SystemSetting> SalesSettings
        {
            get => _salesSettings;
            set => SetProperty(ref _salesSettings, value);
        }

        public BindingList<SystemSetting> FinancialSettings
        {
            get => _financialSettings;
            set => SetProperty(ref _financialSettings, value);
        }

        public byte[] LogoImageData
        {
            get => _logoImageData;
            set
            {
                if (SetProperty(ref _logoImageData, value))
                {
                    if (_companyInfo != null)
                    {
                        _companyInfo.LogoImage = value;
                        _isDirty = true;
                        CommandManager.InvalidateRequerySuggested();
                    }
                }
            }
        }

        #endregion

        #region Commands

        public ICommand SaveCompanyInfoCommand { get; }
        public ICommand BrowseLogoCommand { get; }
        public ICommand ApplySettingsCommand { get; }
        public ICommand ResetSettingsCommand { get; }

        #endregion

        #region Command Implementations

        private void SaveCompanyInfo(object parameter)
        {
            try
            {
                _configService.UpdateCompanyInformation(CompanyInfo);
                MessageService.ShowInformation("Company information saved successfully.", "Success");
                _isDirty = false;
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to save company information: {ex.Message}", "Error");
            }
        }

        private bool CanSaveCompanyInfo(object parameter)
        {
            return _isDirty && CompanyInfo != null && !string.IsNullOrEmpty(CompanyInfo.CompanyName);
        }

        private void BrowseLogo(object parameter)
        {
            // In a real application, this would open a file dialog
            // For now, we'll just simulate selecting an image
            
            // Simulated image data (would be loaded from a file)
            byte[] simulatedImageData = new byte[1024]; // Dummy data
            new Random().NextBytes(simulatedImageData);
            
            LogoImageData = simulatedImageData;
            MessageService.ShowInformation("Logo updated. Click Save to apply changes.", "Logo Updated");
        }

        private void ApplySettings(object parameter)
        {
            try
            {
                // Collect all settings
                var allSettings = new List<SystemSetting>();
                allSettings.AddRange(GeneralSettings);
                allSettings.AddRange(InventorySettings);
                allSettings.AddRange(SalesSettings);
                allSettings.AddRange(FinancialSettings);
                
                // Update all settings in one transaction
                _configService.UpdateSettings(allSettings);
                
                MessageService.ShowInformation("Settings applied successfully.", "Success");
                _isDirty = false;
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to apply settings: {ex.Message}", "Error");
            }
        }

        private bool CanApplySettings(object parameter)
        {
            return _isDirty;
        }

        private void ResetSettings(object parameter)
        {
            if (MessageService.ShowConfirmation("Are you sure you want to reset all settings to their current values? Any unsaved changes will be lost.", "Confirm Reset"))
            {
                LoadSettingsData();
                _isDirty = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool CanResetSettings(object parameter)
        {
            return _isDirty;
        }

        #endregion

        #region Helper Methods

        private void LoadData()
        {
            LoadCompanyInfo();
            LoadSettingsData();
            _isDirty = false;
        }

        private void LoadCompanyInfo()
        {
            try
            {
                CompanyInfo = _configService.GetCompanyInformation();
                LogoImageData = CompanyInfo.LogoImage;
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load company information: {ex.Message}", "Error");
            }
        }

        private void LoadSettingsData()
        {
            try
            {
                // Load settings by category
                GeneralSettings = new BindingList<SystemSetting>(_configService.GetSettingsByCategory("General"));
                InventorySettings = new BindingList<SystemSetting>(_configService.GetSettingsByCategory("Inventory"));
                SalesSettings = new BindingList<SystemSetting>(_configService.GetSettingsByCategory("Sales"));
                FinancialSettings = new BindingList<SystemSetting>(_configService.GetSettingsByCategory("Financial"));
                
                // Add property changed handlers for all settings
                AddPropertyChangedHandlers(GeneralSettings);
                AddPropertyChangedHandlers(InventorySettings);
                AddPropertyChangedHandlers(SalesSettings);
                AddPropertyChangedHandlers(FinancialSettings);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load settings: {ex.Message}", "Error");
            }
        }

        private void AddPropertyChangedHandlers(BindingList<SystemSetting> settings)
        {
            foreach (var setting in settings)
            {
                // Create wrapper to track changes
                var wrapper = new SystemSettingWrapper(setting);
                wrapper.PropertyChanged += (s, e) => 
                {
                    if (e.PropertyName == "SettingValue")
                    {
                        _isDirty = true;
                        CommandManager.InvalidateRequerySuggested();
                    }
                };
            }
        }

        #endregion
    }

    // Wrapper class to track changes to settings
    public class SystemSettingWrapper : INotifyPropertyChanged
    {
        private readonly SystemSetting _setting;

        public SystemSettingWrapper(SystemSetting setting)
        {
            _setting = setting;
        }

        public string SettingValue
        {
            get => _setting.SettingValue;
            set
            {
                if (_setting.SettingValue != value)
                {
                    _setting.SettingValue = value;
                    OnPropertyChanged(nameof(SettingValue));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}