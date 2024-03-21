using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int Fk_StockId { get; set; }
        
        // Navigation property
        [ForeignKey(nameof(Fk_StockId))]
        public virtual Stock? Stock { get; set; }
    }
}
