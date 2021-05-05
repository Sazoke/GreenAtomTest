CREATE TABLE Orders(
	Id INT IDENTITY(0,1) PRIMARY KEY,
	[Name] VARCHAR(30) NOT NULL
)

CREATE TABLE Products(
	Id INT IDENTITY(0,1) PRIMARY KEY,
	[Name] VARCHAR(30) NOT NULL,
)

CREATE TABLE LinksProductsAndOrders(
	OrderId INT,
	ProductId INT,
	[Count] INT DEFAULT 1,
	PRIMARY KEY(OrderId, ProductId),
	CONSTRAINT FK_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
	CONSTRAINT FK_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
)

INSERT INTO Orders ([Name])
VALUES ('First'),('Second'),('Third')

INSERT INTO Products ([Name])
VALUES ('Milk'), ('Meat'), ('Bread'), ('Water')

INSERT INTO LinksProductsAndOrders (OrderId, ProductId, [Count])
VALUES 
(0, 2, 2),
(0, 3, 1),
(1, 1, 10),
(1, 3, 5)

INSERT INTO LinksProductsAndOrders (OrderId, ProductId)
VALUES 
(2, 0),
(2, 1),
(2, 3)