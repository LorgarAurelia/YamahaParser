using System.Collections.Generic;

namespace Parser.Core.Parser
{
    class PartsData
    {
        public List<string> RefNo { get; set; }
        public List<string> PartNo { get; set; }
        public List<string> PartName { get; set; }
        public List<string> Quantity { get; set; }
        public List<string> Remarks { get; set; }
        public List<string> CatalogId { get; set; }
    }
}
