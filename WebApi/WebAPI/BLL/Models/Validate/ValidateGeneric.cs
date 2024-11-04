namespace BLL.Models.Validate
{
    public class ValidateGeneric
    {
        public static bool IsTimeValid(string time)
        {
            return TimeSpan.TryParse(time, out _) && TimeSpan.Parse(time) >= TimeSpan.Zero && TimeSpan.Parse(time) < TimeSpan.FromDays(1);
        }
    }
}
