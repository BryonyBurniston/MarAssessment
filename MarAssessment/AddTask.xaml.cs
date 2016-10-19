using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BryonyBurnistonS00150642
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public AddTask()
        {
            InitializeComponent();

            //Populate comboboxes
            cbxCategory.ItemsSource = Enum.GetNames(typeof(Category));

            int[] priorityList = {1, 2, 3};
            cbxPriority.ItemsSource = priorityList;

        }

        //Close the window
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Add a new Task to list
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            string title = tbxTitle.Text;
            DateTime date = dpDate.SelectedDate.Value;
            string selectedCategory = cbxCategory.Text;      
            Category cat = (Category)Enum.Parse(typeof(Category), selectedCategory);
            int selectedPriority = Convert.ToInt32(cbxPriority.Text);
            
            //Create Task object
            Task task = new Task(title, date, cat, selectedPriority);
            
            //Add it to the list of tasks on the main window
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.taskList.Add(task);
            
            //Close the window
            this.Close();
            
        }
        
        //Clears Title textbox 
        private void tbxTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxTitle.Clear();
        }

        
        
    }
}
