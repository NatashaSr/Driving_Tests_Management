using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    //This class will have all the static variables. 
    public class Configuration
    {
        public static int minimumLessons=20;
        public static int maximumAgeTester=85;
        public static int minimumAgeTester=40;
        public static double minimumAgeTrainee = 18;
        public static int daysMinimumToTest=7;

        //The only static number that I worry to the files, because he change all the time. 
        public static long numberOfTest = 10000000;

        public static int digistsNumberOfTest = 8;

        //From 09:00 until 15:00
        public static int hoursOfWork=6;

        //From Sunday to thursday
        public static int daysOfWeek = 5;

        //To know the current dateTime.
        public static DateTime date_time = new DateTime(); 
    }
}
