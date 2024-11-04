namespace DAL.ModelsRequest.MenuRequest
{
    public class UpdateMenuReq
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string AdminName { get; set; }
        public int Status { get; set; }
    }
}
