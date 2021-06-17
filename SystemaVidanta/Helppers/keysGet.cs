using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemaVidanta.Helppers
{
    public class keysGet
    {
        public static string keygeyuser() {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}