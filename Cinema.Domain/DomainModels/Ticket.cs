using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Domain.DomainModels
{
    public enum Genre
    {
        Action,
        Comedy,
        Drama,
        Thriller,
        Documentary,
        Scifi,
        Horror,
        History
    }
    public class Ticket : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public Genre MovieGenre { get; set; }
        [Required]
        public string MovieYear { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public int TicketPrice { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
