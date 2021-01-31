using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    //This class will have all the enum's
    public class myEnums
    {
        //Enum to types of cars.
        public enum carType {פרטי,דו_גלגלי, משאית_בינונית, משאית_כבדה }

        //Enum to type of gearbox
        public enum gearboxType { ידני, אוטומטי }

        //Enum to gender
        public enum genderType { זכר, נקבה }
        
        //Enum days
        //We will use the ready DayOfWeek in system

        //Enum hours of work 
        public enum hoursOfWork { Hour_9_10, Hour_10_11, Hour_11_12, Hour_12_13, Hour_13_14, Hour_14_15 }

        //Enum of succeed or failed
        public enum succeedOrNot { נכשל, עבר}
    
    }
}
