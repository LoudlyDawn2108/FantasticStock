CREATE DATABASE FantasticStock
GO

USE FantasticStock
GO
-- Current Date and Time (UTC): 2025-03-23 11:31:32
-- Current User's Login: TungCorn

CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(128) NOT NULL,
    DisplayName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    RoleID INT NOT NULL,
    -- constraint FK_Users_Role references dbo.Roles,
    Status NVARCHAR(20) DEFAULT 'Active' NOT NULL,
    TwoFactorEnabled BIT DEFAULT 0 NOT NULL,
    AccountExpiry DATETIME NULL,
    LastLogin DATETIME NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL
);

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

-- Create Category table (merged from both schemas)
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedBy INT NULL,
    CONSTRAINT FK_Category_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserID),
    CONSTRAINT FK_Category_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES [User](UserID)
);

-- Create Brand table (merged from both schemas)
CREATE TABLE Brand (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    BrandName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedBy INT NULL,
    CONSTRAINT FK_Brand_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserID),
    CONSTRAINT FK_Brand_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES [User](UserID)
);

-- Create Supplier table (merged from both schemas)
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

-- Create Product table (merged from both schemas with the requested modifications)
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
    ReorderLevel INT NULL, -- Changed from DECIMAL(18,2) to INT
    TargetStockLevel DECIMAL(18,2) NULL,
    UnitOfMeasure NVARCHAR(50) NULL,
    ManageStock BIT DEFAULT 1 NULL,
    AllowFractionalQuantity BIT DEFAULT 0 NULL,
    ProductImage VARBINARY(MAX) NULL,
    StockQuantity INT DEFAULT 0 NULL, -- Changed from UnitsInStock to StockQuantity
    IsActive BIT DEFAULT 1 NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NULL,
    CreatedBy INT NULL,
    ModifiedDate DATETIME DEFAULT GETDATE() NULL,
    ModifiedBy INT NULL,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    CONSTRAINT FK_Product_Brand FOREIGN KEY (BrandID) REFERENCES Brand(BrandID),
    CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID),
    CONSTRAINT FK_Product_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserID),
    CONSTRAINT FK_Product_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES [User](UserID),
    CONSTRAINT UQ_Product_SKU UNIQUE (SKU)
);

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
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_Sale_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserID)
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
    FOREIGN KEY (SaleID) REFERENCES Sale(SaleID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Create indexes for better performance
CREATE INDEX IX_Product_CategoryID ON Product(CategoryID);
CREATE INDEX IX_Product_BrandID ON Product(BrandID);
CREATE INDEX IX_Product_SupplierID ON Product(SupplierID);
CREATE INDEX IX_Product_ProductName ON Product(ProductName);
CREATE INDEX IX_Supplier_CompanyName ON Supplier(CompanyName);




DECLARE @CurrentDate DATETIME = '2025-03-23 10:54:10'
DECLARE @CurrentUser NVARCHAR(50) = 'TungCorn'

-- Insert sample data for User table
INSERT INTO [User] (Username, PasswordHash, Salt, DisplayName, Email, Phone, RoleID, Status, TwoFactorEnabled, AccountExpiry, LastLogin, CreatedDate, ModifiedDate)
VALUES 
('TungCorn', 'a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6', 'salt123', 'Tùng Corn', 'tungcorn@email.com', '0912345678', 1, 'Active', 0, NULL, @CurrentDate, @CurrentDate, @CurrentDate),
('admin', 'h1a2s3h4p5a6s7s8w9o0r1d2', 'salt456', 'System Admin', 'admin@fantasticstock.com', '0923456789', 1, 'Active', 1, NULL, @CurrentDate, @CurrentDate, @CurrentDate),
('manager', 'q1w2e3r4t5y6u7i8o9p0', 'salt789', 'Store Manager', 'manager@fantasticstock.com', '0934567890', 2, 'Active', 0, NULL, @CurrentDate, @CurrentDate, @CurrentDate);

-- Insert sample data for CustomerType table
INSERT INTO CustomerType (CustomerTypeName, DiscountPercentage, IsActive, CreatedDate, ModifiedDate)
VALUES 
('Regular', 0, 1, @CurrentDate, @CurrentDate),
('Gold', 5.00, 1, @CurrentDate, @CurrentDate),
('Premium', 10.00, 1, @CurrentDate, @CurrentDate);

-- Insert sample data for Customer table
INSERT INTO Customer (CustomerName, ContactName, ContactTitle, Phone, Email, Address, City, State, PostalCode, Country, LoyaltyPoints, IsActive, CustomerTypeID, CreditLimit, Balance, PaymentTerms, TaxID, Notes, CreatedDate, ModifiedDate)
VALUES 
('Công ty TNHH ABC', 'Nguyễn Văn A', 'Giám đốc', '0912345678', 'info@abc.com', '123 Đường Lê Lợi', 'TP Hồ Chí Minh', NULL, '70000', 'Việt Nam', 100, 1, 1, 10000000.00, 5000000.00, 'Net 30', 'VN123456789', 'Khách hàng trung thành', @CurrentDate, @CurrentDate),
('Doanh nghiệp XYZ', 'Trần Thị B', 'Quản lý', '0923456789', 'xyz@email.com', '456 Đường Nguyễn Huệ', 'Hà Nội', NULL, '10000', 'Việt Nam', 250, 1, 2, 20000000.00, 0.00, 'Net 15', 'VN987654321', NULL, @CurrentDate, @CurrentDate),
('Cá nhân Lê Văn C', 'Lê Văn C', NULL, '0934567890', 'levanc@email.com', '789 Đường 3/2', 'Đà Nẵng', NULL, '50000', 'Việt Nam', 50, 1, 3, 5000000.00, 1000000.00, 'COD', NULL, 'Khách hàng mới', @CurrentDate, @CurrentDate);

-- Insert sample data for Category table
INSERT INTO Category (CategoryName, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES 
('Điện thoại', 'Điện thoại di động và phụ kiện', 1, @CurrentDate, 1, @CurrentDate, 1),
('Máy tính', 'Máy tính xách tay, máy tính để bàn và linh kiện', 1, @CurrentDate, 1, @CurrentDate, 1),
('Đồ gia dụng', 'Thiết bị gia dụng và nhà bếp', 1, @CurrentDate, 1, @CurrentDate, 1);

-- Insert sample data for Brand table
INSERT INTO Brand (BrandName, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES 
('Samsung', 'Tập đoàn điện tử hàng đầu Hàn Quốc', 1, @CurrentDate, 1, @CurrentDate, 1),
('Apple', 'Công ty công nghệ đa quốc gia của Mỹ', 1, @CurrentDate, 1, @CurrentDate, 1),
('LG', 'Tập đoàn điện tử và thiết bị gia dụng', 1, @CurrentDate, 1, @CurrentDate, 1);

-- Insert sample data for Supplier table
INSERT INTO Supplier (CompanyName, SupplierName, ContactName, ContactTitle, Address, City, State, PostalCode, Country, Phone, Email, Website, PaymentTerms, IsActive, Notes, CreatedDate, ModifiedDate)
VALUES 
('Công ty Phân phối ABC', 'ABC Distribution', 'Nguyễn Văn X', 'Giám đốc kinh doanh', '123 Đường Lê Lai', 'TP Hồ Chí Minh', NULL, '70000', 'Việt Nam', '0912345678', 'sales@abcdist.com', 'www.abcdist.com', 'Net 30', 1, 'Nhà phân phối chính hãng', @CurrentDate, @CurrentDate),
('Công ty Nhập khẩu XYZ', 'XYZ Import', 'Trần Thị Y', 'Quản lý chuỗi cung ứng', '456 Đường Hai Bà Trưng', 'Hà Nội', NULL, '10000', 'Việt Nam', '0923456789', 'info@xyzimport.com', 'www.xyzimport.com', 'Net 45', 1, NULL, @CurrentDate, @CurrentDate),
('Đại lý Thiết bị DEF', 'DEF Equipment', 'Lê Văn Z', 'Đại diện bán hàng', '789 Đường Nguyễn Hữu Thọ', 'Đà Nẵng', NULL, '50000', 'Việt Nam', '0934567890', 'contact@def.com', 'www.def.com', 'COD', 1, 'Chuyên thiết bị công nghệ cao cấp', @CurrentDate, @CurrentDate);

-- Insert sample data for Product table
INSERT INTO Product (SKU, Barcode, ProductName, Description, CategoryID, CategoryName, BrandID, BrandName, SupplierID, SupplierName, CostPrice, SellingPrice, WholesalePrice, ReorderLevel, TargetStockLevel, UnitOfMeasure, ManageStock, AllowFractionalQuantity, ProductImage, UnitsInStock, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES 
('PHONE-SAM-S22', '8801234567890', 'Samsung Galaxy S22', 'Điện thoại thông minh Samsung Galaxy S22', 1, 'Điện thoại', 1, 'Samsung', 1, 'ABC Distribution', 15000000.00, 18000000.00, 17000000.00, 10, 20, 'Cái', 1, 0, NULL, 15, 1, @CurrentDate, 1, @CurrentDate, 1),
('LAPTOP-APP-MB14', '8901234567890', 'Apple MacBook Pro 14"', 'Máy tính xách tay Apple MacBook Pro 14 inch', 2, 'Máy tính', 2, 'Apple', 2, 'XYZ Import', 35000000.00, 40000000.00, 38000000.00, 5, 10, 'Cái', 1, 0, NULL, 8, 1, @CurrentDate, 1, @CurrentDate, 1),
('TV-LG-OLED65', '9012345678901', 'LG OLED TV 65"', 'Tivi LG OLED 65 inch 4K', 3, 'Đồ gia dụng', 3, 'LG', 3, 'DEF Equipment', 25000000.00, 30000000.00, 28000000.00, 3, 5, 'Cái', 1, 0, NULL, 4, 1, @CurrentDate, 1, @CurrentDate, 1);

-- Insert sample data for Sale table
INSERT INTO Sale (SaleNumber, CustomerID, SaleDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaymentMethod, TenderedAmount, ChangeAmount, Notes, PrintReceipt, EmailReceipt, IsCompleted, CreatedDate, CreatedBy)
VALUES 
('SALE-2025-0001', 1, @CurrentDate, 18000000.00, 1800000.00, 0.00, 19800000.00, 'Tiền mặt', 20000000.00, 200000.00, 'Khách mua trực tiếp tại cửa hàng', 1, 0, 1, @CurrentDate, 1),
('SALE-2025-0002', 2, @CurrentDate, 40000000.00, 4000000.00, 2000000.00, 42000000.00, 'Thẻ tín dụng', 42000000.00, 0.00, 'Khách hàng doanh nghiệp', 1, 1, 1, @CurrentDate, 1),
('SALE-2025-0003', 3, DATEADD(DAY, -1, @CurrentDate), 30000000.00, 3000000.00, 1500000.00, 31500000.00, 'Chuyển khoản', 31500000.00, 0.00, NULL, 0, 1, 1, DATEADD(DAY, -1, @CurrentDate), 1);

-- Insert sample data for SaleItem table
INSERT INTO SaleItem (SaleID, ProductID, Quantity, UnitPrice, Discount, TotalPrice)
VALUES 
(1, 1, 1, 18000000.00, 0.00, 18000000.00),
(2, 2, 1, 40000000.00, 2000000.00, 38000000.00),
(3, 3, 1, 30000000.00, 1500000.00, 28500000.00);