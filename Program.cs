namespace Restaurant_Management;

public class Program
{
    private const string CONNECTION_STRING = "DATA SOURCE=localhost:1521/XEPDB1;PERSIST SECURITY INFO=True;USER ID=admin;Password=admin";
    public static OracleConnection GetConnection()
    {
        return new OracleConnection(CONNECTION_STRING);
    }

    public static void Main()
    {
        //Create Tables
        Database.CreateTables(GetConnection());
        //Implement the interfaces
        IEmployee_WorkDay employee_WorkDay = new Employee_WorkDayRepository(CONNECTION_STRING);
        ISection sectionRepository = new SectionRepository(CONNECTION_STRING);
        IOrder_Item orderItemRepository = new Order_ItemRepository(CONNECTION_STRING);
        ITable tableRepository = new TableRepository(CONNECTION_STRING);
        ICategory categoryRepository = new CategoryRepository(CONNECTION_STRING);
        IItem_Ingredient itemIngredientRepository = new Item_IngredientRepository(CONNECTION_STRING);
        ISupplier_Ingredient supplierIngredientRepository = new Supplier_IngredientRepository(CONNECTION_STRING);
        IEmployee employeeRepository = new EmployeeRepository(CONNECTION_STRING, employee_WorkDay);
        IItem itemRepository = new ItemRepository(CONNECTION_STRING, itemIngredientRepository, orderItemRepository);
        IOrder orderRepository = new OrderRepository(CONNECTION_STRING, orderItemRepository);
        ISupplier supplierRepository = new SupplierRepository(CONNECTION_STRING, supplierIngredientRepository);
        IIngredient ingredientRepository = new IngredientRepository(CONNECTION_STRING, supplierIngredientRepository, itemIngredientRepository);
        IReceipt receipt = new ReceiptRepository(CONNECTION_STRING, orderRepository);
        //Pass the interfaces to the DateSeeder Class 
        DataSeeder dataSeeder = new(employeeRepository, employee_WorkDay, sectionRepository, itemRepository,
            orderRepository, orderItemRepository, tableRepository, categoryRepository, supplierRepository,
            ingredientRepository, itemIngredientRepository, supplierIngredientRepository, receipt);
        //Seed the tables with initial data
        dataSeeder.SeedData();
    }
}