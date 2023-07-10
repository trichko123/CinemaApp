using Cinema.Domain.DomainModels;
using Cinema.Domain.DTO;
using Cinema.Repository.Interface;
using Cinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepositorty;
        private readonly IRepository<Order> _orderRepositorty;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepositorty;
        private readonly IRepository<EmailMessage> _emailMessageRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
                                    IRepository<TicketInOrder> ticketInOrderRepositorty,
                                    IRepository<Order> orderRepositorty,
                                    IUserRepository userRepository,
                                    IRepository<EmailMessage> emailMessageRepository)
        {
            _shoppingCartRepositorty = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepositorty = orderRepositorty;
            _ticketInOrderRepositorty = ticketInOrderRepositorty;
            _emailMessageRepository = emailMessageRepository;
        }

        public bool deleteTicketFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userShoppingCart = loggedInUser.ShoppingCart;
                var itemToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.TicketId.Equals(id)).FirstOrDefault();

                userShoppingCart.TicketsInShoppingCart.Remove(itemToDelete);

                this._shoppingCartRepositorty.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.ShoppingCart;

            var tickets = userShoppingCart.TicketsInShoppingCart.ToList();

            var allTicketPrice = tickets.Select(z => new
            {
                TicketPrice = z.Ticket.TicketPrice,
                Quanitity = z.Quantity
            }).ToList();

            var totalPrice = 0;


            foreach (var item in allTicketPrice)
            {
                totalPrice += item.Quanitity * item.TicketPrice;
            }


            ShoppingCartDto scDto = new ShoppingCartDto
            {
                Tickets = tickets,
                TotalPrice = totalPrice
            };


            return scDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                EmailMessage mail = new EmailMessage
                {
                    MailTo = loggedInUser.Email,
                    Subject = "Successfully created order",
                    Status = false
                };

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepositorty.Insert(order);

                List<TicketInOrder> ticketsInOrder = new List<TicketInOrder>();

                var result = userShoppingCart.TicketsInShoppingCart.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Ticket.Id,
                    Ticket = z.Ticket,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0;

                sb.AppendLine("Your order is completed. The order conains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    totalPrice += item.Quantity * item.Ticket.TicketPrice;

                    sb.AppendLine(i.ToString() + ". " 
                        + item.Ticket.MovieName + " " + item.Ticket.MovieYear + " , genre: " + item.Ticket.MovieGenre +
                        " with price of: " + item.Ticket.TicketPrice + " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total price: " + totalPrice.ToString());


                mail.Content = sb.ToString();


                ticketsInOrder.AddRange(result);

                foreach (var item in ticketsInOrder)
                {
                    this._ticketInOrderRepositorty.Insert(item);
                }

                loggedInUser.ShoppingCart.TicketsInShoppingCart.Clear();

                this._userRepository.Update(loggedInUser);
                this._emailMessageRepository.Insert(mail);

                return true;
            }
            return false;
        }
    }
}
