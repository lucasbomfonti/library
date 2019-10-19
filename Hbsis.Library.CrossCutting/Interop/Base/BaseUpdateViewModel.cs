using System;
using System.ComponentModel.DataAnnotations;

namespace Hbsis.Library.CrossCutting.Interop.Base
{
    public class BaseUpdateViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Version { get; set; }
    }
}