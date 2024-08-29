using Microsoft.AspNetCore.SignalR;
using Streamer.Models;
using Streamer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamer.RabbitMQ_client;

namespace Streamer.Hubs
{
    public  class MyHub : Hub
    {
        private readonly IproductService _productService;
        public MyHub(IproductService productService)
        {
            _productService = productService;
        }

        public async Task SendOrder()
        {
            var product = _productService.Get(); // Fetch product using service
            if (product != null)
            {
                // Send the product object to all connected clients
                await Clients.All.SendAsync("ReceiveProduct", product);
            }
        } 
        public async Task<List<productClass>> GetProducts(productClass product_)
        {
            var product = _productService.Get();  // No need to await since Get() is synchronous

            // Create a list with the single product (if needed)
            var data = new List<productClass> { product };

            return data.ToList();
            await Clients.All.SendAsync("ReceiveProduct", product);
            // Return the list
        }
    }
}

