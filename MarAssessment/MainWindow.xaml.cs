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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace BryonyBurnistonS00150642
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create new list of Tasks called taskList
        public List<Task> taskList = new List<Task>();

        public MainWindow()
        {
            InitializeComponent();

            //Create counter for list items
            updateCount();

            //Display time - Dispatcher Timer
            //Create dt
            DispatcherTimer dt = new DispatcherTimer();
            //Set the interval
            dt.Interval = new TimeSpan(0, 0, 1);
            //Link the tick event handler
            dt.Tick += new EventHandler(dt_Tick);
            //Start the timer
            dt.Start(); 

        }

        //Display the time
        void dt_Tick(object sender, EventArgs e)
        {
            tblkTime.Text = "Time: " + DateTime.Now.ToLongTimeString();

        }

        //Adding a new task - using child window
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Create new child window for AddTask
            AddTask addTask = new AddTask();

            //Link to the child window
            addTask.Owner = this;

            //Display child window, positioned centrally over main window
            addTask.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addTask.ShowDialog();

            //refresh the listbox - updates list and request counter
            refreshList();

        }

        //Complete a task - remove selected task from list
        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            //determine which item is selected
            Task selectedTask = (Task)lbxDisplay.SelectedItem;

            if (selectedTask != null)
            {
                //remove it from the request list
                taskList.Remove(selectedTask);

                //refresh list and update counter
                refreshList();
            }
            else
            {
                //If no selection made
                MessageBox.Show("You have not selected a task to complete");
            }     

        }

        //Load task list from file and display to screen
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            

            //Variables
            string[] taskInfo = new string[3];
            string[] dateInfo = new string[3];
            string tsk, title, pri;
            Task task;
            Category cat;
            DateTime date;
            int year, month, day, priority;

            //read files from file
            string[] lines = File.ReadAllLines("TaskList.txt");


            //Loop through file creating Request objects and adding them to list
            for (int i = 0; i < lines.Length; i++)
            {
                //split based on commas
                tsk = lines[i];
                taskInfo = tsk.Split(',');
                title = taskInfo[0];

                dateInfo = taskInfo[1].Split('/');
                day = Convert.ToInt32(dateInfo[0]);
                month = Convert.ToInt32(dateInfo[1]);
                year = Convert.ToInt32(dateInfo[2]);
                date = new DateTime(year, month, day);
                
                cat = (Category)Enum.Parse(typeof(Category), taskInfo[2]);
                pri = taskInfo[3];
                //need to get data type correct etc
                priority = Convert.ToInt32(pri);              
                

                //create task object
                task = new Task(title, date, cat, priority);

                //add to task list
                taskList.Add(task);
            }

            //Refresh display of list and counter
            refreshList();

        }

        //Save request list to file
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            int numOfItems = taskList.Count;
            string[] lines = new string[numOfItems];
            Task task;

            //Iterate through the Task List
            for (int i = 0; i < numOfItems; i++)
            {
                //get task
                task = taskList.ElementAt(i);

                //Add to array
                lines[i] = task.ToString(",");                
            }

            //Write to file
            File.WriteAllLines("TaskList.txt", lines);

            //Shut down Application
            Application.Current.Shutdown();

        }        

        //Helper method to refresh list display
        private void refreshList()
        {
            lbxDisplay.ItemsSource = null;
            lbxDisplay.ItemsSource = taskList;
            updateCount();
        }

        //Helper method to update list count
        private void updateCount()
        {
            int count = taskList.Count();
            tblkCount.Text = "Counter: " + count.ToString();
        }

        

        

        
       
    }
}
