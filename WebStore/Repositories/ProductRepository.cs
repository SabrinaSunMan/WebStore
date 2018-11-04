﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Interface;
using WebStore.Models;

namespace WebStore.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        private readonly IRepository<Product> _Product;

        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _Product = new Repository<Product>(unitOfWork);
        }
    }
}