CREATE TABLE Employee (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Manager_Id NUMBER,
  First_Name VARCHAR2(100),
  Last_Name VARCHAR2(100),
  Phone_Number NUMBER,
  Adress VARCHAR2(255),
  Salary_per_Hour NUMBER,
  Section_Id NUMBER,
  CONSTRAINT fk_employee_section FOREIGN KEY (Section_Id) REFERENCES Section(Id),
  CONSTRAINT fk_employee_manager FOREIGN KEY (Manager_Id) REFERENCES Employee(Id)
);

CREATE TABLE Employee_WorkDay (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Date DATE,
  Starts TIMESTAMP,
  Ends TIMESTAMP,
  Working_Hours NUMBER,
  Note VARCHAR2(255),
  Employee_Id NUMBER,
  CONSTRAINT fk_employee_workday_employee FOREIGN KEY (Employee_Id) REFERENCES Employee(Id)
);

CREATE TABLE Section (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name VARCHAR2(100)
);

CREATE TABLE Item (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Title VARCHAR2(50),
  Description VARCHAR2(250),
  Price NUMBER,
  Added TIMESTAMP,
  Rating NUMBER(3,2),
  Category_Id NUMBER,
  CONSTRAINT fk_item_category FOREIGN KEY (Category_Id) REFERENCES Category(Id)
);

CREATE TABLE Order (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Item_Id NUMBER, -- Changed to Item_Id
  Time NUMBER, -- Changed to NUMBER
  Date TIMESTAMP,
  Price NUMBER,
  Employee_Id NUMBER,
  Table_Id NUMBER,
  Receipt_Id NUMBER,
  CONSTRAINT fk_order_employee FOREIGN KEY (Employee_Id) REFERENCES Employee(Id),
  CONSTRAINT fk_order_table FOREIGN KEY (Table_Id) REFERENCES "Table"(Id), -- Table is a reserved keyword
  CONSTRAINT fk_order_receipt FOREIGN KEY (Receipt_Id) REFERENCES Receipt(Id)
);

CREATE TABLE Order_Item (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Order_Id NUMBER,
  Item_Id NUMBER,
  CONSTRAINT fk_order_item_order FOREIGN KEY (Order_Id) REFERENCES Order(Id),
  CONSTRAINT fk_order_item_item FOREIGN KEY (Item_Id) REFERENCES Item(Id)
);

CREATE TABLE "Table" (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Number NUMBER,
  Status BOOLEAN, -- Changed to BOOLEAN
  Employee_Id NUMBER,
  CONSTRAINT fk_table_employee FOREIGN KEY (Employee_Id) REFERENCES Employee(Id)
);

CREATE TABLE Receipt (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Date TIMESTAMP,
  Sub_Total NUMBER,
  Taxes NUMBER,
  Discount NUMBER,
  Total NUMBER,
  Table_Id NUMBER,
  CONSTRAINT fk_receipt_table FOREIGN KEY (Table_Id) REFERENCES "Table"(Id)
);

CREATE TABLE Category (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name VARCHAR2(100)
);

CREATE TABLE Supplier (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Full_Name VARCHAR2(255),
  Phone_Number NUMBER
);

CREATE TABLE Ingredient (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Name VARCHAR2(255),
  Price NUMBER,
  Quantity NUMBER,
  Supplier_Id NUMBER,
  CONSTRAINT fk_ingredient_supplier FOREIGN KEY (Supplier_Id) REFERENCES Supplier(Id)
);

CREATE TABLE Item_Ingredient (
  Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  Item_Id NUMBER,
  Ingredient_Id NUMBER,
  CONSTRAINT fk_menu_ingredient_item FOREIGN KEY (Item_Id) REFERENCES Item(Id),
  CONSTRAINT fk_menu_ingredient_ingredient FOREIGN KEY (Ingredient_Id) REFERENCES Ingredient(Id)
);
