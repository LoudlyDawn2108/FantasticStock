-- Create Customers table (referenced by the Payment table)
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerNumber NVARCHAR(20) NOT NULL,
    CustomerName NVARCHAR(100) NOT NULL,
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

-- Create Invoices table (referenced by the Payment table)
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
        REFERENCES Customers(CustomerID)
);

-- Create Users table (referenced by the Payment table for CreatedBy)
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

-- Create Payments table based on the C# model
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
    CONSTRAINT FK_Payments_Customers FOREIGN KEY (CustomerID) 
        REFERENCES Customers(CustomerID),
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

-- Create a view to match the C# model more directly
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
    u.UserName AS CreatedByName,
    p.CreatedDate,
    FORMAT(p.PaymentDate, 'yyyy-MM-dd HH:mm:ss') AS FormattedDate
FROM 
    Payments p
LEFT JOIN 
    Customers c ON p.CustomerID = c.CustomerID
LEFT JOIN 
    Invoices i ON p.InvoiceID = i.InvoiceID
LEFT JOIN 
    Users u ON p.CreatedBy = u.UserID;

-- Stored procedure to apply payment to invoice
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

-- Insert some sample users
INSERT INTO Users (UserName, Email, FirstName, LastName) VALUES 
('admin', 'admin@company.com', 'System', 'Administrator'),
('jsmith', 'jsmith@company.com', 'John', 'Smith'),
('mjones', 'mjones@company.com', 'Mary', 'Jones');

-- Insert sample customers
INSERT INTO Customers (CustomerNumber, CustomerName, ContactName, ContactEmail, ContactPhone) VALUES 
('CUST-001', 'Acme Corporation', 'John Doe', 'john.doe@acme.com', '555-123-4567'),
('CUST-002', 'Widget Industries', 'Jane Smith', 'jane.smith@widget.com', '555-987-6543'),
('CUST-003', 'Tech Solutions', 'Bob Johnson', 'bob@techsolutions.com', '555-555-1234'),
('CUST-004', 'Global Enterprises', 'Sarah Williams', 'sarah@globalent.com', '555-234-5678'),
('CUST-005', 'Local Business', 'Mike Brown', 'mike@localbiz.com', '555-678-9012');

-- Insert sample invoices
INSERT INTO Invoices (InvoiceNumber, CustomerID, InvoiceDate, DueDate, Amount, CreatedBy) VALUES 
('INV-2023-001', 1, '2023-01-15', '2023-02-15', 2500.00, 1),
('INV-2023-002', 2, '2023-01-20', '2023-02-20', 1800.50, 1),
('INV-2023-003', 3, '2023-02-01', '2023-03-01', 4200.75, 2),
('INV-2023-004', 4, '2023-02-10', '2023-03-10', 950.25, 2),
('INV-2023-005', 5, '2023-02-15', '2023-03-15', 3500.00, 1),
('INV-2023-006', 1, '2023-03-01', '2023-04-01', 1750.50, 3),
('INV-2023-007', 2, '2023-03-15', '2023-04-15', 2950.75, 3);

-- Insert sample payments
INSERT INTO Payments (PaymentNumber, PaymentDate, CustomerID, InvoiceID, PaymentMethod, ReferenceNumber, Amount, Notes, CreatedBy) VALUES 
('PMT-2023-001', '2023-02-10', 1, 1, 'Bank Transfer', 'BT-12345', 2500.00, 'Payment for INV-2023-001', 1),
('PMT-2023-002', '2023-02-18', 2, 2, 'Credit Card', 'CC-67890', 1800.50, 'Payment for INV-2023-002', 1),
('PMT-2023-003', '2023-02-25', 3, NULL, 'Check', '1001', 1000.00, 'Partial payment for INV-2023-003', 2),
('PMT-2023-004', '2023-03-05', 4, 4, 'Credit Card', 'CC-23456', 950.25, 'Payment for INV-2023-004', 2),
('PMT-2023-005', '2023-03-12', 5, 5, 'Bank Transfer', 'BT-78901', 2000.00, 'Partial payment for INV-2023-005', 1),
('PMT-2023-006', '2023-03-20', 1, 6, 'Cash', '', 1750.50, 'Payment for INV-2023-006', 3),
('PMT-2023-007', '2023-04-02', 3, 3, 'Bank Transfer', 'BT-34567', 3200.75, 'Remaining payment for INV-2023-003', 2);

-- Update invoice paid amounts based on payments
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


SELECT * FROM Payments