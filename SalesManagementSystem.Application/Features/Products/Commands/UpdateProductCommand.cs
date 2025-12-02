using MediatR;
using SalesManagementSystem.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class UpdateProductCommand : UpdateProductDto, IRequest<bool> 
    {
        public int Id { get; set; } 
    }
}
