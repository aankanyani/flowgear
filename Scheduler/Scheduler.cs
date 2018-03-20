using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;

namespace Scheduler
{
    class Scheduler
    {
        public bool IsDue(int startHour, int startMin, int endHour, int endMin, int intervalSecs)
        {
            var isDue = false;
           if(startHour >= 8 && endHour <= 17 && intervalSecs == 300)
            {
                Thread.Sleep(intervalSecs);
                isDue = !isDue;
            }

            return isDue;
        }
    }
}
