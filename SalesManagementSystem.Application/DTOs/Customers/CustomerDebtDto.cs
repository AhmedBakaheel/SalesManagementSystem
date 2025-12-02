namespace SalesManagementSystem.Application.DTOs.Customers
{
    public class CustomerDebtDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Balance { get; set; } 
    }
}