using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BE;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    class Dal_XML_imp : Idal
    {
        //Definitions and variables
        XElement traineeRoot;
        string traineePath = @"traineeXml.xml";

        XElement testerRoot;
        string testerPath = @"testerXml.xml";

        XElement testRoot;
        string testPath = @"testXml.xml";

        //File helper principally to number of the Test. 
        XElement configRoot;
        string configPath = @"config.xml";

        //Constructor
        public Dal_XML_imp()
        {
            //The truth is that I just have to check one of the files. 
            if (!File.Exists(traineePath) && !File.Exists(testerPath) && !File.Exists(testPath))
            {
                //Create files
                traineeRoot = new XElement("Trainees");
                traineeRoot.Save(traineePath);

                testerRoot = new XElement("Testers");
                testerRoot.Save(testerPath);

                testRoot = new XElement("Tests");
                testRoot.Save(testPath);

                configRoot = new XElement("StaticVariables");
                configRoot.Add(new XElement("numberOfTest", BE.Configuration.numberOfTest));
                configRoot.Save(configPath);
            }
            else //If already exist lets load data from them 
            {
                try
                {
                    traineeRoot = XElement.Load(traineePath);
                    testerRoot = XElement.Load(testerPath);
                    testRoot = XElement.Load(testPath);
                    configRoot = XElement.Load(configPath);
                    BE.Configuration.numberOfTest = long.Parse(configRoot.Element("numberOfTest").Value);
                }
                catch
                {
                    throw new Exception("File upload problem");
                }
            }
        }

        #region help convert functions
        XElement ConvertTesterToXElement(Tester tester)
        {
            XElement testerElement = new XElement("tester");

            foreach (PropertyInfo item in typeof(Tester).GetProperties())
                if (item.Name != "_adressOfTester" && item.Name != "_schedule" && /*item.Name != "_testsOfTester" &&*/ item.Name != "_maxDistanceToDoTest")
                    testerElement.Add(new XElement(item.Name, item.GetValue(tester, null)));

            //To the max distance to test because double is having problems...
            XElement maxDistante = new XElement("_maxDistanceToDoTest", tester._maxDistanceToDoTest);
            testerElement.Add(maxDistante);

            //To the adress because is struct type
            XElement adress = new XElement("_adressOfTester");

            XElement street = new XElement("street", tester._adressOfTester.street);
            XElement houseNumber = new XElement("houseNumber", tester._adressOfTester.houseNumber);
            XElement city = new XElement("city", tester._adressOfTester.city);

            adress.Add(street, houseNumber, city);

            testerElement.Add(adress);

            //Special to the schedule because is a jagged array 
            XElement schedule= new XElement("_schedule");
            for (int i=0;i<BE.Configuration.hoursOfWork;i++)
                for (int j=0;j<BE.Configuration.daysOfWeek;j++)
                {
                    XElement newCell = new XElement("_" + i + "_" + j, tester._schedule[i][j]);
                    schedule.Add(newCell);
                }

            testerElement.Add(schedule);

            //WE deleted this 
            //Special to the list of tests of each tester
            //XElement testsOfTester = new XElement("_testsOfTester");

            //foreach (BE.Test test in tester._testsOfTester)
            //{
            //    testsOfTester.Add(ConvertTestToXElement(test));
            //}

            //testerElement.Add(testsOfTester);

            return testerElement;

        }
        Tester ConvertXElementToTester(XElement element)
        {
            Tester tester = new Tester();
            StructTypes.Adress myAdress = new StructTypes.Adress();

            foreach (PropertyInfo item in typeof(Tester).GetProperties())
            {
                if (item.Name != "_adressOfTester" && item.Name != "_schedule" && /*item.Name != "_testsOfTester" &&*/ item.Name!= "_maxDistanceToDoTest")
                    {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                    object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                    item.SetValue(tester, convertValue);
                }
            }

            //Special to the max distance to test because double is having problems...
            tester._maxDistanceToDoTest = double.Parse(element.Element("_maxDistanceToDoTest").Value);

            //Special to the struct adress;
            myAdress.street = element.Element("_adressOfTester").Element("street").Value;
            myAdress.houseNumber = int.Parse(element.Element("_adressOfTester").Element("houseNumber").Value);
            myAdress.city = element.Element("_adressOfTester").Element("city").Value;

            tester._adressOfTester = myAdress;

            //Special to the schedule of tester
            for (int i = 0; i < BE.Configuration.hoursOfWork; i++)
                for (int j = 0; j < BE.Configuration.daysOfWeek; j++)
                {
                    tester._schedule[i][j] = bool.Parse(element.Element("_schedule").Element("_"+i + "_" + j).Value);
                }

            //We deleted this 
            ////Special to the list of tests of each tester
            //foreach(XElement item in testerRoot.Elements("_testsOfTester"))
            //{
            //    tester._testsOfTester.Add(ConvertXElementToTest(item));
            //}

            return tester;
        }

        /*_______________________________________________*/

        XElement ConvertTraineeToXElement(Trainee trainee)
        {
            XElement traineeElement = new XElement("trainee");
            
            foreach (PropertyInfo item in typeof(Trainee).GetProperties())
                if(item.Name!= "_adressOfTrainee")
                    traineeElement.Add(new XElement(item.Name, item.GetValue(trainee, null)));

            //To the adress becaus is struct type
            XElement adress = new XElement("_adressOfTrainee");

            XElement street = new XElement("street", trainee._adressOfTrainee.street);
            XElement houseNumber = new XElement("houseNumber", trainee._adressOfTrainee.houseNumber);
            XElement city = new XElement("city", trainee._adressOfTrainee.city);

            adress.Add(street, houseNumber, city);

            traineeElement.Add(adress);

            return traineeElement;
        }
        Trainee ConvertXElementToTrainee(XElement element)
        {
            Trainee trainee = new Trainee();
            StructTypes.Adress myAdress = new StructTypes.Adress();

            foreach (PropertyInfo item in typeof(Trainee).GetProperties())
            {
                if(item.Name!= "_adressOfTrainee")
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                    object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                    item.SetValue(trainee, convertValue);
                }
            }

            //Special to the struct adress;
            myAdress.street = element.Element("_adressOfTrainee").Element("street").Value;
            myAdress.houseNumber = int.Parse(element.Element("_adressOfTrainee").Element("houseNumber").Value);
            myAdress.city = element.Element("_adressOfTrainee").Element("city").Value;

            trainee._adressOfTrainee = myAdress;

            return trainee;
        }

        /*_______________________________________________*/

        XElement ConvertTestToXElement(Test test)
        {
            XElement testElement = new XElement("test");

            foreach (PropertyInfo item in typeof(Test).GetProperties())
                if (item.Name != "_leavingAdress" && item.Name != "_endAdress" && item.Name != "_reportsOfTest")
                    testElement.Add(new XElement(item.Name, item.GetValue(test, null)));

            //To the leaving adress because is struct type
            XElement leavingAdress = new XElement("_leavingAdress");

            XElement streetL = new XElement("street", test._leavingAdress.street);
            XElement houseNumberL = new XElement("houseNumber", test._leavingAdress.houseNumber);
            XElement cityL = new XElement("city", test._leavingAdress.city);

            leavingAdress.Add(streetL, houseNumberL, cityL);

            //To the end adress because is struct type
            XElement endAdress = new XElement("_endAdress");

            XElement streetE = new XElement("street", test._endAdress.street);
            XElement houseNumberE = new XElement("houseNumber", test._endAdress.houseNumber);
            XElement cityE = new XElement("city", test._endAdress.city);

            endAdress.Add(streetE, houseNumberE, cityE);

            //To the test report because is struct type
            XElement reportTest = new XElement("_reportsOfTest");

            XElement keepDistance = new XElement("keepDistance", test._reportsOfTest.keepDistance);
            XElement parkingBrewers = new XElement("parkingBrewers", test._reportsOfTest.parkingBrewers);
            XElement lookingAtMirrors = new XElement("lookingAtMirrors", test._reportsOfTest.lookingAtMirrors);
            XElement winkersSignal = new XElement("winkersSignal", test._reportsOfTest.winkersSignal);
            XElement pickUpTheHandbreak = new XElement("pickUpTheHandbreak", test._reportsOfTest.pickUpTheHandbreak);
            XElement PedestrainCrossing = new XElement("PedestrainCrossing", test._reportsOfTest.PedestrainCrossing);
            XElement lookAtTheSigns = new XElement("lookAtTheSigns", test._reportsOfTest.lookAtTheSigns);
            XElement givingRightOfWay = new XElement("givingRightOfWay", test._reportsOfTest.givingRightOfWay);

            reportTest.Add(keepDistance, parkingBrewers, lookingAtMirrors, winkersSignal, pickUpTheHandbreak, PedestrainCrossing, lookAtTheSigns, givingRightOfWay);
            
            testElement.Add(leavingAdress, endAdress, reportTest);

            return testElement;
        }
        Test ConvertXElementToTest(XElement element)
        {
            Test test = new Test();
            StructTypes.Adress myLeavingAdress = new StructTypes.Adress();
            StructTypes.Adress myEndAdress = new StructTypes.Adress();
            StructTypes.testReport myTestReport = new StructTypes.testReport();

            foreach (PropertyInfo item in typeof(Test).GetProperties())
            {
                if (item.Name != "_leavingAdress" && item.Name != "_endAdress" && item.Name != "_reportsOfTest")
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                    object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                    item.SetValue(test, convertValue);
                }
            }

            //Special to the struct leaving adress;
            myLeavingAdress.street = element.Element("_leavingAdress").Element("street").Value;
            myLeavingAdress.houseNumber = int.Parse(element.Element("_leavingAdress").Element("houseNumber").Value);
            myLeavingAdress.city = element.Element("_leavingAdress").Element("city").Value;

            test._leavingAdress = myLeavingAdress;

            //Special to the struct end adress;
            myEndAdress.street = element.Element("_endAdress").Element("street").Value;
            myEndAdress.houseNumber = int.Parse(element.Element("_endAdress").Element("houseNumber").Value);
            myEndAdress.city = element.Element("_endAdress").Element("city").Value;

            test._endAdress = myEndAdress;

            //Special to the struct report of test;
            myTestReport.keepDistance = bool.Parse(element.Element("_reportsOfTest").Element("keepDistance").Value);
            myTestReport.parkingBrewers = bool.Parse(element.Element("_reportsOfTest").Element("parkingBrewers").Value);
            myTestReport.lookingAtMirrors = bool.Parse(element.Element("_reportsOfTest").Element("lookingAtMirrors").Value);
            myTestReport.winkersSignal = bool.Parse(element.Element("_reportsOfTest").Element("winkersSignal").Value);
            myTestReport.pickUpTheHandbreak = bool.Parse(element.Element("_reportsOfTest").Element("pickUpTheHandbreak").Value);
            myTestReport.PedestrainCrossing = bool.Parse(element.Element("_reportsOfTest").Element("PedestrainCrossing").Value);
            myTestReport.lookAtTheSigns = bool.Parse(element.Element("_reportsOfTest").Element("lookAtTheSigns").Value);
            myTestReport.givingRightOfWay = bool.Parse(element.Element("_reportsOfTest").Element("givingRightOfWay").Value);

            test._reportsOfTest = myTestReport;

            return test;
        }
        #endregion

        #region tester functions 
        //Functions to the Tester's
        public void addTester(Tester testerToAdd)
        {
            Trainee trainee = new Trainee();
            Tester tester = new Tester();

            //To verify if already have a trainee with the same ID
            trainee = getTraineeByID(testerToAdd._ID);
            if (trainee != null)
                throw new Exception("כבר קיים תלמיד עם אותו תעודת הזהות");

            //To verify if already have a tester with the same ID
            tester = getTesterByID(testerToAdd._ID);
            if (tester != null)
                throw new Exception("כבר קיים בוחן עם אותו תעודת הזהות");

            //If everething is right will continue. 
            testerRoot.Add(ConvertTesterToXElement(testerToAdd));
            testerRoot.Save(testerPath);
        }
        public void removeTester(Tester testerToRemove)
        {
            XElement XtesterToRemoveX;
            try
            {
                XtesterToRemoveX = (from item in testerRoot.Elements()
                                     where long.Parse(item.Element("_ID").Value) == testerToRemove._ID
                                     select item).FirstOrDefault();
                if (XtesterToRemoveX == null)
                    throw new Exception("מחיקה לא בוצעה");

                XtesterToRemoveX.Remove();
                testerRoot.Save(testerPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateDetailsTester(Tester testerToUpdate)
        {
            XElement testerUpdateX;

            try
            {
                testerUpdateX = (from item in testerRoot.Elements()
                                  where long.Parse(item.Element("_ID").Value) == testerToUpdate._ID
                                  select item).FirstOrDefault();

                if (testerUpdateX == null)
                    throw new Exception("הבוחן הזה לא קיים");

                this.removeTester(testerToUpdate);
                this.addTester(testerToUpdate);
                
                testerRoot.Save(testerPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Tester getTesterByID(long ID)
        {
            XElement currentTester = null;

            try
            {
                currentTester = (from item in testerRoot.Elements()
                                  where long.Parse(item.Element("_ID").Value) == ID
                                  select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (currentTester == null)
                return null;

            return ConvertXElementToTester(currentTester);
        }
        public IEnumerable<Tester> testersList()
        {
            IEnumerable<Tester> testers;
            List<Tester> emptyListToReturn;
            try
            {
                testers = (from testerXML in testerRoot.Elements()
                            select new Tester(ConvertXElementToTester(testerXML)));

                if (testers.Count() == 0)
                {
                    emptyListToReturn = new List<Tester>();
                    return emptyListToReturn;
                }

                return testers;
            }
            catch
            {
                throw new Exception("קרה תקלה בחזרה רשימה של בוחנים");
            }
        }
        #endregion

        #region trainee functions 
        //Function to the Trainee's
        public void addTrainee(Trainee TraineeToAdd)
        {
            Trainee trainee = new Trainee();
            Tester tester = new Tester();

            //To verify if already have a trainee with the same ID
            trainee = getTraineeByID(TraineeToAdd._ID);
            if (trainee != null)
                throw new Exception("כבר קיים תלמיד עם אותו תעודת הזהות");

            //To verify if already have a tester with the same ID
            tester = getTesterByID(TraineeToAdd._ID);
            if(tester!=null)
                throw new Exception("כבר קיים בוחן עם אותו תעודת הזהות");

            //If everething is right will continue. 
            traineeRoot.Add(ConvertTraineeToXElement(TraineeToAdd));
            traineeRoot.Save(traineePath);
        }
        public void removeTrainee(Trainee TraineeToRemove)
        {
            XElement XtraineeToRemoveX;
            try
            {
                XtraineeToRemoveX = (from item in traineeRoot.Elements()
                                   where long.Parse(item.Element("_ID").Value) == TraineeToRemove._ID
                                   select item).FirstOrDefault();
                if(XtraineeToRemoveX == null)
                    throw new Exception("קרה תקלה במחיקה");

                XtraineeToRemoveX.Remove();
                traineeRoot.Save(traineePath);
            }
            catch
            {
                throw new Exception("מחיקה לא בוצעה");
            }
        }
        public void updateDetailsTrainee(Trainee traineeToUpdate)
        {
            XElement traineeUpdateX;

            try
            {
                traineeUpdateX = (from item in traineeRoot.Elements()
                                     where long.Parse(item.Element("_ID").Value) == traineeToUpdate._ID
                                     select item).FirstOrDefault();

                if (traineeUpdateX == null)
                    throw new Exception("התלמיד הזה לא קיים");

                this.removeTrainee(traineeToUpdate);
                this.addTrainee(traineeToUpdate);

                traineeRoot.Save(traineePath);
            }
            catch
            {
                throw new Exception("התלמיד הזה לא קיים");
            }
        }
        public Trainee getTraineeByID(long ID)
        {
            XElement currentTrainee = null;

            try
            {
                currentTrainee = (from item in traineeRoot.Elements()
                                  where long.Parse(item.Element("_ID").Value) == ID
                                  select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (currentTrainee == null)
                return null;

            return ConvertXElementToTrainee(currentTrainee);
        }
        public IEnumerable<Trainee> traineesList()
        {
            IEnumerable<Trainee> trainees;
            List<Trainee> emptyListToReturn;
            try
            {
                trainees = (from traineeXML in traineeRoot.Elements()
                            select new Trainee(ConvertXElementToTrainee(traineeXML)));

                if (trainees.Count() == 0)
                {
                    emptyListToReturn = new List<Trainee>();
                    return emptyListToReturn;
                }
                
                return trainees;
            }
            catch
            {
                throw new Exception("קרה תקלה בחזרה רשימה של תלמידים");
            }
        }
        #endregion

        #region test functions 
        //Function to the Test
        public void addTest(Test testToAdd)
        {
            Test test = new Test();

            //To verify if already have a trainee with the same ID
            test = getTestByNumb(test.num_of_test);
            if (test != null)
                throw new Exception("הטסט הזה כבר קיים");

            //If everething is right will continue. 
            testRoot.Add(ConvertTestToXElement(testToAdd));
            testRoot.Save(testPath);
            
            BE.Configuration.numberOfTest++; //To the next test
            //To update the number of test (Configuration.satic) in the file.
            configRoot.Element("numberOfTest").SetValue(BE.Configuration.numberOfTest);
            configRoot.Save(configPath);

            //We deleted this 
            //To update the list of tests of each tester
            //Tester addTestinTester = new Tester();
            //addTestinTester = getTesterByID(testToAdd.ID_tester);
            //addTestinTester._testsOfTester.Add(testToAdd);
            //this.removeTester(addTestinTester);
            //this.addTester(addTestinTester);
            
        }
        public void removeTest(Test TestToRemove)
        {
            XElement XtestToRemoveX;
            try
            {
                XtestToRemoveX = (from item in testRoot.Elements()
                                     where long.Parse(item.Element("num_of_test").Value) == TestToRemove.num_of_test
                                     select item).FirstOrDefault();
                if (XtestToRemoveX == null)
                    throw new Exception("מחיקה לא בוצעה");

                XtestToRemoveX.Remove();
                testRoot.Save(testPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateDetailsTest(Test testToUpdate)
        {
            XElement testToUpdateX;

            try
            {
                testToUpdateX = (from item in testRoot.Elements()
                                  where long.Parse(item.Element("num_of_test").Value) == testToUpdate.num_of_test
                                  select item).FirstOrDefault();

                if (testToUpdateX == null)
                    throw new Exception("המבחן הזה לא קיים");

                //I create delete test just for the update
                this.removeTest(testToUpdate);
                this.addTest(testToUpdate);

                testRoot.Save(testPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Test getTestByNumb(long numbOfTest)
        {
            XElement currentTest = null;

            try
            {
                currentTest = (from item in testRoot.Elements()
                                  where long.Parse(item.Element("num_of_test").Value) == numbOfTest
                               select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            if (currentTest == null)
                return null;

            return ConvertXElementToTest(currentTest);
        }
        public IEnumerable<Test> testsList()
        {
            IEnumerable<Test> tests;
            List<Test> emptyListToReturn;
            try
            {
                tests = (from testXML in testRoot.Elements()
                            select new Test(ConvertXElementToTest(testXML)));

                if (tests.Count() == 0)
                {
                    emptyListToReturn = new List<Test>();
                    return emptyListToReturn;
                }

                return tests;
            }
            catch
            {
                throw new Exception("קרה תקלה בחזרה רשימה של מבחנים");
            }
        }
        #endregion
    }
}



