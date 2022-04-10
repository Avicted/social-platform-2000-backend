using sp2000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp2000.Application.Models
{
    public class Comment : BaseEntity
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
        // @Note(Avic): EFCore convention
        // Navigation property
        public int PostId { get; set; }
    }
}
