using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NorthwindConsoleApplication.Logger;
using NorthwindConsoleApplication.Model;

namespace NorthwindConsoleApplication.Services.Database
{
    public class DatabaseService
    {
        private readonly NWConsole_48_JPTContext _context;
        private readonly ILoggerManager _logger;

        public DatabaseService(NWConsole_48_JPTContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public TEntity GetById<TEntity>(int id) where TEntity : class 
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAllQueryable<TEntity>() where TEntity : class 
        {
            return _context.Set<TEntity>();
        }

        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
    }
}