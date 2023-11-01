using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class ProductNotInStock : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: product not in stock";

    }
    public class AlreadyExistsSuchAnObject : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: Already Exists Such An Object";

    }
    public class IncorrectData : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: Incorrect Data";

    }

    public class NoSuchProductInCart : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: No Such Product In Cart";

    }
    public class OrderAlreadySent : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: Order Already Sent";

    }
    public class OrderAlreadyDelivered : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: Order Already Delivered";

    }
    public class DalException : Exception  // מחלקה שמתארת שגיאה
    {
        public DalException(Exception ex) : base("exception from dal:", ex) { }
    }

    public class Null:Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Exception: null!!!!!!!!!!";
    }

}
