using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int OrderId { get; set; }
        public eCondition OrderCondition { get; set; }
        public Dictionary<DateTime,eCondition>? OrderConditionWithDate { get; set; }

    }
}
