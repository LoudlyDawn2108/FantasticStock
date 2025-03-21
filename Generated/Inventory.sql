CREATE database Product
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT,
    ModifiedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy INT
);

-- Create Brand table (referenced by Product)
CREATE TABLE Brand (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    BrandName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT,
    ModifiedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy INT
);

-- Create Supplier table
CREATE TABLE Supplier (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(200) NOT NULL,
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
    IsActive BIT DEFAULT 1,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

-- Create User table (for tracking CreatedBy and ModifiedBy)
CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Create Product table with foreign key references
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    SKU NVARCHAR(50) NOT NULL,
    Barcode NVARCHAR(50) NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CategoryID INT NOT NULL,
    BrandID INT NOT NULL,
    SupplierID INT NOT NULL,
    CostPrice DECIMAL(18, 2) NOT NULL,
    SellingPrice DECIMAL(18, 2) NOT NULL,
    WholesalePrice DECIMAL(18, 2) NULL,
    ReorderLevel DECIMAL(18, 2) NOT NULL,
    TargetStockLevel DECIMAL(18, 2) NULL,
    UnitOfMeasure NVARCHAR(50) NULL,
    ManageStock BIT NOT NULL DEFAULT 1,
    AllowFractionalQuantity BIT NOT NULL DEFAULT 0,
    ProductImage VARBINARY(MAX) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy INT NOT NULL,
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy INT NOT NULL,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    CONSTRAINT FK_Product_Brand FOREIGN KEY (BrandID) REFERENCES Brand(BrandID),
    CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID),
    CONSTRAINT FK_Product_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserID),
    CONSTRAINT FK_Product_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES [User](UserID),
    CONSTRAINT UQ_Product_SKU UNIQUE (SKU)
);

-- Create indexes for better performance
CREATE INDEX IX_Product_CategoryID ON Product(CategoryID);
CREATE INDEX IX_Product_BrandID ON Product(BrandID);
CREATE INDEX IX_Product_SupplierID ON Product(SupplierID);
CREATE INDEX IX_Product_ProductName ON Product(ProductName);
CREATE INDEX IX_Supplier_CompanyName ON Supplier(CompanyName);

-- Sửa đổi cột BrandID để cho phép NULL
ALTER TABLE Product
ALTER COLUMN BrandID INT NULL;

-- Sửa đổi các ràng buộc khóa ngoại cho BrandID (nếu có)
ALTER TABLE Product
DROP CONSTRAINT FK_Product_Brand;

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Brand 
FOREIGN KEY (BrandID) REFERENCES Brand(BrandID);

-- Sửa đổi cột WholesalePrice để cho phép NULL
ALTER TABLE Product
ALTER COLUMN WholesalePrice DECIMAL(18, 2) NULL;

-- Sửa đổi cột TargetStockLevel để cho phép NULL
ALTER TABLE Product
ALTER COLUMN TargetStockLevel DECIMAL(18, 2) NULL;

-- Sửa đổi cột UnitOfMeasure để cho phép NULL
ALTER TABLE Product
ALTER COLUMN UnitOfMeasure NVARCHAR(50) NULL;

-- Sửa đổi cột ManageStock để cho phép NULL
ALTER TABLE Product
ALTER COLUMN ManageStock BIT NULL;

-- Sửa đổi cột AllowFractionalQuantity để cho phép NULL
ALTER TABLE Product
ALTER COLUMN AllowFractionalQuantity BIT NULL;

-- Sửa đổi cột IsActive để cho phép NULL
ALTER TABLE Product
ALTER COLUMN IsActive BIT NULL;

-- Sửa đổi cột CreatedBy để cho phép NULL
ALTER TABLE Product
DROP CONSTRAINT FK_Product_CreatedBy;

ALTER TABLE Product
ALTER COLUMN CreatedBy INT NULL;

ALTER TABLE Product
ADD CONSTRAINT FK_Product_CreatedBy 
FOREIGN KEY (CreatedBy) REFERENCES [User](UserID);

-- Sửa đổi cột ModifiedBy để cho phép NULL
ALTER TABLE Product
DROP CONSTRAINT FK_Product_ModifiedBy;

ALTER TABLE Product
ALTER COLUMN ModifiedBy INT NULL;

ALTER TABLE Product
ALTER COLUMN ReorderLevel INT ;



ALTER TABLE Product
ADD CONSTRAINT FK_Product_ModifiedBy 
FOREIGN KEY (ModifiedBy) REFERENCES [User](UserID);


-- Insert 5 technology product categories based on your table structure
INSERT INTO Category (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
VALUES ('Smartphones', 'Smartphones, mobile phones and accessories', 1, GETDATE(), 1);

INSERT INTO Category (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
VALUES ('Laptops', 'Notebook computers, laptops and accessories', 1, GETDATE(), 1);

INSERT INTO Category (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
VALUES ('Wearable Technology', 'Smart watches, fitness trackers and other wearable devices', 1, GETDATE(), 1);

INSERT INTO Category (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
VALUES ('Audio Equipment', 'Headphones, earbuds, speakers and sound systems', 1, GETDATE(), 1);

INSERT INTO Category (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
VALUES ('Computer Components', 'Desktop computer parts, hardware and peripherals', 1, GETDATE(), 1);

INSERT INTO Supplier (CompanyName, ContactName, ContactTitle, Address, City, State, PostalCode, Country, Phone, Email, Website, PaymentTerms, IsActive, Notes)
VALUES (
    'TechGlobal Supplies Inc.',
    'John Smith',
    'Purchasing Manager',
    '123 Main Street',
    'San Francisco',
    'CA',
    '94105',
    'USA',
    '+1 (415) 555-7890',
    'john.smith@techglobal.com',
    'www.techglobalsupplies.com',
    'Net 30',
    1,
    'Reliable supplier for electronic components. Offers volume discounts.'
);

INSERT INTO Supplier (CompanyName, ContactName, ContactTitle, Address, City, State, PostalCode, Country, Phone, Email, Website, PaymentTerms, IsActive, Notes)
VALUES (
    'Asia Electronics Ltd.',
    'Wei Zhang',
    'Sales Director',
    '45 Huangpu Road',
    'Shanghai',
    NULL,
    '200001',
    'China',
    '+86 21 5555 8888',
    'w.zhang@asiaelectronics.cn',
    'www.asiaelectronics.cn',
    'Net 45',
    1,
    'Specializes in smartphone and tablet components. Requires minimum order quantity of 1000 units.'
);


INSERT INTO Product (SKU, Barcode, ProductName, Description, CategoryID, SupplierID, CostPrice, SellingPrice, StockQuantity, ReorderLevel, ProductImage, CreatedDate)
VALUES 
('SKU12345', '123456789012', 'Sample Product', 'This is a sample product description.', 1, 1, 100.00, 150.00, 50, 10, NULL, GETDATE()),
('SKU67890', '987654321098', 'Another Product', 'This is another product description.', 2, 2, 200.00, 250.00, 4, 5, NULL, GETDATE());




-- Thêm bảng InventoryTransaction vào file inventory.sql
CREATE TABLE InventoryTransaction (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    LocationID INT NOT NULL DEFAULT 1, -- Mặc định nếu bạn chỉ có một vị trí
    TransactionType NVARCHAR(50) NOT NULL, -- Purchase, Sale, Adjustment, Transfer, Stock Take
    Quantity DECIMAL(18, 2) NOT NULL,
    PreviousQuantity DECIMAL(18, 2) NOT NULL,
    NewQuantity DECIMAL(18, 2) NOT NULL,
    ReferenceID INT NULL,
    ReferenceType NVARCHAR(50) NULL, -- PO, SO, StockAdjustment, Transfer, etc.
    ReferenceNumber NVARCHAR(50) NULL,
    Notes NVARCHAR(500) NULL,
  --  AdjustmentReason NVARCHAR(200) NULL, -- Trường mới để lưu lý do thay đổi số lượng
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_InventoryTransaction_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    CONSTRAINT FK_InventoryTransaction_User FOREIGN KEY (CreatedBy) REFERENCES [User](UserID)
);

-- Tạo index để tăng hiệu suất truy vấn
CREATE INDEX IX_InventoryTransaction_ProductID ON InventoryTransaction(ProductID);
CREATE INDEX IX_InventoryTransaction_CreatedDate ON InventoryTransaction(CreatedDate);
CREATE INDEX IX_InventoryTransaction_TransactionType ON InventoryTransaction(TransactionType);