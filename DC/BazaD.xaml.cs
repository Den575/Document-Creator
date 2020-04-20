﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DC
{

    public partial class BazaD : UserControl
    {

        public BazaD()
        {
            InitializeComponent();
            LoadData();

        }

        private void LoadData()
        {
            List<User> items = new List<User>();
            try
            {
                XDocument xdoc = XDocument.Load("C:/DC/config.xml");
                foreach (XElement phoneElement in xdoc.Element("computers").Elements("user"))
                {
                    XAttribute nameAttribute = phoneElement.Attribute("name");
                    XAttribute snameAttribute = phoneElement.Attribute("surname");
                    XElement companyElement = phoneElement.Element("computer");
                    XElement priceElement = phoneElement.Element("date");
                    XElement servisTagElement = phoneElement.Element("servistag");

                    if (true)
                    {
                        items.Add(new User() { Name = nameAttribute.Value, Surname = snameAttribute.Value, Computer = companyElement.Value, Date = priceElement.Value, ServisTag = servisTagElement.Value });
                    }

                }
                lvUsers.ItemsSource = items;
            }
            catch(Exception)
            {
                MessageBox.Show("Brak pliku C:/DC/config.xml");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            User u = lvUsers.SelectedItem as User;



            try
            {
                XDocument xdoc = XDocument.Load("C:/DC/config.xml");
                XElement root = xdoc.Element("computers");

                foreach (XElement xe in root.Elements("user").ToList())
                {
                    if (xe.Attribute("name").Value == u.Name && xe.Attribute("surname").Value == u.Surname)
                    {
                        xe.Remove();
                        xdoc.Save("C:/DC/config.xml");
                        LoadData();
                    }
                }
            }
            catch
            {

            }
        }

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            XDocument xdoc = XDocument.Load("C:/DC/config.xml");
            XElement root = xdoc.Element("computers");

            foreach (XElement xe in root.Elements("user").ToList())
            {
                xe.Remove();
                xdoc.Save("C:/DC/config.xml");
                LoadData();
            }

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Plik został zeksportowany do C:/DC/config.xml");
            System.Diagnostics.Process.Start(@"C:/DC/config.xml");
        }
    }

    public class User
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Computer { get; set; }
		public string ServisTag { get; set; }
		public string Date { get; set; }
	
    }
}
