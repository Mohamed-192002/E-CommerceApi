﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepo CategoryRepo { get; }
        public IProductRepo ProductRepo { get; }
        public IBasketRepo BasketRepo { get; }
        public IDeliveryMethodRepo DeliveryMethodRepo { get; }
        public IOrderRepo OrderRepo { get; }

    }
}
