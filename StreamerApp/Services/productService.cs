using Microsoft.EntityFrameworkCore;
using Streamer.Data;
using Streamer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamer.Services
{
    public class productService : IproductService
    {
        private readonly dbContextClass _dbcontext;
        public productService(dbContextClass dbContextClass) { 
        _dbcontext = dbContextClass;
        }
        public IEnumerable<productClass> GetProducts()
        {
            return _dbcontext.Product.ToList();
        }
        public productClass Get()
        {
            var product = _dbcontext.Product.FirstOrDefault();
            if (product != null)
            {
                return product;
            }
            return null;
        }
    }
}
