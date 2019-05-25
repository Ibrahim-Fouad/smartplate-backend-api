namespace SmartPlate.API.Dto
{
    public class SortDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string SortBy { get; set; }

        public string OrderBy { get; set; }
    }
}
