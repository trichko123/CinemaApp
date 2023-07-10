using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Services.Interface
{
    public interface IBackGroundEmailSender
    {
        Task DoWork();
    }
}
