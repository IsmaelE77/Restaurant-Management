using System;

namespace Restaurant_Management.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;

        public OrderService(OrderRepository orderRepository , OrderItemRepository orderItemRepository){
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public void CreateOrder(Order order){
            int orderId = _orderRepository.Add(order);
            foreach(var orderItem in order.OrderItems){
                orderItem.OrderId = orderId;
                _orderItemRepository.Add(orderItem);
            }
        }
    }
}
