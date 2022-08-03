﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    public class ProductSeed : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Kalem 1",
                Price = 100,
                Stock = 20,
                CreateDate = DateTime.Now
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Kalem 2",
                Price = 100,
                Stock = 20,
                CreateDate = DateTime.Now
            },
            new Product
            {
                Id = 1,
                CategoryId = 2,
                Name = "Kitap 1",
                Price = 100,
                Stock = 20,
                CreateDate = DateTime.Now
            },
            new Product
            {
                Id = 1,
                CategoryId = 2,
                Name = "Kitap 2",
                Price = 100,
                Stock = 20,
                CreateDate = DateTime.Now
            });
        }
    }
}