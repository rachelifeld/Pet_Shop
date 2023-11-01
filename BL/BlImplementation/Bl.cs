using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
  
    sealed public class Bl : IBL    {
        public ICart Cart => new BLCart();
     
        public IProduct Product => new BLProduct();
     
        public IOrder Order => new BLOrder();
    }
}


