using System;
using System.ComponentModel.DataAnnotations;

namespace Hbsis.Library.Domain.Base
{
    public class BaseEntity : BaseKey
    {
        public BaseEntity()
        {
            Date = DateTime.Now;
            Version = 1;
            Active = true;
        }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Version { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}