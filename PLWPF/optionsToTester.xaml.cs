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
    /// Interaction logic for optionsToTester.xaml
    /// </summary>

    public partial class optionsToTester : Window
    {
        BE.Tester currentTester;
        BL.IBL bl;

        public optionsToTester()
        {
            InitializeComponent();

            bl = BL.factoryBL.getBL();
            currentTester = new BE.Tester();
            this.DataContext = currentTester;
        }

        //UPDATE DETAILS
        private void ButtonUpdateTester_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UpdateTester upTester = new UpdateTester();
            upTester.receiveTester(currentTester);
            upTester.Show();
        }

        //UPDATE TEST
        private void ButtonUpdateTest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            updateTest upTest = new updateTest();
            upTest.receiveTester(currentTester);
            upTest.Show();
        }

        //REMOVE
        private void ButtonRemoveTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.removeTester(currentTester);
                this.Close();
                MessageBox.Show("הבוחן נמחק בהצלחה");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void receiveTester(BE.Tester testerFromUpdateTester)
        {
            currentTester = new BE.Tester(testerFromUpdateTester);
            this.DataContext = currentTester;
        }
        

    }
}
