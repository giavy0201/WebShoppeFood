namespace DAL.Non_Repository
{
    public static class ValueGeneric
    {
        public const int OffActive = 0;
        public const int Active = 1;
        public const int Inactive = 2;
        public const int Delete = 99;
    }

    public static class ValueOrder
    {
        public const int NewOrder = 0;
        public const int Order = 1;
        public const int Cancel = 2;
        public const int Done = 3;
    }

    public static class ValueDiscountDAL
    {
        public const int Discount = 1;
        public const int NotDiscount = 2;
    }
    public static class ValuePriceDAL
    {
        public const int Ascending = 1;
        public const int Descending = 2;
    }
}
