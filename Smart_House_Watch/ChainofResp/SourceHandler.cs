using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_House_Watch.ChainofResp
{
    public abstract class SourceHandler
    {
        public SourceHandler Successor { get; set; }
        public abstract void HandleRequest(int point);
    }
}
