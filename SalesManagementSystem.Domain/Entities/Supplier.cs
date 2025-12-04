using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Domain.Entities
{
    public class Supplier 
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Email { get; set; }
        public decimal Balance { get; set; } = 0; 
        public bool IsActive { get; set; } = true;
    }
}
