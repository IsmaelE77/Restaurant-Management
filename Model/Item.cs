using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant_Management.Model
{
    public class Item
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }  
        public DateTime Added { get; set; } 
        public decimal Rating { get; set; }
        public int CategoryId { get; set; }
        public List<Item_Ingredient> Item_Ingredients { get; set; } = [];
        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Description: {Description}, Price: {Price}, Added: {Added:yyyy/MM/dd}, Rating: {Rating}, CategoryId: {CategoryId}";
        }
    }
}
