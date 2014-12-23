using System;

namespace Start.Entity
{
    public class PageInfo
    {
        private int pageIndex;
        public int PageIndex { set { pageIndex = value; } get { return pageIndex == 0 ? 1 : pageIndex; } }
        private int pageSize;
        public int PageSize { set { pageSize = value; } get { return pageSize == 0 ? 20 : pageSize; } }
        public int TotalRecord { set; get; }
    }
}
