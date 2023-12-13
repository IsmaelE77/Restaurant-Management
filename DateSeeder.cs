using System;

namespace Restaurant_Management
{
    public class DataSeeder
    {
        private readonly IEmployee _employeeRepository;
        private readonly IEmployee_WorkDay _employeeWorkDayRepository;
        private readonly ISection _sectionRepository;
        private readonly IItem _itemRepository;
        private readonly IOrder _orderRepository;
        private readonly IOrderItem _orderItemRepository;
        private readonly ITable _tableRepository;
        private readonly ICategory _categoryRepository;
        private readonly ISupplier _supplierRepository;
        private readonly IIngredient _ingredientRepository;
        //private readonly IIte _itemIngredientRepository;
        private readonly ISupplier_Ingredient _supplierIngredientRepository;

        public DataSeeder(
            IEmployee employeeRepository,
            IEmployee_WorkDay employeeWorkDayRepository,
            ISection sectionRepository,
            IItem itemRepository,
            IOrder orderRepository,
            IOrderItem orderItemRepository,
            ITable tableRepository,
            ICategory categoryRepository,
            ISupplier supplierRepository,
            IIngredient ingredientRepository,
           // ICrud<Item_Ingredient> itemIngredientRepository,
            ISupplier_Ingredient supplierIngredientRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeWorkDayRepository = employeeWorkDayRepository;
            _sectionRepository = sectionRepository;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _tableRepository = tableRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _ingredientRepository = ingredientRepository;
            //_itemIngredientRepository = itemIngredientRepository;
            _supplierIngredientRepository = supplierIngredientRepository;
        }

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
            //SeedItemIngredients();
            SeedOrders();
            SeedOrderItems();
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
                SectionId = 1
            });

            _employeeRepository.Add(new Employee
            {
                FirstName = "Jane",
                LastName = "Smith",
                PhoneNumber = "987654321",
                Address = "456 Oak St",
                SalaryPerHour = 20.0m,
                SectionId = 2
            });

            _employeeRepository.Add(new Employee
            {
                FirstName = "Alice",
                LastName = "Johnson",
                PhoneNumber = "5551234567",
                Address = "789 Pine St",
                SalaryPerHour = 18.75m,
                SectionId = 1
            });

            _employeeRepository.Add(new Employee
            {
                FirstName = "Bob",
                LastName = "Williams",
                PhoneNumber = "4449876543",
                Address = "321 Cedar St",
                SalaryPerHour = 17.0m,
                SectionId = 3
            });
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
                CategoryId = 1
            });

            _itemRepository.Add(new Item
            {
                Title = "Caesar Salad",
                Description = "Fresh romaine lettuce with Caesar dressing",
                Price = 7.49m,
                Added = DateTime.Now,
                Rating = 4,
                CategoryId = 2
            });

            _itemRepository.Add(new Item
            {
                Title = "Margherita Pizza",
                Description = "Classic tomato and mozzarella pizza",
                Price = 12.99m,
                Added = DateTime.Now,
                Rating = 3,
                CategoryId = 3
            });

            _itemRepository.Add(new Item
            {
                Title = "Pasta Carbonara",
                Description = "Spaghetti with creamy bacon and egg sauce",
                Price = 11.50m,
                Added = DateTime.Now,
                Rating = 2,
                CategoryId = 2
            });
        }

        private void SeedTables()
        {

        }

        private void SeedCategories()
        {

        }

        private void SeedSuppliers()
        {

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
                EmployeeId = 1
            });

            _employeeWorkDayRepository.Add(new Employee_WorkDay
            {
                Date = DateTime.Now.Date.AddDays(-1),
                Starts = DateTime.Now.Date.AddDays(-1).AddHours(11),
                Ends = DateTime.Now.Date.AddDays(-1).AddHours(19),
                WorkingHours = 8,
                Note = "Evening shift",
                EmployeeId = 2
            });

            _employeeWorkDayRepository.Add(new Employee_WorkDay
            {
                Date = DateTime.Now.Date.AddDays(-2),
                Starts = DateTime.Now.Date.AddDays(-2).AddHours(8),
                Ends = DateTime.Now.Date.AddDays(-2).AddHours(16),
                WorkingHours = 8,
                Note = "Morning shift",
                EmployeeId = 3
            });

            _employeeWorkDayRepository.Add(new Employee_WorkDay
            {
                Date = DateTime.Now.Date.AddDays(-3),
                Starts = DateTime.Now.Date.AddDays(-3).AddHours(10),
                Ends = DateTime.Now.Date.AddDays(-3).AddHours(18),
                WorkingHours = 8,
                Note = "Regular workday",
                EmployeeId = 4
            });
        }

        private void SeedOrders()
        {
            _orderRepository.Add(new Order
            {
                Date = DateTime.Now,
                Price = 25.99m,
                EmployeeId = 1,
                TableId = 1,
                ReceiptId = 1
            });

            _orderRepository.Add(new Order
            {
                Date = DateTime.Now.AddDays(-1),
                Price = 15.49m,
                EmployeeId = 2,
                TableId = 2,
                ReceiptId = 2
            });

            _orderRepository.Add(new Order
            {
                Date = DateTime.Now.AddDays(-2),
                Price = 32.99m,
                EmployeeId = 3,
                TableId = 3,
                ReceiptId = 3
            });

            _orderRepository.Add(new Order
            {
                Date = DateTime.Now.AddDays(-3),
                Price = 18.50m,
                EmployeeId = 4,
                TableId = 4,
                ReceiptId = 4
            });
        }

        private void SeedOrderItems()
        {
            _orderItemRepository.Add(new OrderItem
            {
                Quantity = 2,
                UnitPrice = 9.99m,
                OrderId = 1,
                ItemId = 1
            });

            _orderItemRepository.Add(new OrderItem
            {
                Quantity = 1,
                UnitPrice = 7.49m,
                OrderId = 2,
                ItemId = 2
            });

            _orderItemRepository.Add(new OrderItem
            {
                Quantity = 3,
                UnitPrice = 12.99m,
                OrderId = 3,
                ItemId = 3
            });

            _orderItemRepository.Add(new OrderItem
            {
                Quantity = 2,
                UnitPrice = 11.50m,
                OrderId = 4,
                ItemId = 4
            });
        }

        private void SeedSupplierIngredients()
        {
            _supplierIngredientRepository.Add(new Supplier_Ingredient
            {
                Ingredient_Id = 1,
                Supplier_Id = 1,
                Quantity = 10,
                Price = 50.0m
            });

            _supplierIngredientRepository.Add(new Supplier_Ingredient
            {
                Ingredient_Id = 2,
                Supplier_Id = 2,
                Quantity = 5,
                Price = 20.0m
            });
        }
    }
}
