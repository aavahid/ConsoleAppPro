using System;
using Domain.Common;

namespace Domain.Models
{
    public class Restaurant : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public List<Product>? Products { get; set; }
        public string MenuType { get; set; }
    }
}