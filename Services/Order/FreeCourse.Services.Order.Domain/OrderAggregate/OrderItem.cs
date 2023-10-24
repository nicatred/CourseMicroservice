using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem: Entity
    {
        public string ProductId { get;private set; }
        public string ProductName { get; private set; }
        public string PrictureUrl { get; private set; }
        public Decimal Price { get; private set; }
        public OrderItem()
        {
            
        }
        public OrderItem(string productId, string productName, string prictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PrictureUrl = prictureUrl;
            Price = price;
        }

        public void UpdateOrderItem(string productName, string prictureUrl, decimal price)
        {
            ProductName = productName;
            PrictureUrl = prictureUrl;
            Price = price;
        }
    }
}
