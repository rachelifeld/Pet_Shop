using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;


sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    public IOrder order { get; } = new Dal.DalOrder();
    public IProduct product { get; } = new Dal.DalProduct();
    public IOrderItem orderItem { get; } = new Dal.DalOrderItem();
    DalXml()
    {

    }
}

