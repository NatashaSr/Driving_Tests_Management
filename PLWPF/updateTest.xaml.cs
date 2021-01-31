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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for updateTest.xaml
    /// </summary>
    public partial class updateTest : Window
    {
        BE.Test currentTest;
        BE.Tester thisTester;
        BL.IBL bl;
        BE.StructTypes.Adress newAdress;
        int toInt;
        BE.StructTypes.testReport testReport;

        IEnumerable<BE.Test> ourListOfTests;

        public updateTest()
        {
            InitializeComponent();

            bl = BL.factoryBL.getBL();
            thisTester = new BE.Tester();
            currentTest = new BE.Test();
            newAdress = new BE.StructTypes.Adress();
            testReport = new BE.StructTypes.testReport();

            ourListOfTests =this.passedTestsWithoutExamination(thisTester._ID);
            this.ComboBoxTestsToUpdate.ItemsSource = ourListOfTests;

            this.ComboBoxTestsToUpdate.DisplayMemberPath = "num_of_test";
            this.ComboBoxTestsToUpdate.SelectedValuePath = "num_of_test";
            
            this.DataContext = currentTest;

        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //I have to add this here, because struct doesnt work with data binding
                newAdress.street = this.textBoxStreet.Text;

                if (Int32.TryParse(this.textBoxApart.Text, out toInt))
                    newAdress.houseNumber = toInt;
                else
                    throw new Exception("מספר הבית אינו תקין");

                newAdress.city = this.textBoxCity.Text;

                currentTest._endAdress= newAdress;

                //Here we updating the reports manual 
                testReport.keepDistance =KeepDistancePanelPass.IsChecked.Value;
                testReport.parkingBrewers = ParkingBrewersPanelPass.IsChecked.Value;
                testReport.lookingAtMirrors= LookingAtMirrorsePanelPass.IsChecked.Value;
                testReport.winkersSignal= WinkersSignalPanelPass.IsChecked.Value;
                testReport.pickUpTheHandbreak= PickUpTheHandbreakPanelPass.IsChecked.Value;
                testReport.lookAtTheSigns= LookAtTheSignsPanelPass.IsChecked.Value;
                testReport.givingRightOfWay= GivingRightOfWaPanelPass.IsChecked.Value;

                currentTest._reportsOfTest = testReport;

                //Here we updating the remark manual 
                currentTest._remark = textBoxRemark.Text;

                bl.updateDetailsTest(currentTest);

                //If everithing worked right 
                this.Close();

                if(currentTest._succeedOrFailed==true)
                    MessageBox.Show(" תלמיד מספר " + currentTest.ID_Trainee + " עבר בהצלחה  ");
                else
                    MessageBox.Show(" תלמיד מספר " + currentTest.ID_Trainee + " נכשל בטסט ");

                optionsToTester opTester = new optionsToTester();
                opTester.receiveTester(thisTester);
                opTester.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void receiveTester(BE.Tester testerFromOptionsToTester)
        {
           thisTester = new BE.Tester(testerFromOptionsToTester);

           ourListOfTests = this.passedTestsWithoutExamination(thisTester._ID);
           this.ComboBoxTestsToUpdate.ItemsSource = ourListOfTests;

            currentTest = new BE.Test();
            this.DataContext = currentTest;
        }

        //Function that return the list of tets that already passed and didnt have result yets belonging to a tester that we receive. 
        IEnumerable<BE.Test> passedTestsWithoutExamination(long IDofTester)
        {
            IEnumerable<BE.Test> listTestsToCheck;
            listTestsToCheck = bl.testsList();
            listTestsToCheck.ToList();

            DateTime thisDateTime = new DateTime();
            thisDateTime = DateTime.Now;

            IEnumerable<BE.Test> myList = listTestsToCheck.Where(t => t.ID_tester == IDofTester && t._dateAndHourOfTest<= thisDateTime && t._remark == "");
            myList.ToList();

            List<BE.Test> emptyList = new List<BE.Test>();

            if (myList.Count() == 0)
                return emptyList;

            return myList;
        }
        
        private void NumbOfTestComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxTestsToUpdate.SelectedItem is BE.Test)
            {
                this.currentTest = new BE.Test((BE.Test)this.ComboBoxTestsToUpdate.SelectedItem);
                this.DataContext = currentTest;
            }
        }
    }
}
