namespace Restaurant_Management;

public class Program
{
    private const string CONNECTION_STRING = "DATA SOURCE=localhost:1521/XEPDB1;PERSIST SECURITY INFO=True;USER ID=admin;Password=admin";
    public static OracleConnection GetConnection()
    {
        return new OracleConnection(CONNECTION_STRING);
    }
    public static void SeedData()
    {
        DataSeeder dataSeeder = new(GetIEmployee(), GetIEmployee_WorkDay(), GetISection(), GetIItem(),
            GetIOrder(), GetIOrder_Item(), GetITable(), GetICategory(), GetISupplier(),
            GetIIngredient(), GetIItem_Ingredient(), GetISupplier_Ingredient(), GetIReceipt());
        //Seed the tables with initial data
        dataSeeder.SeedData();
    }
    public static ICategory GetICategory() => new CategoryRepository(CONNECTION_STRING);
    public static IEmployee_WorkDay GetIEmployee_WorkDay() => new Employee_WorkDayRepository(CONNECTION_STRING);
    public static ISection GetISection() => new SectionRepository(CONNECTION_STRING);
    public static IOrder_Item GetIOrder_Item() => new Order_ItemRepository(CONNECTION_STRING);
    public static ITable GetITable() => new TableRepository(CONNECTION_STRING);
    public static IItem_Ingredient GetIItem_Ingredient() => new Item_IngredientRepository(CONNECTION_STRING);
    public static ISupplier_Ingredient GetISupplier_Ingredient() => new Supplier_IngredientRepository(CONNECTION_STRING);
    public static IEmployee GetIEmployee() => new EmployeeRepository(CONNECTION_STRING, GetIEmployee_WorkDay());
    public static IItem GetIItem() => new ItemRepository(CONNECTION_STRING, GetIItem_Ingredient(), GetIOrder_Item());
    public static IOrder GetIOrder() => new OrderRepository(CONNECTION_STRING, GetIOrder_Item());
    public static ISupplier GetISupplier() => new SupplierRepository(CONNECTION_STRING, GetISupplier_Ingredient());
    public static IIngredient GetIIngredient() => new IngredientRepository(CONNECTION_STRING, GetISupplier_Ingredient(), GetIItem_Ingredient());
    public static IReceipt GetIReceipt() => new ReceiptRepository(CONNECTION_STRING, GetIOrder());


    public static void Main()
    {
        Database.CreateTables(GetConnection());
    }
}