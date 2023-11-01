using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    private DalList()
    {

    }
    public IOrder order => new DalOrder();
    public IOrderItem orderItem => new DalOrderItem();
    public IProduct product => new DalProduct();
    public static IDal Instance { get; } = new DalList();
}
