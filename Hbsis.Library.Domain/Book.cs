using Hbsis.Library.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hbsis.Library.Domain
{
    public class Book : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(500)")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Image { get; set; }
    }
}