using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FantasticStock.Models;

namespace FantasticStock.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAuditService _auditService;

        public ConfigService()
        {
            _databaseService = ServiceLocator.GetService<IDatabaseService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
        }

        public CompanyInformation GetCompanyInformation()
        {
            string query = "SELECT * FROM CompanyInformation WHERE CompanyID = 1";
            DataTable dataTable = _databaseService.ExecuteQuery(query);
            
            if (dataTable.Rows.Count == 0)
                return new CompanyInformation();
                
            DataRow row = dataTable.Rows[0];
            
            return new CompanyInformation
            {
                CompanyID = Convert.ToInt32(row["CompanyID"]),
                CompanyName = row["CompanyName"].ToString(),
                Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : null,
                City = row["City"] != DBNull.Value ? row["City"].ToString() : null,
                State = row["State"] != DBNull.Value ? row["State"].ToString() : null,
                ZipCode = row["ZipCode"] != DBNull.Value ? row["ZipCode"].ToString() : null,
                Country = row["Country"] != DBNull.Value ? row["Country"].ToString() : null,
                Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                Website = row["Website"] != DBNull.Value ? row["Website"].ToString() : null,
                TaxID = row["TaxID"] != DBNull.Value ? row["TaxID"].ToString() : null,
                LogoImage = row["LogoImage"] != DBNull.Value ? (byte[])row["LogoImage"] : null,
                BusinessHours = row["BusinessHours"] != DBNull.Value ? row["BusinessHours"].ToString() : null,
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])
            };
        }

        public bool UpdateCompanyInformation(CompanyInformation company)
        {
            // Get existing company info for audit
            CompanyInformation existingInfo = GetCompanyInformation();

            string query = @"
                UPDATE CompanyInformation
                SET CompanyName = @CompanyName,
                    Address = @Address,
                    City = @City,
                    State = @State,
                    ZipCode = @ZipCode,
                    Country = @Country,
                    Phone = @Phone,
                    Email = @Email,
                    Website = @Website,
                    TaxID = @TaxID,
                    LogoImage = @LogoImage,
                    BusinessHours = @BusinessHours,
                    ModifiedDate = @ModifiedDate
                WHERE CompanyID = 1";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyName", company.CompanyName),
                new SqlParameter("@Address", company.Address ?? (object)DBNull.Value),
                new SqlParameter("@City", company.City ?? (object)DBNull.Value),
                new SqlParameter("@State", company.State ?? (object)DBNull.Value),
                new SqlParameter("@ZipCode", company.ZipCode ?? (object)DBNull.Value),
                new SqlParameter("@Country", company.Country ?? (object)DBNull.Value),
                new SqlParameter("@Phone", company.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Email", company.Email ?? (object)DBNull.Value),
                new SqlParameter("@Website", company.Website ?? (object)DBNull.Value),
                new SqlParameter("@TaxID", company.TaxID ?? (object)DBNull.Value),
                new SqlParameter("@LogoImage", company.LogoImage ?? (object)DBNull.Value),
                new SqlParameter("@BusinessHours", company.BusinessHours ?? (object)DBNull.Value),
                new SqlParameter("@ModifiedDate", DateTime.Parse("2025-03-02 15:58:15"))
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            
            if (rowsAffected > 0)
            {
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "CompanyInfoUpdate",
                    "CompanyInformation",
                    "1",
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingInfo),
                    Newtonsoft.Json.JsonConvert.SerializeObject(company)
                );
            }

            return rowsAffected > 0;
        }

        public List<SystemSetting> GetAllSettings()
        {
            var settings = new List<SystemSetting>();
            
            string query = "SELECT * FROM SystemSettings ORDER BY SettingCategory, SettingName";
            DataTable dataTable = _databaseService.ExecuteQuery(query);
            
            foreach (DataRow row in dataTable.Rows)
            {
                settings.Add(MapSystemSetting(row));
            }
            
            return settings;
        }

        public List<SystemSetting> GetSettingsByCategory(string category)
        {
            var settings = new List<SystemSetting>();
            
            string query = "SELECT * FROM SystemSettings WHERE SettingCategory = @Category ORDER BY SettingName";
            var parameter = new SqlParameter("@Category", category);
            
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);
            
            foreach (DataRow row in dataTable.Rows)
            {
                settings.Add(MapSystemSetting(row));
            }
            
            return settings;
        }

        public SystemSetting GetSettingByName(string category, string name)
        {
            string query = "SELECT * FROM SystemSettings WHERE SettingCategory = @Category AND SettingName = @Name";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Category", category),
                new SqlParameter("@Name", name)
            };
            
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameters);
            
            if (dataTable.Rows.Count == 0)
                return null;
                
            return MapSystemSetting(dataTable.Rows[0]);
        }

        public bool UpdateSetting(SystemSetting setting)
        {
            // Get existing setting for audit
            SystemSetting existingSetting = GetSettingByName(setting.SettingCategory, setting.SettingName);
            if (existingSetting == null)
                return false;

            string query = @"
                UPDATE SystemSettings
                SET SettingValue = @Value,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy
                WHERE SettingCategory = @Category AND SettingName = @Name";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Value", setting.SettingValue),
                new SqlParameter("@ModifiedDate", DateTime.Parse("2025-03-02 15:58:15")),
                new SqlParameter("@ModifiedBy", CurrentUser.UserID),
                new SqlParameter("@Category", setting.SettingCategory),
                new SqlParameter("@Name", setting.SettingName)
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            
            if (rowsAffected > 0)
            {
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "SettingUpdate",
                    "SystemSettings",
                    $"{setting.SettingCategory}.{setting.SettingName}",
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingSetting),
                    Newtonsoft.Json.JsonConvert.SerializeObject(setting)
                );
            }

            return rowsAffected > 0;
        }

        public bool UpdateSettings(List<SystemSetting> settings)
        {
            try
            {
                _databaseService.ExecuteInTransaction((connection, transaction) =>
                {
                    foreach (var setting in settings)
                    {
                        string query = @"
                            UPDATE SystemSettings
                            SET SettingValue = @Value,
                                ModifiedDate = @ModifiedDate,
                                ModifiedBy = @ModifiedBy
                            WHERE SettingCategory = @Category AND SettingName = @Name";

                        var command = new SqlCommand(query, connection, transaction);
                        command.Parameters.AddWithValue("@Value", setting.SettingValue);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Parse("2025-03-02 15:58:15"));
                        command.Parameters.AddWithValue("@ModifiedBy", CurrentUser.UserID);
                        command.Parameters.AddWithValue("@Category", setting.SettingCategory);
                        command.Parameters.AddWithValue("@Name", setting.SettingName);
                        command.ExecuteNonQuery();
                    }
                });

                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "BulkSettingsUpdate",
                    "SystemSettings",
                    "Multiple",
                    null,
                    Newtonsoft.Json.JsonConvert.SerializeObject(settings.Select(s => new { s.SettingCategory, s.SettingName, s.SettingValue }))
                );

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private SystemSetting MapSystemSetting(DataRow row)
        {
            return new SystemSetting
            {
                SettingID = Convert.ToInt32(row["SettingID"]),
                SettingCategory = row["SettingCategory"].ToString(),
                SettingName = row["SettingName"].ToString(),
                SettingValue = row["SettingValue"] != DBNull.Value ? row["SettingValue"].ToString() : null,
                DataType = row["DataType"].ToString(),
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]),
                ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["ModifiedBy"]) : null
            };
        }
    }
}