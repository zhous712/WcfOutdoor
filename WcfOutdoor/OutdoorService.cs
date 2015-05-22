using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using IWcfOutdoor;

namespace WcfOutdoor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class OutdoorService : IOutdoorService
    {
    }
}
