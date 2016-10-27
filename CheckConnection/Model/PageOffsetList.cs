using System.Collections.Generic;

namespace CheckConnection.Model
{
    public class PageOffsetList : System.ComponentModel.IListSource
    {
        private int _count = 0;
        public PageOffsetList(int pcount)
        {
            _count = pcount;
        }
        public bool ContainsListCollection { get; protected set; }

        public System.Collections.IList GetList()
        {
            // Return a list of page offsets based on "totalRecords" and "pageSize"
            var pageOffsets = new List<int>();
            for (int offset = 0; offset < _count; offset += 10)
                pageOffsets.Add(offset);
            return pageOffsets;
        }
    }

}
