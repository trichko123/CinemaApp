using Cinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string UserId { get; set; }
        public CinemaApplicationUser User { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
