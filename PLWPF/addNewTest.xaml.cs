using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for addNewTest.xaml
    /// </summary>
    public partial class addNewTest : Window
    {
        BE.Trainee traineeToTest;
        BE.Test newTest;
        BL.IBL bl;
        BE.StructTypes.Adress leavingAdress;
        int toInt;
        int hour;
        DateTime currentTime;

        //To help me with the work with Thread and the Web request
        BackgroundWorker myBGW;
        String API_KEY = @"Kgd1wzGrptITKIvK6IngG3MXUG1rUMrt";
        bool isRunnig;
        List<BE.Tester> testersDistanceOK;
        IEnumerable<BE.Tester> testers;

        public addNewTest()
        {
            InitializeComponent();

            bl = BL.factoryBL.getBL();
            traineeToTest = new BE.Trainee();
            newTest = new BE.Test();
            leavingAdress = new BE.StructTypes.Adress();

            this.DataContext = newTest;
            isRunnig = false;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {   try
            {
                //All the logic to add test is inside the background worker
                if (isRunnig == true)
                    throw new Exception("נא להמתין");

                isRunnig = true;

                //To fix the hour of the test
                if (Int32.TryParse(this.textBoxChooseHour.Text, out hour))
                    newTest._dateAndHourOfTest = new DateTime(newTest._dateOfTest.Year, newTest._dateOfTest.Month, newTest._dateOfTest.Day, hour, 0, 0);
                else
                    throw new Exception("שעת המבחן אינה תקינה");

                //I have to add this here, because struct doesnt work with data binding
                leavingAdress.street = this.textBoxStreet.Text;

                if (Int32.TryParse(this.textBoxNumbHome.Text, out toInt))
                    leavingAdress.houseNumber = toInt;
                else
                    throw new Exception("מספר הבית אינו תקין");

                leavingAdress.city = this.textBoxCity.Text;

                newTest._leavingAdress = leavingAdress;

                //To check if the date time already pass
                currentTime = new DateTime();
                currentTime = DateTime.Now;
                if (newTest._dateAndHourOfTest < currentTime)
                    throw new Exception("התאריך הזה כבר עבר");

                testersDistanceOK = new List<BE.Tester>();

                //Here we start working with BackgroundWorker
                myBGW = new BackgroundWorker();

                //To send
                testers = bl.testersList();
                testers.ToList();

                myBGW.DoWork += Backgroundworker_DoWork;
                myBGW.RunWorkerCompleted += Backgroundworker_RunWorkerCompleted;
                myBGW.RunWorkerAsync(testers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //We dont have exceptions to throw  when we calculate distance with BackgroundWorker
                isRunnig = false;
            }
        }

        public void receiveTrainee(BE.Trainee traineeFromOptionsToTrainee)
        {
            traineeToTest = new BE.Trainee(traineeFromOptionsToTrainee);
            newTest.ID_Trainee = traineeToTest._ID;
            this.DataContext = newTest;
        }
        
        private void Backgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //My finish
            isRunnig = false;
            BackgroundWorker BW = sender as BackgroundWorker;
            BW.Dispose();
        }
        private void Backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<BE.Tester> listTester = new List<BE.Tester>();
                foreach (BE.Tester item in e.Argument as IEnumerable<BE.Tester>)
                    listTester.Add(item);

                double result;

                foreach (BE.Tester item in listTester)
                {
                    string origin = item._adressOfTester.street + " " + item._adressOfTester.houseNumber + " st. " + item._adressOfTester.city;
                    string destination = leavingAdress.street + " " + leavingAdress.houseNumber + " st. " + leavingAdress.city;
                    string KEY = API_KEY;

                    string url = @"https://www.mapquestapi.com/directions/v2/route" +
                    @"?key=" + KEY +
                    @"&from=" + origin +
                    @"&to=" + destination +
                    @"&outFormat=xml" +
                    @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                    @"&enhancedNarrative=false&avoidTimedConditions=false";

                    //request from MapQuest service the distance between the 2 addresses
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader sreader = new StreamReader(dataStream);
                    string responsereader = sreader.ReadToEnd();
                    response.Close();
                    //the response is given in an XML format
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(responsereader);

                    if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
                    //we have the expected answer
                    {
                        //display the returned distance
                        XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                        double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);

                        result = (distInMiles * 1.609344);
                        //because the result does not relate to the points written in the file in the distance part
                        result = result*0.001;
                        e.Result = result;

                        if (result <= item._maxDistanceToDoTest)
                            testersDistanceOK.Add(item);
                    }

                    else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
                    //we have an answer that an error occurred, one of the addresses is not found
                    {
                        e.Result = 15.5;
                        result = 15.5;
                        if (result < item._maxDistanceToDoTest)
                            testersDistanceOK.Add(item);
                    }
                    else //busy network or other error...
                    {
                        e.Result = 35.5;
                        result = 35.5;
                        if (result < item._maxDistanceToDoTest)
                            testersDistanceOK.Add(item);
                    }
                }

                //Here we have to send a list to bl with all the tester that for them the distance is ok.
                //To bl receive the list of testers that for them the distance is ok.
                bl.receiveTestersAcceptingDistance(testersDistanceOK);
                //Just if everithing is correct and legal, will set the test
                bl.addTest(newTest);

                isRunnig = false;

                //This continue if there isnt excemption
                //Because from this thread I cant close the main window, so I have to use Dispacher. 
                Action<string> action = actionSetCloseAndFinaText;
                string finalShow="!המבחן נקבע בהצלחה" + "\n" + newTest.num_of_test + " :מספר מבחן" + "\n" + newTest._dateAndHourOfTest.ToString() + " :בתאריך ובשעה";
                this.Dispatcher.BeginInvoke(action, finalShow);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //to continue after the add, and to use with dispatcher
        void actionSetCloseAndFinaText(string finalText)
        {
            this.Close();
            MessageBox.Show(finalText);
        }
    }
}
