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
    /// Interaction logic for UpdateTrainee.xaml
    /// </summary>
    public partial class UpdateTrainee : Window
    {
        BE.Trainee traineeToUpdate;
        BL.IBL bl;
        BE.StructTypes.Adress newAdress;
        int toInt;
        
        public UpdateTrainee()
        {
            InitializeComponent();

            this.comboBoxCarType.ItemsSource= Enum.GetValues(typeof(BE.myEnums.carType));
            this.comboBoxGearboxType.ItemsSource=Enum.GetValues(typeof(BE.myEnums.gearboxType));

            bl = BL.factoryBL.getBL();
            traineeToUpdate = new BE.Trainee();
            this.DataContext = traineeToUpdate;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //I have to add this here, because struct doesnt work with data binding
                newAdress.street = this.textBoxStreet.Text;

                if (Int32.TryParse(this.textBoxApart.Text , out toInt))
                    newAdress.houseNumber = toInt;
                else
                    throw new Exception("מספר הבית אינו תקין");
                
                newAdress.city = this.textBoxCity.Text;

                traineeToUpdate._adressOfTrainee = newAdress;

                //Just if everithing is correct and legal, will implemet the update
                bl.updateDetailsTrainee(traineeToUpdate);

                //This continue if there isnt excemption
                this.Close();

                optionsToTrainee opTrainee = new optionsToTrainee();
                opTrainee.receiveTrainee(traineeToUpdate);
                opTrainee.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void receiveTrainee(BE.Trainee traineeFromNewTrainee)
        {
            traineeToUpdate = new BE.Trainee(traineeFromNewTrainee);
            this.textBoxStreet.Text = traineeToUpdate._adressOfTrainee.street;
            this.textBoxApart.Text =Convert.ToString(traineeToUpdate._adressOfTrainee.houseNumber);
            this.textBoxCity.Text = traineeToUpdate._adressOfTrainee.city;
            this.DataContext = traineeToUpdate;
        }
    }
}
