CREATE TABLE Cards (
	Id INT CONSTRAINT PK_Cards PRIMARY KEY IDENTITY,
	Number VARCHAR(25) NOT NULL,
	PIN VARCHAR(25) NOT NULL,
	[ExpireDate] DateTime NOT NULL,
	IsBlocked BIT NOT NULL DEFAULT 0,
	Balance MONEY NOT NULL DEFAULT 0
)

CREATE TABLE Operations (
	Id INT CONSTRAINT PK_Operations PRIMARY KEY IDENTITY,
	IdTarjeta INT NOT NULL FOREIGN KEY REFERENCES Cards(Id),
	[Date] DATETIME NOT NULL DEFAULT GETDATE(),
	Code VARCHAR(25),
	Amount MONEY
)

INSERT INTO [dbo].[Cards]
VALUES ('4952687422238569', '456123', DATEADD(YEAR, 15, GETDATE()), 0, 500000),
('1111222233334444', '789456', DATEADD(YEAR, 10, GETDATE()), 0, 320000),
('7856254012493274', '1234', DATEADD(YEAR, 8, GETDATE()), 0, 1000000)