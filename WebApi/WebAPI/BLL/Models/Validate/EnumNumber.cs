namespace BLL.Models.Validate
{
    public class EnumNumber
    {
        public enum StatusID
        {
            InActive = 0,
            Active = 1,
            Delete = 99
        }
        public enum CartStatus
        {
            NewOrder = 0,
            Order = 1,
            Cancel = 2,
            Done = 3
        }
    }
}
