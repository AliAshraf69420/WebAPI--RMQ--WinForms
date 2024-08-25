using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamer.Models;

namespace Streamer.Services
{
    public interface IproductService
    {
        public IEnumerable<productClass> GetProducts();
        public productClass Get();
    }
}
