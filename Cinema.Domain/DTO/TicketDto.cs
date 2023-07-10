using Cinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DTO
{
    public class TicketDto
    {
        public List<Ticket> Tickets { get; set; }
        public DateTime Date { get; set; }
    }
}
