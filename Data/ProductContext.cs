﻿using EAuction.Products.Api.Data.Abstractions;
using EAuction.Products.Api.Entities;
using EAuction.Products.Api.Settings;
using MongoDB.Driver;

namespace EAuction.Products.Api.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Products = database.GetCollection<Product>(settings.ProductCollectionName);
            Bids = database.GetCollection<Bid>(settings.BidCollectionName);
        }

        public IMongoCollection<Product> Products { get; set; }

        public IMongoCollection<Bid> Bids { get; set; }
    }
}