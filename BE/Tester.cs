using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BE
{
    //Class that represent a details of a tester.
    public class Tester : INotifyPropertyChanged
    {
        #region Variables and properties

        long ID;
        //Property of ID 
        public long _ID
        {
            get
            {
                return ID;
            }
            set
            {
                if (value.ToString().Length < 9)
                    throw new Exception("מספר ת.ז לא תקין-פחות מ-9 ספרות");
                ID = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_ID"));
                }
            }
        }

        string lastName;
        //Property of lastName
        public string last_name
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("last_name"));
                }
            }
        }

        string firstName;
        //Property of firstName
        public string first_name
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("first_name"));
                }
            }
        }

        DateTime dateOfBirth;
        //Property of dateOfBirth
        public DateTime birth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("birth"));
                }
            }
        }

        int age;
        //Property of age
        public int _age
        {
            get
            {
                Configuration.date_time = DateTime.Now;

                if (Configuration.date_time.Month > dateOfBirth.Month)
                    age = Configuration.date_time.Year - dateOfBirth.Year;
                else
                {
                    if (Configuration.date_time.Month < dateOfBirth.Month)
                        age = Configuration.date_time.Year - dateOfBirth.Year - 1;
                    else
                    {
                        if (Configuration.date_time.Day < dateOfBirth.Day)
                            age = Configuration.date_time.Year - dateOfBirth.Year - 1;
                        else
                            age = Configuration.date_time.Year - dateOfBirth.Year;
                    }
                }

                return age;
            }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_age"));
                }
            }
        }

        myEnums.genderType genderOfTester;
        //Property to the gender
        public myEnums.genderType gender
        {
            get
            {
                return genderOfTester;
            }
            set
            {
                genderOfTester = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("gender"));
                }
            }
        }

        long phoneNumber;
        //Property to phoneNumber
        public long _phoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (value.ToString().Length < 9)
                    throw new Exception("מספר פלאפון לא תקין-פחות מ-9 ספרות");

                phoneNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_phoneNumber"));
                }
            }
        }

        StructTypes.Adress adressOfTester;
        //Property to adressOfTester
        public StructTypes.Adress _adressOfTester
        {
            get
            {
                return adressOfTester;
            }
            set
            {
                adressOfTester = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_adressOfTester"));
                }
            }
        }

        int yearsOfExperience;
        //Property to yearsOfExperience
        public int _yearsOfExperience
        {
            get
            {
                return yearsOfExperience;
            }
            set
            {
                yearsOfExperience = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_yearsOfExperience"));
                }
            }
        }

        int MaxTestsPerWeek;
        //Property to MaxTestsPerWeek
        public int _MaxTestsPerWeek
        {
            get
            {
                return MaxTestsPerWeek;
            }
            set
            {
                MaxTestsPerWeek = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_MaxTestsPerWeek"));
                }
            }
        }

        myEnums.carType carTypeOfTester;
        //Property to carType
        public myEnums.carType _typeCarOfTester
        {
            get
            {
                return carTypeOfTester;
            }
            set
            {
                carTypeOfTester = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_typeCarOfTester"));
                }
            }
        }

        //Matrix that represent the days and hours the tester work in the week.
        //Lines will be the hours betwen 9:00-15:00
        //And the rows will be the days of week Sunday-Thursday. 
        bool[][] schedule = new bool[Configuration.hoursOfWork][];
        //Property to schedule
        public bool[][] _schedule
        {
            get
            {
                return schedule;
            }
            set
            {
                schedule = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_schedule"));
                }
            }
        }

        double maxDistanceToDoTest;
        //Property to maxDistanceToDoTest
        public double _maxDistanceToDoTest
        {
            get
            {
                return maxDistanceToDoTest;
            }
            set
            {
                maxDistanceToDoTest = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_maxDistanceToDoTest"));
                }
            }
        }

        //We deleted this
        //List<Test> testsOfTester;
        ////Property of testsOfTester
        //public List<Test> _testsOfTester
        //{
        //    get
        //    {
        //        List<Test> helperList;
        //        helperList = new List<Test>();

        //        DateTime dateTime = new DateTime();
        //        dateTime = DateTime.Now;
                
        //        foreach(Test item in testsOfTester)
        //        {
        //            if (item._dateAndHourOfTest < dateTime)
        //                helperList.Add(item);
        //        }

        //        foreach (Test item in helperList)
        //            testsOfTester.RemoveAt(testsOfTester.IndexOf(item));

        //        return testsOfTester;
        //    }
        //    set
        //    {
        //         if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("_testsOfTester"));
        //        }
        //    }
        //}

        string password;
        //Property to password
        public string _password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_password"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        //Default constructor of Tester
        public Tester()
        {
            ID = 0;
            lastName= "";
            firstName= "";
            dateOfBirth=new DateTime(1996,6,3);
            genderOfTester= new myEnums.genderType();
            phoneNumber= 972500000;
            adressOfTester=new StructTypes.Adress("",-1,"");
            yearsOfExperience=-1;
            MaxTestsPerWeek=-1;
            carTypeOfTester= new myEnums.carType();

            //to matrix schedule-TO DO NEW
            //To add how many columns will have and initialiaze with false
            for (int i = 0; i < Configuration.hoursOfWork; i++)
                schedule[i] = new bool[] { false, false, false, false, false };
            
            maxDistanceToDoTest=-1;

            //We deleted this
            //testsOfTester = new List<Test>();

            password = "";
        }

        //Copy Constructor of Tester
        public Tester(Tester testerToCopy)
        {
            ID = testerToCopy._ID;
            lastName = testerToCopy.last_name;
            firstName = testerToCopy.first_name;
            dateOfBirth = testerToCopy.birth;
            genderOfTester = testerToCopy.gender;
            phoneNumber = testerToCopy._phoneNumber;
            adressOfTester = testerToCopy._adressOfTester;
            yearsOfExperience = testerToCopy._yearsOfExperience;
            MaxTestsPerWeek = testerToCopy._MaxTestsPerWeek;
            carTypeOfTester = testerToCopy._typeCarOfTester;

            //to matrix schedule-TO DO NEW
            //To add how many columns will have and initialiaze with false
            for (int i = 0; i < Configuration.hoursOfWork; i++)
                schedule[i] = new bool[] { false, false, false, false, false };

            //to copy the matrix schedule
            for (int i = 0; i < Configuration.hoursOfWork; i++)
                for (int j = 0; j < Configuration.daysOfWeek; j++)
                    schedule[i][j] = testerToCopy._schedule[i][j];

            maxDistanceToDoTest = testerToCopy._maxDistanceToDoTest;

            //We deleted this 
            ////To copy the testsOfTester
            //testsOfTester = new List<Test>();
            //foreach (Test item in testerToCopy._testsOfTester)
            //    testsOfTester.Add(item);

            password = testerToCopy._password ;
        }

        //To return the full name of the tester.
        public string nameOfTester()
        {
            return firstName + " " + lastName;
        }

        //To do the override of ToString
        public override string ToString()
        {
            string stringWithAllDetails;
            stringWithAllDetails = "Tester- \n";

            stringWithAllDetails += "ID: " + ID + "\n" +
                "Last Name: " + lastName + "First Name: " + firstName + "\n" +
                "Date of birth: " + dateOfBirth + "\n" + "Gender: " + genderOfTester + "\n"
                + "Phone number: " + phoneNumber + "\n" + "Adress: " + adressOfTester + "\n";
            return stringWithAllDetails;
        }
    }
}
