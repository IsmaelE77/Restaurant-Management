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
        _employeeRepository.Add(new Employee
        {
            FirstName = "Sarah",
            LastName = "Brown",
            PhoneNumber = "777888999",
            Address = "555 Elm St",
            SalaryPerHour = 22.5m,
            SectionId = 2,
            ManagerId = 2
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Chris",
            LastName = "Miller",
            PhoneNumber = "333222111",
            Address = "999 Maple St",
            SalaryPerHour = 19.25m,
            SectionId = 3,
            ManagerId = 2
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Eva",
            LastName = "Chen",
            PhoneNumber = "111444777",
            Address = "777 Birch St",
            SalaryPerHour = 21.0m,
            SectionId = 1,
            ManagerId = 3
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "David",
            LastName = "Lopez",
            PhoneNumber = "888555222",
            Address = "222 Pine St",
            SalaryPerHour = 16.8m,
            SectionId = 2,
            ManagerId = 3
        });
        _employeeRepository.Add(new Employee
        {
            FirstName = "Emily",
            LastName = "Wilson",
            PhoneNumber = "666999111",
            Address = "111 Oak St",
            SalaryPerHour = 18.0m,
            SectionId = 3,
            ManagerId = 4
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "John",
            LastName = "Lee",
            PhoneNumber = "333666888",
            Address = "888 Cedar St",
            SalaryPerHour = 23.0m,
            SectionId = 1,
            ManagerId = 4
        });
        _employeeRepository.Add(new Employee
        {
            FirstName = "Grace",
            LastName = "Turner",
            PhoneNumber = "111222333",
            Address = "333 Birch St",
            SalaryPerHour = 19.5m,
            SectionId = 2,
            ManagerId = 5
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Tom",
            LastName = "Harrison",
            PhoneNumber = "777444555",
            Address = "444 Cedar St",
            SalaryPerHour = 16.0m,
            SectionId = 1,
            ManagerId = 5
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Olivia",
            LastName = "Murray",
            PhoneNumber = "555888777",
            Address = "777 Maple St",
            SalaryPerHour = 21.75m,
            SectionId = 3,
            ManagerId = 6
        });

        _employeeRepository.Add(new Employee
        {
            FirstName = "Max",
            LastName = "Turner",
            PhoneNumber = "222555444",
            Address = "999 Oak St",
            SalaryPerHour = 18.25m,
            SectionId = 2,
            ManagerId = 6
        });
        Console.WriteLine("14 Rows added to Employee Table");
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
        _sectionRepository.Add(new Section
        {
            Name = "VIP Lounge"
        });

        _sectionRepository.Add(new Section
        {
            Name = "Private Dining Room"
        });

        _sectionRepository.Add(new Section
        {
            Name = "Rooftop"
        });
        Console.WriteLine("7 Rows added to Section Table");
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
        _itemRepository.Add(new Item
        {
            Title = "Chicken Alfredo",
            Description = "Fettuccine pasta with creamy chicken Alfredo sauce",
            Price = 14.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Mushroom Risotto",
            Description = "Creamy mushroom risotto with Parmesan cheese",
            Price = 10.50m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Steak Frites",
            Description = "Grilled steak with crispy French fries",
            Price = 19.75m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Fish Tacos",
            Description = "Grilled fish tacos with cilantro lime sauce",
            Price = 13.25m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Vegetarian Sushi",
            Description = "Assorted vegetarian sushi rolls",
            Price = 16.50m,
            Added = DateTime.Now,
            Rating = 3,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Chocolate Fondue",
            Description = "Rich chocolate fondue with assorted dippings",
            Price = 8.99m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Caprese Sandwich",
            Description = "Fresh mozzarella, tomatoes, and basil on ciabatta",
            Price = 9.75m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Shrimp Scampi",
            Description = "Garlic butter shrimp over linguine",
            Price = 18.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Cobb Salad",
            Description = "Classic Cobb salad with grilled chicken",
            Price = 11.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "BBQ Ribs",
            Description = "Slow-cooked BBQ ribs with coleslaw",
            Price = 22.99m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Vegetable Stir-Fry",
            Description = "Assorted vegetables stir-fried in soy sauce",
            Price = 14.25m,
            Added = DateTime.Now,
            Rating = 3,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Grilled Salmon",
            Description = "Grilled salmon fillet with lemon butter sauce",
            Price = 16.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Vegetable Lasagna",
            Description = "Layers of pasta, vegetables, and creamy bechamel",
            Price = 13.75m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Sushi Boat",
            Description = "Assorted sushi served on a boat-shaped platter",
            Price = 24.50m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Tiramisu",
            Description = "Classic Italian tiramisu dessert",
            Price = 8.49m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Lemon Sorbet",
            Description = "Refreshing lemon-flavored sorbet",
            Price = 6.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Beef Stir-Fry",
            Description = "Tender beef strips stir-fried with vegetables",
            Price = 15.25m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Shrimp and Avocado Salad",
            Description = "Grilled shrimp with avocado over mixed greens",
            Price = 17.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Chicken Parmesan",
            Description = "Breaded chicken with marinara and melted cheese",
            Price = 18.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Lobster Ravioli",
            Description = "Ravioli stuffed with lobster meat in creamy sauce",
            Price = 22.75m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Vegetable Tempura",
            Description = "Assorted vegetables in crispy tempura batter",
            Price = 11.99m,
            Added = DateTime.Now,
            Rating = 3,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Key Lime Pie",
            Description = "Refreshing key lime pie with graham cracker crust",
            Price = 7.95m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Hawaiian Poke Bowl",
            Description = "Fresh tuna and vegetables in a Hawaiian poke bowl",
            Price = 19.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Ratatouille",
            Description = "Provencal vegetable stew with herbs and olive oil",
            Price = 12.25m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Caramelized Onion Tart",
            Description = "Savory tart with caramelized onions and Gruyere cheese",
            Price = 14.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Mango Sorbet",
            Description = "Sweet and tangy mango-flavored sorbet",
            Price = 6.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Teriyaki Chicken Bowl",
            Description = "Grilled chicken with teriyaki sauce over rice",
            Price = 14.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Cajun Shrimp Pasta",
            Description = "Spicy Cajun-style shrimp pasta",
            Price = 16.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Caprese Pizza",
            Description = "Pizza with fresh tomatoes, mozzarella, and basil",
            Price = 13.75m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Chicken Quesadilla",
            Description = "Grilled chicken and cheese stuffed in a quesadilla",
            Price = 10.99m,
            Added = DateTime.Now,
            Rating = 3,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Vegetable Curry",
            Description = "Mixed vegetables in flavorful curry sauce",
            Price = 11.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Peach Cobbler",
            Description = "Warm peach cobbler with a scoop of vanilla ice cream",
            Price = 8.95m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Pesto Shrimp Linguine",
            Description = "Linguine pasta with pesto sauce and shrimp",
            Price = 17.25m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Spinach and Artichoke Dip",
            Description = "Creamy spinach and artichoke dip with tortilla chips",
            Price = 9.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Beef Tacos",
            Description = "Seasoned beef in soft corn tortillas with toppings",
            Price = 12.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Mango Lassi",
            Description = "Refreshing yogurt-based drink with mango",
            Price = 4.99m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 4
        });
        _itemRepository.Add(new Item
        {
            Title = "Chicken Marsala",
            Description = "Chicken breast in Marsala wine sauce with mushrooms",
            Price = 18.99m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 1
        });
        _itemRepository.Add(new Item
        {
            Title = "Gourmet Macaroni and Cheese",
            Description = "Macaroni pasta in rich and creamy cheese sauce",
            Price = 13.75m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 3
        });
        _itemRepository.Add(new Item
        {
            Title = "Shrimp Po' Boy Sandwich",
            Description = "Crispy shrimp in a baguette with lettuce and remoulade",
            Price = 15.50m,
            Added = DateTime.Now,
            Rating = 4,
            Category_Id = 2
        });
        _itemRepository.Add(new Item
        {
            Title = "Chocolate Mousse",
            Description = "Decadent chocolate mousse dessert",
            Price = 7.99m,
            Added = DateTime.Now,
            Rating = 5,
            Category_Id = 4
        });
        Console.WriteLine("44 Rows added to Item Table");
    }


    private void SeedTables()
    {
        for (int i = 1; i <= 26; i++)
        {
            _tableRepository.Add(new Table { Number = i, Status = i % 2 == 0 ? "Available" : "Unavailable" });
        }
        Console.WriteLine("26 Rows added to Table Table");
    }

    private void SeedCategories()
    {
        _categoryRepository.Add(new Category { Name = "Appetizers" });
        _categoryRepository.Add(new Category { Name = "Sides" });
        _categoryRepository.Add(new Category { Name = "Mains" });
        _categoryRepository.Add(new Category { Name = "Desserts" });
        _categoryRepository.Add(new Category { Name = "Drinks" });
        _categoryRepository.Add(new Category { Name = "Salads" });
        _categoryRepository.Add(new Category { Name = "Seafood" });
        _categoryRepository.Add(new Category { Name = "Vegetarian" });
        _categoryRepository.Add(new Category { Name = "Specials" });
        _categoryRepository.Add(new Category { Name = "Breakfast" });
        _categoryRepository.Add(new Category { Name = "Pasta" });
        _categoryRepository.Add(new Category { Name = "Grilled" });
        _categoryRepository.Add(new Category { Name = "Mexican" });
        _categoryRepository.Add(new Category { Name = "Soups" });
        Console.WriteLine("14 Rows added to Category Table");
    }

    private void SeedSuppliers()
    {
        _supplierRepository.Add(new Supplier { Full_Name = "Abu_Andrew", Phone_Number = "0946145738" });
        _supplierRepository.Add(new Supplier { Full_Name = "Mr_Sure21", Phone_Number = "0965490736" });
        _supplierRepository.Add(new Supplier { Full_Name = "Ismael", Phone_Number = "0933987231" });
        _supplierRepository.Add(new Supplier { Full_Name = "Abu_Yasser", Phone_Number = "0999823453" });
        _supplierRepository.Add(new Supplier { Full_Name = "Global_Foods", Phone_Number = "0912345678" });
        _supplierRepository.Add(new Supplier { Full_Name = "FreshProduce_Co", Phone_Number = "0976543210" });
        _supplierRepository.Add(new Supplier { Full_Name = "Quality_Meats", Phone_Number = "0987654321" });
        _supplierRepository.Add(new Supplier { Full_Name = "Beverage_Distributors", Phone_Number = "0923456789" });
        _supplierRepository.Add(new Supplier { Full_Name = "Tech_Supplies", Phone_Number = "0956789012" });
        _supplierRepository.Add(new Supplier { Full_Name = "Office_Solutions", Phone_Number = "0912345678" });
        _supplierRepository.Add(new Supplier { Full_Name = "Clothing_Warehouse", Phone_Number = "0976543210" });
        _supplierRepository.Add(new Supplier { Full_Name = "Electronics_Direct", Phone_Number = "0987654321" });
        _supplierRepository.Add(new Supplier { Full_Name = "Home_Decor_Supplies", Phone_Number = "0923456789" });
        _supplierRepository.Add(new Supplier { Full_Name = "Auto_Parts_Supply", Phone_Number = "0956789012" });
        _supplierRepository.Add(new Supplier { Full_Name = "Garden_Supplies", Phone_Number = "0912345678" });
        Console.WriteLine("15 Rows added to Suppliers Table");
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

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Chicken Breast",
            Quantity = 3.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Mozzarella Cheese",
            Quantity = 2.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Lettuce",
            Quantity = 1.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Olive Oil",
            Quantity = 0.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Garlic",
            Quantity = 0.75m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Onion",
            Quantity = 1.2m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Bell Pepper",
            Quantity = 1.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Bread Crumbs",
            Quantity = 0.8m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Parmesan Cheese",
            Quantity = 1.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Basil",
            Quantity = 0.3m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Tomato",
            Quantity = 2.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Ground Turkey",
            Quantity = 2.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Spinach",
            Quantity = 1.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Mushrooms",
            Quantity = 0.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Penne Pasta",
            Quantity = 1.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Soy Sauce",
            Quantity = 0.75m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Ginger",
            Quantity = 0.3m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Cauliflower",
            Quantity = 1.2m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Cumin",
            Quantity = 1.0m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Cayenne Pepper",
            Quantity = 0.5m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Cheddar Cheese",
            Quantity = 0.8m
        });

        _ingredientRepository.Add(new Ingredient
        {
            Name = "Black Beans",
            Quantity = 1.5m
        });

        Console.WriteLine("25 Rows added to Ingredient Table");
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

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-4),
            Starts = DateTime.Now.Date.AddDays(-4).AddHours(9),
            Ends = DateTime.Now.Date.AddDays(-4).AddHours(17),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 1
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-5),
            Starts = DateTime.Now.Date.AddDays(-5).AddHours(11),
            Ends = DateTime.Now.Date.AddDays(-5).AddHours(19),
            WorkingHours = 8,
            Note = "Evening shift",
            Employee_Id = 2
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-6),
            Starts = DateTime.Now.Date.AddDays(-6).AddHours(8),
            Ends = DateTime.Now.Date.AddDays(-6).AddHours(16),
            WorkingHours = 8,
            Note = "Morning shift",
            Employee_Id = 3
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-7),
            Starts = DateTime.Now.Date.AddDays(-7).AddHours(10),
            Ends = DateTime.Now.Date.AddDays(-7).AddHours(18),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 4
        });
        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-20),
            Starts = DateTime.Now.Date.AddDays(-20).AddHours(9),
            Ends = DateTime.Now.Date.AddDays(-20).AddHours(17),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 1
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-21),
            Starts = DateTime.Now.Date.AddDays(-21).AddHours(11),
            Ends = DateTime.Now.Date.AddDays(-21).AddHours(19),
            WorkingHours = 8,
            Note = "Evening shift",
            Employee_Id = 2
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-22),
            Starts = DateTime.Now.Date.AddDays(-22).AddHours(8),
            Ends = DateTime.Now.Date.AddDays(-22).AddHours(16),
            WorkingHours = 8,
            Note = "Morning shift",
            Employee_Id = 3
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-23),
            Starts = DateTime.Now.Date.AddDays(-23).AddHours(10),
            Ends = DateTime.Now.Date.AddDays(-23).AddHours(18),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 4
        });

        _employeeWorkDayRepository.Add(new Employee_WorkDay
        {
            Date = DateTime.Now.Date.AddDays(-24),
            Starts = DateTime.Now.Date.AddDays(-24).AddHours(9),
            Ends = DateTime.Now.Date.AddDays(-24).AddHours(17),
            WorkingHours = 8,
            Note = "Regular workday",
            Employee_Id = 1
        });
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
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-4),
            Price = 22.75m,
            Employee_Id = 5,
            Table_Id = 5
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-5),
            Price = 14.99m,
            Employee_Id = 6,
            Table_Id = 6
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-6),
            Price = 30.25m,
            Employee_Id = 7,
            Table_Id = 7
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-7),
            Price = 20.50m,
            Employee_Id = 8,
            Table_Id = 8
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-8),
            Price = 18.99m,
            Employee_Id = 9,
            Table_Id = 9
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-9),
            Price = 27.50m,
            Employee_Id = 10,
            Table_Id = 10
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-10),
            Price = 19.75m,
            Employee_Id = 11,
            Table_Id = 11
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-11),
            Price = 23.49m,
            Employee_Id = 12,
            Table_Id = 12
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-12),
            Price = 16.75m,
            Employee_Id = 13,
            Table_Id = 13
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-13),
            Price = 21.99m,
            Employee_Id = 14,
            Table_Id = 14
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-14),
            Price = 28.50m,
            Employee_Id = 1,
            Table_Id = 15
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-15),
            Price = 17.25m,
            Employee_Id = 2,
            Table_Id = 16
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-16),
            Price = 26.99m,
            Employee_Id = 3,
            Table_Id = 17
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-17),
            Price = 14.50m,
            Employee_Id = 4,
            Table_Id = 18
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-18),
            Price = 31.75m,
            Employee_Id = 5,
            Table_Id = 19
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-19),
            Price = 23.99m,
            Employee_Id = 6,
            Table_Id = 20
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-20),
            Price = 16.50m,
            Employee_Id = 7,
            Table_Id = 21
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-21),
            Price = 29.75m,
            Employee_Id = 8,
            Table_Id = 22
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-22),
            Price = 19.99m,
            Employee_Id = 9,
            Table_Id = 23
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-23),
            Price = 24.50m,
            Employee_Id = 10,
            Table_Id = 24
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-24),
            Price = 18.75m,
            Employee_Id = 11,
            Table_Id = 25
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-25),
            Price = 22.99m,
            Employee_Id = 12,
            Table_Id = 1
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-26),
            Price = 21.25m,
            Employee_Id = 13,
            Table_Id = 2
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-27),
            Price = 27.50m,
            Employee_Id = 14,
            Table_Id = 3
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-28),
            Price = 16.99m,
            Employee_Id = 1,
            Table_Id = 4
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-29),
            Price = 30.75m,
            Employee_Id = 2,
            Table_Id = 5
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-30),
            Price = 19.99m,
            Employee_Id = 3,
            Table_Id = 6
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-31),
            Price = 24.50m,
            Employee_Id = 4,
            Table_Id = 7
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-32),
            Price = 18.75m,
            Employee_Id = 5,
            Table_Id = 8
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-33),
            Price = 22.99m,
            Employee_Id = 6,
            Table_Id = 9
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-34),
            Price = 21.50m,
            Employee_Id = 7,
            Table_Id = 10
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-35),
            Price = 28.75m,
            Employee_Id = 8,
            Table_Id = 11
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-36),
            Price = 25.25m,
            Employee_Id = 9,
            Table_Id = 12
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-37),
            Price = 19.49m,
            Employee_Id = 10,
            Table_Id = 13
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-38),
            Price = 31.99m,
            Employee_Id = 11,
            Table_Id = 14
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-39),
            Price = 16.50m,
            Employee_Id = 12,
            Table_Id = 15
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-40),
            Price = 29.75m,
            Employee_Id = 13,
            Table_Id = 16
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-41),
            Price = 22.99m,
            Employee_Id = 14,
            Table_Id = 17
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-42),
            Price = 27.50m,
            Employee_Id = 1,
            Table_Id = 18
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-43),
            Price = 18.75m,
            Employee_Id = 2,
            Table_Id = 19
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-44),
            Price = 24.99m,
            Employee_Id = 3,
            Table_Id = 20
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-45),
            Price = 30.25m,
            Employee_Id = 4,
            Table_Id = 21
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-46),
            Price = 17.50m,
            Employee_Id = 5,
            Table_Id = 22
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-47),
            Price = 23.99m,
            Employee_Id = 6,
            Table_Id = 23
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-48),
            Price = 26.50m,
            Employee_Id = 7,
            Table_Id = 24
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-49),
            Price = 15.75m,
            Employee_Id = 8,
            Table_Id = 25
        });
        _orderRepository.Add(new Order
        {
            Date = DateTime.Now.AddDays(-50),
            Price = 20.99m,
            Employee_Id = 9,
            Table_Id = 1
        });
        Console.WriteLine("50 Rows added to Order Table");
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
            Item_Id = 4});
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 7.49m,
            Order_Id = 5,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 6,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 7,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 8,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 9,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 10,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 11,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 12,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 13,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 14,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 15,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 16,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 17,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 18,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 19,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 20,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 21,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 22,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 23,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 24,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 25,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 26,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 27,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 28,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 29,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 30,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 31,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 32,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 33,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 34,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 35,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 36,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 37,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 38,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 39,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 40,
            Item_Id = 1
        });

        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 41,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 42,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 43,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 44,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 45,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 46,
            Item_Id = 1
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 7.49m,
            Order_Id = 47,
            Item_Id = 2
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 3,
            UnitPrice = 12.99m,
            Order_Id = 48,
            Item_Id = 3
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 2,
            UnitPrice = 11.50m,
            Order_Id = 49,
            Item_Id = 4
        });
        _orderItemRepository.Add(new Order_Item
        {
            Quantity = 1,
            UnitPrice = 9.99m,
            Order_Id = 50,
            Item_Id = 1
        });
        Console.WriteLine("50 Rows added to Order_Item Table");
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
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 5,
            Supplier_Id = 4,
            Date = DateTime.Now,
            Quantity = 8,
            Price = 12890.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 3,
            Supplier_Id = 5,
            Date = DateTime.Now,
            Quantity = 15,
            Price = 45000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 4,
            Supplier_Id = 6,
            Date = DateTime.Now,
            Quantity = 20,
            Price = 180.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 2,
            Supplier_Id = 7,
            Date = DateTime.Now,
            Quantity = 12,
            Price = 30000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 1,
            Supplier_Id = 8,
            Date = DateTime.Now,
            Quantity = 25,
            Price = 70000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 3,
            Supplier_Id = 9,
            Date = DateTime.Now,
            Quantity = 18,
            Price = 60000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 4,
            Supplier_Id = 10,
            Date = DateTime.Now,
            Quantity = 10,
            Price = 150.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 5,
            Supplier_Id = 11,
            Date = DateTime.Now,
            Quantity = 7,
            Price = 7890.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 1,
            Supplier_Id = 12,
            Date = DateTime.Now,
            Quantity = 30,
            Price = 105000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 2,
            Supplier_Id = 13,
            Date = DateTime.Now,
            Quantity = 22,
            Price = 45000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 3,
            Supplier_Id = 14,
            Date = DateTime.Now,
            Quantity = 18,
            Price = 60000.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 4,
            Supplier_Id = 15,
            Date = DateTime.Now,
            Quantity = 25,
            Price = 180.0m
        });
        _supplierIngredientRepository.Add(new Supplier_Ingredient
        {
            Ingredient_Id = 5,
            Supplier_Id = 15,
            Date = DateTime.Now,
            Quantity = 20,
            Price = 12890.0m
        });
        Console.WriteLine("26 Rows added to Supplier_Ingredient Table");
    }
}