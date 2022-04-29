using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Models
{
    public class Transiction
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public List<Item> Products { get; set; }

      
    }

    public class Item
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }


}
