using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPostgres
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]
        public string Address { get; set; }

        public bool IsVIP { get; set; }

        public ObservableCollection<Order> Orders { get; set; }
    }
}
