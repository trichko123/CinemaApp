using Cinema.Domain.DomainModels;
using Cinema.Domain.DTO;
using Cinema.Domain.Identity;
using Cinema.Repository.Interface;
using Cinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _TicketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        public TicketService(IRepository<Ticket> TicketRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, IUserRepository userRepository)
        {
            _TicketRepository = TicketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        public void CreateNewTicket(Ticket t)
        {
            this._TicketRepository.Insert(t);
        }

        public void DeleteTicket(Guid id)
        {
            var Ticket = this.GetDetailsForTicket(id);
            this._TicketRepository.Delete(Ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return this._TicketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._TicketRepository.Get(id);
        }

        public void UpdateExistingTicket(Ticket t)
        {
            this._TicketRepository.Update(t);
        }
        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.ShoppingCart;

            if (item.TicketId != null && userShoppingCard != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }
        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                Ticket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }
    }
}
