using System.Collections.Generic;
using AdminDomain.Models;

namespace AdminDomain.Services
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