namespace Restaurant_Management;

public class Database
{
    public static void CreateTables(OracleConnection connection){
        connection.Open();

        // Create tables if they don't exist
        CreateTable(connection, "Employee", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Manager_Id NUMBER,
            First_Name VARCHAR2(100) NOT NULL,
            Last_Name VARCHAR2(100) NOT NULL,
            Phone_Number VARCHAR2(20) NOT NULL,
            Address VARCHAR2(255) NOT NULL,
            Salary_per_Hour NUMBER(10,4) NOT NULL,
            Section_Id NUMBER NOT NULL"
        );

        CreateTable(connection, "Employee_WorkDay", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Date"" DATE,
            Starts TIMESTAMP NOT NULL,
            Ends TIMESTAMP NOT NULL,
            Working_Hours NUMBER(38) NOT NULL,
            Note VARCHAR2(255),
            Employee_Id NUMBER NOT NULL"
        );

        CreateTable(connection, "Section", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR2(100) NOT NULL UNIQUE"
        );

        CreateTable(connection, "Item", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Title VARCHAR2(50) NOT NULL,
            Description VARCHAR2(250),
            Price NUMBER(10,4) NOT NULL,
            Added TIMESTAMP NOT NULL,
            Rating NUMBER(3,2),
            Category_Id NUMBER NOT NULL"
        );

        CreateTable(connection, "Order", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Date"" TIMESTAMP NOT NULL,
            Price NUMBER(12,2) NOT NULL,
            Employee_Id NUMBER NOT NULL,
            Table_Id NUMBER NOT NULL,
            Receipt_Id NUMBER"
        );

        CreateTable(connection, "Order_Item", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Quantity NUMBER(38) NOT NULL,
            Unit_Price NUMBER(10,4) NOT NULL,
            Order_Id NUMBER NOT NULL,
            Item_Id NUMBER NOT NULL"
        );

        CreateTable(connection, "Table", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Number"" NUMBER(38) NOT NULL UNIQUE,
            Status VARCHAR2(100) NOT NULL"
        );

        CreateTable(connection, "Receipt", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Date"" TIMESTAMP NOT NULL,
            Sub_Total NUMBER(12,2),
            Taxes NUMBER(10,4) NOT NULL,
            Discount NUMBER(10,4) NOT NULL,
            Total NUMBER(12,2) NOT NULL,
            Table_Id NUMBER NOT NULL"
        );

        CreateTable(connection, "Category", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR2(100) NOT NULL UNIQUE"
        );

        CreateTable(connection, "Supplier", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Full_Name VARCHAR2(255) NOT NULL,
            Phone_Number VARCHAR2(20) NOT NULL"
        );

        CreateTable(connection, "Ingredient", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR2(255) NOT NULL UNIQUE,
            Quantity NUMBER(12,2) NOT NULL"
        );

        CreateTable(connection, "Item_Ingredient", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Item_Id NUMBER NOT NULL,
            Ingredient_Id NUMBER NOT NULL"
        );
        CreateTable(connection, "Supplier_Ingredient", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Ingredient_Id NUMBER NOT NULL,
            Supplier_Id NUMBER NOT NULL,
            ""Date"" TIMESTAMP NOT NULL,
            Quantity NUMBER(38) NOT NULL,
            Price NUMBER(12,2) NOT NULL"
        );

        // Add foreign key constraints if they don't exist
        AddForeignKeyConstraint(connection, "Employee", "FK_Employee_Section_Id", "Section_Id", "Section", "Id",false);
        AddForeignKeyConstraint(connection, "Employee_WorkDay", "FK_Employee_WorkDay_Employee_Id", "Employee_Id", "Employee", "Id",false);
        AddForeignKeyConstraint(connection, "Employee", "FK_Employee_Manager_Id", "Manager_Id", "Employee", "Id",false);
        AddForeignKeyConstraint(connection, "Receipt", "FK_Receipt_Table_Id", "Table_Id", "Table", "Id",false);
        AddForeignKeyConstraint(connection, "Order_Item", "FK_Order_Item_Item_Id", "Item_Id", "Item", "Id",false);
        AddForeignKeyConstraint(connection, "Order_Item", "FK_Order_Item_Order_Id", "Order_Id", "Order", "Id",true);
        AddForeignKeyConstraint(connection, "Order", "FK_Order_Employee_Id", "Employee_Id", "Employee", "Id",false);
        AddForeignKeyConstraint(connection, "Order", "FK_Order_Table_Id", "Table_Id", "Table", "Id",false);
        AddForeignKeyConstraint(connection, "Order", "FK_Order_Receipt_Id", "Receipt_Id", "Receipt", "Id",true);
        AddForeignKeyConstraint(connection, "Item", "FK_Item_Category_Id", "Category_Id", "Category", "Id",false);
        AddForeignKeyConstraint(connection, "Item_Ingredient", "FK_Item_Ingredient_Item_Id", "Item_Id", "Item", "Id",false);
        AddForeignKeyConstraint(connection, "Item_Ingredient", "FK_Item_Ingredient_Ingredient_Id", "Ingredient_Id", "Ingredient", "Id",false);
        AddForeignKeyConstraint(connection, "Supplier_Ingredient", "FK_Supplier_Ingredient_Ingredient_Id", "Ingredient_Id", "Ingredient", "Id",false);
        AddForeignKeyConstraint(connection, "Supplier_Ingredient", "FK_Supplier_Ingredient_Supplier_Id", "Supplier_Id", "Supplier", "Id",false);

        connection.Close();
        
        Console.WriteLine("Tables created successfully.");
    }

    private static void CreateTable(OracleConnection connection, string tableName, string tableDefinition)
    {
        if (!TableExists(connection, tableName))
        {
            string createTableQuery = $"CREATE TABLE \"{tableName}\" ({tableDefinition})";
            ExecuteQuery(connection, createTableQuery);
        }else{
            Console.WriteLine("table found before");
        }
    }

    private static bool TableExists(OracleConnection connection ,string tableName)
    {
        using OracleCommand command = new($"SELECT COUNT(*) FROM USER_TABLES WHERE TABLE_NAME = '{tableName}'", connection);
        int count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    private static void AddForeignKeyConstraint(OracleConnection connection, string tableName, string constraintName, string columnName, string referencedTable, string referencedColumnName , bool OnDeleteCascade)
    {
        if (!ForeignKeyConstraintExists(connection, tableName, constraintName))
        {
            string addForeignKeyQuery;
            if(OnDeleteCascade)
                addForeignKeyQuery = $"ALTER TABLE \"{tableName}\" ADD CONSTRAINT {constraintName} FOREIGN KEY ({columnName}) REFERENCES \"{referencedTable}\"({referencedColumnName}) ON DELETE CASCADE";
            else
                addForeignKeyQuery = $"ALTER TABLE \"{tableName}\" ADD CONSTRAINT {constraintName} FOREIGN KEY ({columnName}) REFERENCES \"{referencedTable}\"({referencedColumnName})";
            ExecuteQuery(connection, addForeignKeyQuery);
        }
        else 
        {
            Console.WriteLine($"constraint {constraintName} found before");
        }
    }

    private static bool ForeignKeyConstraintExists(OracleConnection connection, string tableName, string constraintName)
    {
        using (OracleCommand command = new($"SELECT COUNT(*) FROM ALL_CONS_COLUMNS WHERE TABLE_NAME = '{tableName}' AND CONSTRAINT_NAME = '{constraintName.ToUpper()}'", connection))
        {
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }

    private static void ExecuteQuery(OracleConnection connection, string query)
    {
        using OracleCommand command = new(query, connection);
        command.ExecuteNonQuery();
    }
}