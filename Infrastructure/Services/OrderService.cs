using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        // private readonly IGenericRepository<Order> _orderRepo;
        // private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        // private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        // public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,
        // IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
           _unitOfWork = unitOfWork;
            // _productRepo = productRepo;
            // _dmRepo = dmRepo;
            // _orderRepo = orderRepo;
        }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        // get basket from the repo
        var basket = await _basketRepo.GetBasketAsync(basketId);

        //get the item from the product repo
        var items =  new List<OrderItem>();

        foreach(var item in basket.Items)
        {
            // var productItem = await _productRepo.GetByIdAsync(item.Id);
            var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

            var itemOrdered =  new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem =  new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }
        
        // get the delivery methods from repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
        
        // calc subTotal
        var subTotal =  items.Sum(item => item.Price * item.Quantity);

        //create order
        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subTotal);
        _unitOfWork.Repository<Order>().Add(order);

        // save to DB
        var result = await _unitOfWork.Complete();

        if (result <=0) return null;
        
        // delete basket

        await _basketRepo.DeleteBasketAsync(basketId);

        // return order
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(id,buyerEmail);

        return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);

        return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
}
}