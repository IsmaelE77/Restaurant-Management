namespace Restaurant_Management;

public class Program
{
    private const string CONNECTION_STRING = "DATA SOURCE=localhost:1521/XEPDB1;PERSIST SECURITY INFO=True;USER ID=admin;Password=admin";

    public static void Main()
    {
        Database.CreateTables(GetConnection());
        IEmployee employeeRepository = new EmployeeRepository(CONNECTION_STRING);
        ISection sectionRepository = new SectionRepository(CONNECTION_STRING);
        IItem itemRepository = new ItemRepository(CONNECTION_STRING);
        IOrder orderRepository = new OrderRepository(CONNECTION_STRING);
        IOrderItem orderItemRepository = new OrderItemRepository(CONNECTION_STRING);
        ITable tableRepository = new TableRepository(CONNECTION_STRING);
        ICategory categoryRepository = new CategoryRepository(CONNECTION_STRING);
        ISupplier supplierRepository = new SupplierRepository(CONNECTION_STRING);
        IIngredient ingredientRepository = new IngredientRepository(CONNECTION_STRING);
        IItem_Ingredient itemIngredientRepository = new Item_IngredientRepository(CONNECTION_STRING);
        ISupplier_Ingredient supplierIngredientRepository = new Supplier_IngredientRepository(CONNECTION_STRING);
        // Create the DataSeeder object
        DataSeeder dataSeeder = new(
            employeeRepository,
            sectionRepository,
            itemRepository,
            orderRepository,
            orderItemRepository,
            tableRepository,
            categoryRepository,
            supplierRepository,
            ingredientRepository,
            itemIngredientRepository,
            supplierIngredientRepository);
        dataSeeder.SeedData();
    }
    public static OracleConnection GetConnection()
    {
        return new OracleConnection(CONNECTION_STRING);
    }
}