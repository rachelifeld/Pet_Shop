using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

static public class Factory
{
    public static IBL Get()
    {
        return new BlImplementation.Bl();
    }
}
