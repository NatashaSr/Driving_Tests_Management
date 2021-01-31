using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BE
{
    //Will represent driving test
    public class Test : INotifyPropertyChanged
    {
        #region Variables and properties
        long numOfTest;
        //Property to numOfTest
        public long num_of_test
        {
            get
            {
                return numOfTest;
            }
            set
            {
                numOfTest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("num_of_test"));
                }
            }
        }

        long IDofTester;
        //Propert to IDofTester
        public long ID_tester
        {
            get
            {
                return IDofTester;
            }
            set
            {
                IDofTester = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID_tester"));
                }
            }
        }

        long IDofTrainee;
        //Propert to IDofTrainee
        public long ID_Trainee
        {
            get
            {
                return IDofTrainee;
            }
            set
            {
                IDofTrainee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID_Trainee"));
                }
            }
        }

        DateTime dateOfTest;
        //Property to dateOfTest
        public DateTime _dateOfTest
        {
            get
            {
                return dateOfTest;
            }
            set
            {
                dateOfTest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_dateOfTest"));
                }
            }
        }

        DateTime dateAndHourOfTest;
        //Property to dateAndHourOfTest 
        public DateTime _dateAndHourOfTest
        {
            get
            {
                return dateAndHourOfTest;
            }
            set
            {
                dateAndHourOfTest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_dateAndHourOfTest"));
                }
            }
        }

        StructTypes.Adress leavingAdress;
        //Property to leavingAdress
        public StructTypes.Adress _leavingAdress
        {
            get
            {
                return leavingAdress;
            }
            set
            {
                leavingAdress = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_leavingAdress"));
                }
            }
        }

        StructTypes.Adress endAdress;
        //Property to endAdress
        public StructTypes.Adress _endAdress
        {
            get
            {
                return endAdress;
            }
            set
            {
                endAdress = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_endAdress"));
                }
            }
        }

        myEnums.carType carTypeOfThisTest;
        //Property to carType
        public myEnums.carType _typeCarOfTest
        {
            get
            {
                return carTypeOfThisTest;
            }
            set
            {
                carTypeOfThisTest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_typeCarOfTest"));
                }
            }
        }

        StructTypes.testReport reportsOfTest;
        //Property to reportsOfTest
        public StructTypes.testReport _reportsOfTest
        {
            get
            {
                return reportsOfTest;
            }
            set
            {
                reportsOfTest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_reportsOfTest"));
                }
            }
        }

        bool succeedOrFailed;
        //Property of succeedOrFailed
        public bool _succeedOrFailed
        {
            get
            {
                succeedOrFailed = this.checkResults();
                return succeedOrFailed;
            }
            set
            {
                succeedOrFailed = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_succeedOrFailed"));
                }
            }
        }

        //Comments of the tester
        string remark;
        //Property to remark
        public string _remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_remark"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        //Defaukt constructor 
        public Test()
        {
        numOfTest= Configuration.numberOfTest;

        IDofTester = 0;
        IDofTrainee= 0;
        dateOfTest= new DateTime(2019,02,01);
        dateAndHourOfTest= new DateTime();
        leavingAdress= new StructTypes.Adress();
        endAdress=new StructTypes.Adress();
        carTypeOfThisTest = new myEnums.carType();
        reportsOfTest = new StructTypes.testReport(false);    
        succeedOrFailed = checkResults();
        remark = "";
        }

        //Copy Constructor to Test
        public Test(Test testToCopy)
        {
            numOfTest = testToCopy.num_of_test;
            IDofTester = testToCopy.ID_tester;
            IDofTrainee = testToCopy.ID_Trainee;
            dateOfTest = testToCopy.dateOfTest;
            dateAndHourOfTest = testToCopy.dateAndHourOfTest;
            leavingAdress = testToCopy._leavingAdress;
            endAdress = testToCopy._endAdress;
            carTypeOfThisTest = testToCopy._typeCarOfTest;
            reportsOfTest = testToCopy._reportsOfTest;
            succeedOrFailed = testToCopy._succeedOrFailed;
            remark = testToCopy._remark;
        }

        //Function that decide if a test is or not succeed
        bool checkResults()
        {
            int sum = 0;
            
            if (reportsOfTest.keepDistance ==true)
                sum += 10;

            if (reportsOfTest.parkingBrewers == true)
                sum += 5;

            if (reportsOfTest.lookingAtMirrors == true)
                sum += 35;

            if (reportsOfTest.winkersSignal == true)
                sum += 10;

            if (reportsOfTest.pickUpTheHandbreak == true)
                sum += 5;

            if (reportsOfTest.PedestrainCrossing == true)
                sum += 15;

            if (reportsOfTest.lookAtTheSigns == true)
                sum += 20;

            if (sum >= 70 && reportsOfTest.givingRightOfWay == true)
                return true;
            else
                return false;
        }

        //To do the override of ToString
        public override string ToString()
        {
            string stringWithAllDetails;
            stringWithAllDetails = "Test- \n";
            
            stringWithAllDetails += "Number of test: " + numOfTest + "\n" +
                "ID of Tester: " + IDofTester + "ID of Tester: " + IDofTrainee + "\n" +
                "Date and time of test: " + dateAndHourOfTest + "\n" + "Leaving adress of the test: " + leavingAdress + "\n"
                + "Result of the test: ";
            if (succeedOrFailed == false)
                stringWithAllDetails += myEnums.succeedOrNot.נכשל + "\n";
            else
                stringWithAllDetails += myEnums.succeedOrNot.עבר + "\n";
            return stringWithAllDetails;
        }
    }
}
