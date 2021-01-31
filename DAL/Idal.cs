using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        //Functions to the Tester's
        void addTester(Tester testerToAdd);
        void removeTester(Tester testerToRemove);
        void updateDetailsTester(Tester testerToUpdate);
        IEnumerable<Tester> testersList();

        //Function to the Trainee's
        void addTrainee(Trainee TraineeToAdd);
        void removeTrainee(Trainee TraineeToRemove);
        void updateDetailsTrainee(Trainee traineeToUpdate);
        IEnumerable<Trainee> traineesList();

        //Function to the Test
        void addTest(Test testToAdd);
        void updateDetailsTest(Test testToUpdate);
        IEnumerable<Test> testsList();
        
    }
}
