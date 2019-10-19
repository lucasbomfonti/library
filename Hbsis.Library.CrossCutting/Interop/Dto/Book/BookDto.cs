using System;

namespace Hbsis.Library.CrossCutting.Interop.Dto.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int Version { get; set; }
    }
}