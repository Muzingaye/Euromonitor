using System;
using System.Collections.Generic;
using DataFramework.Entities;
using DataFramework.Repository;

namespace ServiceFramework.Services
{
    public class ProductServices : IService<Product>
    {
        private ProductRepository productRepository;

        public ProductServices(ProductRepository product)
        {
            this.productRepository = product;
        }

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public Product GetFirst(Func<Product, bool> filters)
        {
            return productRepository.GetFirst(filters);
        }

        public IEnumerable<Product> GetWhere(Func<Product, bool> filters)
        {
            return productRepository.GetWhere(filters);
        }

        public bool Insert(Product instance)
        {
            return productRepository.Insert(instance);
        }

        public bool InsertRange(List<Product> instance)
        {
            return productRepository.InsertRange(instance);
        }

        public bool Update(Product instance)
        {
            return productRepository.Update(instance);
        }
    }
}