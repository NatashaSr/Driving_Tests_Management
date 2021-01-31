using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    //This will be factory and singleton
    public class factoryBL
    {
        static IBL bl = null;

        public static IBL getBL()
        {
            if(bl==null)
                bl=new BL_imp();
            return bl;
        }
    }
}
