using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    internal class Dal_imp : Idal
    {
        #region Functions to the Tester's
        //Functions to the Tester's
        public void addTester(Tester testerToAdd)
        {
            if(DataSource.listTesters.Count!=0)
            {
                foreach (Tester item in DataSource.listTesters)
                {
                    if (item._ID == testerToAdd._ID)
                        throw new Exception("כבר קיים בוחן עם אותו ת.ז");
                }
            }

            if(DataSource.listTrainees.Count!=0)
            {
                foreach (Trainee item in DataSource.listTrainees)
                {
                    if (item._ID == testerToAdd._ID)
                        throw new Exception("כבר קיים תלמיד עם אותו ת.ז");
                }
            }

            DataSource.listTesters.Add(testerToAdd);
        }

        public void removeTester(Tester testerToRemove)
        {
            int i = 0;
            foreach (Tester item in DataSource.listTesters)
            {
                if (item._ID == testerToRemove._ID)
                {
                    DataSource.listTesters.RemoveAt(i);
                    return;
                }
                i++;
            }
        }

        public void updateDetailsTester(Tester testerToUpdate)
        {
            int i = 0;
            foreach (Tester item in DataSource.listTesters)
            {
                if (item._ID == testerToUpdate._ID)
                {
                    DataSource.listTesters[i] = testerToUpdate;
                    return;
                }
                i++;
            }
            if(i+1 == DataSource.listTesters.Count())
                throw new Exception("הבוחן הזה לא קיים");
        }

        public IEnumerable<Tester> testersList()
        {
            if(DataSource.listTesters.Count()!=0)
            {
                var listToReturn = from item in DataSource.listTesters
                                   select new Tester(item);
                return listToReturn;
            }
            List<Tester> emptyListToReturn = new List<Tester>();
            return emptyListToReturn;
        }
        #endregion

        #region Functions to the Trainees
        public void addTrainee(Trainee TraineeToAdd)
        {
            if(DataSource.listTrainees.Count!=0)
            {
                foreach (Trainee item in DataSource.listTrainees)
                {
                    if (item._ID == TraineeToAdd._ID)
                        throw new Exception("התלמיד הזה לא קיים");
                }
            }
            
            if(DataSource.listTesters.Count!=0)
            {
                foreach (Tester item in DataSource.listTesters)
                {
                    if (item._ID == TraineeToAdd._ID)
                        throw new Exception("כבר קיים בוחן עם אותו ת.ז");
                }
            }

            DataSource.listTrainees.Add(TraineeToAdd);
        }

        public void removeTrainee(Trainee TraineeToRemove)
        {
            int i = 0;
            foreach (Trainee item in DataSource.listTrainees)
            {
                if (item._ID == TraineeToRemove._ID)
                {
                    DataSource.listTrainees.RemoveAt(i);
                    return;
                }
                i++;
            }
        }

        public void updateDetailsTrainee(Trainee traineeToUpdate)
        {
            int i = 0;
            foreach (Trainee item in DataSource.listTrainees)
            {
                if (item._ID == traineeToUpdate._ID)
                {
                    DataSource.listTrainees[i] = traineeToUpdate;
                    return;
                }
                i++;
            }
            if (i + 1 == DataSource.listTrainees.Count())
                throw new Exception("התלמיד הזה לא קיים");
        }
        public IEnumerable<Trainee> traineesList()
        {
            if(DataSource.listTrainees.Count() != 0)
            {
                var listToReturn = from item in DataSource.listTrainees
                                   select new Trainee(item);
                return listToReturn;
            }
            List<Trainee> emptyListToReturn = new List<Trainee>();
            return emptyListToReturn;
        }
        #endregion

        #region Functions to the Test
        public void addTest(Test testToAdd)
        {
            if(DataSource.listTests.Count!=0)
            {
                foreach (Test item in DataSource.listTests)
                {
                    if (item.num_of_test == testToAdd.num_of_test)
                        throw new Exception("הטסט הזה לא קיים");
                }
            }
            
            DataSource.listTests.Add(testToAdd);

            //We deleted this 
            //To add in the tests list of the tester a new test 
            //var tester = from item in DataSource.listTesters
            //             where testToAdd.ID_tester == item._ID
            //             select item;

            ////In this tester list will be just one element 
            //foreach(Tester item in tester)
            //    addTest(item,testToAdd);
        }

        public void updateDetailsTest(Test testToUpdate)
        {
            int i = 0;
            foreach (Test item in DataSource.listTests)
            {
                if (item.num_of_test == testToUpdate.num_of_test)
                {
                    DataSource.listTests[i] = testToUpdate;
                    return;
                }
                i++;
            }
            if (i + 1 == DataSource.listTests.Count())
                throw new Exception("הטסט הזה לא קיים");
        }
        public IEnumerable<Test> testsList()
        {
            var listToReturn = from item in DataSource.listTests
                               select new Test(item);
            return listToReturn;
        }
        #endregion

        //We deleted this 
        //Function that will add a new test in the list of tests of this tester
        //public void addTest(Tester inThisTesterAdd,Test testToAdd)
        //{
        //    inThisTesterAdd._testsOfTester.Add(testToAdd);
        //}
    }
}
