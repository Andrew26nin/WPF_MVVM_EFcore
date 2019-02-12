using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Order
    {
        [Key]
        public int Number { get; set; }
        public string Description { get; set; }


        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
