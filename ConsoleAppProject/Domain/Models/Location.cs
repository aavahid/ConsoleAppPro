using System;
using Domain.Common;

namespace Domain.Models
{
    public class Location : BaseEntity
    {
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

