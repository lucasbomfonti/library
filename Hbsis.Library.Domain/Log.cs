using Hbsis.Library.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hbsis.Library.Domain
{
    public class Log : BaseEntity
    {
        public Log(string field, string value)
        {
            Field = field;
            Value = value;
        }

        [Required]
        public Guid OriginId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Table { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Field { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(MAX)")]
        [MaxLength(8000)]
        public string Value { get; set; }
    }
}