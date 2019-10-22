using Hbsis.Library.CrossCutting.Interop.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Hbsis.Library.CrossCutting.Extensions
{
    public static class ControllerExtensions
    {
        public static ErrorResponseDto GetErrors(this ModelStateDictionary modelState)
        {
            return new ErrorResponseDto
            {
                Errors = modelState.Where(w => w.Value.Errors.Any()).Select(s =>
                    new ItemErroReponseDto(s.Key)
                    {
                        Messages = s.Value.Errors.Select(so => !string.IsNullOrEmpty(so.ErrorMessage) ? so.ErrorMessage : so.Exception.Message).ToList()
                    }).ToList()
            };
        }
    }
}