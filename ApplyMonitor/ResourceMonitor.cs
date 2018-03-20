using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ApplyMonitor
{
    public static class ResourceMonitor
    {
        static Dictionary<WeakReference, ResourceSnapshot> _monitoredObjects = new Dictionary<WeakReference, ResourceSnapshot>();

        public static void AddMonitoredCollection(string name, string instanceKey, ICollection collection)
        {
            if (collection == null) throw new NullReferenceException();

            lock (_monitoredObjects)
            {
                _monitoredObjects.Add(
                    new WeakReference(collection),
                    new ResourceSnapshot() { Name = name, InstanceKey = instanceKey });
            }
        }

        public static void AddMonitoredResources(string name, string instanceKey, IMonitoredResources resource)
        {
            if (resource == null) throw new NullReferenceException();

            lock (_monitoredObjects)
            {
                _monitoredObjects.Add(new WeakReference(resource), new ResourceSnapshot() { Name = name, InstanceKey = instanceKey });
            }
        }

        public static List<ResourceSnapshot> CreateSnapshot()
        {
            List<ResourceSnapshot> rs = new List<ResourceSnapshot>();

            List<WeakReference> toCull = new List<WeakReference>();

            GC.Collect();

            lock (_monitoredObjects)
            {
                foreach (KeyValuePair<WeakReference, ResourceSnapshot> kvp in _monitoredObjects)
                {
                    if (!kvp.Key.IsAlive)
                    {
                        toCull.Add(kvp.Key);
                    }
                    else
                    {
                        //presumably the object could die any time so we gracefully handle this
                        try
                        {
                            if (kvp.Key.Target as IMonitoredResources != null)
                            {
                                foreach (KeyValuePair<string, double> resourceSizes in ((IMonitoredResources)kvp.Key.Target).GetResourceSizes())
                                {
                                    rs.Add(new ResourceSnapshot()
                                    {
                                        Name=kvp.Value.Name,
                                        InstanceKey = kvp.Value.InstanceKey,
                                        ResourceName = resourceSizes.Key,
                                        Value = resourceSizes.Value
                                    });
                                }
                            }
                            else
                            {
                                rs.Add(new ResourceSnapshot()
                                {
                                    Name = kvp.Value.Name,
                                    InstanceKey = kvp.Value.InstanceKey,
                                    ResourceName = "Collection Size",
                                    Value = ((ICollection)kvp.Key.Target).Count
                                }
                                    );
                            }
                        }
                        catch { }
                    }
                }
            }

            foreach (WeakReference wr in toCull)
                _monitoredObjects.Remove(wr);

            return rs;
        }


    }
}
