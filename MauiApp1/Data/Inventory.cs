using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MauiApp1.Data
{
    public class InventoryItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the item name.")]
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public DateTime? LastOutDate { get; set; }
    }

    public class InventoryHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the item name.")]
        public string ItemName { get; set; }
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Please provide a date taken out.")]
        public DateTime OutDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Approver is required.")]
        public string ApprovedBy { get; set; }

        [Required(ErrorMessage = "Withdrawer is required.")]
        public string TakenBy { get; set; }
    }
}
