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
    /// Interaction logic for optionsToTrainee.xaml
    /// </summary>
    public partial class optionsToTrainee : Window
    {
        BE.Trainee currentTrainee;
        BL.IBL bl;

        public optionsToTrainee()
        {
            InitializeComponent();

            bl = BL.factoryBL.getBL();
            currentTrainee = new BE.Trainee();
            this.DataContext = currentTrainee;
        }

        //UPDATE DETAILS
        private void ButtonUpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UpdateTrainee upTrainee = new UpdateTrainee();
            upTrainee.receiveTrainee(currentTrainee);
            upTrainee.Show();
        }

        //SET A TEST
        private void ButtonSetTest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            addNewTest setTest = new addNewTest();
            setTest.receiveTrainee(currentTrainee);
            setTest.Show();
        }

        //REMOVE
        private void ButtonRemoveTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.removeTrainee(currentTrainee);
                this.Close();
                MessageBox.Show("התלמיד נמחק בהצלחה");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void receiveTrainee(BE.Trainee traineeFromUpdateTrainee)
        {
            currentTrainee = new BE.Trainee(traineeFromUpdateTrainee);
            this.DataContext = currentTrainee;
        }
    }
}
