using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BryonyBurnistonS00150642
{

    //Category enum
    public enum Category
    {
        Personal,
        Work,
        Other
    }

    public class Task
    {
        //Properties - title, date, category(enum), priority(int array)
        public string TaskTitle { get; set; }
        public DateTime DueDate { get; set; }
        public Category TypeOfCategory { get; set; }
        public int Priority { get; set; }

        //Constructors
        public Task()
        {
            TaskTitle = "";
            DueDate = DateTime.Now;
            TypeOfCategory = Category.Other;
            Priority = 0;
        }

        public Task(string taskTitle, DateTime dueDate, Category category, int priority)
        {
            TaskTitle = taskTitle;
            DueDate = dueDate;
            TypeOfCategory = category;
            Priority = priority;
        }


        //Methods

        //Used to display information in list box
        public override string ToString()
        {
            int daysToDueDate = DaysTilDue(DueDate);
            return string.Format("{0}: Category - {1}, Priority {2}, Due in {3} days", TaskTitle, TypeOfCategory, Priority, daysToDueDate);
        }

        //Used to convert data to comma separated format
        public string ToString(string delimiter)
        {
            return TaskTitle + "," + DueDate.ToShortDateString() + "," + TypeOfCategory + "," + Priority;
        }

        //Used to save information to file
        public string ToFileFomat()
        {
            return string.Format("{0},{1},{2},{3}", TaskTitle, DueDate.ToShortDateString(), TypeOfCategory, Priority);
        }

        //Method to calculate days from now til due date of task
        public int DaysTilDue(DateTime date)
        {
            int days = date.Day - DateTime.Now.Day;

            return days; 
        }


    }
}
