-- 1. Create the Groups table
CREATE TABLE Groups (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 2. Create the Participants table
CREATE TABLE Participants (
    Id SERIAL PRIMARY KEY,
    GroupId INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    
    -- If a group is deleted, delete all its participants
    CONSTRAINT FK_Participants_Groups FOREIGN KEY (GroupId) 
        REFERENCES Groups(Id) ON DELETE CASCADE
);

-- 3. Create the Expenses table
CREATE TABLE Expenses (
    Id SERIAL PRIMARY KEY,
    GroupId INT NOT NULL,
    PayerId INT NOT NULL,
    Description TEXT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    -- If a group is deleted, delete its expenses
    CONSTRAINT FK_Expenses_Groups FOREIGN KEY (GroupId) 
        REFERENCES Groups(Id) ON DELETE CASCADE,
        
    -- IMPORTANT: Prevent deleting a participant if they have paid for an expense
    CONSTRAINT FK_Expenses_Payer FOREIGN KEY (PayerId) 
        REFERENCES Participants(Id) ON DELETE RESTRICT
);

-- 4. Create the ExpenseSplits table (How the bill is divided)
CREATE TABLE ExpenseSplits (
    Id SERIAL PRIMARY KEY,
    ExpenseId INT NOT NULL,
    ParticipantId INT NOT NULL,
    OwedAmount DECIMAL(18,2) NOT NULL,

    -- If an expense is deleted, delete the splits associated with it
    CONSTRAINT FK_ExpenseSplits_Expenses FOREIGN KEY (ExpenseId) 
        REFERENCES Expenses(Id) ON DELETE CASCADE,

    -- Prevent deleting a participant if they owe money
    CONSTRAINT FK_ExpenseSplits_Participant FOREIGN KEY (ParticipantId) 
        REFERENCES Participants(Id) ON DELETE RESTRICT
);