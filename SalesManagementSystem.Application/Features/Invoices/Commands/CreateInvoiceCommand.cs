using MediatR;
using SalesManagementSystem.Application.DTOs.Invoices;
using SalesManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Invoices.Commands
{
    public class CreateInvoiceCommand : IRequest<InvoiceCreateResponse>
    {
        public int CustomerId { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<InvoiceDetailDto> Details { get; set; } = new List<InvoiceDetailDto>();
    }
}
