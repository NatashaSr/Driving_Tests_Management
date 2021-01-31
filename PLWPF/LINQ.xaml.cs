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
    /// Interaction logic for LINQ.xaml
    /// </summary>
    public partial class LINQ : Window
    {
        
            BL.IBL bl;
        
            public LINQ()
            {
                InitializeComponent();
                bl = BL.factoryBL.getBL();
            }

            private void ButtonTester_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                 if (this.ComboBoxTesterBy.SelectedItem == this.AllTesters)
                    {
                    LinqToALL lg = new LinqToALL();
                    lg.Source = bl.testersList();
                    
                        this.showListsTester.Content = lg;
                        return;
                    }

                if (this.ComboBoxTesterBy.SelectedItem == this.specializationOfTesters)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTesterByCarType();

                    
                          this.showListsTester.Content = lg;
                        return;
                    }

                    if (this.ComboBoxTesterBy.SelectedItem == this.AgeOfTesters)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTesterByAge();

                        this.showListsTester.Content = lg;
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private void ButtonTest_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                if (this.ComboBoxTestBy.SelectedItem == this.AllTests)
                {
                    LinqToALL lg = new LinqToALL();
                    lg.Source = bl.testsList();

                    this.showListsTest.Content = lg;
                    return;
                }
                if (this.ComboBoxTestBy.SelectedItem == this.successOrNot)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTestBySuccessOrNot();

                        this.showListsTest.Content = lg;
                        return;
                    }

                    if (this.ComboBoxTestBy.SelectedItem == this.doingOrNot)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTestByDone();

                        this.showListsTest.Content = lg;
                        return;
                    }

                    if (this.ComboBoxTestBy.SelectedItem == this.IdOfTest)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTestByIdTester();

                        this.showListsTest.Content = lg;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private void ButtonTrainee_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                if (this.ComboBoxTraineeBy.SelectedItem == this.AllTrainees)
                {
                    LinqToALL lg = new LinqToALL();
                    lg.Source = bl.traineesList();

                    this.showListsTrainee.Content = lg;
                    return;
                }

                if (this.ComboBoxTraineeBy.SelectedItem == this.schoolName)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTraineeByDrivingSchool();

                        this.showListsTrainee.Content = lg;
                        return;
                    }

                    if (this.ComboBoxTraineeBy.SelectedItem == this.driningTeacher)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTraineeByNameOfTeacher();

                        this.showListsTrainee.Content = lg;
                        return;
                    }

                    if (this.ComboBoxTraineeBy.SelectedItem == this.numdOfTestsDoing)
                    {
                        LinqGroupingUserControl1 lg = new LinqGroupingUserControl1();
                        lg.Source = bl.groupTraineeByNumOfTests();

                        this.showListsTrainee.Content = lg;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
