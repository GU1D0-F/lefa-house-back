-- Payments table
CREATE TABLE Payments (
    Id SERIAL PRIMARY KEY,
    Amount DECIMAL(10, 2) NOT NULL,
    Description TEXT NOT NULL,
    Category TEXT NOT NULL,
    Date DATE NOT NULL,
    UserName TEXT NOT NULL
);

-- Expenses table
CREATE TABLE Expenses (
    Id SERIAL PRIMARY KEY,
    Amount DECIMAL(10, 2) NOT NULL,
    Description TEXT NOT NULL,
    Category TEXT NOT NULL,
    Date DATE NOT NULL,
    UserName TEXT NOT NULL
);

-- Transactions table
CREATE TABLE Transactions (
    Id SERIAL PRIMARY KEY,
    Action TEXT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Description TEXT NOT NULL,
    Category TEXT NOT NULL,
    Date DATE NOT NULL
);
