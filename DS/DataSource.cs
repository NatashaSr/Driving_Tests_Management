using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BE;

namespace DS
{
    public class DataSource
    {
        public static ObservableCollection<Tester> listTesters=new ObservableCollection<Tester>();
        public static ObservableCollection<Trainee> listTrainees=new ObservableCollection<Trainee>();
        public static ObservableCollection<Test> listTests= new ObservableCollection<Test>();
    }
}
