using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the items to withdraw.")]
        public List<InventoryItem> Items { get; set; }

        [Required(ErrorMessage = "Please provide a date requested.")]
        public DateTime RequestDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Withdrawer is required.")]
        public string RequestedBy { get; set; }
    }
}
