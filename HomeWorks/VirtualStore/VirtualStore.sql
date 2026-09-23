-- Продукты.
CREATE TABLE "Products"
(
	"ProductID" UUID NOT NULL PRIMARY KEY,									-- Основной ключ.
	"ProductName" VARCHAR(1024),											-- Название продукта.
	"Description" VARCHAR(1024),											-- Описание.
	"Price" NUMERIC(10, 2),													-- Цена.
	"QuantityInStock" BIGINT												-- Количество на складе.
);

-- Пользователи.
CREATE TABLE "Users"
(
	"UserID" UUID NOT NULL PRIMARY KEY,										-- Основной ключ.
	"UserName" VARCHAR(1024),												-- Имя пользователя.
	"Email" VARCHAR(1024),													-- Электронная почта.
	"RegistrationDate" TIMESTAMP											-- Дата регистрации.
);

-- Заказы.
CREATE TABLE "Orders"
(
	"OrderID" UUID NOT NULL PRIMARY KEY,									-- Основной ключ.
	"UserID" UUID,															-- Внешний ключ.
	"OrderDate" TIMESTAMP,													-- Дата заказа.
	"Status" VARCHAR(1024),													-- Статус.
	CONSTRAINT "fk_UserID" FOREIGN KEY ("UserID")
		REFERENCES "Users" ("UserID")
);

-- Детали заказа.
CREATE TABLE "OrderDetails"
(
	"OrderDetailID" UUID NOT NULL PRIMARY KEY,								-- Основной ключ.
	"OrderID" UUID,															-- Внешний ключ.
	"ProductID" UUID,														-- Внешний ключ.
	"Quantity" BIGINT,														-- Количество.
	"TotalCost" NUMERIC(10, 2),												-- Общая стоимость.
	CONSTRAINT "fk_OrderID" FOREIGN KEY ("OrderID")
		REFERENCES "Orders" ("OrderID"),
	CONSTRAINT "fk_ProductID" FOREIGN KEY ("ProductID")
		REFERENCES "Products" ("ProductID")
);

-- Индексы.
-- Заказы: поиск заказов по пользователю с сортировкой по дате.
CREATE INDEX "idx_Orders_UserID" ON "Orders" ("UserID", "OrderDate");
-- Заказы: фильтр по статусу.
CREATE INDEX "idx_Orders_Status" ON "Orders" ("Status", "OrderDate");
-- Детали заказа: детали конкретного заказа.
CREATE INDEX "idx_OrderDetails_OrderID" ON "OrderDetails" ("OrderID");
-- Детали заказа: анализ продаж по товарам.
CREATE INDEX "idx_OrderDetails_ProductID" ON "OrderDetails" ("ProductID");
-- Продукты: 5 самых дорогих товаров.
CREATE INDEX "idx_Products_Price" ON "Products" ("Price" DESC);
-- Продукты: товары с низким запасом (менее 5 штук) — частичный индекс.
CREATE INDEX "idx_Products_QuantityInStock" ON "Products" ("QuantityInStock")
	WHERE "QuantityInStock" < 5;
-- Пользователи: уникальность email (вход в любом регистре).
CREATE UNIQUE INDEX "idx_Users_Email" ON "Users" (LOWER("Email"));