using ActiveMq_Utils;
using MvvMTransport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppliTransportSNCF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
       
    {
        public string recup { set; get; }
        Model model = new Model();
        private string user = ConfigurationManager.AppSettings["ActiveMQ_user"];
        private string pwd = ConfigurationManager.AppSettings["ActiveMQ_pwd"];
        private string host = ConfigurationManager.AppSettings["ActiveMQ_host"];
        private string port = ConfigurationManager.AppSettings["ActiveMQ_port"];
        private string topic = ConfigurationManager.AppSettings["ActiveMQ_topic"];
        ActivMQListner listner;
        public MainWindow()
        {
            InitializeComponent();
            listner = new ActivMQListner(user, pwd, host, port, topic); // lien connexion
            listner.eventMsg += Listner_eventMsg; //abooné a evenement substriber
                                             // listner.start(); //lancer l'évenement
            Thread th = new Thread(ListenenMsg);
          
            th.Start();
       

        }


        private void Listner_eventMsg(ActivMQListner l, Apache.NMS.ITextMessage msg)
        {
            Console.WriteLine(msg.Text); // je vais afficher dans la console
            recup = msg.Text;

            // cette manière n'est pas avec le MVVM !!!!!
           /* Dispatcher.Invoke(() =>
            {
                lab_VilleActuelle.Content = recup;


            });*/
           

        }

        public void ListenenMsg()
        {
            listner.start();
        }
    }
}
