using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //This will be factory and singleton
    public class factoryDal
    {
        static Idal dal = null;

        public static Idal getDAL()
        {
            if (dal == null)
                dal = new Dal_XML_imp(); //FILES
                //dal = new Dal_imp();

            return dal;
        }
    }
}
