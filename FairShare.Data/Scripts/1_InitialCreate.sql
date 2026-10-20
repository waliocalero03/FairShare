-- 1. Create the Groups table
CREATE TABLE IF NOT EXISTS Groups (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Code VARCHAR(50) NOT NULL UNIQUE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 2. Create the Participants table
CREATE TABLE IF NOT EXISTS Participants (
    Id SERIAL PRIMARY KEY,
    GroupId INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    
    CONSTRAINT FK_Participants_Groups FOREIGN KEY (GroupId) 
        REFERENCES Groups(Id) ON DELETE CASCADE
);

-- 3. Create the Expenses table
CREATE TABLE IF NOT EXISTS Expenses (
    Id SERIAL PRIMARY KEY,
    GroupId INT NOT NULL,
    PayerId INT NOT NULL,
    Description TEXT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT FK_Expenses_Groups FOREIGN KEY (GroupId) 
        REFERENCES Groups(Id) ON DELETE CASCADE,
        
    CONSTRAINT FK_Expenses_Payer FOREIGN KEY (PayerId) 
        REFERENCES Participants(Id) ON DELETE RESTRICT
);

-- 4. Create the ExpenseSplits table
CREATE TABLE IF NOT EXISTS ExpenseSplits (
    Id SERIAL PRIMARY KEY,
    ExpenseId INT NOT NULL,
    ParticipantId INT NOT NULL,
    OwedAmount DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_ExpenseSplits_Expenses FOREIGN KEY (ExpenseId) 
        REFERENCES Expenses(Id) ON DELETE CASCADE,

    CONSTRAINT FK_ExpenseSplits_Participant FOREIGN KEY (ParticipantId) 
        REFERENCES Participants(Id) ON DELETE RESTRICT
);