namespace KnockAdm
{
    public class GetUsersQuery
    {
        public string SearchTerm { get; set; }
        public string SortColumn { get; set; }
        public string SortType { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}