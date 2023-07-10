using Cinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Ticket Ticket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
