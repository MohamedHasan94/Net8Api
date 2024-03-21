using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Core.Managers;
using api.Data;
using api.Models;

namespace api.Core
{
    public class UnitOfWork: IUnitOfWork
    {
        readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        StockManager? _stockManager;
        public StockManager StockManager
        {
            get
            {
                if (_stockManager == null)
                {
                    _stockManager = new StockManager(_dbContext);
                }
                return _stockManager;
            }
        }

        CommentManager? _commentManager;
        public CommentManager CommentManager
        {
            get
            {
                if (_commentManager == null)
                {
                    _commentManager = new CommentManager(_dbContext);
                }
                return _commentManager;
            }
        } 
    }
}