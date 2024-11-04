namespace BLL.Models
{
    public static class GetKeyCache
    {
        public const string ListCity = "List-City";
        public const string ListCategory = "List-Category";
        public const string ListContent = "List-Content";
        public const string ListContentByCate = "List-Content-ByCate";
        public const string ListDistrictByCity = "List-District-ByCity";
        public const string ListWardByCity = "List-Ward-ByCity";
        public const string LocationByWardID = "Location-ByWard";
        public const string InfoStoreBy = "Info-Store";
        public const string ListMenuByStore = "List-Menu-ByStore";
        public const string ListFoodByMenu = "List-Food-ByMenu";
        public const string ListFoodByStore = "List-Food-ByStore";
        public const string LocationByWard = "Location-ByWard";
        public const string ListStoreSearchDefault = "List-Store-Search-Default";
        public const string ListStorePreDefault = "List-Store-Pre-Default";
        public const string ListStoreDistrictDefault = "List-Store-District-Default";
        public const string ListProductByStore = "List-Product-ByStore";
        public const string SelectCollection = "CollectionID";
        public const string CollectionDefault = "Collection-Default";
        public const string ListStoreOfCollectionDefault = "List-Store-Collection-Default";

        public static string CreateKey(params string[] keys)
        {
            string key = "";
            foreach (string s in keys)
            {
                key += s + "-";
            }
            return key.ToString()[..^1];
        }

    }
}
