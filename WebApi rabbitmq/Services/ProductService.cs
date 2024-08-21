using Microsoft.EntityFrameworkCore;
using WebApi_rabbitmq.Data;
using WebApi_rabbitmq.Models;
namespace WebApi_rabbitmq.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbcontext;
        public ProductService(DbContextClass dbcontext) { 
        _dbcontext = dbcontext;
        }
        public IEnumerable<ProductClass> GetProductList() { 
        return _dbcontext.Product_.ToList();
        }
        public ProductClass GetProductByID(int id)
        {
            return _dbcontext.Product_.Where(x => x.ProductID == id).FirstOrDefault();
        }
        public ProductClass AddProduct(ProductClass product_)
        {
            var result = _dbcontext.Product_.Add(product_);
            _dbcontext.SaveChanges();
            return result.Entity;
        }
        public ProductClass UpdateProduct(ProductClass product_)
        {
            var result = _dbcontext.Product_.Update(product_);
            _dbcontext.SaveChanges();
            return result.Entity;
        }
        public bool DeleteProduct(int Id)
        {
            var filteredData = _dbcontext.Product_.Where(x => x.ProductID == Id).FirstOrDefault();
            var result = _dbcontext.Remove(filteredData);
            _dbcontext.SaveChanges();
            return result != null ? true : false;
        }
    }
}
