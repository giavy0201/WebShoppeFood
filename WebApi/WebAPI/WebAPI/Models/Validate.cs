using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Models
{
    public class Validate
    {
        public static string ValidateInput(ModelStateDictionary modelState)
        {
            var values = modelState.Values.ToList();
            string error = string.Empty;
            foreach (var value in values)
            {
                foreach (var e in value.Errors)
                {
                    error += e.ErrorMessage + " . ";
                }
            }
            return error.ToString()[..^2];
        }
    }
}
