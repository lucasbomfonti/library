using System.Collections.Generic;

namespace Hbsis.Library.CrossCutting.Interop.Dto
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto()
        {
            Message = "The given model is invalid.";
        }

        public ErrorResponseDto(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
        public List<ItemErroReponseDto> Errors { get; set; }
    }
}