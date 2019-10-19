using System.Collections.Generic;

namespace Hbsis.Library.CrossCutting.Interop.Dto
{
    public class ItemErroReponseDto
    {
        public ItemErroReponseDto()
        {
        }

        public ItemErroReponseDto(string field)
        {
            Field = field;
        }

        public ItemErroReponseDto(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }

        public string Field { get; set; }
        public List<string> Messages { get; set; }
    }
}