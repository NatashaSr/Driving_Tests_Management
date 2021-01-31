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
    /// Interaction logic for UpdateTester.xaml
    /// </summary>
    public partial class UpdateTester : Window
    {
        BE.Tester testerToUpdate;
        BL.IBL bl;
        BE.StructTypes.Adress newAdress;
        int toInt;

        public UpdateTester()
        {
            InitializeComponent();

            this.comboBoxCarType.ItemsSource = Enum.GetValues(typeof(BE.myEnums.carType));

            bl = BL.factoryBL.getBL();
            testerToUpdate = new BE.Tester();
            this.DataContext = testerToUpdate;
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

                testerToUpdate._adressOfTester = newAdress;

                //Just if everithing is correct and legal, will implemet the update
                bl.updateDetailsTester(testerToUpdate);

                //This continue if there isnt ecemption
                this.Close();

                optionsToTester opTester = new optionsToTester();
                opTester.receiveTester(testerToUpdate);
                opTester.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void receiveTester(BE.Tester testerFromNewTester)
        {
            testerToUpdate = new BE.Tester(testerFromNewTester);
            this.textBoxStreet.Text = testerToUpdate._adressOfTester.street;
            this.textBoxApart.Text = Convert.ToString(testerToUpdate._adressOfTester.houseNumber);
            this.textBoxCity.Text = testerToUpdate._adressOfTester.city;
            this.DataContext = testerToUpdate;
        }
    }
}
