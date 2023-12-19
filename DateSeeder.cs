namespace Restaurant_Management;

public class DataSeeder(IEmployee _employeeRepository, IEmployee_WorkDay _employeeWorkDayRepository, 
    ISection _sectionRepository, IItem _itemRepository, IOrder _orderRepository, IOrder_Item _orderItemRepository,
    ITable _tableRepository, ICategory _categoryRepository, ISupplier _supplierRepository,
    IIngredient _ingredientRepository, IItem_Ingredient _itemIngredientRepository,
    ISupplier_Ingredient _supplierIngredientRepository, IReceipt _receipt)
{
    public void SeedData()
    {
        SeedSections();
        SeedEmployees();
        SeedEmployeeWorkDays();
        SeedCategories();
        SeedItems();
        SeedTables();
        SeedSuppliers();
        SeedIngredients();
        SeedSupplierIngredients();
        SeedItemIngredients();
        SeedOrders();
        SeedOrderItems();
    }

    private void SeedItemIngredients()
    {
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 1, Ingredient_Id = 1 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 1, Ingredient_Id = 3 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 2, Ingredient_Id = 3 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 2, Ingredient_Id = 4 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 3, Ingredient_Id = 1 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 3, Ingredient_Id = 2 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 4, Ingredient_Id = 3 });
        _itemIngredientRepository.Add(new Item_Ingredient { Item_Id = 4, Ingredient_Id = 4 });
        Console.WriteLine("8 Rows added to Item_Ingredient Table");
    }

    private void SeedEmployees()
    {
        _employeeRepository.Add(new Employee
        {
            FirstName = "Nader",
            LastName = "Doe",
            PhoneNumber = "123456789",
            Address = "123 Main St",
            SalaryPerHour = 15.5m,
            SectionId = 1,
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Jane",
            LastName = "Smith",
            PhoneNumber = "987654321",
            Address = "456 Oak St",
            SalaryPerHour = 20.0m,
            SectionId = 2,
            ManagerId = 1
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Alice",
            LastName = "Johnson",
            PhoneNumber = "5551234567",
            Address = "789 Pine St",
            SalaryPerHour = 18.75m,
            SectionId = 1,
            ManagerId = 1
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Bob",
            LastName = "Williams",
            PhoneNumber = "4449876543",
            Address = "321 Cedar St",
            SalaryPerHour = 17.0m,
            SectionId = 3,
            ManagerId = 1
        });
        Console.WriteLine("4 Rows added to Employee Table");
    }

    private void SeedSections()
    {
        _sectionRepository.Add(new Section
        {
            Name = "Kitchen"
        });

        _sectionRepository.Add(new Section
        {
            Name = "Front of House"
        });

        _sectionRepository.Add(new Section
        {
            Name = "Bar"
        });

        _sectionRepository.Add(new Section
        {
            Name = "Patio"
        });
        Console.WriteLine("4 Rows added to Section Table");
    }

    private void SeedItems()
    {
        _itemRepository.Add(new Item
        {
            Title = "Burger",
            Description = "Classic beef burger",
            Price = 9.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });

        _itemRepository.Add(new Item
        {
            Title = "Caesar Salad",
            Description = "Fresh romaine lettuce with Caesar dressing",
            Price = 7.49m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });

        _itemRepository.Add(new Item
        {
            Title = "Margherita Pizza",
            Description = "Classic tomato and mozzarella pizza",
            Price = 12.99m,
            Added = DateTime.Now,
            Rating = 3,
            Category_Id = 3
        });

        _itemRepository.Add(new Item
        {
            Title = "Pasta Carbonara",
            Description = "Spaghetti with creamy bacon and egg sauce",
            Price = 11.50m,
            Added = DateTime.Now,
            Rating = 2,
            Category_Id = 2
        });
        Console.WriteLine("4 Rows added to Item Table");

    }

    private void SeedTables()
    {
        for (int i = 1; i <= 4; i++)
        {
            _tableRepository.Add(new Table { Number = i, Status = i % 2 == 0 ? "Available" : "Unavailable" });
        }
        Console.WriteLine("4 Rows added to Table Table");
    }

    private void SeedCategories()
    {
        _categoryRepository.Add(new Category { Name = "Appetizers" });
        _categoryRepository.Add(new Category { Name = "Sides" });
        _categoryRepository.Add(new Category { Name = "Mains" });
        _categoryRepository.Add(new Category { Name = "Desserts" });
        _categoryRepository.Add(new Category { Name = "Drinks" });
        Console.WriteLine("4 Rows added to Category Table");
    }

    private void SeedSuppliers()
    {
        _supplierRepository.Add(new Supplier { Full_Name = "Abu_Andrew", Phone_Number = "0946145738" });
        _supplierRepository.Add(new Supplier { Full_Name = "Mr_Sure21", Phone_Number = "0965490736" });
        _supplierRepository.Add(new Supplier { Full_Name = "Ismael", Phone_Number = "0933987231" });
        _supplierRepository.Add(new Supplier { Full_Name = "Abu_Yasser", Phone_Number = "0999823453" });
        Console.WriteLine("4 Rows added to Suppliers Table");
    }

    private void SeedIngredients()
    {
        _ingredientRepository.Add(new Ingredient
        {
            Name = "Ground Beef",
            Quantity = 5
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Romaine Lettuce",
            Quantity = 2.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Tomato Sauce",
            Quantity = 3.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Spaghetti",
            Quantity = 1.5m
        });
        Console.WriteLine("4 Rows added to Ingredient Table");
    }

    private void SeedEmployeeWorkDays()
    {
        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date,
            Starts = DateTime.Now.Date.AddHours(9),
            Ends = DateTime.Now.Date.AddHours(17),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 1
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-1),
            Starts = DateTime.Now.Date.AddDays(-1).AddHours(11),
            Ends = DateTime.Now.Date.AddDays(-1).AddHours(19),
            WorkingHours = 8,
            Note = "Evening shift",
            Employee_Id = 2
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-2),
            Starts = DateTime.Now.Date.AddDays(-2).AddHours(8),
            Ends = DateTime.Now.Date.AddDays(-2).AddHours(16),
            WorkingHours = 8,
            Note = "Morning shift",
            Employee_Id = 3
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-3),
            Starts = DateTime.Now.Date.AddDays(-3).AddHours(10),
            Ends = DateTime.Now.Date.AddDays(-3).AddHours(18),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 4
        });
        Console.WriteLine("4 Rows added to Employee_WorkDay Table");
    }

    private void SeedOrders()
    {
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now,
            Price = 25.99m,
            Employee_Id = 1,
            Table_Id = 1
        });

        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-1),
            Price = 15.49m,
            Employee_Id = 2,
            Table_Id = 2
        });

        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-2),
            Price = 32.99m,
            Employee_Id = 3,
            Table_Id = 3
        });

        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-3),
            Price = 18.50m,
            Employee_Id = 4,
            Table_Id = 4
        });
        Console.WriteLine("4 Rows added to Order Table");
    }

    private void SeedOrderItems()
    {
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 9.99m,
            Order_Id = 1,
            Item_Id = 1
        });

        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 7.49m,
            Order_Id = 2,
            Item_Id = 2
        });

        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 3,
            Item_Id = 3
        });

        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 4,
            Item_Id = 4
        });
        Console.WriteLine("4 Rows added to Order_Item Table");
    }

    private void SeedSupplierIngredients()
    {
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 1,
            Supplier_Id = 1,
            Date = DateTime.Now,
            Quantity = 10,
            Price = 50445.03m
        });

        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 4,
            Supplier_Id = 2,
            Date = DateTime.Now,
            Quantity = 5,
            Price = 20.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 2,
            Supplier_Id = 3,
            Date = DateTime.Now,
            Quantity = 5,
            Price = 23453.4m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 3,
            Supplier_Id = 3,
            Date = DateTime.Now,
            Quantity = 5,
            Price = 234530.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 2,
            Supplier_Id = 3,
            Date = DateTime.Now,
            Quantity = 5,
            Price = 20435.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 1,
            Supplier_Id = 2,
            Date = DateTime.Now,
            Quantity = 5,
            Price = 22340.0m
        });
        Console.WriteLine("6 Rows added to Supplier_Ingredient Table");
    }
}