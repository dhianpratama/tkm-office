namespace Core
{
    public class BaseSearchQueryModel
    {
        public int Page { get; set; }
        public int DataPerPage { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAsc { get; set; }
        public int TotalData { get; set; }
        public SearchKeyword Search { get; set; }

        public class SearchKeyword
        {
            public string Keyword { get; set; }
            public string[] Fields { get; set; }
        }
    }
}
