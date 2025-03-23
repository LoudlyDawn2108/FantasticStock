-- Create ExpenseCategories table (referenced by Expense table)
CREATE TABLE ExpenseCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

-- Create Suppliers table (referenced by Expense table)
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100),
    ContactEmail NVARCHAR(100),
    ContactPhone NVARCHAR(20),
    Address NVARCHAR(255),
    City NVARCHAR(50),
    State NVARCHAR(50),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(50),
    Notes NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

-- Create Users table (referenced by Expense table for CreatedBy)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
);

-- Create Expenses table based on the C# model
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
    
    -- Foreign Key constraints
    CONSTRAINT FK_Expenses_Suppliers FOREIGN KEY (SupplierID) 
        REFERENCES Suppliers(SupplierID),
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

-- Create a computed column view to match the C# model more directly
CREATE VIEW vw_Expenses AS
SELECT 
    e.ExpenseID,
    e.ExpenseNumber,
    e.ExpenseDate,
    e.SupplierID,
    s.SupplierName,
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
    u.UserName AS CreatedByName,
    e.CreatedDate,
    FORMAT(e.ExpenseDate, 'yyyy-MM-dd HH:mm:ss') AS FormattedDate
FROM 
    Expenses e
LEFT JOIN 
    Suppliers s ON e.SupplierID = s.SupplierID
LEFT JOIN 
    ExpenseCategories c ON e.CategoryID = c.CategoryID
LEFT JOIN 
    Users u ON e.CreatedBy = u.UserID;

-- Insert some sample expense categories
INSERT INTO ExpenseCategories (CategoryName, Description) VALUES 
('Office Supplies', 'General office supplies and stationery'),
('Rent', 'Office and facility rent expenses'),
('Utilities', 'Electricity, water, internet, and other utilities'),
('Travel', 'Business travel expenses including airfare, hotel, and meals'),
('Equipment', 'Office equipment, computers, and furniture'),
('Marketing', 'Advertising, promotion, and marketing expenses'),
('Professional Services', 'Legal, accounting, and consulting services'),
('Insurance', 'Business insurance premiums'),
('Subscriptions', 'Software subscriptions and memberships'),
('Miscellaneous', 'Other uncategorized expenses');

-- Insert some sample suppliers
INSERT INTO Suppliers (SupplierName, ContactName, ContactEmail, ContactPhone) VALUES 
('ABC Office Supply', 'John Doe', 'john.doe@abcsupply.com', '555-123-4567'),
('XYZ Property Management', 'Jane Smith', 'jane.smith@xyzprop.com', '555-987-6543'),
('City Utilities', 'Customer Service', 'service@cityutilities.com', '555-555-1234'),
('Tech Solutions Inc.', 'Robert Johnson', 'robert@techsolutions.com', '555-234-5678'),
('Global Insurance Co.', 'Susan Williams', 'susan@globalinsurance.com', '555-678-9012');

-- Insert sample users
INSERT INTO Users (UserName, Email, FirstName, LastName) VALUES 
('admin', 'admin@company.com', 'System', 'Administrator'),
('jsmith', 'jsmith@company.com', 'John', 'Smith'),
('mjones', 'mjones@company.com', 'Mary', 'Jones');

-- Insert sample expenses
INSERT INTO Expenses (ExpenseNumber, ExpenseDate, SupplierID, CategoryID, PaymentMethod, 
                     ReferenceNumber, Amount, TaxAmount, Notes, IsTaxDeductible, CreatedBy) VALUES 
('EXP-2023-001', '2023-01-15', 1, 1, 'Credit Card', 'CC-1234', 150.00, 13.50, 'Monthly office supplies', 1, 1),
('EXP-2023-002', '2023-01-30', 2, 2, 'Bank Transfer', 'BT-5678', 2000.00, 0.00, 'Monthly office rent', 1, 1),
('EXP-2023-003', '2023-02-05', 3, 3, 'Credit Card', 'CC-2345', 425.75, 38.32, 'Utility bills for January', 1, 2),
('EXP-2023-004', '2023-02-15', 1, 1, 'Cash', '', 65.25, 5.87, 'Paper, pens, and notebooks', 1, 2),
('EXP-2023-005', '2023-02-28', 4, 5, 'Check', '1001', 1299.99, 117.00, 'New laptop for accounting', 1, 1),
('EXP-2023-006', '2023-03-10', 5, 8, 'Bank Transfer', 'BT-6789', 850.00, 0.00, 'Quarterly business insurance premium', 1, 3),
('EXP-2023-007', '2023-03-15', NULL, 10, 'Cash', '', 75.00, 6.75, 'Team lunch meeting', 0, 3),
('EXP-2023-008', '2023-03-31', 2, 2, 'Bank Transfer', 'BT-7890', 2000.00, 0.00, 'Monthly office rent', 1, 1),
('EXP-2023-009', '2023-04-05', 3, 3, 'Credit Card', 'CC-3456', 398.50, 35.87, 'Utility bills for February', 1, 2),
('EXP-2023-010', '2023-04-15', 1, 1, 'Credit Card', 'CC-4567', 212.30, 19.11, 'Office supplies and printer toner', 1, 1);

SELECT * FROM Expenses