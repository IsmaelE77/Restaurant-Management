namespace Restaurant_Management;

public class Database
{
    public static void CreateTables(OracleConnection connection){
        connection.Open();

        // Create tables if they don't exist
        CreateTable(connection, "Employee", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Manager_Id NUMBER,
            First_Name VARCHAR2(100),
            Last_Name VARCHAR2(100),
            Phone_Number VARCHAR2(20),
            Address VARCHAR2(255),
            Salary_per_Hour NUMBER(10,4),
            Section_Id NUMBER"
        );

        CreateTable(connection, "Employee_WorkDay", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Date"" DATE,
            Starts TIMESTAMP,
            Ends TIMESTAMP,
            Working_Hours NUMBER(38),
            Note VARCHAR2(255),
            Employee_Id NUMBER"
        );

        CreateTable(connection, "Section", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR2(100)"
        );

        CreateTable(connection, "Item", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Title VARCHAR2(50),
            Description VARCHAR2(250),
            Price NUMBER(10,4),
            Added TIMESTAMP,
            Rating NUMBER(3,2),
            Category_Id NUMBER"
        );

        CreateTable(connection, "Order", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Date"" TIMESTAMP,
            Price NUMBER(10,4),
            Employee_Id NUMBER,
            Table_Id NUMBER,
            Receipt_Id NUMBER"
        );

        CreateTable(connection, "Order_Item", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Quantity NUMBER(38),
            Unit_Price NUMBER(10,4),
            Order_Id NUMBER,
            Item_Id NUMBER"
        );

        CreateTable(connection, "Table", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Number"" NUMBER(38),
            Status VARCHAR2(100)"
        );

        CreateTable(connection, "Receipt", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            ""Date"" TIMESTAMP,
            Sub_Total NUMBER(10,4),
            Taxes NUMBER(10,4),
            Discount NUMBER(10,4),
            Total NUMBER(10,4),
            Table_Id NUMBER"
        );

        CreateTable(connection, "Category", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR2(100)"
        );

        CreateTable(connection, "Supplier", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Full_Name VARCHAR2(255),
            Phone_Number VARCHAR2(20)"
        );

        CreateTable(connection, "Ingredient", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR2(255),
            Price NUMBER(10,4),
            Quantity NUMBER(38),
            Supplier_Id NUMBER"
        );

        CreateTable(connection, "Item_Ingredient", @"
            Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Item_Id NUMBER,
            Ingredient_Id NUMBER"
        );

        // Add foreign key constraints if they don't exist
        AddForeignKeyConstraint(connection, "Employee", "FK_Employee_Section_Id", "Section_Id", "Section", "Id");
        AddForeignKeyConstraint(connection, "Employee_WorkDay", "FK_Employee_WorkDay_Employee_Id", "Employee_Id", "Employee", "Id");
        AddForeignKeyConstraint(connection, "Employee", "FK_Employee_Manager_Id", "Manager_Id", "Employee", "Id");
        AddForeignKeyConstraint(connection, "Receipt", "FK_Receipt_Table_Id", "Table_Id", "Table", "Id");
        AddForeignKeyConstraint(connection, "Order_Item", "FK_Order_Item_Item_Id", "Item_Id", "Item", "Id");
        AddForeignKeyConstraint(connection, "Order_Item", "FK_Order_Item_Order_Id", "Order_Id", "Order", "Id");
        AddForeignKeyConstraint(connection, "Order", "FK_Order_Employee_Id", "Employee_Id", "Employee", "Id");
        AddForeignKeyConstraint(connection, "Order", "FK_Order_Table_Id", "Table_Id", "Table", "Id");
        AddForeignKeyConstraint(connection, "Order", "FK_Order_Receipt_Id", "Receipt_Id", "Receipt", "Id");
        AddForeignKeyConstraint(connection, "Item", "FK_Item_Category_Id", "Category_Id", "Category", "Id");
        AddForeignKeyConstraint(connection, "Ingredient", "FK_Ingredient_Supplier_Id", "Supplier_Id", "Supplier", "Id");
        AddForeignKeyConstraint(connection, "Item_Ingredient", "FK_Item_Ingredient_Item_Id", "Item_Id", "Item", "Id");
        AddForeignKeyConstraint(connection, "Item_Ingredient", "FK_Item_Ingredient_Ingredient_Id", "Ingredient_Id", "Ingredient", "Id");

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
        using (OracleCommand command = new($"SELECT COUNT(*) FROM USER_TABLES WHERE TABLE_NAME = '{tableName}'", connection))
        {
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }

    private static void AddForeignKeyConstraint(OracleConnection connection, string tableName, string constraintName, string columnName, string referencedTable, string referencedColumnName)
    {
        if (!ForeignKeyConstraintExists(connection, tableName, constraintName))
        {
            string addForeignKeyQuery = $"ALTER TABLE \"{tableName}\" ADD CONSTRAINT {constraintName} FOREIGN KEY ({columnName}) REFERENCES \"{referencedTable}\"({referencedColumnName})";
            ExecuteQuery(connection, addForeignKeyQuery);
        }else{
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
        using (OracleCommand command = new(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }

}
