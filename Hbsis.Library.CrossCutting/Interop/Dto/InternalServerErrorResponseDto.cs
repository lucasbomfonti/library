namespace Hbsis.Library.CrossCutting.Interop.Dto
{
    public class InternalServerErrorResponseDto
    {
        public InternalServerErrorResponseDto()
        {
            Message = "An error has occurred, try again later.";
        }

        public InternalServerErrorResponseDto(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}