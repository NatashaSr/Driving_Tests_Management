using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        //Functions to the Tester's
        void addTester(Tester testerToAdd);
        void removeTester(Tester testerToRemove);
        void updateDetailsTester(Tester testerToUpdate);
        IEnumerable<Tester> testersList();
        IEnumerable<IGrouping<BE.myEnums.carType, Tester>> groupTesterByCarType(bool sort = false);
        IEnumerable<IGrouping<int, Tester>> groupTesterByAge(bool sort = false);

        //Function to the Trainee's
        void addTrainee(Trainee TraineeToAdd);
        void removeTrainee(Trainee TraineeToRemove);
        void updateDetailsTrainee(Trainee traineeToUpdate);
        IEnumerable<Trainee> traineesList();
        IEnumerable<IGrouping<string, Trainee>> groupTraineeByDrivingSchool(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> groupTraineeByNameOfTeacher(bool sort = false);
        IEnumerable<IGrouping<int, Trainee>> groupTraineeByNumOfTests(bool sort = false);

        //Function to the Test
        void addTest(Test testToAdd);
        void updateDetailsTest(Test testToUpdate);
        IEnumerable<Test> testsList();
        IEnumerable<IGrouping<bool, Test>> groupTestBySuccessOrNot(bool sort = false);
        IEnumerable<IGrouping<DateTime, Test>> groupTestByDone(bool sort = false);
        IEnumerable<IGrouping<long, Test>> groupTestByIdTester(bool sort = false);

        //Helper function
        void receiveTestersAcceptingDistance(List<Tester> listOfTesters);
    }
}
