using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class PagedResult<T>  // Thêm <T> vào đây
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string? Keyword { get; set; }
    }
}
