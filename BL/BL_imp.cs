using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BE;

namespace BL
{
    internal class BL_imp : IBL
    {
        #region variables

        DAL.Idal dalImp = DAL.factoryDal.getDAL();

        IEnumerable<Tester> listTesterToCheck;
        IEnumerable<Trainee> listTraineeToCheck;
        IEnumerable<Test> listTestsToCheck;

        //To help me in the search for a free and appropriate tester for a specif test
        IEnumerable<Tester> testersByCarType;
        IEnumerable<Tester> testersDistanceOK;

        Trainee trainee;

        //Delegate that return bool and parameter is long 
        //We use this in the funtion addTest
        delegate bool thisDelegate(long numb);

        //This Date time will help us in the function of traineesTests
        DateTime thisDateTime;

        #endregion

        #region Functions of the Tester
        public void addTester(Tester testerToAdd)
        {
            //If have an error in thee validation, for example the user put letter in the textBox of Id, the ID will continue the same ID, that the constructor put. 
            if (testerToAdd._ID == 0 || testerToAdd._ID < 0)
                throw new Exception("מספר ת.ז אינו תקין");

            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            if (listTesterToCheck.Count() != 0)
            {
                //Check if already have a tester with the same ID. 
                foreach (Tester item in listTesterToCheck)
                {
                    if (item._ID == testerToAdd._ID)
                        throw new Exception("כבר קיים בוחן עם אותו מספר זהות");
                }
            }

            listTraineeToCheck = dalImp.traineesList();
            listTraineeToCheck.ToList();

            if (listTraineeToCheck.Count() != 0)
            {
                //Check if already have a trainee with the same ID. 
                foreach (Trainee item in listTraineeToCheck)
                {
                    if (item._ID == testerToAdd._ID)
                        throw new Exception("קיים תלמיד עם אותו תעודת זהות");
                }
            }

            //Check if the age is ok. 
            if (testerToAdd._age < Configuration.minimumAgeTester)
                throw new Exception("הבוחן לא יכול להיות מתחת לגיל 40");
            if (testerToAdd._age > Configuration.maximumAgeTester)
                throw new Exception("הבוחן לא יכול להיות מעל גיל 85 ");

            try
            {
                dalImp.addTester(testerToAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void removeTester(Tester testerToRemove)
        {
            dalImp.removeTester(testerToRemove);
        }
        public void updateDetailsTester(Tester testerToUpdate)
        {
            if (testerToUpdate._phoneNumber == 972 || testerToUpdate._phoneNumber < 0)
                throw new Exception("מספר הפלאפון אינו תקין");

            if (testerToUpdate._yearsOfExperience == -1 || testerToUpdate._yearsOfExperience < 0)
                throw new Exception("מספר ניסיון אינו תקין");

            if (testerToUpdate._maxDistanceToDoTest == -1 || testerToUpdate._maxDistanceToDoTest < 0)
                throw new Exception("מספר מרחק מקסימלי אינו תקין");

            if (testerToUpdate._MaxTestsPerWeek == -1 || testerToUpdate._MaxTestsPerWeek < 0)
                throw new Exception("מספר שעות עבודה אינו תקין");


            int max = 0;
            //To count the max tests per week that this tester do
            max = counterMaxPerWeek(testerToUpdate);
            if (max > testerToUpdate._MaxTestsPerWeek)
                throw new Exception("יש לך יותר שעות עבודה ממה שרציתה");

            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            int i = 0;
            foreach (Tester item in listTesterToCheck)
            {
                if (item._ID == testerToUpdate._ID)
                {
                    try
                    {
                        dalImp.updateDetailsTester(testerToUpdate);
                        return;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                i++;
            }
            if (i + 1 == listTesterToCheck.Count())
                throw new Exception("הבוחן הזה לא קיים");
        }
        public IEnumerable<Tester> testersList()
        {
            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            return listTesterToCheck;
        }
        #endregion

        #region Functions of the Trainee
        public void addTrainee(Trainee TraineeToAdd)
        {
            //If have an error in thee validation, for example the user put letter in the textBox of Id, the ID will continue the same ID, that the constructor put. 
            if (TraineeToAdd._ID == 0 || TraineeToAdd._ID < 0)
                throw new Exception("מספר ת.ז אינו תקין");

            listTraineeToCheck = dalImp.traineesList();
            listTraineeToCheck.ToList();

            if (listTraineeToCheck.Count() != 0)
            {
                //Check if already have a trainee with the same ID. 
                foreach (Trainee item in listTraineeToCheck)
                {
                    if (item._ID == TraineeToAdd._ID)
                        throw new Exception("כבר קיים תלמיד עם אותו ת.ז");
                }
            }

            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            if (listTesterToCheck.Count() != 0)
            {
                //Check if already have a tester with the same ID. 
                foreach (Tester item in listTesterToCheck)
                {
                    if (item._ID == TraineeToAdd._ID)
                        throw new Exception("כבר קיים בוחן עם אותו ת.ז");
                }
            }

            //Check if the age is ok. 
            if (TraineeToAdd._age < Configuration.minimumAgeTrainee)
                throw new Exception("הגיל של התלמיד פחות מהחוק");

            try
            {
                dalImp.addTrainee(TraineeToAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void removeTrainee(Trainee TraineeToRemove)
        {
            dalImp.removeTrainee(TraineeToRemove);
        }
        public void updateDetailsTrainee(Trainee traineeToUpdate)
        {
            if (traineeToUpdate._phoneNumber == 972 || traineeToUpdate._phoneNumber < 0)
                throw new Exception("מספר הפלאפון אינו תקין");

            if (traineeToUpdate._adressOfTrainee.houseNumber == -1 || traineeToUpdate._adressOfTrainee.houseNumber < 0)
                throw new Exception("מספר הבית אינו תקין");

            if (traineeToUpdate._lessons == -1 || traineeToUpdate._lessons < 0)
                throw new Exception("מספר השיעורים אינו תקין");

            if (traineeToUpdate._typeCarOfTrainee == myEnums.carType.משאית_כבדה || traineeToUpdate._typeCarOfTrainee == myEnums.carType.משאית_בינונית || traineeToUpdate._typeCarOfTrainee == myEnums.carType.דו_גלגלי)
                if(traineeToUpdate._gearboxTypeOfTrainee!=myEnums.gearboxType.ידני)
                    throw new Exception("סוג רכב הזה אין אפשרות תיבת הילוכים אטומטי");

            listTraineeToCheck = dalImp.traineesList();
            listTraineeToCheck.ToList();

            int i = 0;
            foreach (Trainee item in listTraineeToCheck)
            {
                if (item._ID == traineeToUpdate._ID)
                {
                    try
                    {
                        dalImp.updateDetailsTrainee(traineeToUpdate);
                        return;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                i++;
            }
            if (i + 1 == listTraineeToCheck.Count())
                throw new Exception("התלמיז הזה לא קיים");
        }

        public IEnumerable<Trainee> traineesList()
        {
            listTraineeToCheck = dalImp.traineesList();
            listTraineeToCheck.ToList();

            return listTraineeToCheck;
        }
        #endregion

        #region Functions of the Test
        public void addTest(Test testToAdd)
        {
            thisDelegate checkTrainee = delegate (long number)
               {
                   listTraineeToCheck = dalImp.traineesList();
                   listTraineeToCheck.ToList();

                   foreach (Trainee item in listTraineeToCheck)
                   {
                       if (item._ID == number)
                           return true;
                   }
                   return false;
               };

            //Check if the Trainee exist
            if (!checkTrainee(testToAdd.ID_Trainee))
                throw new Exception("התלמיד הזה לא קיים");

            //Check if the number test have 8 digits
            if (!checkDigits(testToAdd.num_of_test))
                throw new Exception("המספר של הטסט פחות מ-8 ספרות");

            //Check if the number test already exist
            if (!numberTestExist(testToAdd.num_of_test))
                throw new Exception("המספר הטסט כבר קיים");

            //Check if this trainee has already done 20 lessons
            if (!checkTraineeLessons(testToAdd.ID_Trainee))
                throw new Exception("אתה צריך עוד שיעורם כדי לקבוע מבחן");

            //Check if this Trainee already passed a test with the same carType.
            if (checkIfLicenseExist(testToAdd.ID_Trainee))
                throw new Exception("כבר יש לך רישיון בסוג רכב הזה");

            //Check if the student has a test that has not yet been performed
            if (checkIfHaveTest(testToAdd.ID_Trainee))
                throw new Exception("!!נקבע לך טסט שעוד לא בוצע. אנא המתן");

            //Check if seven days have passed since the last test of this Trainee
            if (!checkSevenDays(testToAdd.ID_Trainee, testToAdd._dateOfTest))
                throw new Exception("אתה צריך לחכות מינימום 7 ימים כדי לנסות עוד מבחן");

            //To have the same car of type to trainee to this test
            trainee = new Trainee();
            trainee = returnTrainee(testToAdd.ID_Trainee);
            testToAdd._typeCarOfTest = trainee._typeCarOfTrainee;

            //Check if the Tester have the same type of car of this Trainee
            findTestersByCarType(testToAdd._typeCarOfTest);
            if (testersByCarType.Count() == 0)
                throw new Exception("לא קיים בוחן עם אותו סוג רכב");

            if (testToAdd._dateAndHourOfTest.Hour > 14 || testToAdd._dateAndHourOfTest.Hour < 9)
                throw new Exception("9:00-15:00 שעה לא תקינה, תבחר שעות בין");

            /* Checking if have Tester(in the list of testers) available in this Date test
            +Checking if the tester that we chosed is free in the hour of our Test.
            Checking if the Tester not pass the maxTestPerWeek(have to count how many tests already have the tester in this week)
            I guess that the schedule of the tester is build according to the max tests per week that he can do. And we already
            checking according to her schedule.  */
            Tester tester = new Tester();

            tester = findTesterToTest(testToAdd._dateAndHourOfTest);
            if (tester._ID == 0) 
                throw new Exception( "אין בוחן פנוי בתאריך ושעה שביקשת" +"\n"+ "תנסה בשעה או תאריך אחר" );

            testToAdd.ID_tester = tester._ID;

            try
            {
                dalImp.addTest(testToAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateDetailsTest(Test testToUpdate)
        {
            //First have to check if all details of ReportTest is full
            //Here we will check if the tester write the remarks, if yes thats means that he check all details of ReportTest
            if (testToUpdate._remark == "")
                throw new Exception("אתה צריך למלא כל הפרטים לפני העדכון");

            //Second we have to check if this test exist 
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            int i = 0;
            foreach (Test item in listTestsToCheck)
            {
                if (item.num_of_test == testToUpdate.num_of_test)
                {
                    try
                    {
                        dalImp.updateDetailsTest(testToUpdate);
                        return;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                i++;
            }
            if (i + 1 == listTestsToCheck.Count())
                throw new Exception("הטסט הזה לא קיים");
        }
        public IEnumerable<Test> testsList()
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            return listTestsToCheck;
        }
        #endregion

        #region Help functions 

        //Return Trainee from the list of Trainees
        public Trainee returnTrainee(long numberID)
        {
            listTraineeToCheck = dalImp.traineesList();
            listTraineeToCheck.ToList();

            foreach (Trainee item in listTraineeToCheck)
            {
                if (item._ID == numberID)
                    return item;
            }
            return null;
        }

        //Check if the number(ID) test already exist
        public bool numberTestExist(long numbTest)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            if (listTestsToCheck.Count() != 0)
            {
                foreach (Test item in listTestsToCheck)
                {
                    if (item.num_of_test == numbTest)
                        return false;
                }
            }
            return true;
        }

        //Check if the number test have 8 digits
        public bool checkDigits(long number)
        {
            int count = 0;
            while (number > 0)
            {
                number = number / 10;
                count++;
            }
            if (count == Configuration.digistsNumberOfTest)
                return true;
            return false;
        }

        //Check if seven days have passed since the last test of this Trainee
        public bool checkSevenDays(long IdTrainee, DateTime dateOfThisTest)
        {
            //To get the car type of this Trainee 
            trainee = new Trainee();
            trainee = returnTrainee(IdTrainee);

            //To save the last test that we are searching
            Test lastTest = new Test();

            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            if (listTestsToCheck.Count() != 0)
                lastTest = listTestsToCheck.LastOrDefault(t => t.ID_Trainee == IdTrainee && t._typeCarOfTest == trainee._typeCarOfTrainee);
            else
                return true;

            if (lastTest == null)
                return true;

            DateTime dateNewTest = new DateTime(dateOfThisTest.Year, dateOfThisTest.Month, dateOfThisTest.Day);
            DateTime dateLastTest = new DateTime(lastTest._dateOfTest.Year, lastTest._dateOfTest.Month, lastTest._dateOfTest.Day);

            thisDateTime = new DateTime();
            thisDateTime = DateTime.Now;


            TimeSpan timeDays = dateNewTest - dateLastTest;

            if ((timeDays.Days <= Configuration.daysMinimumToTest) || (dateLastTest <= thisDateTime))
                return false;

            return true;
        }

        //Check if this Trainee already passed a test with the same carType.
        public bool checkIfLicenseExist(long IdTrainee)
        {
            //To get the car type of this Trainee 
            Trainee trainee = new Trainee();
            trainee = returnTrainee(IdTrainee);

            //To save the test that he passed
            Test testSuccess = new Test();

            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            if (listTestsToCheck.Count() != 0)
                testSuccess = listTestsToCheck.FirstOrDefault(t => t.ID_Trainee == IdTrainee && t._typeCarOfTest == trainee._typeCarOfTrainee && t._succeedOrFailed == true);
            else
                return false;

            //I did first or default so if we didnt find some element, the value will be null
            if (testSuccess == null)
                return false;

            //If we find already a test that this trainee passed. 
            return true;
        }

        //Check if this trainee has already done 20 lessons
        public bool checkTraineeLessons(long IdNumber)
        {
            Trainee trainee = new Trainee();
            trainee = returnTrainee(IdNumber);

            if (trainee._lessons < Configuration.minimumLessons)
                return false;
            return true;
        }

        //Function that give us tester with the car type that we need  
        public void findTestersByCarType(myEnums.carType carType)
        {
            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            testersByCarType = from Tester item in listTesterToCheck
                               where item._typeCarOfTester == carType
                               select new Tester(item);

            testersByCarType.ToList();
        }

        public Tester findTesterToTest(DateTime dateAndTime)
        {
            DayOfWeek dayOfTest = dateAndTime.DayOfWeek;
            int day = (int)dayOfTest;

            int hourOfTest = dateAndTime.Hour;
            int hour;

            Tester tester = new Tester();

            switch (hourOfTest)
            {
                case 9:
                    hour = 0;
                    break;
                case 10:
                    hour = 1;
                    break;
                case 11:
                    hour = 2;
                    break;
                case 12:
                    hour = 3;
                    break;
                case 13:
                    hour = 4;
                    break;
                case 14:
                    hour = 5;
                    break;
                default:
                    throw new Exception("9:00-15:00 תבחר שעות בין");
            }

            var testersSameTypeCarAndWorkInThisDayOfWeek = from Tester item in testersByCarType
                       where ifWork(item, day, hour) == true
                       select new Tester(item);

            testersSameTypeCarAndWorkInThisDayOfWeek.ToList();
            //Now we have a list of testers with the same car type and working in this day of week

            var testersSameTypeCarAndWorkInThisDayOfWeekAndAvailable = from Tester item in testersSameTypeCarAndWorkInThisDayOfWeek
                                                                       where isntAvailable(item, dateAndTime) == false
                                                                       select new Tester(item);
            testersSameTypeCarAndWorkInThisDayOfWeekAndAvailable.ToList();
            //Now we have a list of testers with the same car type, that work in this day of week, and that are free in this date and hour
            if (testersSameTypeCarAndWorkInThisDayOfWeekAndAvailable.Count() == 0)
                return tester;
            
            //Now we just have to check about the distace...
            foreach (Tester item in testersSameTypeCarAndWorkInThisDayOfWeekAndAvailable)
            {
                tester = testersDistanceOK.FirstOrDefault(t => t._ID == item._ID);
                if(tester != null)
                    return tester;

            }
            tester = new Tester();
            return tester;
        }

        /*This function will receive from PLWPF a list of testers that 
        the maxDistance of them is greater than a distance between than 
        and the leaving adress of the test*/
        public void receiveTestersAcceptingDistance(List<Tester> listOfTesters)
        {
            testersDistanceOK = from Tester item in listOfTesters
                                select new Tester(item);
            testersDistanceOK.ToList();
            return ;
        }

        //This function check if have a test to this trainee that he didnt implement yet 
        bool checkIfHaveTest(long IDofTrainee)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            Test testToCheck = new Test();

            thisDateTime = DateTime.Now;

            testToCheck = listTestsToCheck.LastOrDefault(t => t.ID_Trainee == IDofTrainee && t._dateAndHourOfTest >= thisDateTime);

            if (testToCheck == null)
                return false;

            return true;
        }

        //This function return a List of testers that living from a X distance from a Adress that we receive. 
        IEnumerable<Tester> testersDistance(StructTypes.Adress adress, double disX)
        {
            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            Random rnd = new Random();
            var ourList = from t in listTesterToCheck
                              //this will be our distance between adress tester and adress that we receive
                          let numb = rnd.Next(1, 200)
                          where numb == disX
                          select new Tester(t);
            ourList.ToList();

            return ourList;
        }

        //This functions return a list of testers that are free in the date time that we receive
        IEnumerable<Tester> freeTesters(DateTime dateTime)
        {
            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            int day = dateTime.Day;
            int hour = dateTime.Hour;

            IEnumerable<Tester> returnList = listTesterToCheck.Where(t => (ifWork(t, day, hour) == true) && (isntAvailable(t, dateTime) == false));

            returnList.ToList();
            return returnList;
        }

        //This function return a list of tests that are true in the condition that we receive
        IEnumerable<Test> conditionTests(Predicate<Test> ourPredicate)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            var ourList = from item in listTestsToCheck
                          where ourPredicate(item) == true
                          select new Test(item);

            ourList.ToList();
            return ourList;
        }

        //This function return the number of test that the trainee that we receive did
        int traineesTests(Trainee trainee)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            thisDateTime = new DateTime();
            thisDateTime = DateTime.Now;

            IEnumerable<Test> ourList = listTestsToCheck.Where(t => t.ID_Trainee == trainee._ID && t._dateOfTest < thisDateTime);
            ourList.ToList();
            return ourList.Count();
        }

        //This function will check if this trainee can receive Driving License
        //This function will pass in the list of tests from the end to the begging
        bool drivingLicense(Trainee trainee)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            bool flag = false;

            foreach (Test item in listTestsToCheck)
            {
                if ((item.ID_Trainee == trainee._ID) && (item._typeCarOfTest == trainee._typeCarOfTrainee) &&
                    (item._succeedOrFailed == true))
                    flag = true;
            }
            return flag;
        }

        //This function will return a list of test that will be in the day of the month that we receive. 
        IEnumerable<Test> testsInThisDay(DateTime dayAndMonth)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            int day = dayAndMonth.Day;
            int month = dayAndMonth.Month;

            var ourList = from item in listTestsToCheck
                          where (item._dateOfTest.Day == day) && (item._dateOfTest.Month == month)
                          select new Test(item);

            ourList.ToList();
            return ourList;
        }

        //Function that count the max tests per week that this tester do accord to his schecdule matrix
        int counterMaxPerWeek(Tester tester)
        {
            int counter = 0;
            for (int i = 0; i < Configuration.hoursOfWork; i++)
                for (int j = 0; j < Configuration.daysOfWeek; j++)
                {
                    if (tester._schedule[i][j] == true)
                        counter++;
                }
            return counter;

        }

        //Function that check if this Tester already have a test in this day and this hour
        bool isntAvailable(Tester tester, DateTime dateTimeTocheck)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            if(listTestsToCheck.Count()!=0)
            {
                foreach (Test item in listTestsToCheck)
                {
                    if (item.ID_tester == tester._ID && item._dateAndHourOfTest == dateTimeTocheck)
                        return true;
                }
            }
            return false;
        }

        //Function that check if this tester work in this day and this hour 
        public bool ifWork(Tester tester, int day, int hour)
        {
            if (tester._schedule[hour][day] == true)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Grouping functions 

        //Return all the testers by car type
        public IEnumerable<IGrouping<BE.myEnums.carType, Tester>> groupTesterByCarType(bool sort = false)
        {
            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTestersList()
                    orderby t.last_name[0]
                    group t by t._typeCarOfTester into ts
                    select ts;
                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTestersList()
                    group t by t._typeCarOfTester into ts
                    select ts;
                ourList.ToList();
                return ourList;
            }
        }


        public IEnumerable<IGrouping<int, Tester>> groupTesterByAge(bool sort = false)
        {


            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTestersList()
                    orderby t.last_name[0]
                    group t by t._age into ts
                    select ts;
                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTestersList()
                    group t by t._age into ts
                    select ts;
                ourList.ToList();
                return ourList;
            }
        }



        //Return all the trainee by driving school
        public IEnumerable<IGrouping<string, Trainee>> groupTraineeByDrivingSchool(bool sort = false)
        {

            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTraineesList()
                    orderby t.last_name[0]
                    group t by t._drivingSchool into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTraineesList()
                    group t by t._drivingSchool into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
        }

        //Return all the trainee by teacher
        public IEnumerable<IGrouping<string, Trainee>> groupTraineeByNameOfTeacher(bool sort = false)
        {
            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTraineesList()
                    orderby t.last_name[0]
                    group t by t._nameOfTeacher into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTraineesList()
                    group t by t._nameOfTeacher into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
        }

        //Return all the trainee by number of test that he did
        public IEnumerable<IGrouping<int, Trainee>> groupTraineeByNumOfTests(bool sort = false)
        {

            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTraineesList()
                    let numbs = this.traineesTests(t)
                    orderby numbs, t.last_name[0]
                    group t by numbs into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTraineesList()
                    let numbs = this.traineesTests(t)
                    group t by numbs into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
        }


        public IEnumerable<IGrouping<bool, Test>> groupTestBySuccessOrNot(bool sort = false)
        {
            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTestsList()
                    orderby t.num_of_test
                    group t by t._succeedOrFailed into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTestsList()
                    group t by t._succeedOrFailed into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
        }


        public IEnumerable<IGrouping<DateTime, Test>> groupTestByDone(bool sort = false)
        {
            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTestsList(x => x._dateOfTest <= DateTime.Now)
                    orderby t.num_of_test
                    group t by t._dateOfTest into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTestsList(x => x._dateOfTest <= DateTime.Now)
                    group t by t._dateOfTest into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
        }


        public IEnumerable<IGrouping<long, Test>> groupTestByIdTester(bool sort = false)
        {
            //If have sort we will sort by the first letter of the last name.
            if (sort)
            {
                var ourList =
                    from t in FuncTestsList()
                    orderby t.num_of_test
                    group t by t.ID_tester into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
            else
            {
                var ourList =
                    from t in FuncTestsList()
                    group t by t.ID_tester into ts
                    select ts;

                ourList.ToList();
                return ourList;
            }
        }
        #endregion

        #region FUNC<T>
        //FUNC<T> of Tester
        public IEnumerable<Tester> FuncTestersList(Func<Tester, bool> predicate = null)
        {
            listTesterToCheck = dalImp.testersList();
            listTesterToCheck.ToList();

            IEnumerable<Tester> toReturn;

            if (predicate == null)
            {
                toReturn = from item in listTesterToCheck
                           select new Tester(item);

                toReturn.ToList();
                return toReturn;
            }

            toReturn = from item in listTesterToCheck
                       where predicate(item)
                       select new Tester(item);

            return toReturn;
        }


        //FUNC<T> of Trainee
        public IEnumerable<Trainee> FuncTraineesList(Func<Trainee, bool> predicate = null)
        {
            listTraineeToCheck = dalImp.traineesList();
            listTraineeToCheck.ToList();

            IEnumerable<Trainee> toReturn;

            if (predicate == null)
            {
                toReturn = from item in listTraineeToCheck
                           select new Trainee(item);

                toReturn.ToList();
                return toReturn;
            }

            toReturn = from item in listTraineeToCheck
                       where predicate(item)
                       select new Trainee(item);

            return toReturn;
        }

        //FUNC<T> of Test
        public IEnumerable<Test> FuncTestsList(Func<Test, bool> predicate = null)
        {
            listTestsToCheck = dalImp.testsList();
            listTestsToCheck.ToList();

            IEnumerable<Test> toReturn;

            if (predicate == null)
            {
                toReturn = from item in listTestsToCheck
                           select new Test(item);

                toReturn.ToList();
                return toReturn;
            }

            toReturn = from item in listTestsToCheck
                       where predicate(item)
                       select new Test(item);

            return toReturn;
        }
        #endregion
    }
}
