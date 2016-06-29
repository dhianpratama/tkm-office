using System.Collections.Generic;

namespace Core
{
    public class PageResponseInfo
    {
        public List<object> Data { get; set; }
        public int TotalData { get; set; }
    }

    public class PageResponseInfoSingleData
    {
        public object Data { get; set; }
        public int TotalData { get; set; }
    }
}
