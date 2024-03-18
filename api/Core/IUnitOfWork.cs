using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Core.Managers;

namespace api.Core
{
    public interface IUnitOfWork
    {
        StockManager StockManager { get; }
    }
}