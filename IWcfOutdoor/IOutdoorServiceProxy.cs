using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRadio.RadioSmart.Common.Wcf;

namespace IWcfOutdoor
{
    public class IOutdoorServiceProxy : ServiceProxyBase<IOutdoorService>, IOutdoorService
    {
        public IOutdoorServiceProxy() : base("IOutdoorService") { }
    }
}
