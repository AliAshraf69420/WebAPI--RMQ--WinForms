using WebApi_rabbitmq.Models;
namespace WebApi_rabbitmq.Services
{
    public interface IProductService
    {
        public IEnumerable<ProductClass> GetProductList();
        public ProductClass GetProductByID(int id);
        public ProductClass AddProduct(ProductClass product);
        public ProductClass UpdateProduct(ProductClass product);
        public bool DeleteProduct(int id);
    }
}
