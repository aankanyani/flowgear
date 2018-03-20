using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplyMonitor
{
    public interface IMonitoredResources
    {
        Dictionary<string, double> GetResourceSizes();

    }
}
