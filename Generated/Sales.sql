create database FantasticStock
go

use FantasticStock
go

-- Customer Table
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100),
    ContactTitle NVARCHAR(100),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    City NVARCHAR(50),
    State NVARCHAR(50),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(50),
    LoyaltyPoints INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CustomerTypeID INT,
    CustomerTypeName NVARCHAR(50),
    CreditLimit DECIMAL(18,2),
    Balance DECIMAL(18,2),
    PaymentTerms NVARCHAR(50),
    TaxID NVARCHAR(50),
    Notes NTEXT,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

-- Product Table
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    SKU NVARCHAR(50) NOT NULL,
    Barcode NVARCHAR(50),
    ProductName NVARCHAR(100) NOT NULL,
    Description NTEXT,
    CategoryID INT,
    CategoryName NVARCHAR(50),
    BrandID INT,
    BrandName NVARCHAR(50),
    SupplierID INT,
    SupplierName NVARCHAR(50),
    CostPrice DECIMAL(18,2) NOT NULL,
    SellingPrice DECIMAL(18,2) NOT NULL,
    WholesalePrice DECIMAL(18,2),
    ReorderLevel DECIMAL(10,2),
    TargetStockLevel DECIMAL(10,2),
    UnitOfMeasure NVARCHAR(20),
    ManageStock BIT DEFAULT 1,
    AllowFractionalQuantity BIT DEFAULT 0,
    ProductImage VARBINARY(MAX),
    UnitsInStock INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT,
    ModifiedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy INT
);

-- Category Table
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL,
    Description NTEXT,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

-- Brand Table
CREATE TABLE Brand (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    BrandName NVARCHAR(50) NOT NULL,
    Description NTEXT,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

-- Supplier Table
CREATE TABLE Supplier (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    City NVARCHAR(50),
    State NVARCHAR(50),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

-- Sale Table
CREATE TABLE Sale (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    SaleNumber NVARCHAR(20) NOT NULL,
    CustomerID INT,
    SaleDate DATETIME DEFAULT GETDATE(),
    SubTotal DECIMAL(18,2) NOT NULL,
    TaxAmount DECIMAL(18,2) NOT NULL,
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL,
    PaymentMethod NVARCHAR(50),
    TenderedAmount DECIMAL(18,2),
    ChangeAmount DECIMAL(18,2),
    Notes NTEXT,
    PrintReceipt BIT DEFAULT 0,
    EmailReceipt BIT DEFAULT 0,
    IsCompleted BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- SaleItem Table
CREATE TABLE SaleItem (
    SaleItemID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Discount DECIMAL(18,2) DEFAULT 0,
    TotalPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (SaleID) REFERENCES Sale(SaleID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- CustomerType Table
CREATE TABLE CustomerType (
    CustomerTypeID INT PRIMARY KEY IDENTITY(1,1),
    CustomerTypeName NVARCHAR(50) NOT NULL,
    DiscountPercentage DECIMAL(5,2) DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

-- Add foreign key relationships
ALTER TABLE Customer
ADD CONSTRAINT FK_Customer_CustomerType FOREIGN KEY (CustomerTypeID) REFERENCES CustomerType(CustomerTypeID);

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID);

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Brand FOREIGN KEY (BrandID) REFERENCES Brand(BrandID);

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID);

-- Category inserts
INSERT INTO Category (CategoryName, Description, IsActive) VALUES
('Electronics', 'Electronic devices and accessories', 1),
('Office Supplies', 'Office and stationery products', 1),
('Computer Peripherals', 'Computer accessories and add-ons', 1),
('Mobile Accessories', 'Accessories for smartphones and tablets', 1),
('Audio Equipment', 'Headphones, speakers and audio devices', 1);

-- Brand inserts
INSERT INTO Brand (BrandName, Description, IsActive) VALUES
('TechMaster', 'High-quality electronics and computing devices', 1),
('ConnectPro', 'Professional connectivity solutions', 1),
('AudioMax', 'Premium audio equipment', 1),
('MobilePlus', 'Mobile device accessories', 1),
('OfficeOne', 'Office equipment and supplies', 1);

-- Supplier inserts
INSERT INTO Supplier (SupplierName, ContactName, Phone, Email, Address, City, State, PostalCode, Country, IsActive) VALUES
('Global Tech Distributors', 'James Wilson', '(555) 111-2222', 'jwilson@gtd.com', '123 Supply St', 'Chicago', 'IL', '60601', 'USA', 1),
('Electronics Wholesale Inc', 'Linda Chen', '(555) 222-3333', 'lchen@ewi.com', '456 Vendor Ave', 'San Jose', 'CA', '95112', 'USA', 1),
('Office Supply Co.', 'Michael Brown', '(555) 333-4444', 'mbrown@officesupply.com', '789 Business Blvd', 'Dallas', 'TX', '75201', 'USA', 1);

-- Product inserts (matching your sample data plus additional items)
INSERT INTO Product (SKU, Barcode, ProductName, Description, CategoryID, CategoryName, BrandID, BrandName, SupplierID, SupplierName, 
                    CostPrice, SellingPrice, WholesalePrice, ReorderLevel, TargetStockLevel, UnitOfMeasure, 
                    ManageStock, AllowFractionalQuantity, IsActive, UnitsInStock, CreatedDate, ModifiedDate) VALUES
('LAP001', '1001', 'Laptop', '15.6" High-performance laptop with SSD', 1, 'Electronics', 1, 'TechMaster', 1, 'Global Tech Distributors', 
 699.99, 899.99, 849.99, 5, 15, 'Each', 1, 0, 1, 12, GETDATE(), GETDATE()),
 
('PHN001', '1002', 'Smartphone', 'Latest model smartphone with dual camera', 1, 'Electronics', 4, 'MobilePlus', 2, 'Electronics Wholesale Inc', 
 399.99, 499.99, 469.99, 10, 30, 'Each', 1, 0, 1, 25, GETDATE(), GETDATE()),
 
('MON001', '1003', 'Monitor', '24" HD LED Monitor with HDMI', 3, 'Computer Peripherals', 1, 'TechMaster', 1, 'Global Tech Distributors', 
 189.99, 249.99, 229.99, 8, 20, 'Each', 1, 0, 1, 15, GETDATE(), GETDATE()),
 
('KBD001', '1004', 'Keyboard', 'Wireless ergonomic keyboard', 3, 'Computer Peripherals', 2, 'ConnectPro', 1, 'Global Tech Distributors', 
 35.99, 49.99, 44.99, 15, 40, 'Each', 1, 0, 1, 30, GETDATE(), GETDATE()),
 
('MOU001', '1005', 'Mouse', 'Wireless optical mouse', 3, 'Computer Peripherals', 2, 'ConnectPro', 1, 'Global Tech Distributors', 
 19.99, 29.99, 26.99, 15, 50, 'Each', 1, 0, 1, 45, GETDATE(), GETDATE()),
 
('HEA001', '1006', 'Headphones', 'Noise-cancelling over-ear headphones', 5, 'Audio Equipment', 3, 'AudioMax', 2, 'Electronics Wholesale Inc', 
 59.99, 79.99, 74.99, 10, 25, 'Each', 1, 0, 1, 22, GETDATE(), GETDATE()),
 
('USB001', '1007', 'USB Drive', '64GB USB 3.0 Flash Drive', 3, 'Computer Peripherals', 1, 'TechMaster', 2, 'Electronics Wholesale Inc', 
 14.99, 19.99, 17.99, 20, 60, 'Each', 1, 0, 1, 55, GETDATE(), GETDATE()),
 
('HDD001', '1008', 'External HDD', '1TB Portable External Hard Drive', 3, 'Computer Peripherals', 1, 'TechMaster', 2, 'Electronics Wholesale Inc', 
 74.99, 99.99, 89.99, 8, 25, 'Each', 1, 0, 1, 18, GETDATE(), GETDATE()),
 
('SPK001', '1009', 'Bluetooth Speaker', 'Portable waterproof bluetooth speaker', 5, 'Audio Equipment', 3, 'AudioMax', 2, 'Electronics Wholesale Inc', 
 34.99, 49.99, 44.99, 12, 35, 'Each', 1, 0, 1, 28, GETDATE(), GETDATE()),
 
('PEN001', '1010', 'Pen Set', 'Premium ballpoint pen set (12 count)', 2, 'Office Supplies', 5, 'OfficeOne', 3, 'Office Supply Co.', 
 7.99, 12.99, 10.99, 20, 50, 'Pack', 1, 0, 1, 42, GETDATE(), GETDATE());

-- CustomerType inserts
INSERT INTO CustomerType (CustomerTypeName, DiscountPercentage, IsActive) VALUES
('Regular', 0.00, 1),
('Silver', 3.00, 1),
('Gold', 5.00, 1),
('Platinum', 8.00, 1);

-- Customer inserts
INSERT INTO Customer (CustomerName, ContactName, Phone, Email, Address, City, State, PostalCode, Country, 
                     LoyaltyPoints, IsActive, CustomerTypeID, CustomerTypeName, CreatedDate, ModifiedDate) VALUES
('John Smith', 'John Smith', '555-123-4567', 'john@example.com', '123 Main St', 'New York', 'NY', '10001', 'USA',
 150, 1, 2, 'Silver', GETDATE(), GETDATE()),
 
('Jane Doe', 'Jane Doe', '555-987-6543', 'jane@example.com', '456 Park Ave', 'Boston', 'MA', '02108', 'USA',
 320, 1, 3, 'Gold', GETDATE(), GETDATE()),
 
('Bob Johnson', 'Bob Johnson', '555-456-7890', 'bob@example.com', '789 Broadway', 'Chicago', 'IL', '60601', 'USA',
 75, 1, 1, 'Regular', GETDATE(), GETDATE()),
 
('Alice Brown', 'Alice Brown', '555-789-0123', 'alice@example.com', '321 Oak St', 'San Francisco', 'CA', '94105', 'USA',
 210, 1, 3, 'Gold', GETDATE(), GETDATE()),
 
('Acme Corporation', 'Mark Wilson', '555-123-4567', 'info@acmecorp.com', '123 Main St', 'New York', 'NY', '10001', 'USA',
 250, 1, 3, 'Gold', GETDATE(), GETDATE()),
 
('TechSolutions Inc.', 'Sarah Miller', '555-987-6543', 'contact@techsolutions.com', '456 Innovation Ave', 'San Francisco', 'CA', '94105', 'USA',
 120, 1, 2, 'Silver', GETDATE(), GETDATE()),
 
('Global Enterprises', 'David Lee', '555-456-7890', 'sales@globalent.com', '789 Business Blvd', 'Chicago', 'IL', '60601', 'USA',
 350, 1, 4, 'Platinum', GETDATE(), GETDATE()),
 
('Local Shop', 'Jennifer Garcia', '555-222-3333', 'shop@local.com', '321 Small St', 'Portland', 'OR', '97205', 'USA',
 75, 0, 1, 'Regular', GETDATE(), GETDATE()),
 
('Big Retail Chain', 'Robert Taylor', '555-777-8888', 'orders@bigretail.com', '555 Commerce Way', 'Dallas', 'TX', '75201', 'USA',
 500, 1, 4, 'Platinum', GETDATE(), GETDATE());

-- Sample Sales Data
INSERT INTO Sale (SaleNumber, CustomerID, SaleDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, 
                 PaymentMethod, TenderedAmount, ChangeAmount, PrintReceipt, EmailReceipt, IsCompleted, CreatedDate)
VALUES
('SALE-001', 1, DATEADD(day, -5, GETDATE()), 899.99, 72.00, 0.00, 971.99, 'Credit Card', 971.99, 0.00, 1, 1, 1, DATEADD(day, -5, GETDATE())),
('SALE-002', 2, DATEADD(day, -4, GETDATE()), 529.98, 42.40, 26.50, 545.88, 'Cash', 550.00, 4.12, 1, 0, 1, DATEADD(day, -4, GETDATE())),
('SALE-003', 3, DATEADD(day, -3, GETDATE()), 19.99, 1.60, 0.00, 21.59, 'Cash', 25.00, 3.41, 1, 0, 1, DATEADD(day, -3, GETDATE())),
('SALE-004', 4, DATEADD(day, -2, GETDATE()), 329.97, 26.40, 16.50, 339.87, 'Credit Card', 339.87, 0.00, 0, 1, 1, DATEADD(day, -2, GETDATE())),
('SALE-005', 2, DATEADD(day, -1, GETDATE()), 149.97, 12.00, 7.50, 154.47, 'Debit Card', 154.47, 0.00, 1, 1, 1, DATEADD(day, -1, GETDATE()));

-- Sample Sale Items
INSERT INTO SaleItem (SaleID, ProductID, Quantity, UnitPrice, Discount, TotalPrice)
VALUES
(1, 1, 1, 899.99, 0.00, 899.99),
(2, 2, 1, 499.99, 25.00, 474.99),
(2, 7, 1, 19.99, 1.00, 18.99),
(3, 7, 1, 19.99, 0.00, 19.99),
(4, 3, 1, 249.99, 12.50, 237.49),
(4, 6, 1, 79.98, 4.00, 75.98),
(5, 5, 1, 29.99, 1.50, 28.49),
(5, 7, 2, 19.99, 2.00, 37.98),
(5, 10, 1, 12.99, 0.65, 12.34);
