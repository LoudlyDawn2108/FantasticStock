-- =================================================
-- Create Database
-- =================================================
CREATE DATABASE AdminDomain;
GO

USE AdminDomain;
GO

-- =================================================
-- Security Tables
-- =================================================
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(128) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Department NVARCHAR(50) NULL,
    RoleID INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsLocked BIT NOT NULL DEFAULT 0,
    LoginAttempts INT NOT NULL DEFAULT 0,
    LastLoginDate DATETIME NULL,
    RequirePasswordChange BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    CONSTRAINT CK_Email_Format CHECK (Email LIKE '%_@_%._%')
);

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    IsSystem BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL
);

CREATE TABLE Permissions (
    PermissionID INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    Category NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE RolePermissions (
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NULL,
    PRIMARY KEY (RoleID, PermissionID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
);

-- =================================================
-- Configuration Tables
-- =================================================
CREATE TABLE Settings (
    SettingID INT PRIMARY KEY IDENTITY(1,1),
    SettingKey NVARCHAR(50) NOT NULL UNIQUE,
    SettingValue NVARCHAR(MAX) NULL,
    ValueType NVARCHAR(50) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsUserConfigurable BIT NOT NULL DEFAULT 0,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL
);

CREATE TABLE UserPreferences (
    PreferenceID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    PreferenceKey NVARCHAR(50) NOT NULL,
    PreferenceValue NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT UC_UserPreference UNIQUE (UserID, PreferenceKey)
);

-- =================================================
-- Monitoring Tables
-- =================================================
CREATE TABLE AuditLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NULL,
    Username NVARCHAR(50) NULL,
    ActionType NVARCHAR(50) NOT NULL,
    EntityType NVARCHAR(50) NOT NULL,
    EntityID NVARCHAR(50) NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    ActionDate DATETIME NOT NULL DEFAULT GETDATE(),
    IPAddress NVARCHAR(50) NULL,
    AdditionalInfo NVARCHAR(MAX) NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE SystemLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    LogLevel NVARCHAR(20) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    Exception NVARCHAR(MAX) NULL,
    Source NVARCHAR(255) NULL,
    LogDate DATETIME NOT NULL DEFAULT GETDATE(),
    UserID INT NULL,
    Username NVARCHAR(50) NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- =================================================
-- Inventory Tables
-- =================================================
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    ParentCategoryID INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (ParentCategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100) NULL,
    ContactTitle NVARCHAR(50) NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(255) NULL,
    City NVARCHAR(50) NULL,
    State NVARCHAR(50) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(50) NULL,
    Website NVARCHAR(100) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    SKU NVARCHAR(50) NOT NULL UNIQUE,
    Barcode NVARCHAR(50) NULL,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CategoryID INT NULL,
    SupplierID INT NULL,
    CostPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    SellingPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    WholesalePrice DECIMAL(18,2) NULL,
    ReorderLevel INT NULL,
    TargetStockLevel INT NULL,
    UnitOfMeasure NVARCHAR(20) NULL,
    ManageStock BIT NOT NULL DEFAULT 1,
    AllowFractionalQuantity BIT NOT NULL DEFAULT 0,
    ImagePath NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE ProductInventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    LocationID INT NOT NULL DEFAULT 1,
    QuantityOnHand DECIMAL(18,3) NOT NULL DEFAULT 0,
    QuantityReserved DECIMAL(18,3) NOT NULL DEFAULT 0,
    QuantityAvailable AS (QuantityOnHand - QuantityReserved),
    LastStockTakeDate DATETIME NULL,
    LastUpdatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastUpdatedBy INT NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (LastUpdatedBy) REFERENCES Users(UserID),
    CONSTRAINT UC_ProductLocation UNIQUE (ProductID, LocationID)
);

CREATE TABLE InventoryLocations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    LocationName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

CREATE TABLE InventoryTransactions (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    LocationID INT NOT NULL DEFAULT 1,
    TransactionType NVARCHAR(50) NOT NULL, -- Purchase, Sale, Adjustment, Transfer, etc.
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    Quantity DECIMAL(18,3) NOT NULL,
    UnitPrice DECIMAL(18,2) NULL,
    ReferenceNumber NVARCHAR(50) NULL,
    ReferenceType NVARCHAR(50) NULL, -- PO, SO, Adjustment ID, etc.
    Notes NVARCHAR(MAX) NULL,
    CreatedBy INT NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (LocationID) REFERENCES InventoryLocations(LocationID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

-- =================================================
-- Sales Tables
-- =================================================
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerNumber NVARCHAR(20) NOT NULL UNIQUE,
    CustomerName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100) NULL,
    ContactTitle NVARCHAR(50) NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    BillingAddress NVARCHAR(255) NULL,
    BillingCity NVARCHAR(50) NULL,
    BillingState NVARCHAR(50) NULL,
    BillingPostalCode NVARCHAR(20) NULL,
    BillingCountry NVARCHAR(50) NULL,
    ShippingAddress NVARCHAR(255) NULL,
    ShippingCity NVARCHAR(50) NULL,
    ShippingState NVARCHAR(50) NULL,
    ShippingPostalCode NVARCHAR(20) NULL,
    ShippingCountry NVARCHAR(50) NULL,
    CustomerType NVARCHAR(50) NULL, -- Retail, Wholesale, etc.
    PriceLevel NVARCHAR(20) NULL, -- Standard, Wholesale, VIP, etc.
    CreditLimit DECIMAL(18,2) NULL,
    TaxExempt BIT NOT NULL DEFAULT 0,
    TaxNumber NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE SalesOrders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    OrderNumber NVARCHAR(20) NOT NULL UNIQUE,
    CustomerID INT NOT NULL,
    CustomerName NVARCHAR(100) NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    RequiredDate DATETIME NULL,
    ShippedDate DATETIME NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Draft', -- Draft, Confirmed, Shipped, Delivered, Invoiced, Cancelled
    PaymentStatus NVARCHAR(20) NOT NULL DEFAULT 'Pending', -- Pending, Partial, Paid
    ShippingMethod NVARCHAR(50) NULL,
    ShippingAddress NVARCHAR(255) NULL,
    ShippingCity NVARCHAR(50) NULL,
    ShippingState NVARCHAR(50) NULL,
    ShippingPostalCode NVARCHAR(20) NULL,
    ShippingCountry NVARCHAR(50) NULL,
    Subtotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    ShippingAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount AS (Subtotal + TaxAmount + ShippingAmount - DiscountAmount),
    Notes NVARCHAR(MAX) NULL,
    InternalNotes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE SalesOrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    OrderNumber NVARCHAR(20) NULL,
    ProductID INT NOT NULL,
    ProductName NVARCHAR(100) NOT NULL,
    SKU NVARCHAR(50) NOT NULL,
    Quantity DECIMAL(18,3) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
    DiscountAmount AS (UnitPrice * Quantity * DiscountPercent / 100),
    TaxPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
    TaxAmount AS (UnitPrice * Quantity * (1 - DiscountPercent / 100) * TaxPercent / 100),
    LineTotal AS (UnitPrice * Quantity * (1 - DiscountPercent / 100)),
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (OrderID) REFERENCES SalesOrders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceNumber NVARCHAR(20) NOT NULL UNIQUE,
    OrderID INT NULL,
    CustomerID INT NOT NULL,
    CustomerName NVARCHAR(100) NULL,
    InvoiceDate DATETIME NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Issued', -- Issued, Partially Paid, Paid, Overdue, Void
    BillingAddress NVARCHAR(255) NULL,
    BillingCity NVARCHAR(50) NULL,
    BillingState NVARCHAR(50) NULL,
    BillingPostalCode NVARCHAR(20) NULL,
    BillingCountry NVARCHAR(50) NULL,
    Subtotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    AmountPaid DECIMAL(18,2) NOT NULL DEFAULT 0,
    Balance AS (TotalAmount - AmountPaid),
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (OrderID) REFERENCES SalesOrders(OrderID),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE InvoiceDetails (
    InvoiceDetailID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT NOT NULL,
    ProductID INT NOT NULL,
    ProductName NVARCHAR(100) NOT NULL,
    SKU NVARCHAR(50) NOT NULL,
    Quantity DECIMAL(18,3) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
    DiscountAmount AS (UnitPrice * Quantity * DiscountPercent / 100),
    TaxPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
    TaxAmount AS (UnitPrice * Quantity * (1 - DiscountPercent / 100) * TaxPercent / 100),
    LineTotal AS (UnitPrice * Quantity * (1 - DiscountPercent / 100)),
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- =================================================
-- Financial Tables
-- =================================================
CREATE TABLE PaymentMethods (
    PaymentMethodID INT PRIMARY KEY IDENTITY(1,1),
    MethodName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    RequiresApproval BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentNumber NVARCHAR(20) NOT NULL UNIQUE,
    CustomerID INT NOT NULL,
    CustomerName NVARCHAR(100) NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethodID INT NOT NULL,
    PaymentMethod NVARCHAR(50) NULL,
    Amount DECIMAL(18,2) NOT NULL,
    ReferenceNumber NVARCHAR(50) NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Received', -- Received, Cleared, Failed, Voided
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (PaymentMethodID) REFERENCES PaymentMethods(PaymentMethodID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE PaymentAllocations (
    AllocationID INT PRIMARY KEY IDENTITY(1,1),
    PaymentID INT NOT NULL,
    InvoiceID INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

CREATE TABLE ExpenseCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE Expenses (
    ExpenseID INT PRIMARY KEY IDENTITY(1,1),
    ExpenseNumber NVARCHAR(20) NOT NULL UNIQUE,
    CategoryID INT NOT NULL,
    CategoryName NVARCHAR(100) NULL,
    PayeeID INT NULL, -- Supplier ID if applicable
    PayeeName NVARCHAR(100) NOT NULL,
    ExpenseDate DATETIME NOT NULL,
    DueDate DATETIME NULL,
    PaymentMethodID INT NULL,
    PaymentMethod NVARCHAR(50) NULL,
    Description NVARCHAR(255) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount AS (Amount + TaxAmount),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Unpaid', -- Unpaid, Paid, Void
    ReferenceNumber NVARCHAR(50) NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CategoryID) REFERENCES ExpenseCategories(CategoryID),
    FOREIGN KEY (PayeeID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (PaymentMethodID) REFERENCES PaymentMethods(PaymentMethodID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE TaxRates (
    TaxRateID INT PRIMARY KEY IDENTITY(1,1),
    TaxName NVARCHAR(50) NOT NULL UNIQUE,
    Rate DECIMAL(5,2) NOT NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    Description NVARCHAR(255) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

CREATE TABLE FiscalYears (
    FiscalYearID INT PRIMARY KEY IDENTITY(1,1),
    YearName NVARCHAR(50) NOT NULL UNIQUE,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsClosed BIT NOT NULL DEFAULT 0,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID),
    CONSTRAINT CK_FiscalYear_Dates CHECK (EndDate > StartDate)
);

CREATE TABLE AccountingPeriods (
    PeriodID INT PRIMARY KEY IDENTITY(1,1),
    FiscalYearID INT NOT NULL,
    PeriodNumber INT NOT NULL,
    PeriodName NVARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsClosed BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy INT NULL,
    FOREIGN KEY (FiscalYearID) REFERENCES FiscalYears(FiscalYearID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID),
    CONSTRAINT CK_Period_Dates CHECK (EndDate > StartDate),
    CONSTRAINT UC_Period_FiscalYear UNIQUE (FiscalYearID, PeriodNumber)
);

-- =================================================
-- Reference Data Inserts
-- =================================================

-- Add default roles
INSERT INTO Roles (RoleName, Description, IsSystem)
VALUES
    ('Administrator', 'Full system access', 1),
    ('Manager', 'Access to most features except system configuration', 0),
    ('Sales', 'Access to sales-related functions', 0),
    ('Inventory', 'Access to inventory-related functions', 0),
    ('Finance', 'Access to financial functions', 0),
    ('ReadOnly', 'Read-only access to all data', 0);
GO

-- Add default permissions
INSERT INTO Permissions (PermissionName, Description, Category)
VALUES
    -- Security permissions
    ('USERS_VIEW', 'View users', 'Security'),
    ('USERS_CREATE', 'Create users', 'Security'),
    ('USERS_EDIT', 'Edit users', 'Security'),
    ('USERS_DELETE', 'Delete users', 'Security'),
    ('ROLES_VIEW', 'View roles', 'Security'),
    ('ROLES_CREATE', 'Create roles', 'Security'),
    ('ROLES_EDIT', 'Edit roles', 'Security'),
    ('ROLES_DELETE', 'Delete roles', 'Security'),
    
    -- Configuration permissions
    ('SETTINGS_VIEW', 'View system settings', 'Configuration'),
    ('SETTINGS_EDIT', 'Edit system settings', 'Configuration'),
    
    -- Monitoring permissions
    ('LOGS_VIEW', 'View system logs', 'Monitoring'),
    ('AUDIT_VIEW', 'View audit logs', 'Monitoring'),
    
    -- Inventory permissions
    ('INVENTORY_VIEW', 'View inventory', 'Inventory'),
    ('INVENTORY_CREATE', 'Create inventory items', 'Inventory'),
    ('INVENTORY_EDIT', 'Edit inventory items', 'Inventory'),
    ('INVENTORY_DELETE', 'Delete inventory items', 'Inventory'),
    ('CATEGORIES_MANAGE', 'Manage product categories', 'Inventory'),
    ('SUPPLIERS_MANAGE', 'Manage suppliers', 'Inventory'),
    ('STOCK_ADJUST', 'Adjust stock levels', 'Inventory'),
    
    -- Sales permissions
    ('CUSTOMERS_VIEW', 'View customers', 'Sales'),
    ('CUSTOMERS_CREATE', 'Create customers', 'Sales'),
    ('CUSTOMERS_EDIT', 'Edit customers', 'Sales'),
    ('CUSTOMERS_DELETE', 'Delete customers', 'Sales'),
    ('ORDERS_VIEW', 'View sales orders', 'Sales'),
    ('ORDERS_CREATE', 'Create sales orders', 'Sales'),
    ('ORDERS_EDIT', 'Edit sales orders', 'Sales'),
    ('ORDERS_DELETE', 'Delete sales orders', 'Sales'),
    ('ORDERS_FULFILL', 'Fulfill sales orders', 'Sales'),
    ('INVOICES_VIEW', 'View invoices', 'Sales'),
    ('INVOICES_CREATE', 'Create invoices', 'Sales'),
    ('INVOICES_EDIT', 'Edit invoices', 'Sales'),
    ('INVOICES_DELETE', 'Delete invoices', 'Sales'),
    
    -- Financial permissions
    ('PAYMENTS_VIEW', 'View payments', 'Financial'),
    ('PAYMENTS_CREATE', 'Create payments', 'Financial'),
    ('PAYMENTS_EDIT', 'Edit payments', 'Financial'),
    ('PAYMENTS_DELETE', 'Delete payments', 'Financial'),
    ('EXPENSES_VIEW', 'View expenses', 'Financial'),
    ('EXPENSES_CREATE', 'Create expenses', 'Financial'),
    ('EXPENSES_EDIT', 'Edit expenses', 'Financial'),
    ('EXPENSES_DELETE', 'Delete expenses', 'Financial'),
    ('REPORTS_VIEW', 'View financial reports', 'Financial'),
    ('PERIODS_MANAGE', 'Manage accounting periods', 'Financial');
GO

-- Assign all permissions to Administrator role
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 1, PermissionID FROM Permissions;
GO

-- Create default admin user (password is 'Admin@123')
INSERT INTO Users (Username, PasswordHash, FirstName, LastName, Email, RoleID, CreatedBy)
VALUES ('admin', 'AQAAAAEAACcQAAAAEKXsu0McrUKpBBF3F9FL01/jRXGQ7oVnroQ0zZFwNzlYZwLJLxlqCFSZ26Vrh5Qo1A==', 'System', 'Administrator', 'admin@example.com', 1, 1);
GO

-- Create default inventory location
INSERT INTO InventoryLocations (LocationName, Description, CreatedBy)
VALUES ('Main Warehouse', 'Primary inventory storage location', 1);
GO

-- Add default payment methods
INSERT INTO PaymentMethods (MethodName, Description, CreatedBy)
VALUES 
    ('Cash', 'Cash payment', 1),
    ('Credit Card', 'Credit card payment', 1),
    ('Bank Transfer', 'Direct bank transfer', 1),
    ('Check', 'Check payment', 1);
GO

-- Add default tax rates
INSERT INTO TaxRates (TaxName, Rate, IsDefault, CreatedBy)
VALUES 
    ('No Tax', 0.00, 0, 1),
    ('Standard Rate', 10.00, 1, 1),
    ('Reduced Rate', 5.00, 0, 1);
GO

-- Add default settings
INSERT INTO Settings (SettingKey, SettingValue, ValueType, Category, Description, IsUserConfigurable)
VALUES
    ('CompanyName', 'My Company', 'string', 'General', 'Company name used in reports and documents', 1),
    ('CompanyEmail', 'info@mycompany.com', 'string', 'General', 'Default company email address', 1),
    ('CompanyPhone', '555-123-4567', 'string', 'General', 'Company phone number', 1),
    ('CompanyAddress', '123 Business St, Business City, 12345', 'string', 'General', 'Company address', 1),
    ('DefaultTaxRate', '2', 'int', 'Financial', 'Default tax rate ID', 1),
    ('InvoiceDueDays', '30', 'int', 'Financial', 'Default number of days until invoice is due', 1),
    ('LowStockThreshold', '5', 'int', 'Inventory', 'Threshold for low stock warnings', 1),
    ('EnableStockWarnings', 'true', 'boolean', 'Inventory', 'Enable low stock warnings', 1),
    ('AllowNegativeStock', 'false', 'boolean', 'Inventory', 'Allow stock to go below zero', 1),
    ('MaxLoginAttempts', '5', 'int', 'Security', 'Maximum failed login attempts before locking account', 1),
    ('PasswordExpiryDays', '90', 'int', 'Security', 'Days before password expires', 1),
    ('SessionTimeoutMinutes', '30', 'int', 'Security', 'Minutes before idle session expires', 1),
    ('NextOrderNumber', '1001', 'string', 'Sales', 'Next order number to be used', 0),
    ('NextInvoiceNumber', '1001', 'string', 'Financial', 'Next invoice number to be used', 0),
    ('NextPaymentNumber', '1001', 'string', 'Financial', 'Next payment number to be used', 0),
    ('NextExpenseNumber', '1001', 'string', 'Financial', 'Next expense number to be used', 0),
    ('NextCustomerNumber', '1001', 'string', 'Sales', 'Next customer number to be used', 0);
GO

-- =================================================
-- Create Foreign Key Constraints
-- (for circular references added after table creation)
-- =================================================
ALTER TABLE Users
ADD CONSTRAINT FK_Users_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID);

ALTER TABLE Users
ADD CONSTRAINT FK_Users_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID);

ALTER TABLE Users
ADD CONSTRAINT FK_Users_RoleID FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);

ALTER TABLE Roles
ADD CONSTRAINT FK_Roles_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID);

ALTER TABLE Roles
ADD CONSTRAINT FK_Roles_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID);

ALTER TABLE RolePermissions
ADD CONSTRAINT FK_RolePermissions_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID);

ALTER TABLE Settings
ADD CONSTRAINT FK_Settings_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID);

-- =================================================
-- Create Indexes
-- =================================================
-- Security indexes
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_RoleID ON Users(RoleID);
CREATE INDEX IX_Users_IsActive ON Users(IsActive);

CREATE INDEX IX_Permissions_Category ON Permissions(Category);

-- Monitoring indexes
CREATE INDEX IX_AuditLogs_UserID ON AuditLogs(UserID);
CREATE INDEX IX_AuditLogs_ActionDate ON AuditLogs(ActionDate);
CREATE INDEX IX_AuditLogs_EntityType_EntityID ON AuditLogs(EntityType, EntityID);

CREATE INDEX IX_SystemLogs_LogLevel ON SystemLogs(LogLevel);
CREATE INDEX IX_SystemLogs_LogDate ON SystemLogs(LogDate);
CREATE INDEX IX_SystemLogs_UserID ON SystemLogs(UserID);

-- Configuration indexes
CREATE INDEX IX_Settings_Category ON Settings(Category);

CREATE INDEX IX_UserPreferences_UserID ON UserPreferences(UserID);

-- Inventory indexes
CREATE INDEX IX_Products_SKU ON Products(SKU);
CREATE INDEX IX_Products_Barcode ON Products(Barcode);
CREATE INDEX IX_Products_CategoryID ON Products(CategoryID);
CREATE INDEX IX_Products_SupplierID ON Products(SupplierID);
CREATE INDEX IX_Products_IsActive ON Products(IsActive);

CREATE INDEX IX_Categories_ParentCategoryID ON Categories(ParentCategoryID);
CREATE INDEX IX_Categories_IsActive ON Categories(IsActive);

CREATE INDEX IX_Suppliers_CompanyName ON Suppliers(CompanyName);
CREATE INDEX IX_Suppliers_IsActive ON Suppliers(IsActive);

CREATE INDEX IX_ProductInventory_ProductID ON ProductInventory(ProductID);
CREATE INDEX IX_ProductInventory_LocationID ON ProductInventory(LocationID);

CREATE INDEX IX_InventoryTransactions_ProductID ON InventoryTransactions(ProductID);
CREATE INDEX IX_InventoryTransactions_TransactionDate ON InventoryTransactions(TransactionDate);
CREATE INDEX IX_InventoryTransactions_TransactionType ON InventoryTransactions(TransactionType);
CREATE INDEX IX_InventoryTransactions_ReferenceNumber ON InventoryTransactions(ReferenceNumber);

-- Sales indexes
CREATE INDEX IX_Customers_CustomerNumber ON Customers(CustomerNumber);
CREATE INDEX IX_Customers_CustomerName ON Customers(CustomerName);
CREATE INDEX IX_Customers_Email ON Customers(Email);
CREATE INDEX IX_Customers_IsActive ON Customers(IsActive);

CREATE INDEX IX_SalesOrders_OrderNumber ON SalesOrders(OrderNumber);
CREATE INDEX IX_SalesOrders_CustomerID ON SalesOrders(CustomerID);
CREATE INDEX IX_SalesOrders_OrderDate ON SalesOrders(OrderDate);
CREATE INDEX IX_SalesOrders_Status ON SalesOrders(Status);
CREATE INDEX IX_SalesOrders_PaymentStatus ON SalesOrders(PaymentStatus);

CREATE INDEX IX_SalesOrderDetails_OrderID ON SalesOrderDetails(OrderID);
CREATE INDEX IX_SalesOrderDetails_ProductID ON SalesOrderDetails(ProductID);

CREATE INDEX IX_Invoices_InvoiceNumber ON Invoices(InvoiceNumber);
CREATE INDEX IX_Invoices_CustomerID ON Invoices(CustomerID);
CREATE INDEX IX_Invoices_OrderID ON Invoices(OrderID);
CREATE INDEX IX_Invoices_InvoiceDate ON Invoices(InvoiceDate);
CREATE INDEX IX_Invoices_DueDate ON Invoices(DueDate);
CREATE INDEX IX_Invoices_Status ON Invoices(Status);

CREATE INDEX IX_InvoiceDetails_InvoiceID ON InvoiceDetails(InvoiceID);
CREATE INDEX IX_InvoiceDetails_ProductID ON InvoiceDetails(ProductID);

-- Financial indexes
CREATE INDEX IX_Payments_PaymentNumber ON Payments(PaymentNumber);
CREATE INDEX IX_Payments_CustomerID ON Payments(CustomerID);
CREATE INDEX IX_Payments_PaymentDate ON Payments(PaymentDate);
CREATE INDEX IX_Payments_PaymentMethodID ON Payments(PaymentMethodID);
CREATE INDEX IX_Payments_Status ON Payments(Status);

CREATE INDEX IX_PaymentAllocations_PaymentID ON PaymentAllocations(PaymentID);
CREATE INDEX IX_PaymentAllocations_InvoiceID ON PaymentAllocations(InvoiceID);

CREATE INDEX IX_Expenses_ExpenseNumber ON Expenses(ExpenseNumber);
CREATE INDEX IX_Expenses_CategoryID ON Expenses(CategoryID);
CREATE INDEX IX_Expenses_PayeeID ON Expenses(PayeeID);
CREATE INDEX IX_Expenses_ExpenseDate ON Expenses(ExpenseDate);
CREATE INDEX IX_Expenses_Status ON Expenses(Status);

CREATE INDEX IX_ExpenseCategories_CategoryName ON ExpenseCategories(CategoryName);

CREATE INDEX IX_TaxRates_IsDefault ON TaxRates(IsDefault);

CREATE INDEX IX_FiscalYears_StartDate_EndDate ON FiscalYears(StartDate, EndDate);
CREATE INDEX IX_FiscalYears_IsClosed ON FiscalYears(IsClosed);

CREATE INDEX IX_AccountingPeriods_FiscalYearID ON AccountingPeriods(FiscalYearID);
CREATE INDEX IX_AccountingPeriods_StartDate_EndDate ON AccountingPeriods(StartDate, EndDate);
CREATE INDEX IX_AccountingPeriods_IsClosed ON AccountingPeriods(IsClosed);

-- =================================================
-- Create Views
-- =================================================

-- Current Stock Levels View
CREATE VIEW vw_CurrentStockLevels AS
SELECT 
    p.ProductID,
    p.SKU,
    p.Barcode,
    p.ProductName,
    c.CategoryName,
    s.CompanyName AS SupplierName,
    pi.QuantityOnHand,
    pi.QuantityReserved,
    pi.QuantityAvailable,
    p.ReorderLevel,
    p.TargetStockLevel,
    p.UnitOfMeasure,
    p.CostPrice,
    p.SellingPrice,
    p.IsActive,
    CASE 
        WHEN pi.QuantityAvailable <= p.ReorderLevel THEN 'Low'
        WHEN pi.QuantityAvailable <= p.TargetStockLevel THEN 'Medium'
        ELSE 'Good'
    END AS StockStatus,
    pi.LastStockTakeDate,
    pi.LastUpdatedDate
FROM Products p
JOIN ProductInventory pi ON p.ProductID = pi.ProductID
LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
WHERE p.ManageStock = 1;
GO

-- Inventory Valuation View
CREATE VIEW vw_InventoryValuation AS
SELECT
    p.ProductID,
    p.SKU,
    p.ProductName,
    c.CategoryName,
    pi.QuantityOnHand,
    p.CostPrice,
    (pi.QuantityOnHand * p.CostPrice) AS ValueAtCost,
    p.SellingPrice,
    (pi.QuantityOnHand * p.SellingPrice) AS ValueAtRetail
FROM Products p
JOIN ProductInventory pi ON p.ProductID = pi.ProductID
LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE p.ManageStock = 1 AND p.IsActive = 1;
GO

-- Sales By Customer View
CREATE VIEW vw_SalesByCustomer AS
SELECT
    c.CustomerID,
    c.CustomerNumber,
    c.CustomerName,
    COUNT(DISTINCT so.OrderID) AS OrderCount,
    SUM(so.Subtotal) AS TotalSales,
    SUM(so.TaxAmount) AS TotalTax,
    SUM(so.Subtotal + so.TaxAmount + so.ShippingAmount - so.DiscountAmount) AS TotalAmount,
    MAX(so.OrderDate) AS LastOrderDate
FROM Customers c
LEFT JOIN SalesOrders so ON c.CustomerID = so.CustomerID
WHERE so.Status <> 'Cancelled' OR so.Status IS NULL
GROUP BY c.CustomerID, c.CustomerNumber, c.CustomerName;
GO

-- Outstanding Invoices View
CREATE VIEW vw_OutstandingInvoices AS
SELECT
    i.InvoiceID,
    i.InvoiceNumber,
    i.CustomerID,
    c.CustomerName,
    i.InvoiceDate,
    i.DueDate,
    i.TotalAmount,
    i.AmountPaid,
    i.Balance,
    i.Status,
    DATEDIFF(DAY, i.DueDate, GETDATE()) AS DaysOverdue,
    CASE
        WHEN DATEDIFF(DAY, i.DueDate, GETDATE()) <= 0 THEN 'Current'
        WHEN DATEDIFF(DAY, i.DueDate, GETDATE()) <= 30 THEN '1-30 Days'
        WHEN DATEDIFF(DAY, i.DueDate, GETDATE()) <= 60 THEN '31-60 Days'
        WHEN DATEDIFF(DAY, i.DueDate, GETDATE()) <= 90 THEN '61-90 Days'
        ELSE 'Over 90 Days'
    END AS AgingCategory
FROM Invoices i
JOIN Customers c ON i.CustomerID = c.CustomerID
WHERE i.Balance > 0 AND i.Status <> 'Void';
GO

-- Product Sales Report View
CREATE VIEW vw_ProductSalesReport AS
SELECT
    p.ProductID,
    p.SKU,
    p.ProductName,
    c.CategoryName,
    COUNT(DISTINCT sod.OrderDetailID) AS TimesSold,
    SUM(sod.Quantity) AS TotalQuantitySold,
    SUM(sod.LineTotal) AS TotalRevenue,
    SUM(sod.LineTotal) - (SUM(sod.Quantity) * p.CostPrice) AS EstimatedProfit
FROM Products p
LEFT JOIN SalesOrderDetails sod ON p.ProductID = sod.ProductID
LEFT JOIN SalesOrders so ON sod.OrderID = so.OrderID
LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE so.Status <> 'Cancelled' OR so.Status IS NULL
GROUP BY p.ProductID, p.SKU, p.ProductName, c.CategoryName, p.CostPrice;
GO

-- =================================================
-- Create Stored Procedures
-- =================================================

-- Create procedure to adjust stock levels
CREATE PROCEDURE sp_AdjustStock
    @ProductID INT,
    @LocationID INT = 1,
    @Quantity DECIMAL(18,3),
    @Reason NVARCHAR(255),
    @UserID INT,
    @ReferenceNumber NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Update inventory quantity
        UPDATE ProductInventory
        SET QuantityOnHand = QuantityOnHand + @Quantity,
            LastUpdatedDate = GETDATE(),
            LastUpdatedBy = @UserID
        WHERE ProductID = @ProductID AND LocationID = @LocationID;
        
        -- If no rows were updated, insert a new record
        IF @@ROWCOUNT = 0
        BEGIN
            INSERT INTO ProductInventory (ProductID, LocationID, QuantityOnHand, LastUpdatedDate, LastUpdatedBy)
            VALUES (@ProductID, @LocationID, @Quantity, GETDATE(), @UserID);
        END
        
        -- Record the transaction
        INSERT INTO InventoryTransactions 
            (ProductID, LocationID, TransactionType, Quantity, Notes, CreatedBy, ReferenceNumber)
        VALUES 
            (@ProductID, @LocationID, 'Adjustment', @Quantity, @Reason, @UserID, @ReferenceNumber);
        
        COMMIT TRANSACTION;
        
        SELECT 'Success' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        
        SELECT 
            'Error' AS Result,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH;
END;
GO

-- Create procedure to generate invoice from order
CREATE PROCEDURE sp_GenerateInvoiceFromOrder
    @OrderID INT,
    @UserID INT,
    @DueDays INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @InvoiceNumber NVARCHAR(20);
    DECLARE @CustomerID INT;
    DECLARE @CustomerName NVARCHAR(100);
    DECLARE @BillingAddress NVARCHAR(255);
    DECLARE @BillingCity NVARCHAR(50);
    DECLARE @BillingState NVARCHAR(50);
    DECLARE @BillingPostalCode NVARCHAR(20);
    DECLARE @BillingCountry NVARCHAR(50);
    DECLARE @Subtotal DECIMAL(18,2);
    DECLARE @TaxAmount DECIMAL(18,2);
    DECLARE @TotalAmount DECIMAL(18,2);
    DECLARE @InvoiceID INT;
    DECLARE @InvoiceDate DATETIME = GETDATE();
    DECLARE @DueDate DATETIME;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Check if order exists and is not already invoiced
        IF NOT EXISTS (SELECT 1 FROM SalesOrders WHERE OrderID = @OrderID AND Status <> 'Invoiced')
        BEGIN
            RAISERROR('Order not found or already invoiced', 16, 1);
        END
        
        -- Get next invoice number
        SELECT @InvoiceNumber = 'INV-' + SettingValue 
        FROM Settings 
        WHERE SettingKey = 'NextInvoiceNumber';
        
        -- Update next invoice number
        UPDATE Settings 
        SET SettingValue = CAST(CAST(SettingValue AS INT) + 1 AS NVARCHAR(50)),
            ModifiedDate = GETDATE(),
            ModifiedBy = @UserID
        WHERE SettingKey = 'NextInvoiceNumber';
        
        -- Get order details
        SELECT 
            @CustomerID = CustomerID,
            @CustomerName = CustomerName,
            @BillingAddress = ShippingAddress,
            @BillingCity = ShippingCity,
            @BillingState = ShippingState,
            @BillingPostalCode = ShippingPostalCode,
            @BillingCountry = ShippingCountry,
            @Subtotal = Subtotal,
            @TaxAmount = TaxAmount,
            @TotalAmount = Subtotal + TaxAmount + ShippingAmount - DiscountAmount
        FROM SalesOrders
        WHERE OrderID = @OrderID;
        
        -- Set due date
            SELECT @DueDays = CAST(SettingValue AS INT)
            FROM Settings
            WHERE SettingKey = 'InvoiceDueDays';
        END
        
        SET @DueDate = DATEADD(DAY, @DueDays, @InvoiceDate);
        
        -- Create invoice
        INSERT INTO Invoices (
            InvoiceNumber,
            OrderID,
            CustomerID,
            CustomerName,
            InvoiceDate,
            DueDate,
            Status,
            BillingAddress,
            BillingCity,
            BillingState,
            BillingPostalCode,
            BillingCountry,
            Subtotal,
            TaxAmount,
            TotalAmount,
            CreatedDate,
            CreatedBy
        )
        VALUES (
            @InvoiceNumber,
            @OrderID,
            @CustomerID,
            @CustomerName,
            @InvoiceDate,
            @DueDate,
            'Issued',
            @BillingAddress,
            @BillingCity,
            @BillingState,
            @BillingPostalCode,
            @BillingCountry,
            @Subtotal,
            @TaxAmount,
            @TotalAmount,
            GETDATE(),
            @UserID
        );
        
        -- Get new invoice ID
        SET @InvoiceID = SCOPE_IDENTITY();
        
        -- Copy order items to invoice items
        INSERT INTO InvoiceDetails (
            InvoiceID,
            ProductID,
            ProductName,
            SKU,
            Quantity,
            UnitPrice,
            DiscountPercent,
            TaxPercent,
            Notes,
            CreatedDate
        )
        SELECT 
            @InvoiceID,
            sod.ProductID,
            sod.ProductName,
            sod.SKU,
            sod.Quantity,
            sod.UnitPrice,
            sod.DiscountPercent,
            sod.TaxPercent,
            sod.Notes,
            GETDATE()
        FROM SalesOrderDetails sod
        WHERE sod.OrderID = @OrderID;
        
        -- Update order status
        UPDATE SalesOrders
        SET Status = 'Invoiced',
            ModifiedDate = GETDATE(),
            ModifiedBy = @UserID
        WHERE OrderID = @OrderID;
        
        COMMIT TRANSACTION;
        
        -- Return the new invoice ID
        SELECT @InvoiceID AS InvoiceID, @InvoiceNumber AS InvoiceNumber, 'Success' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        
        SELECT 
            'Error' AS Result,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH;
END;
GO

-- Create procedure to apply payment to invoice
CREATE PROCEDURE sp_ApplyPayment
    @CustomerID INT,
    @Amount DECIMAL(18,2),
    @PaymentMethodID INT,
    @PaymentMethod NVARCHAR(50),
    @ReferenceNumber NVARCHAR(50),
    @Notes NVARCHAR(MAX),
    @UserID INT,
    @InvoiceAllocations XML
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @PaymentNumber NVARCHAR(20);
    DECLARE @PaymentID INT;
    DECLARE @CustomerName NVARCHAR(100);
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Get customer name
        SELECT @CustomerName = CustomerName
        FROM Customers
        WHERE CustomerID = @CustomerID;
        
        -- Get next payment number
        SELECT @PaymentNumber = 'PAY-' + SettingValue 
        FROM Settings 
        WHERE SettingKey = 'NextPaymentNumber';
        
        -- Update next payment number
        UPDATE Settings 
        SET SettingValue = CAST(CAST(SettingValue AS INT) + 1 AS NVARCHAR(50)),
            ModifiedDate = GETDATE(),
            ModifiedBy = @UserID
        WHERE SettingKey = 'NextPaymentNumber';
        
        -- Create payment record
        INSERT INTO Payments (
            PaymentNumber,
            CustomerID,
            CustomerName,
            PaymentDate,
            PaymentMethodID,
            PaymentMethod,
            Amount,
            ReferenceNumber,
            Notes,
            CreatedBy
        )
        VALUES (
            @PaymentNumber,
            @CustomerID,
            @CustomerName,
            GETDATE(),
            @PaymentMethodID,
            @PaymentMethod,
            @Amount,
            @ReferenceNumber,
            @Notes,
            @UserID
        );
        
        SET @PaymentID = SCOPE_IDENTITY();
        
        -- Process invoice allocations
        INSERT INTO PaymentAllocations (
            PaymentID,
            InvoiceID,
            Amount,
            CreatedBy
        )
        SELECT 
            @PaymentID,
            InvoiceNode.value('@InvoiceID', 'INT'),
            InvoiceNode.value('@Amount', 'DECIMAL(18,2)'),
            @UserID
        FROM @InvoiceAllocations.nodes('/Allocations/Invoice') AS T(InvoiceNode);
        
        -- Update invoice payment amounts
        UPDATE i
        SET i.AmountPaid = i.AmountPaid + pa.AllocatedAmount,
            i.Status = CASE 
                WHEN i.AmountPaid + pa.AllocatedAmount >= i.TotalAmount THEN 'Paid'
                WHEN i.AmountPaid + pa.AllocatedAmount > 0 THEN 'Partially Paid'
                ELSE i.Status
            END,
            i.ModifiedDate = GETDATE(),
            i.ModifiedBy = @UserID
        FROM Invoices i
        INNER JOIN (
            SELECT 
                InvoiceID, 
                SUM(Amount) AS AllocatedAmount
            FROM PaymentAllocations
            WHERE PaymentID = @PaymentID
            GROUP BY InvoiceID
        ) pa ON i.InvoiceID = pa.InvoiceID;
        
        COMMIT TRANSACTION;
        
        SELECT @PaymentID AS PaymentID, @PaymentNumber AS PaymentNumber, 'Success' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        
        SELECT 
            'Error' AS Result,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH;
END;
GO

-- Create procedure to generate financial report data
CREATE PROCEDURE sp_GenerateFinancialReport
    @StartDate DATE,
    @EndDate DATE,
    @ReportType VARCHAR(50) -- 'Income', 'Expense', 'Profit'
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @ReportType = 'Income'
    BEGIN
        -- Income by month
        SELECT 
            YEAR(InvoiceDate) AS Year,
            MONTH(InvoiceDate) AS Month,
            FORMAT(DATEFROMPARTS(YEAR(InvoiceDate), MONTH(InvoiceDate), 1), 'MMM yyyy') AS Period,
            SUM(TotalAmount) AS TotalIncome,
            COUNT(InvoiceID) AS InvoiceCount
        FROM Invoices
        WHERE InvoiceDate BETWEEN @StartDate AND @EndDate
            AND Status <> 'Void'
        GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate)
        ORDER BY YEAR(InvoiceDate), MONTH(InvoiceDate);
    END
    ELSE IF @ReportType = 'Expense'
    BEGIN
        -- Expenses by month and category
        SELECT 
            YEAR(e.ExpenseDate) AS Year,
            MONTH(e.ExpenseDate) AS Month,
            FORMAT(DATEFROMPARTS(YEAR(e.ExpenseDate), MONTH(e.ExpenseDate), 1), 'MMM yyyy') AS Period,
            ec.CategoryName,
            SUM(e.Amount + e.TaxAmount) AS TotalExpense,
            COUNT(e.ExpenseID) AS ExpenseCount
        FROM Expenses e
        JOIN ExpenseCategories ec ON e.CategoryID = ec.CategoryID
        WHERE e.ExpenseDate BETWEEN @StartDate AND @EndDate
            AND e.Status <> 'Void'
        GROUP BY YEAR(e.ExpenseDate), MONTH(e.ExpenseDate), ec.CategoryName
        ORDER BY YEAR(e.ExpenseDate), MONTH(e.ExpenseDate), ec.CategoryName;
    END
    ELSE IF @ReportType = 'Profit'
    BEGIN
        -- Monthly profit (income - expense)
        WITH MonthlyIncome AS (
            SELECT 
                YEAR(InvoiceDate) AS Year,
                MONTH(InvoiceDate) AS Month,
                SUM(TotalAmount) AS TotalIncome
            FROM Invoices
            WHERE InvoiceDate BETWEEN @StartDate AND @EndDate
                AND Status <> 'Void'
            GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate)
        ),
        MonthlyExpense AS (
            SELECT 
                YEAR(ExpenseDate) AS Year,
                MONTH(ExpenseDate) AS Month,
                SUM(Amount + TaxAmount) AS TotalExpense
            FROM Expenses
            WHERE ExpenseDate BETWEEN @StartDate AND @EndDate
                AND Status <> 'Void'
            GROUP BY YEAR(ExpenseDate), MONTH(ExpenseDate)
        )
        SELECT 
            COALESCE(i.Year, e.Year) AS Year,
            COALESCE(i.Month, e.Month) AS Month,
            FORMAT(DATEFROMPARTS(COALESCE(i.Year, e.Year), COALESCE(i.Month, e.Month), 1), 'MMM yyyy') AS Period,
            COALESCE(i.TotalIncome, 0) AS TotalIncome,
            COALESCE(e.TotalExpense, 0) AS TotalExpense,
            COALESCE(i.TotalIncome, 0) - COALESCE(e.TotalExpense, 0) AS NetProfit,
            CASE 
                WHEN COALESCE(i.TotalIncome, 0) = 0 THEN 0
                ELSE ((COALESCE(i.TotalIncome, 0) - COALESCE(e.TotalExpense, 0)) / COALESCE(i.TotalIncome, 0)) * 100 
            END AS ProfitMarginPercent
        FROM MonthlyIncome i
        FULL OUTER JOIN MonthlyExpense e ON i.Year = e.Year AND i.Month = e.Month
        ORDER BY Year, Month;
    END
END;
GO

-- Create procedure to get dashboard data
CREATE PROCEDURE sp_GetDashboardData
    @StartDate DATE,
    @EndDate DATE,
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Summary Metrics
    SELECT
        (SELECT COUNT(*) FROM SalesOrders WHERE OrderDate BETWEEN @StartDate AND @EndDate) AS TotalOrders,
        (SELECT COALESCE(SUM(TotalAmount), 0) FROM Invoices WHERE InvoiceDate BETWEEN @StartDate AND @EndDate AND Status <> 'Void') AS TotalSales,
        (SELECT COALESCE(SUM(Amount), 0) FROM Payments WHERE PaymentDate BETWEEN @StartDate AND @EndDate AND Status <> 'Voided') AS TotalPayments,
        (SELECT COALESCE(SUM(Amount + TaxAmount), 0) FROM Expenses WHERE ExpenseDate BETWEEN @StartDate AND @EndDate AND Status <> 'Void') AS TotalExpenses,
        (SELECT COUNT(*) FROM Products WHERE IsActive = 1) AS ActiveProducts;
    
    -- Low Stock Items
    SELECT TOP 10
        p.ProductID,
        p.SKU,
        p.ProductName,
        pi.QuantityAvailable,
        p.ReorderLevel
    FROM Products p
    JOIN ProductInventory pi ON p.ProductID = pi.ProductID
    WHERE p.IsActive = 1 
      AND pi.QuantityAvailable <= p.ReorderLevel
      AND p.ManageStock = 1
    ORDER BY pi.QuantityAvailable ASC;
    
    -- Recent Orders
    SELECT TOP 10
        OrderID,
        OrderNumber,
        CustomerName,
        OrderDate,
        Status,
        TotalAmount
    FROM SalesOrders
    ORDER BY OrderDate DESC;
    
    -- Outstanding Invoices
    SELECT TOP 10
        InvoiceID,
        InvoiceNumber,
        CustomerName,
        InvoiceDate,
        DueDate,
        TotalAmount,
        AmountPaid,
        Balance,
        DATEDIFF(DAY, DueDate, GETDATE()) AS DaysOverdue
    FROM Invoices
    WHERE Status <> 'Paid' AND Status <> 'Void'
    ORDER BY DueDate ASC;
    
    -- Recent User Activity
    SELECT TOP 10
        a.LogID,
        a.ActionDate,
        a.UserID,
        a.Username,
        a.ActionType,
        a.EntityType,
        a.EntityID
    FROM AuditLogs a
    ORDER BY a.ActionDate DESC;
END;
GO

-- =================================================
-- Create Triggers
-- =================================================

-- Trigger to record audit log for user changes
CREATE TRIGGER trg_Users_Audit
ON Users
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ActionType NVARCHAR(50);
    
    -- Determine the action type
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @ActionType = 'Update';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @ActionType = 'Insert';
    ELSE
        SET @ActionType = 'Delete';
    
    -- For inserts
    IF @ActionType = 'Insert'
    BEGIN
        INSERT INTO AuditLogs (
            UserID,
            Username,
            ActionType,
            EntityType,
            EntityID,
            NewValues,
            ActionDate
        )
        SELECT
            i.CreatedBy,
            (SELECT Username FROM Users WHERE UserID = i.CreatedBy),
            'Insert',
            'User',
            i.UserID,
            (SELECT * FROM inserted WHERE UserID = i.UserID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
            GETDATE()
        FROM inserted i;
    END
    
    -- For updates
    IF @ActionType = 'Update'
    BEGIN
        INSERT INTO AuditLogs (
            UserID,
            Username,
            ActionType,
            EntityType,
            EntityID,
            OldValues,
            NewValues,
            ActionDate
        )
        SELECT
            i.ModifiedBy,
            (SELECT Username FROM Users WHERE UserID = i.ModifiedBy),
            'Update',
            'User',
            i.UserID,
            (SELECT * FROM deleted WHERE UserID = i.UserID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
            (SELECT * FROM inserted WHERE UserID = i.UserID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
            GETDATE()
        FROM inserted i
        JOIN deleted d ON i.UserID = d.UserID;
    END
    
    -- For deletes
    IF @ActionType = 'Delete'
    BEGIN
        -- This would need a session context value to capture who deleted the user
        -- For now, using a default system ID
        INSERT INTO AuditLogs (
            UserID,
            ActionType,
            EntityType,
            EntityID,
            OldValues,
            ActionDate
        )
        SELECT
            1, -- System User ID
            'Delete',
            'User',
            d.UserID,
            (SELECT * FROM deleted WHERE UserID = d.UserID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
            GETDATE()
        FROM deleted d;
    END
END;
GO

-- Trigger to update product inventory on sales order changes
CREATE TRIGGER trg_SalesOrderDetails_UpdateInventory
ON SalesOrderDetails
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @UserID INT = 1; -- Default system user, ideally would get from session
    
    -- Get order status for determining whether to adjust inventory
    DECLARE @OrderID INT;
    DECLARE @OrderStatus NVARCHAR(20);
    
    -- For inserts and updates, get the OrderID from inserted
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        SELECT TOP 1 @OrderID = OrderID FROM inserted;
    END
    -- For deletes, get the OrderID from deleted
    ELSE
    BEGIN
        SELECT TOP 1 @OrderID = OrderID FROM deleted;
    END
    
    SELECT @OrderStatus = Status FROM SalesOrders WHERE OrderID = @OrderID;
    
    -- Only perform inventory adjustment if order status requires it
    -- (assuming 'Confirmed' status means order confirmed but not shipped/delivered yet)
    IF @OrderStatus IN ('Confirmed', 'Shipped', 'Delivered', 'Invoiced')
    BEGIN
        -- For inserts (new order items)
        IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted)
        BEGIN
            -- Reserve inventory for new order items
            UPDATE pi
            SET pi.QuantityReserved = pi.QuantityReserved + i.Quantity,
                pi.LastUpdatedDate = GETDATE(),
                pi.LastUpdatedBy = @UserID
            FROM ProductInventory pi
            JOIN inserted i ON pi.ProductID = i.ProductID
            JOIN Products p ON pi.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
            
            -- Record the transaction
            INSERT INTO InventoryTransactions (
                ProductID, 
                LocationID, 
                TransactionType, 
                Quantity, 
                Notes, 
                CreatedBy, 
                ReferenceNumber,
                ReferenceType
            )
            SELECT 
                i.ProductID, 
                1, -- Default location
                'Reserved', 
                i.Quantity, 
                'Reserved for order ' + CONVERT(NVARCHAR(20), i.OrderID),
                @UserID,
                so.OrderNumber,
                'SalesOrder'
            FROM inserted i
            JOIN SalesOrders so ON i.OrderID = so.OrderID
            JOIN Products p ON i.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
        END
        
        -- For updates (modified order items)
        IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        BEGIN
            -- Adjust reservation based on quantity difference
            UPDATE pi
            SET pi.QuantityReserved = pi.QuantityReserved + (i.Quantity - d.Quantity),
                pi.LastUpdatedDate = GETDATE(),
                pi.LastUpdatedBy = @UserID
            FROM ProductInventory pi
            JOIN inserted i ON pi.ProductID = i.ProductID
            JOIN deleted d ON i.OrderDetailID = d.OrderDetailID
            JOIN Products p ON pi.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
            
            -- Record the transaction if there's a change in quantity
            INSERT INTO InventoryTransactions (
                ProductID, 
                LocationID, 
                TransactionType, 
                Quantity, 
                Notes, 
                CreatedBy, 
                ReferenceNumber,
                ReferenceType
            )
            SELECT 
                i.ProductID, 
                1, -- Default location
                'Adjustment', 
                (i.Quantity - d.Quantity), 
                'Order item adjusted for order ' + CONVERT(NVARCHAR(20), i.OrderID),
                @UserID,
                so.OrderNumber,
                'SalesOrder'
            FROM inserted i
            JOIN deleted d ON i.OrderDetailID = d.OrderDetailID
            JOIN SalesOrders so ON i.OrderID = so.OrderID
            JOIN Products p ON i.ProductID = p.ProductID
            WHERE p.ManageStock = 1 AND i.Quantity <> d.Quantity;
        END
        
        -- For deletes (removed order items)
        IF NOT EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        BEGIN
            -- Unreserve inventory for removed order items
            UPDATE pi
            SET pi.QuantityReserved = pi.QuantityReserved - d.Quantity,
                pi.LastUpdatedDate = GETDATE(),
                pi.LastUpdatedBy = @UserID
            FROM ProductInventory pi
            JOIN deleted d ON pi.ProductID = d.ProductID
            JOIN Products p ON pi.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
            
            -- Record the transaction
            INSERT INTO InventoryTransactions (
                ProductID, 
                LocationID, 
                TransactionType, 
                Quantity, 
                Notes, 
                CreatedBy, 
                ReferenceNumber,
                ReferenceType
            )
            SELECT 
                d.ProductID, 
                1, -- Default location
                'Unreserved', 
                -d.Quantity, 
                'Removed from order ' + CONVERT(NVARCHAR(20), d.OrderID),
                @UserID,
                so.OrderNumber,
                'SalesOrder'
            FROM deleted d
            JOIN SalesOrders so ON d.OrderID = so.OrderID
            JOIN Products p ON d.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
        END
    END
END;
GO

-- Trigger to update stock when order status changes
CREATE TRIGGER trg_SalesOrders_StatusChange
ON SalesOrders
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Only proceed if the status has changed
    IF UPDATE(Status)
    BEGIN
        DECLARE @UserID INT = 1; -- Default system user, ideally would get from session
        
        -- Handle status change to Shipped or Delivered (inventory deduction)
        IF EXISTS (
            SELECT 1 FROM inserted i 
            JOIN deleted d ON i.OrderID = d.OrderID
            WHERE d.Status NOT IN ('Shipped', 'Delivered') 
            AND i.Status IN ('Shipped', 'Delivered')
        )
        BEGIN
            -- Move from reserved to actual inventory reduction
            UPDATE pi
            SET pi.QuantityOnHand = pi.QuantityOnHand - sod.Quantity,
                pi.QuantityReserved = pi.QuantityReserved - sod.Quantity,
                pi.LastUpdatedDate = GETDATE(),
                pi.LastUpdatedBy = @UserID
            FROM ProductInventory pi
            JOIN SalesOrderDetails sod ON pi.ProductID = sod.ProductID
            JOIN inserted i ON sod.OrderID = i.OrderID
            JOIN Products p ON pi.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
            
            -- Record the transaction
            INSERT INTO InventoryTransactions (
                ProductID, 
                LocationID, 
                TransactionType, 
                Quantity, 
                Notes, 
                CreatedBy, 
                ReferenceNumber,
                ReferenceType
            )
            SELECT 
                sod.ProductID, 
                1, -- Default location
                'Sale', 
                -sod.Quantity, 
                'Shipped for order ' + i.OrderNumber,
                @UserID,
                i.OrderNumber,
                'SalesOrder'
            FROM SalesOrderDetails sod
            JOIN inserted i ON sod.OrderID = i.OrderID
            JOIN Products p ON sod.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
        END
        
        -- Handle status change to Cancelled (unreserve inventory)
        IF EXISTS (
            SELECT 1 FROM inserted i 
            JOIN deleted d ON i.OrderID = d.OrderID
            WHERE d.Status <> 'Cancelled' 
            AND i.Status = 'Cancelled'
        )
        BEGIN
            -- Unreserve inventory
            UPDATE pi
            SET pi.QuantityReserved = pi.QuantityReserved - sod.Quantity,
                pi.LastUpdatedDate = GETDATE(),
                pi.LastUpdatedBy = @UserID
            FROM ProductInventory pi
            JOIN SalesOrderDetails sod ON pi.ProductID = sod.ProductID
            JOIN inserted i ON sod.OrderID = i.OrderID
            JOIN Products p ON pi.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
            
            -- Record the transaction
            INSERT INTO InventoryTransactions (
                ProductID, 
                LocationID, 
                TransactionType, 
                Quantity, 
                Notes, 
                CreatedBy, 
                ReferenceNumber,
                ReferenceType
            )
            SELECT 
                sod.ProductID, 
                1, -- Default location
                'Cancelled', 
                sod.Quantity, 
                'Order cancelled: ' + i.OrderNumber,
                @UserID,
                i.OrderNumber,
                'SalesOrder'
            FROM SalesOrderDetails sod
            JOIN inserted i ON sod.OrderID = i.OrderID
            JOIN Products p ON sod.ProductID = p.ProductID
            WHERE p.ManageStock = 1;
        END
    END
END;
GO

-- =================================================
-- Create Functions
-- =================================================

-- Function to get the current balance for a customer
CREATE FUNCTION fn_GetCustomerBalance (@CustomerID INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Balance DECIMAL(18,2);
    
    SELECT @Balance = COALESCE(SUM(Balance), 0)
    FROM Invoices
    WHERE CustomerID = @CustomerID
      AND Status <> 'Void';
      
    RETURN @Balance;
END;
GO

-- Function to get the product's current inventory status
CREATE FUNCTION fn_GetProductInventoryStatus (@ProductID INT)
RETURNS TABLE
AS
RETURN (
    SELECT
        p.ProductID,
        p.SKU,
        p.ProductName,
        pi.QuantityOnHand,
        pi.QuantityReserved,
        pi.QuantityAvailable,
        p.ReorderLevel,
        p.TargetStockLevel,
        CASE 
            WHEN pi.QuantityAvailable <= 0 THEN 'Out of Stock'
            WHEN pi.QuantityAvailable <= p.ReorderLevel THEN 'Low Stock'
            WHEN pi.QuantityAvailable <= p.TargetStockLevel THEN 'Medium Stock'
            ELSE 'Good Stock'
        END AS StockStatus
    FROM Products p
    LEFT JOIN ProductInventory pi ON p.ProductID = pi.ProductID
    WHERE p.ProductID = @ProductID
);
GO

-- Function to calculate aging for outstanding invoices
CREATE FUNCTION fn_CalculateInvoiceAging (@AsOfDate DATE = NULL)
RETURNS TABLE
AS
RETURN (
    WITH AgingData AS (
        SELECT
            i.InvoiceID,
            i.InvoiceNumber,
            i.CustomerID,
            c.CustomerName,
            i.InvoiceDate,
            i.DueDate,
            i.TotalAmount,
            i.AmountPaid,
            i.Balance,
            DATEDIFF(DAY, i.DueDate, ISNULL(@AsOfDate, GETDATE())) AS DaysOverdue,
            CASE
                WHEN i.DueDate >= ISNULL(@AsOfDate, GETDATE()) THEN 'Current'
                WHEN DATEDIFF(DAY, i.DueDate, ISNULL(@AsOfDate, GETDATE())) BETWEEN 1 AND 30 THEN '1-30 Days'
                WHEN DATEDIFF(DAY, i.DueDate, ISNULL(@AsOfDate, GETDATE())) BETWEEN 31 AND 60 THEN '31-60 Days'
                WHEN DATEDIFF(DAY, i.DueDate, ISNULL(@AsOfDate, GETDATE())) BETWEEN 61 AND 90 THEN '61-90 Days'
                ELSE 'Over 90 Days'
            END AS AgingCategory
        FROM Invoices i
        JOIN Customers c ON i.CustomerID = c.CustomerID
        WHERE i.Balance > 0 AND i.Status <> 'Void'
    )
    SELECT
        AgingCategory,
        COUNT(InvoiceID) AS InvoiceCount,
        SUM(Balance) AS TotalAmount,
        CustomerID,
        CustomerName,
        InvoiceID,
        InvoiceNumber,
        InvoiceDate,
        DueDate,
        Balance,
        DaysOverdue
    FROM AgingData
    GROUP BY
        AgingCategory,
        CustomerID,
        CustomerName,
        InvoiceID,
        InvoiceNumber,
        InvoiceDate,
        DueDate,
        Balance,
        DaysOverdue
);
GO

-- =================================================
-- Database Creation Complete
-- =================================================

PRINT 'AdminDomain database schema created successfully on ' + CONVERT(VARCHAR, GETDATE(), 121) + ' by ' + CURRENT_USER;
PRINT 'Last modified by: LoudlyDawn2108 at 2025-03-07 05:02:43';
GO