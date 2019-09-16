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
            string command = string.Empty;

            //checks to see command OR checks to see if user control
            if(btn.Tag != null)
            {
                command = btn.Tag.ToString();
                if (command.Contains("."))
                {
                    LoadUserControl(command);
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
            string command = string.Empty;

            //checks to see command OR checks to see if user control
            if (btn.Tag != null)
            {
                command = btn.Tag.ToString();
                if (command.Contains("."))
                {
                    LoadUserControl(command);
                }
                else
                {
                    ProcessButtonsCommand(command);
                }
            }
        }

        public void CloseUserControl()
        {
            contentArea.Children.Clear();
        }

        public void DisplayUserControl(UserControl userControl)
        {
            //close current control
            CloseUserControl();

            //adds new control to screen
            contentArea.Children.Add(userControl);

        }

        public void LoadUserControl(string controlName)
        {
            Type  usercontrolType = null;
            UserControl userControl = null;

            //create type from control type parameter
            usercontrolType = Type.GetType(controlName);
            if (usercontrolType == null)
            {
                MessageBox.Show(controlName + "doesn't exist!");
            }
            else
            {
                //create new instance of control
                userControl = (UserControl)Activator.CreateInstance(usercontrolType);
                if (userControl != null)
                {
                    //display wanted user control
                    DisplayUserControl(userControl);
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