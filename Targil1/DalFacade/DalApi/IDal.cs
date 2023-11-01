using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        public IOrder order { get;  }
        public IOrderItem orderItem { get; }
        public IProduct product { get; }

    }
}
