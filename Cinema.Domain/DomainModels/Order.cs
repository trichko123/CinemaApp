using Cinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public CinemaApplicationUser User { get; set; }
        public IEnumerable<TicketInOrder> TicketsInOrder { get; set; }
    }
}
