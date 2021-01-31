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
    /// Interaction logic for NewTrainee.xaml
    /// </summary>
    public partial class NewTrainee : Window
    {
        BE.Trainee trainee;
        BL.IBL bl;

        public NewTrainee()
        {
            InitializeComponent();

            trainee = new BE.Trainee();
            this.DataContext = trainee;

            bl = BL.factoryBL.getBL();

            this.traineeGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.myEnums.genderType));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //This is special to the password box, because it not depency property 
                //And I dont have to worry about htis because password we just do one time.
                trainee._password = passwordBox.Password;

                bl.addTrainee(trainee);

                //The continue if there isnt excemption
                this.Close();

                UpdateTrainee upTrainee = new UpdateTrainee();
                upTrainee.receiveTrainee(trainee);
                upTrainee.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


