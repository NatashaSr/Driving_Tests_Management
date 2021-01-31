using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL bl;

        IEnumerable<Tester> currentTesters;
        IEnumerable<Trainee> currentTrainees;

        BE.Tester myTester;
        BE.Trainee myTrainee;

        long ID;
        string password;
        long toLong;
        
        public MainWindow()
        {
            InitializeComponent();

            bl = BL.factoryBL.getBL();
            
            currentTesters = bl.testersList();
            currentTrainees = bl.traineesList();

            myTester = new BE.Tester();
            myTrainee = new BE.Trainee();
            
        }

        private void ButtonNewStudent_Click(object sender, RoutedEventArgs e)
        {
            Window newTrainee = new NewTrainee();
            newTrainee.Show();
        }

        private void ButtonNewTester_Click(object sender, RoutedEventArgs e)
        {
            Window newTester= new NewTester();
            newTester.Show();
        }

        private void ButtonToEnter_Click(object sender, RoutedEventArgs e)
        {
            bl = BL.factoryBL.getBL();

            currentTesters = bl.testersList();
            currentTrainees = bl.traineesList();

            myTester = new BE.Tester();
            myTrainee = new BE.Trainee();

            try
            {
                //To check the ID
                if (Int64.TryParse(this.textBoxID.Text, out toLong))
                    ID = toLong;
                else
                    throw new Exception("ת.ז אינו תקין");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //To check the password;
            password = passwordBox.Password;
            
            foreach (Tester testerItem in currentTesters)
                if (testerItem._ID == ID && testerItem._password == password)
                {
                    myTester = new BE.Tester(testerItem);
                    optionsToTester opTester = new optionsToTester();
                    opTester.receiveTester(myTester);
                    opTester.Show();

                    return;
                }
            
            foreach(Trainee traineeItem in currentTrainees)
                if (traineeItem._ID == ID && traineeItem._password == password)
                {
                     myTrainee = new BE.Trainee(traineeItem);
                    optionsToTrainee opTrainee = new optionsToTrainee();
                    opTrainee.receiveTrainee(myTrainee);
                    opTrainee.Show();

                    return;
                }

            try
            {
                //If we arrive here throw that we didnt find
                throw new Exception("אחד או יותר מהפרטים אינם תקינים");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ButtonLINQ_Click(object sender, RoutedEventArgs e)
        {
            Window linq = new LINQ();
            linq.Show();
            
        }
    }
}
