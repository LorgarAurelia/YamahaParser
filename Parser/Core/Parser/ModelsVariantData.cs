﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Parser
{
    class ModelsVariantData
    {
        public List<string> ModelTypeCode { get; set; }
        public List<string> ProductNo { get; set; }
        public List<string> ColorType { get; set; }
        public List<string> ColorName { get; set; }
        public List<string> ProdCategory { get; set; }
        public List<string> ProdPictureNo { get; set; }
        public List<string> ProdPictureFileURL { get; set; }
        public List<string> YearsId { get; set; }
    }
}
