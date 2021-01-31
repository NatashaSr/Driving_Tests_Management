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
    /// Interaction logic for NewTester.xaml
    /// </summary>
    public partial class NewTester : Window
    {
        BE.Tester tester;
        BL.IBL bl;

        public NewTester()
        {
            InitializeComponent();

            tester = new BE.Tester();
            this.DataContext = tester;

            bl = BL.factoryBL.getBL();

            this.testerGenderComboBox.ItemsSource= Enum.GetValues(typeof(BE.myEnums.genderType));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //This is special to the password box, because it not depency property 
                //And I dont have to worry about htis because password we just do one time.
                tester._password = passwordBox.Password;

                bl.addTester(tester);

                //The continue if there isnt excemption
                this.Close();

                UpdateTester upTester = new UpdateTester();
                upTester.receiveTester(tester);
                upTester.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
