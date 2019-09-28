using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TinkerJems.Wpf
{   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AddNewJewelryItem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string command;

            //checks to see command OR checks to see if user control
            if(btn.Tag != null)
            {
                command = btn.Tag.ToString();
                if (command.Contains("."))
                {
                }
                else
                {
                    ProcessButtonsCommand(command);
                }
            }
        }

        private void EditJewelryItem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string command;

            //checks to see command OR checks to see if user control
            if (btn.Tag != null)
            {
                command = btn.Tag.ToString();
                if (command.Contains("."))
                {
                }
                else
                {
                    ProcessButtonsCommand(command);
                }
            }
        }    

       

        public void ProcessButtonsCommand(string command)
        {
            switch (command.ToLower())
            {
                case "exit":
                    this.Close();
                    break;

                default:
                    break;

            }
        }
    }
}