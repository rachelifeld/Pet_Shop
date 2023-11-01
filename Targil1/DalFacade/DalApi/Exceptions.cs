using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class ObjectNotFound : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: Object Not Found";

    }
    public class AlreadyExistsSuchAnObject : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: Already Exists Such An Object";

    }
    public class NoMoreSpaceInArray : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: No More Space In Array";

    }
  
}
