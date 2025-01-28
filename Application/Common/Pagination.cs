using Application.Contract;
namespace Application.Common
{
    public partial class Pagination : IPagination
    {
        public int curentPage { set; get; } = 1;
        public int pageSize { set; get; } = 10;
        public string keyword { set; get; } = string.Empty;

    }
    public partial class PaginationWithParent : IPagination
    {
        public int curentPage { set; get; } = 1;
        public int pageSize { set; get; } = 10;
        public string keyword { set; get; } = string.Empty;
        public Guid ParentId { set; get; }
    }
}
