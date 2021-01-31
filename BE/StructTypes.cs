using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BE
{
    public class StructTypes 
    {
        //Struct to adress
        public struct Adress
        {
            public string street;

            public int houseNumber;

            public string city;
            

            public Adress(string street_u, int numb, string city_u)
            {
                street = street_u;
                houseNumber =numb;
                city = city_u;
            }

            public override string ToString()
            {
                return street + " " + houseNumber + " " + city;
            }
        }

        //Struct that represent report of test
        public struct testReport
        {   
            //שמירת מרחק
            public bool keepDistance;
            //חניה ברוורס  
            public bool parkingBrewers;
            //התבוננות במראות
            public bool lookingAtMirrors;
            //איתותים
            public bool winkersSignal;
            //הרמת בלם יד
            public bool pickUpTheHandbreak;
            //מעבר חצייה
            public bool PedestrainCrossing;
            //התייחסות לתמרורים
            public bool lookAtTheSigns;
            //מתן זכות קדימה
            public bool givingRightOfWay;

            public testReport(bool f)
            {
                //שמירת מרחק
                keepDistance = f;
                //חניה ברוורס  
                parkingBrewers = f;
                //התבוננות במראות
                lookingAtMirrors = f;
                //איתותים
                winkersSignal = f;
                //הרמת בלם יד
                pickUpTheHandbreak = f;
                //מעבר חצייה
                PedestrainCrossing = f;
                //התייחסות לתמרורים
                lookAtTheSigns = f;
                //מתן זכות קדימה
                givingRightOfWay = f;

            }

        }
    }
}
