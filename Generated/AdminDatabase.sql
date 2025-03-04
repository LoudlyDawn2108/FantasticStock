-- Create the Administrative Domain Database
CREATE DATABASE AdminDomain;
GO

USE AdminDomain;
GO

-- User Management Tables
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Permissions (
    PermissionID INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL UNIQUE,
    Category NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE RolePermissions (
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleID, PermissionID),
    CONSTRAINT FK_RolePermissions_Role FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    CONSTRAINT FK_RolePermissions_Permission FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(128) NOT NULL,
    DisplayName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    RoleID INT NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active', -- Active, Inactive, Locked
    TwoFactorEnabled BIT NOT NULL DEFAULT 0,
    AccountExpiry DATETIME NULL,
    LastLogin DATETIME NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Users_Role FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TABLE UserScheduleRestrictions (
    RestrictionID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    DayOfWeek INT NOT NULL, -- 1-7 (Sunday-Saturday)
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    CONSTRAINT FK_UserSchedule_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE UserActivityLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ActivityType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    IPAddress NVARCHAR(50),
    Timestamp DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ActivityLog_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- System Configuration Tables
CREATE TABLE CompanyInformation (
    CompanyID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    City NVARCHAR(100),
    State NVARCHAR(100),
    ZipCode NVARCHAR(20),
    Country NVARCHAR(100),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Website NVARCHAR(255),
    TaxID NVARCHAR(50),
    LogoImage VARBINARY(MAX),
    BusinessHours NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE SystemSettings (
    SettingID INT PRIMARY KEY IDENTITY(1,1),
    SettingCategory NVARCHAR(50) NOT NULL,
    SettingName NVARCHAR(100) NOT NULL,
    SettingValue NVARCHAR(MAX),
    DataType NVARCHAR(50) NOT NULL, -- String, Int, Boolean, DateTime, etc.
    Description NVARCHAR(255),
    ModifiedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy INT,
    CONSTRAINT UK_SystemSettings UNIQUE (SettingCategory, SettingName),
    CONSTRAINT FK_SystemSettings_User FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

-- Backup Management Tables
CREATE TABLE BackupHistory (
    BackupID INT PRIMARY KEY IDENTITY(1,1),
    BackupName NVARCHAR(100) NOT NULL,
    BackupPath NVARCHAR(500) NOT NULL,
    Description NVARCHAR(255),
    BackupSize BIGINT,
    Duration INT, -- in seconds
    IncludeAttachments BIT NOT NULL DEFAULT 0,
    CompressionLevel INT NOT NULL DEFAULT 0, -- 0=None, 1=Low, 2=Medium, 3=High
    IsEncrypted BIT NOT NULL DEFAULT 0,
    CreatedBy INT,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Backup_User FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

CREATE TABLE ScheduledBackups (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    BackupPath NVARCHAR(500) NOT NULL,
    Description NVARCHAR(255),
    IncludeAttachments BIT NOT NULL DEFAULT 0,
    CompressionLevel INT NOT NULL DEFAULT 0,
    IsEncrypted BIT NOT NULL DEFAULT 0,
    EncryptionPassword NVARCHAR(255),
    ScheduleType NVARCHAR(20) NOT NULL, -- Daily, Weekly, Monthly
    ScheduleTime TIME NOT NULL,
    DayOfWeek INT, -- For weekly backups
    DayOfMonth INT, -- For monthly backups
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedBy INT,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ScheduledBackup_User FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

-- System Monitoring and Audit Tables
CREATE TABLE ErrorLog (
    ErrorID INT PRIMARY KEY IDENTITY(1,1),
    ErrorModule NVARCHAR(50) NOT NULL,
    ErrorMessage NVARCHAR(MAX) NOT NULL,
    StackTrace NVARCHAR(MAX),
    SeverityLevel INT NOT NULL, -- 1=Info, 2=Warning, 3=Error, 4=Critical
    UserID INT,
    IPAddress NVARCHAR(50),
    Timestamp DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ErrorLog_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE AuditLog (
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    EventType NVARCHAR(50) NOT NULL, -- Login, Logout, DataChange, PermissionChange
    TableName NVARCHAR(100),
    RecordID NVARCHAR(50),
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    IPAddress NVARCHAR(50),
    SeverityLevel INT NOT NULL, -- 1=Info, 2=Warning, 3=Critical
    Timestamp DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_AuditLog_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE SystemResources (
    ResourceID INT PRIMARY KEY IDENTITY(1,1),
    ResourceType NVARCHAR(50) NOT NULL, -- CPU, Memory, Disk
    ResourceValue FLOAT NOT NULL,
    ServerName NVARCHAR(100) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE()
);

-- Insert default roles
INSERT INTO Roles (RoleName, Description)
VALUES 
('Admin', 'System Administrator with full access'),
('Manager', 'Manager with elevated privileges'),
('Sales', 'Sales department user'),
('Inventory', 'Inventory management user'),
('Finance', 'Finance department user');

-- Insert default permissions
INSERT INTO Permissions (PermissionName, Category, Description)
VALUES 
-- User Management permissions
('USER_VIEW', 'User Management', 'View user information'),
('USER_CREATE', 'User Management', 'Create new users'),
('USER_EDIT', 'User Management', 'Edit existing users'),
('USER_DELETE', 'User Management', 'Delete users'),
-- System Configuration permissions
('CONFIG_VIEW', 'System Configuration', 'View system configurations'),
('CONFIG_EDIT', 'System Configuration', 'Edit system configurations'),
-- Backup & Restore permissions
('BACKUP_VIEW', 'Backup Management', 'View backup history'),
('BACKUP_CREATE', 'Backup Management', 'Create new backups'),
('BACKUP_RESTORE', 'Backup Management', 'Restore from backups'),
-- Monitoring permissions
('MONITORING_VIEW', 'System Monitoring', 'View system monitoring data'),
('LOGS_VIEW', 'System Monitoring', 'View system logs'),
('LOGS_CLEAR', 'System Monitoring', 'Clear system logs');

-- Assign all permissions to Admin role
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 1, PermissionID FROM Permissions;

-- Insert admin user (password is 'admin123' with salt)
INSERT INTO Users (Username, PasswordHash, Salt, DisplayName, Email, RoleID)
VALUES ('admin', 'f07a147a4e14f6027c9d248a379c2212d7cd4fb5e34908de6c732978de4e239c', 'adminSalt123', 'System Administrator', 'admin@example.com', 1);

-- Insert default company information
INSERT INTO CompanyInformation (CompanyName, Address, City, State, ZipCode, Country, Phone, Email)
VALUES ('My Company', '123 Main St', 'Springfield', 'IL', '12345', 'USA', '555-123-4567', 'info@mycompany.com');

-- Insert default system settings
INSERT INTO SystemSettings (SettingCategory, SettingName, SettingValue, DataType, Description)
VALUES 
('General', 'CurrencyFormat', '$#,##0.00', 'String', 'Default currency format'),
('General', 'DateFormat', 'MM/dd/yyyy', 'String', 'Default date format'),
('General', 'TimeFormat', 'HH:mm:ss', 'String', 'Default time format'),
('General', 'DefaultLanguage', 'en-US', 'String', 'Default system language'),
('Financial', 'FiscalYearStart', '01/01', 'String', 'Start date of fiscal year (MM/dd)'),
('Financial', 'DefaultTaxRate', '7.5', 'Decimal', 'Default tax rate percentage'),
('Sales', 'ReceiptHeader', 'Thank you for your purchase!', 'String', 'Text to display at top of receipts'),
('Sales', 'ReceiptFooter', 'Please come again!', 'String', 'Text to display at bottom of receipts'),
('Inventory', 'LowStockThreshold', '10', 'Int', 'Default low stock alert threshold');
GO