using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BE
{
    public class Trainee : INotifyPropertyChanged
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

        myEnums.genderType genderOfTrainee;
        //Property to the gender
        public myEnums.genderType gender
        {
            get
            {
                return genderOfTrainee;
            }
            set
            {
                genderOfTrainee= value;
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

        StructTypes.Adress adressOfTrainee;
        //Property to Adress of Trainee
        public StructTypes.Adress _adressOfTrainee
        {
            get
            {
                return adressOfTrainee;
            }
            set
            {
                adressOfTrainee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_adressOfTrainee"));
                }
            }
        }

        myEnums.carType carTypeOfTrainee;
        //Property to carType
        public myEnums.carType _typeCarOfTrainee
        {
            get
            {
                return carTypeOfTrainee;
            }
            set
            {
                carTypeOfTrainee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_typeCarOfTrainee"));
                }
            }
        }

        myEnums.gearboxType gearboxTypeOfTrainee;
        //Property to gearboxTypeOfTrainee
        public myEnums.gearboxType _gearboxTypeOfTrainee
        {
            get
            {
                return gearboxTypeOfTrainee;
            }
            set
            {
                gearboxTypeOfTrainee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_gearboxTypeOfTrainee"));
                }
            }
        }
        string drivingSchool;
        //Property to drivingSchool
        public string _drivingSchool
        {
            get
            {
                return drivingSchool;
            }
            set
            {
                drivingSchool = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_drivingSchool"));
                }
            }
        }

        string nameOfTeacher;
        //Property to nameOfTeacher
        public string _nameOfTeacher
        {
            get
            {
                return nameOfTeacher;
            }
            set
            {
                nameOfTeacher = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_nameOfTeacher"));
                }
            }
        }

        int numberOfLessons;
        //Property of numberOfLessons
        public int _lessons
        {
            get
            {
                return numberOfLessons;
            }
            set
            {
                numberOfLessons = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("_lessons"));
                }
            }
        }

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
        public Trainee()
        {
            ID =0 ;
            lastName = "";
            firstName = "";
            dateOfBirth = new DateTime(1996,6,3);
            genderOfTrainee = new myEnums.genderType();
            phoneNumber =972500000;
            adressOfTrainee = new StructTypes.Adress("", -1, "");
            carTypeOfTrainee = new myEnums.carType();
            gearboxTypeOfTrainee =new myEnums.gearboxType();
            drivingSchool= "";
            nameOfTeacher = "";
            numberOfLessons=-1;
            password = "";
        }

        //Copy Constructor to Trainee
        public Trainee(Trainee traineeToCopy)
        {
            ID = traineeToCopy._ID;
            lastName = traineeToCopy.last_name;
            firstName = traineeToCopy.first_name;
            dateOfBirth = traineeToCopy.birth;
            genderOfTrainee = traineeToCopy.gender;
            phoneNumber = traineeToCopy._phoneNumber;
            adressOfTrainee = traineeToCopy._adressOfTrainee;
            carTypeOfTrainee = traineeToCopy._typeCarOfTrainee;
            gearboxTypeOfTrainee = traineeToCopy._gearboxTypeOfTrainee;
            drivingSchool = traineeToCopy._drivingSchool;
            nameOfTeacher = traineeToCopy._nameOfTeacher;
            numberOfLessons = traineeToCopy._lessons;
            password = traineeToCopy._password;
        }

        //To do the override of ToString
        public override string ToString()
        {
            string stringWithAllDetails;
            stringWithAllDetails = "Trainee- \n";

            stringWithAllDetails += "ID: " + ID + "\n" +
                "Last Name: " + lastName + "First Name: " + firstName + "\n" +
                "Date of birth: " + dateOfBirth + "\n" + "Gender: " + genderOfTrainee + "\n"
                + "Phone number: " + phoneNumber + "\n" + "Adress: " + adressOfTrainee + "\n";
            return stringWithAllDetails;
        }
    }
}
