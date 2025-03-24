-- Force drop the database if it exists
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'FantasticStock1')
BEGIN
    ALTER DATABASE FantasticStock1 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE FantasticStock1;
END
GO

CREATE DATABASE FantasticStock1;
GO
USE FantasticStock1;
GO


-- =============================================
-- USER MANAGEMENT TABLES
-- =============================================

-- Create Roles table
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1, 1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);


-- Create Users table (combining both versions)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1, 1),
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


-- Create UserScheduleRestrictions table
CREATE TABLE UserScheduleRestrictions (
    RestrictionID INT PRIMARY KEY IDENTITY(1, 1),
    UserID INT NOT NULL,
    DayOfWeek INT NOT NULL, -- 1-7 (Sunday-Saturday)
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    CONSTRAINT FK_UserSchedule_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


-- Create UserActivityLog table
CREATE TABLE UserActivityLog (
    LogID INT PRIMARY KEY IDENTITY(1, 1),
    UserID INT NOT NULL,
    ActivityType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    IPAddress NVARCHAR(50),
    Timestamp DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ActivityLog_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- =============================================
-- CUSTOMER MANAGEMENT TABLES
-- =============================================

-- Create CustomerType table
CREATE TABLE CustomerType (
    CustomerTypeID INT PRIMARY KEY IDENTITY(1,1),
    CustomerTypeName NVARCHAR(50) NOT NULL,
    DiscountPercentage DECIMAL(5,2) DEFAULT 0 NULL,
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL
);



-- Create Customer table
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100) NULL,
    ContactTitle NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    City NVARCHAR(50) NULL,
    State NVARCHAR(50) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(50) NULL,
    LoyaltyPoints INT DEFAULT 0 NULL,
    IsActive BIT DEFAULT 1 NULL,
    CustomerTypeID INT NULL,
    CustomerTypeName NVARCHAR(50) NULL,
    CreditLimit DECIMAL(18,2) NULL,
    Balance DECIMAL(18,2) NULL,
    PaymentTerms NVARCHAR(50) NULL,
    TaxID NVARCHAR(50) NULL,
    Notes NTEXT NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    CONSTRAINT FK_Customer_CustomerType FOREIGN KEY (CustomerTypeID) REFERENCES CustomerType(CustomerTypeID)
);

-- =============================================
-- INVENTORY MANAGEMENT TABLES
-- =============================================

-- Create Category table
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedBy INT NULL,
    CONSTRAINT FK_Category_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT FK_Category_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

-- Create Brand table
CREATE TABLE Brand (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    BrandName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedBy INT NULL,
    CONSTRAINT FK_Brand_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT FK_Brand_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID)
);

-- Create Supplier table
CREATE TABLE Supplier (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(200) NOT NULL,
    SupplierName NVARCHAR(100) NULL,
    ContactName NVARCHAR(100) NULL,
    ContactTitle NVARCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    City NVARCHAR(100) NULL,
    State NVARCHAR(100) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(100) NULL,
    Phone NVARCHAR(30) NULL,
    Email NVARCHAR(255) NULL,
    Website NVARCHAR(255) NULL,
    PaymentTerms NVARCHAR(255) NULL,
    IsActive BIT DEFAULT 1 NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL
);

-- Create Product table
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    SKU NVARCHAR(50) NOT NULL,
    Barcode NVARCHAR(50) NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CategoryID INT NULL,
    CategoryName NVARCHAR(50) NULL,
    BrandID INT NULL,
    BrandName NVARCHAR(50) NULL,
    SupplierID INT NULL,
    SupplierName NVARCHAR(50) NULL,
    CostPrice DECIMAL(18,2) NOT NULL,
    SellingPrice DECIMAL(18,2) NOT NULL,
    WholesalePrice DECIMAL(18,2) NULL,
    ReorderLevel INT NULL,
    TargetStockLevel DECIMAL(18,2) NULL,
    UnitOfMeasure NVARCHAR(50) NULL,
    ManageStock BIT DEFAULT 1 NULL,
    AllowFractionalQuantity BIT DEFAULT 0 NULL,
    ProductImage VARBINARY(MAX) NULL,
    StockQuantity INT DEFAULT 0 NULL,
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedBy INT NULL,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    CONSTRAINT FK_Product_Brand FOREIGN KEY (BrandID) REFERENCES Brand(BrandID),
    CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID),
    CONSTRAINT FK_Product_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT FK_Product_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID),
    CONSTRAINT UQ_Product_SKU UNIQUE (SKU)
);

-- =============================================
-- SALES MANAGEMENT TABLES
-- =============================================

-- Create Sale table
CREATE TABLE Sale (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    SaleNumber NVARCHAR(20) NOT NULL,
    CustomerID INT NULL,
    SaleDate DATETIME DEFAULT GETDATE() NULL,
    SubTotal DECIMAL(18,2) NOT NULL,
    TaxAmount DECIMAL(18,2) NOT NULL,
    DiscountAmount DECIMAL(18,2) DEFAULT 0 NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    PaymentMethod NVARCHAR(50) NULL,
    TenderedAmount DECIMAL(18,2) NULL,
    ChangeAmount DECIMAL(18,2) NULL,
    Notes NTEXT NULL,
    PrintReceipt BIT DEFAULT 0 NULL,
    EmailReceipt BIT DEFAULT 0 NULL,
    IsCompleted BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    CONSTRAINT FK_Sale_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_Sale_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

-- Create SaleItem table
CREATE TABLE SaleItem (
    SaleItemID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Discount DECIMAL(18,2) DEFAULT 0 NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_SaleItem_Sale FOREIGN KEY (SaleID) REFERENCES Sale(SaleID),
    CONSTRAINT FK_SaleItem_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE ExpenseCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);


-- =============================================
-- EXPENSE MANAGEMENT INTEGRATION
-- =============================================


CREATE TABLE Expenses (
    ExpenseID INT PRIMARY KEY IDENTITY(1,1),
    ExpenseNumber NVARCHAR(20) NOT NULL,
    ExpenseDate DATETIME NOT NULL,
    SupplierID INT NULL,
    CategoryID INT NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    ReferenceNumber NVARCHAR(50),
    Amount DECIMAL(18, 2) NOT NULL,
    TaxAmount DECIMAL(18, 2) NOT NULL DEFAULT 0,
    Notes NVARCHAR(MAX),
    IsTaxDeductible BIT NOT NULL DEFAULT 0,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    
    -- Foreign Key constraints - linking to existing tables
    CONSTRAINT FK_Expenses_Supplier FOREIGN KEY (SupplierID) 
        REFERENCES Supplier(SupplierID),
    CONSTRAINT FK_Expenses_Categories FOREIGN KEY (CategoryID) 
        REFERENCES ExpenseCategories(CategoryID),
    CONSTRAINT FK_Expenses_Users FOREIGN KEY (CreatedBy) 
        REFERENCES Users(UserID)
);

-- Create index on common search fields
CREATE INDEX IX_Expenses_ExpenseDate ON Expenses(ExpenseDate);
CREATE INDEX IX_Expenses_SupplierID ON Expenses(SupplierID);
CREATE INDEX IX_Expenses_CategoryID ON Expenses(CategoryID);
CREATE INDEX IX_Expenses_ExpenseNumber ON Expenses(ExpenseNumber);

-- Create a view for Expenses (updated to use existing tables)
GO
CREATE VIEW vw_Expenses AS
SELECT 
    e.ExpenseID,
    e.ExpenseNumber,
    e.ExpenseDate,
    e.SupplierID,
    s.CompanyName AS SupplierName, -- Updated to match field from Supplier table
    e.CategoryID,
    c.CategoryName,
    e.PaymentMethod,
    e.ReferenceNumber,
    e.Amount,
    e.TaxAmount,
    (e.Amount + e.TaxAmount) AS TotalAmount,
    e.Notes,
    e.IsTaxDeductible,
    e.CreatedBy,
    u.Username AS CreatedByName, -- Updated to match field from Users table
    e.CreatedDate,
    FORMAT(e.ExpenseDate, 'yyyy-MM-dd HH:mm:ss') AS FormattedDate
FROM 
    Expenses e
LEFT JOIN 
    Supplier s ON e.SupplierID = s.SupplierID
LEFT JOIN 
    ExpenseCategories c ON e.CategoryID = c.CategoryID
LEFT JOIN 
    Users u ON e.CreatedBy = u.UserID;
GO

-- =============================================
-- PAYMENT MANAGEMENT INTEGRATION
-- =============================================

-- Create Invoices table (using existing Customer table)
CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceNumber NVARCHAR(20) NOT NULL,
    CustomerID INT NOT NULL,
    InvoiceDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    PaidAmount DECIMAL(18, 2) DEFAULT 0,
    Balance AS (Amount - PaidAmount),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Open', -- Open, Paid, Partial, Overdue, Cancelled
    Notes NVARCHAR(MAX),
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Invoices_Customers FOREIGN KEY (CustomerID)
        REFERENCES Customer(CustomerID), -- Updated to reference Customer instead of Customers
    CONSTRAINT FK_Invoices_Users FOREIGN KEY (CreatedBy)
        REFERENCES Users(UserID)
);

-- Create Payments table referencing existing tables
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentNumber NVARCHAR(20) NOT NULL,
    PaymentDate DATETIME NOT NULL,
    CustomerID INT NOT NULL,
    InvoiceID INT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL, -- Cash, Check, Credit Card, Bank Transfer
    ReferenceNumber NVARCHAR(50), -- Check number, transaction ID, etc.
    Amount DECIMAL(18, 2) NOT NULL,
    Notes NVARCHAR(MAX),
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    
    -- Foreign Key constraints
    CONSTRAINT FK_Payments_Customer FOREIGN KEY (CustomerID) 
        REFERENCES Customer(CustomerID), -- Updated to reference Customer instead of Customers
    CONSTRAINT FK_Payments_Invoices FOREIGN KEY (InvoiceID) 
        REFERENCES Invoices(InvoiceID),
    CONSTRAINT FK_Payments_Users FOREIGN KEY (CreatedBy) 
        REFERENCES Users(UserID)
);

-- Create index on common search fields
CREATE INDEX IX_Payments_PaymentDate ON Payments(PaymentDate);
CREATE INDEX IX_Payments_CustomerID ON Payments(CustomerID);
CREATE INDEX IX_Payments_InvoiceID ON Payments(InvoiceID);
CREATE INDEX IX_Payments_PaymentNumber ON Payments(PaymentNumber);

-- Create a view for Payments
GO
CREATE VIEW vw_Payments AS
SELECT 
    p.PaymentID,
    p.PaymentNumber,
    p.PaymentDate,
    p.CustomerID,
    c.CustomerName,
    p.InvoiceID,
    i.InvoiceNumber,
    p.PaymentMethod,
    p.ReferenceNumber,
    p.Amount,
    p.Notes,
    p.CreatedBy,
    u.Username AS CreatedByName, -- Updated to match field from Users table
    p.CreatedDate,
    FORMAT(p.PaymentDate, 'yyyy-MM-dd HH:mm:ss') AS FormattedDate
FROM 
    Payments p
LEFT JOIN 
    Customer c ON p.CustomerID = c.CustomerID -- Updated to reference Customer instead of Customers
LEFT JOIN 
    Invoices i ON p.InvoiceID = i.InvoiceID
LEFT JOIN 
    Users u ON p.CreatedBy = u.UserID;

-- Create the ApplyPaymentToInvoice stored procedure
GO
CREATE PROCEDURE ApplyPaymentToInvoice
    @PaymentID INT,
    @InvoiceID INT,
    @Amount DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    DECLARE @InvoiceTotal DECIMAL(18, 2);
    DECLARE @CurrentPaid DECIMAL(18, 2);
    
    -- Get invoice amount and currently paid amount
    SELECT @InvoiceTotal = Amount, @CurrentPaid = PaidAmount
    FROM Invoices
    WHERE InvoiceID = @InvoiceID;
    
    -- Validate the amount doesn't exceed the invoice balance
    IF (@Amount > (@InvoiceTotal - @CurrentPaid))
    BEGIN
        ROLLBACK;
        RAISERROR('Payment amount exceeds invoice balance', 16, 1);
        RETURN;
    END
    
    -- Update the payment to link it to the invoice
    UPDATE Payments
    SET InvoiceID = @InvoiceID
    WHERE PaymentID = @PaymentID;
    
    -- Update the invoice paid amount
    UPDATE Invoices
    SET PaidAmount = PaidAmount + @Amount,
        Status = CASE 
                    WHEN PaidAmount + @Amount >= Amount THEN 'Paid' 
                    ELSE 'Partial' 
                 END
    WHERE InvoiceID = @InvoiceID;
    
    COMMIT;
END
GO


-- =============================================
-- AUDIT AND MONITORING TABLES
-- =============================================

-- Create ErrorLog table
CREATE TABLE ErrorLog (
    ErrorID INT PRIMARY KEY IDENTITY(1, 1),
    ErrorModule NVARCHAR(50) NOT NULL,
    ErrorMessage NVARCHAR(MAX) NOT NULL,
    StackTrace NVARCHAR(MAX),
    SeverityLevel INT NOT NULL, -- 1=Info, 2=Warning, 3=Error, 4=Critical
    UserID INT,
    IPAddress NVARCHAR(50),
    Timestamp DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ErrorLog_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create AuditLog table
CREATE TABLE AuditLog (
    AuditID INT PRIMARY KEY IDENTITY(1, 1),
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

-- Create indexes for better performance
CREATE INDEX IX_Product_CategoryID ON Product(CategoryID);
CREATE INDEX IX_Product_BrandID ON Product(BrandID);
CREATE INDEX IX_Product_SupplierID ON Product(SupplierID);
CREATE INDEX IX_Product_ProductName ON Product(ProductName);
CREATE INDEX IX_Supplier_CompanyName ON Supplier(CompanyName);
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_RoleID ON Users(RoleID);
CREATE INDEX IX_Sale_CustomerID ON Sale(CustomerID);
CREATE INDEX IX_Sale_CreatedDate ON Sale(CreatedDate);
CREATE INDEX IX_Customer_CustomerTypeID ON Customer(CustomerTypeID);

-- =============================================
-- INSERT SAMPLE DATA
-- =============================================

-- Insert default roles
INSERT INTO Roles (RoleName, Description)
VALUES 
    ('Admin', 'System Administrator with full access'),
    ('Manager', 'Manager with elevated privileges'),
    ('Sales', 'Sales department user'),
    ('Inventory', 'Inventory management user'),
    ('Finance', 'Finance department user');

-- Insert admin user (password hashes are for demonstration purposes)
INSERT INTO Users (
    Username,
    PasswordHash,
    Salt,
    DisplayName,
    Email,
    Phone,
    RoleID,
    Status,
    TwoFactorEnabled
)
VALUES 
    ('TungCorn', 'a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6', 'salt123', 'Tùng Corn', 'tungcorn@email.com', '0912345678', 1, 'Active', 0),
    ('admin', 'h1a2s3h4p5a6s7s8w9o0r1d2', 'salt456', 'System Admin', 'admin@fantasticstock.com', '0923456789', 1, 'Active', 1),
    ('manager', 'q1w2e3r4t5y6u7i8o9p0', 'salt789', 'Store Manager', 'manager@fantasticstock.com', '0934567890', 2, 'Active', 0);

-- Insert sample CustomerType data
INSERT INTO CustomerType (CustomerTypeName, DiscountPercentage, IsActive)
VALUES 
    ('Regular', 0, 1),
    ('Gold', 5.00, 1),
    ('Premium', 10.00, 1);

-- Insert sample Customer data
INSERT INTO Customer (CustomerName, ContactName, ContactTitle, Phone, Email, Address, City, Country, PostalCode, CustomerTypeID, LoyaltyPoints, IsActive)
VALUES 
    ('Công ty TNHH ABC', 'Nguyễn Văn A', 'Giám đốc', '0912345678', 'info@abc.com', '123 Đường Lê Lợi', 'TP Hồ Chí Minh', 'Việt Nam', '70000', 1, 100, 1),
    ('Doanh nghiệp XYZ', 'Trần Thị B', 'Quản lý', '0923456789', 'xyz@email.com', '456 Đường Nguyễn Huệ', 'Hà Nội', 'Việt Nam', '10000', 2, 250, 1),
    ('Cá nhân Lê Văn C', 'Lê Văn C', NULL, '0934567890', 'levanc@email.com', '789 Đường 3/2', 'Đà Nẵng', 'Việt Nam', '50000', 3, 50, 1);

-- Insert sample Category data
INSERT INTO Category (CategoryName, Description, IsActive, CreatedBy)
VALUES 
    ('Điện thoại', 'Điện thoại di động và phụ kiện', 1, 1),
    ('Máy tính', 'Máy tính xách tay, máy tính để bàn và linh kiện', 1, 1),
    ('Đồ gia dụng', 'Thiết bị gia dụng và nhà bếp', 1, 1);

-- Insert sample Brand data
INSERT INTO Brand (BrandName, Description, IsActive, CreatedBy)
VALUES 
    ('Samsung', 'Tập đoàn điện tử hàng đầu Hàn Quốc', 1, 1),
    ('Apple', 'Công ty công nghệ đa quốc gia của Mỹ', 1, 1),
    ('LG', 'Tập đoàn điện tử và thiết bị gia dụng', 1, 1);

-- Insert sample Supplier data
INSERT INTO Supplier (CompanyName, SupplierName, ContactName, ContactTitle, Address, City, Country, PostalCode, Phone, Email, IsActive)
VALUES 
    ('Công ty Phân phối ABC', 'ABC Distribution', 'Nguyễn Văn X', 'Giám đốc kinh doanh', '123 Đường Lê Lai', 'TP Hồ Chí Minh', 'Việt Nam', '70000', '0912345678', 'sales@abcdist.com', 1),
    ('Công ty Nhập khẩu XYZ', 'XYZ Import', 'Trần Thị Y', 'Quản lý chuỗi cung ứng', '456 Đường Hai Bà Trưng', 'Hà Nội', 'Việt Nam', '10000', '0923456789', 'info@xyzimport.com', 1),
    ('Đại lý Thiết bị DEF', 'DEF Equipment', 'Lê Văn Z', 'Đại diện bán hàng', '789 Đường Nguyễn Hữu Thọ', 'Đà Nẵng', 'Việt Nam', '50000', '0934567890', 'contact@defequip.com', 1);

-- Insert sample Product data
INSERT INTO Product (SKU, Barcode, ProductName, Description, CategoryID, BrandID, SupplierID, CostPrice, SellingPrice, WholesalePrice, ReorderLevel, StockQuantity, IsActive, CreatedBy)
VALUES 
    ('PHONE-SAM-S22', '8801234567890', 'Samsung Galaxy S22', 'Điện thoại thông minh Samsung Galaxy S22', 1, 1, 1, 15000000.00, 18000000.00, 17000000.00, 10, 25, 1, 1),
    ('LAPTOP-APP-MB14', '8901234567890', 'Apple MacBook Pro 14"', 'Máy tính xách tay Apple MacBook Pro 14 inch', 2, 2, 2, 35000000.00, 40000000.00, 38000000.00, 5, 12, 1, 1),
    ('TV-LG-OLED65', '9012345678901', 'LG OLED TV 65"', 'Tivi LG OLED 65 inch 4K', 3, 3, 3, 25000000.00, 30000000.00, 28000000.00, 3, 8, 1, 1);

-- Insert sample Sale data (using the current date)
DECLARE @CurrentDate DATETIME = GETDATE();

INSERT INTO Sale (SaleNumber, CustomerID, SaleDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaymentMethod, TenderedAmount, ChangeAmount, Notes, PrintReceipt, EmailReceipt, IsCompleted, CreatedBy)
VALUES 
    ('SALE-2025-0001', 1, @CurrentDate, 18000000.00, 1800000.00, 0.00, 19800000.00, 'Tiền mặt', 20000000.00, 200000.00, 'Khách mua trực tiếp tại cửa hàng', 1, 0, 1, 1),
    ('SALE-2025-0002', 2, @CurrentDate, 40000000.00, 4000000.00, 2000000.00, 42000000.00, 'Thẻ tín dụng', 42000000.00, 0.00, 'Khách hàng doanh nghiệp', 1, 1, 1, 1),
    ('SALE-2025-0003', 3, DATEADD(DAY, -1, @CurrentDate), 30000000.00, 3000000.00, 1500000.00, 31500000.00, 'Chuyển khoản', 31500000.00, 0.00, NULL, 0, 1, 1, 1);

-- Insert sample SaleItem data
INSERT INTO SaleItem (SaleID, ProductID, Quantity, UnitPrice, Discount, TotalPrice)
VALUES 
    (1, 1, 1, 18000000.00, 0.00, 18000000.00),
    (2, 2, 1, 40000000.00, 2000000.00, 38000000.00),
    (3, 3, 1, 30000000.00, 1500000.00, 28500000.00);

INSERT INTO ExpenseCategories (CategoryName, Description, IsActive) VALUES 
('Office Supplies', 'General office supplies and stationery', 1),
('Rent', 'Office and facility rent expenses', 1),
('Utilities', 'Electricity, water, internet, and other utilities', 1),
('Travel', 'Business travel expenses including airfare, hotel, and meals', 1),
('Equipment', 'Office equipment, computers, and furniture', 1),
('Marketing', 'Advertising, promotion, and marketing expenses', 1),
('Professional Services', 'Legal, accounting, and consulting services', 1),
('Insurance', 'Business insurance premiums', 1),
('Software', 'Software licenses and subscriptions', 1),
('Maintenance', 'Repairs and maintenance of facilities and equipment', 1);

INSERT INTO Expenses (
    ExpenseNumber, 
    ExpenseDate, 
    SupplierID, 
    CategoryID, 
    PaymentMethod, 
    ReferenceNumber, 
    Amount, 
    TaxAmount, 
    Notes, 
    IsTaxDeductible, 
    CreatedBy
) VALUES 
-- March 2025
('EXP-2025-001', '2025-03-01', 1, 1, 'Credit Card', 'CC-45678', 2500.00, 250.00, 'Monthly office supplies restock', 1, 1),
('EXP-2025-002', '2025-03-02', 2, 2, 'Bank Transfer', 'BT-98765', 15000.00, 0.00, 'Monthly rent payment for main office', 1, 2),
('EXP-2025-003', '2025-03-05', 3, 3, 'Credit Card', 'CC-56789', 3500.00, 350.00, 'Utility bills for February 2025', 1, 3),
('EXP-2025-004', '2025-03-10', 1, 5, 'Check', 'CK-12345', 8900.00, 890.00, 'New computers for accounting team', 1, 1),
('EXP-2025-005', '2025-03-12', 3, 6, 'Credit Card', 'CC-67890', 6500.00, 650.00, 'Digital marketing campaign', 1, 2),
('EXP-2025-006', '2025-03-15', 2, 8, 'Bank Transfer', 'BT-87654', 4200.00, 0.00, 'Business liability insurance premium', 1, 1),
('EXP-2025-007', '2025-03-18', 3, 7, 'Bank Transfer', 'BT-76543', 7500.00, 750.00, 'Legal consultation services', 1, 3),
('EXP-2025-008', '2025-03-20', 1, 9, 'Credit Card', 'CC-78901', 3200.00, 320.00, 'Annual software licenses renewal', 1, 2),
('EXP-2025-009', '2025-03-22', 2, 10, 'Check', 'CK-23456', 1800.00, 180.00, 'Office equipment repairs', 1, 1),
('EXP-2025-010', '2025-03-25', 3, 4, 'Credit Card', 'CC-89012', 5500.00, 550.00, 'Business trip to supplier conference', 1, 3),

('EXP-2025-011', '2025-02-02', 1, 1, 'Credit Card', 'CC-34567', 1800.00, 180.00, 'Office supplies', 1, 2),
('EXP-2025-012', '2025-02-03', 2, 2, 'Bank Transfer', 'BT-87654', 15000.00, 0.00, 'Monthly rent payment', 1, 1),
('EXP-2025-013', '2025-02-07', 3, 3, 'Credit Card', 'CC-45678', 3200.00, 320.00, 'Utility bills for January 2025', 1, 3),
('EXP-2025-014', '2025-02-12', 1, 6, 'Check', 'CK-34567', 4500.00, 450.00, 'Print advertising materials', 1, 2),
('EXP-2025-015', '2025-02-15', 2, 7, 'Bank Transfer', 'BT-76543', 3800.00, 380.00, 'Accounting services', 1, 1),
('EXP-2025-016', '2025-02-18', 3, 9, 'Credit Card', 'CC-56789', 2500.00, 250.00, 'Cloud services subscription', 1, 3),
('EXP-2025-017', '2025-02-22', 1, 4, 'Credit Card', 'CC-67890', 3500.00, 350.00, 'Client meeting travel expenses', 1, 1),
('EXP-2025-018', '2025-02-25', 2, 10, 'Check', 'CK-45678', 1200.00, 120.00, 'HVAC system maintenance', 1, 2),
('EXP-2025-019', '2025-02-27', 3, 5, 'Bank Transfer', 'BT-65432', 6800.00, 680.00, 'Office furniture for new hires', 1, 3),
('EXP-2025-020', '2025-02-28', 1, 8, 'Bank Transfer', 'BT-54321', 2200.00, 0.00, 'Employee health insurance contribution', 1, 1);

INSERT INTO Invoices (
    InvoiceNumber, 
    CustomerID, 
    InvoiceDate, 
    DueDate, 
    Amount, 
    Status, 
    Notes, 
    CreatedBy
) VALUES
-- March 2025
('INV-2025-001', 1, '2025-03-01', '2025-03-31', 12500.00, 'Open', 'Monthly product supply', 1),
('INV-2025-002', 2, '2025-03-03', '2025-04-02', 8750.50, 'Open', 'Custom order - Widget Industries', 2),
('INV-2025-003', 3, '2025-03-05', '2025-04-04', 15200.75, 'Open', 'Software implementation services', 3),
('INV-2025-004', 1, '2025-03-08', '2025-04-07', 6300.25, 'Open', 'Additional services requested', 1),
('INV-2025-005', 2, '2025-03-12', '2025-04-11', 9500.00, 'Open', 'Hardware supply', 2),
('INV-2025-006', 3, '2025-03-15', '2025-04-14', 4750.50, 'Open', 'Maintenance contract', 3),
('INV-2025-007', 1, '2025-03-18', '2025-04-17', 22950.75, 'Open', 'Quarterly service package', 1),
('INV-2025-008', 2, '2025-03-22', '2025-04-21', 5600.25, 'Open', 'Consulting hours', 2),
('INV-2025-009', 3, '2025-03-25', '2025-04-24', 18500.00, 'Open', 'System upgrade', 3),
('INV-2025-010', 1, '2025-03-28', '2025-04-27', 7250.50, 'Open', 'Training services', 1),

-- February 2025 (some already paid)
('INV-2025-011', 2, '2025-02-01', '2025-03-03', 9200.00, 'Paid', 'Monthly product supply', 2),
('INV-2025-012', 3, '2025-02-05', '2025-03-07', 11500.50, 'Partial', 'Custom software development', 3),
('INV-2025-013', 1, '2025-02-08', '2025-03-10', 6800.75, 'Paid', 'Hardware purchase', 1),
('INV-2025-014', 2, '2025-02-12', '2025-03-14', 14300.25, 'Open', 'Annual service contract', 2),
('INV-2025-015', 3, '2025-02-15', '2025-03-17', 5500.00, 'Paid', 'Consulting services', 3),
('INV-2025-016', 1, '2025-02-18', '2025-03-20', 8750.50, 'Partial', 'System implementation', 1),
('INV-2025-017', 2, '2025-02-22', '2025-03-24', 12950.75, 'Open', 'Hardware and software bundle', 2),
('INV-2025-018', 3, '2025-02-25', '2025-03-27', 9600.25, 'Partial', 'Training and support', 3);

-- Insert sample payments using existing CustomerIDs and UserIDs
INSERT INTO Payments (
    PaymentNumber, 
    PaymentDate, 
    CustomerID, 
    InvoiceID, 
    PaymentMethod, 
    ReferenceNumber, 
    Amount, 
    Notes, 
    CreatedBy
) VALUES
-- March 2025 Payments
('PMT-2025-001', '2025-03-02', 1, NULL, 'Cash', '', 5000.00, 'Advance payment for future orders', 1),
('PMT-2025-002', '2025-03-05', 2, 2, 'Bank Transfer', 'BT-123456', 4375.25, 'Partial payment for INV-2025-002', 2),
('PMT-2025-003', '2025-03-08', 3, 3, 'Credit Card', 'CC-789012', 5000.00, 'Partial payment for INV-2025-003', 3),
('PMT-2025-004', '2025-03-10', 1, 1, 'Bank Transfer', 'BT-234567', 12500.00, 'Full payment for INV-2025-001', 1),
('PMT-2025-005', '2025-03-15', 2, 5, 'Check', 'CK-345678', 9500.00, 'Full payment for INV-2025-005', 2),
('PMT-2025-006', '2025-03-20', 3, 6, 'Bank Transfer', 'BT-456789', 4750.50, 'Full payment for INV-2025-006', 3),
('PMT-2025-007', '2025-03-22', 1, 7, 'Credit Card', 'CC-567890', 10000.00, 'Partial payment for INV-2025-007', 1),
('PMT-2025-008', '2025-03-25', 2, 8, 'Bank Transfer', 'BT-678901', 5600.25, 'Full payment for INV-2025-008', 2),
('PMT-2025-009', '2025-03-28', 3, 9, 'Credit Card', 'CC-789012', 9250.00, 'Partial payment for INV-2025-009', 3),
('PMT-2025-010', '2025-03-30', 1, 10, 'Check', 'CK-890123', 7250.50, 'Full payment for INV-2025-010', 1),

-- February 2025 Payments
('PMT-2025-011', '2025-02-03', 2, 11, 'Bank Transfer', 'BT-901234', 9200.00, 'Full payment for INV-2025-011', 2),
('PMT-2025-012', '2025-02-08', 3, 12, 'Credit Card', 'CC-012345', 6000.00, 'Partial payment for INV-2025-012', 3),
('PMT-2025-013', '2025-02-10', 1, 13, 'Bank Transfer', 'BT-123456', 6800.75, 'Full payment for INV-2025-013', 1),
('PMT-2025-014', '2025-02-18', 3, 15, 'Check', 'CK-234567', 5500.00, 'Full payment for INV-2025-015', 3),
('PMT-2025-015', '2025-02-20', 1, 16, 'Bank Transfer', 'BT-345678', 4000.00, 'Partial payment for INV-2025-016', 1),
('PMT-2025-016', '2025-02-27', 3, 18, 'Credit Card', 'CC-456789', 5000.00, 'Partial payment for INV-2025-018', 3);

-- Update invoice status based on payments
-- This will ensure that the PaidAmount and Status fields are consistent
UPDATE i
SET i.PaidAmount = p.PaidAmount,
    i.Status = CASE 
                  WHEN p.PaidAmount >= i.Amount THEN 'Paid' 
                  WHEN p.PaidAmount > 0 THEN 'Partial' 
                  ELSE 'Open' 
              END
FROM Invoices i
INNER JOIN (
    SELECT InvoiceID, SUM(Amount) AS PaidAmount
    FROM Payments
    WHERE InvoiceID IS NOT NULL
    GROUP BY InvoiceID
) p ON i.InvoiceID = p.InvoiceID;
    


-- Insert admin user (password is 'admin123' with salt)
-- Script to generate 25 additional users with different roles
-- For demonstration purposes, all users have the password "password123" with different salts
INSERT INTO Users (
        Username,
        PasswordHash,
        Salt,
        DisplayName,
        Email,
        Phone,
        RoleID,
        Status,
        TwoFactorEnabled
    )
VALUES -- Admin users
    (
        'jsmith',
        '8a9bcf1e8d2e46c4c7703a165b9b6eb97894a358b0c2510464a9d36142844c29',
        'salt123',
        'John Smith',
        'jsmith@example.com',
        '555-101-1001',
        1,
        'Active',
        0
    ),
    (
        'agarcia',
        'f07c348cc750db500d6c4516ad30628dafd943b4e99ab022c0acf758e11994c1',
        'salt456',
        'Ana Garcia',
        'agarcia@example.com',
        '555-101-1002',
        1,
        'Active',
        1
    ),
    -- Manager users
    (
        'rwilliams',
        'd9a2557fdb8c04e7e9f99a3fbc6a86a3853a211f74232b259a81f1201c271374',
        'salt789',
        'Robert Williams',
        'rwilliams@example.com',
        '555-202-2001',
        2,
        'Active',
        0
    ),
    (
        'mjones',
        'b447ed4dad25bd1b90b3906470981df7f8c76eb8b22ed5cddb7c0eff95626fb4',
        'salt234',
        'Mary Jones',
        'mjones@example.com',
        '555-202-2002',
        2,
        'Active',
        0
    ),
    (
        'klee',
        '9b0118463c759db3a1a67e13fb3d10923486146c835017ab9ea323cd7eef4dbe',
        'salt345',
        'Kevin Lee',
        'klee@example.com',
        '555-202-2003',
        2,
        'Active',
        0
    ),
    (
        'sbrown',
        '17f96789a863dd87d26f7220b36651e96a3cc4b51f29b415a67e8d28b440b9cb',
        'salt456',
        'Sarah Brown',
        'sbrown@example.com',
        '555-202-2004',
        2,
        'Active',
        0
    ),
    -- Sales users
    (
        'tanderson',
        '6d0f99c38c80f99298d930ada8237419ff3bad5f28442c25ed662fd768b6c572',
        'salt567',
        'Thomas Anderson',
        'tanderson@example.com',
        '555-303-3001',
        3,
        'Active',
        0
    ),
    (
        'mrodriguez',
        '5a88bbd28e77c8fc41cabcb17e556f5b2d5f6e24c3c62e9f07535038c0edb143',
        'salt678',
        'Maria Rodriguez',
        'mrodriguez@example.com',
        '555-303-3002',
        3,
        'Active',
        0
    ),
    (
        'jnguyen',
        '1b37826db5bc5131eb3294d30cb4173d52bbc8eb16c3909aa51752327e349532',
        'salt789',
        'James Nguyen',
        'jnguyen@example.com',
        '555-303-3003',
        3,
        'Active',
        0
    ),
    (
        'lmartinez',
        '3e652110bf387714d1b7e22f7736365794028d70886423a13d610160adb0ede7',
        'salt890',
        'Lisa Martinez',
        'lmartinez@example.com',
        '555-303-3004',
        3,
        'Active',
        0
    ),
    (
        'dwhite',
        'af15fce56a77331f851d11e1d06c7454cb91df1643009c6ecc25b91c73c3ba9a',
        'salt012',
        'David White',
        'dwhite@example.com',
        '555-303-3005',
        3,
        'Inactive',
        0
    ),
    (
        'pjackson',
        'db171078f31128bb85143574a9fc55f9bd9971b120f7234aa879d0877b334c7d',
        'salt123',
        'Patricia Jackson',
        'pjackson@example.com',
        '555-303-3006',
        3,
        'Active',
        0
    ),
    -- Inventory users
    (
        'rchang',
        '98a1bec8c2453b505c4eb6b4a1c27cbe632ab524d70e8aa4d8dfea79aa9aae56',
        'salt234',
        'Richard Chang',
        'rchang@example.com',
        '555-404-4001',
        4,
        'Active',
        0
    ),
    (
        'kthomas',
        '0d7cc1b95e75761dd31bbd4ca7fb68678dc90e8cba87cd53f137222fe54c2390',
        'salt345',
        'Karen Thomas',
        'kthomas@example.com',
        '555-404-4002',
        4,
        'Active',
        0
    ),
    (
        'amoore',
        'e3c5c5fff41fd9e3e68fa234a0c01d0642a8ae21203c16bca2b3636d35a0d3f5',
        'salt456',
        'Andrew Moore',
        'amoore@example.com',
        '555-404-4003',
        4,
        'Active',
        0
    ),
    (
        'mclark',
        '7e063b2b3d3b45d7c4030f12b4e91088a108be7717b1faec4e81175be973ebcb',
        'salt567',
        'Michelle Clark',
        'mclark@example.com',
        '555-404-4004',
        4,
        'Locked',
        0
    ),
    (
        'jwalker',
        'c079868deda034811f391e7745a919d56b7b115951ffb9f0989a2ea1e5600b8f',
        'salt678',
        'Joseph Walker',
        'jwalker@example.com',
        '555-404-4005',
        4,
        'Active',
        0
    ),
    -- Finance users
    (
        'lrobinson',
        '2bdfcc5db787f25c2e6db28d3e7a95e9bf0541738d0d7147def37ef22c03da93',
        'salt789',
        'Linda Robinson',
        'lrobinson@example.com',
        '555-505-5001',
        5,
        'Active',
        1
    ),
    (
        'djohnson',
        'bd0aa443a4fd5badfdfc07af700ab3797ff8937e37dbba94310b8fa7aa84f379',
        'salt890',
        'Daniel Johnson',
        'djohnson@example.com',
        '555-505-5002',
        5,
        'Active',
        0
    ),
    (
        'cwright',
        '066ee8bb75399843ec5bd296a48cf70a9d328c71a8520164e434c9838f691728',
        'salt012',
        'Carol Wright',
        'cwright@example.com',
        '555-505-5003',
        5,
        'Active',
        0
    ),
    (
        'ehernandez',
        'f5e0063af468dab9c0f258fb6fff8e8b43b9df8efca82af5e94b59fb586b7241',
        'salt234',
        'Eric Hernandez',
        'ehernandez@example.com',
        '555-505-5004',
        5,
        'Active',
        0
    ),
    (
        'gkim',
        'efc848589eb41c24d88ba89abe643740bc68334e45e0a3918c0798d9d6a7f947',
        'salt345',
        'Grace Kim',
        'gkim@example.com',
        '555-505-5005',
        5,
        'Active',
        0
    ),
    (
        'dlopez',
        'a43042f17729cf7760521521ba31be693a33f6cb1ce0078c8966c71a34063c49',
        'salt456',
        'Daniel Lopez',
        'dlopez@example.com',
        '555-505-5006',
        5,
        'Active',
        0
    ),
    (
        'bsmith',
        '43d82ca592685202c15ddcf110f20f97399ca45d01173c501c3b2518fb5ad56f',
        'salt567',
        'Betty Smith',
        'bsmith@example.com',
        '555-505-5007',
        5,
        'Active',
        0
    );
-- VALUES ('admin', 'f07a147a4e14f6027c9d248a379c2212d7cd4fb5e34908de6c732978de4e239c', 'adminSalt123', 'System Administrator', 'admin@example.com', 1);
