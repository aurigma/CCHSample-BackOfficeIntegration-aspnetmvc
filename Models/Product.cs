using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace CustomersCanvasSample.Models
{
    [Table("Products")]
    public class Product
    {
        public Product(string id, string name, float price, string imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
        }

        public string Id { get; private set; }
        public string Name { get; set; }
        public string ImageUrl { get; private set; }
        public float Price { get; set; }
    }
}
