using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        public IProduct Product { get; }
        public ICart Cart { get; }
        public IOrder Order { get; }

    }
}
