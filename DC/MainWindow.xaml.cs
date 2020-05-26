using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace DC
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            if (!Directory.Exists("C:/DC")) 
            {
                Directory.CreateDirectory("C:/DC");
            }
            if (!File.Exists("C:/DC/config.xml"))
            {
                XDocument xdoc = new XDocument();

                XElement computers = new XElement("computers");                

                xdoc.Add(computers);

                xdoc.Save("C:/DC/config.xml");
                //File.SetAttributes("C:/DC/config.xml", FileAttributes.Hidden);
            }
            File.WriteAllBytes("C:/DC/WZOR.docx", Properties.Resources.WZOR);

            InitializeComponent();
            UserControl usc = null;
            usc = new UserControlHome();
            GridMain.Children.Add(usc);


            //this.WindowState = WindowState.Minimized;
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new BazaD();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemSettings":
                    usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemHelp":
                    usc = new UserControlInfo();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemExit":
                    Application.Current.Shutdown();
                    break;
                default:
                    MessageBox.Show("Null");
                    break;
            }
        }



        public void ButtonPopUpLogOut_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

        public void Buttom_Minimiaze(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        public void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
    }
}
