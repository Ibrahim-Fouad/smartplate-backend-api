using SmartPlate.API.Extensions;

namespace SmartPlate.API.Dto
{
    public class SortDto
    {
        public int PageSize { get; }
        public int PageNumber { get; }
        public string SortBy { get; }
        public bool IsAscending { get; }

        public SortDto(string sortBy, string orderBy, int pageSize, int pageNumber)
        {
            IsAscending = orderBy.ToLower() != "desc";
            SortBy = sortBy.ToCamelCase();
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}