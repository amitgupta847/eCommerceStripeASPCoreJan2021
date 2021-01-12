﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
  public class StoreContextSeed
  {

    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {

      try
      {
        //load product brands first.
        if (!context.ProductBrands.Any())
        {
          var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

          var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

          foreach (var brand in brands)
          {
            context.ProductBrands.Add(brand);
          }

          await context.SaveChangesAsync();
        }

        //then load product types
        if (!context.ProductTypes.Any())
        {
          var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
          var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

          foreach (var type in types)
          {
            context.ProductTypes.Add(type);
          }

          await context.SaveChangesAsync();
        }



        //finally load the products
        if (!context.Products.Any())
        {
          var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
          var products = JsonSerializer.Deserialize<List<Product>>(productsData);

          foreach (var product in products)
          {
            context.Products.Add(product);
          }

          await context.SaveChangesAsync();
        }

      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogError(ex.Message);
      }

    }

  }
}
