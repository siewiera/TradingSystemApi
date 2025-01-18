﻿using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public class Adress
    {
        public int Id  { get; set; }
        [Required]
        public string Street  { get; set; }
        public string HouseNo { get; set; }
        [Required]
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
