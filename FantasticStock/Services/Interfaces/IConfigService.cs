using System.Collections.Generic;
using FantasticStock.Models;

namespace FantasticStock.Services.Admin
{
    public interface IConfigService
    {
        CompanyInformation GetCompanyInformation();
        bool UpdateCompanyInformation(CompanyInformation company);
        List<SystemSetting> GetAllSettings();
        List<SystemSetting> GetSettingsByCategory(string category);
        SystemSetting GetSettingByName(string category, string name);
        bool UpdateSetting(SystemSetting setting);
        bool UpdateSettings(List<SystemSetting> settings);
    }
}