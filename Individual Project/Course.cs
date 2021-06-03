using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    public class Course : Connections<Course>
    {
        string title;
        string stream;
        string type;
        DateTime start_date;
        DateTime end_date;
        public readonly int ID;

        public Course(string title, string stream, string type, DateTime start_date, DateTime end_date)
        {

            this.title = title;
            this.stream = stream;
            this.type = type;
            this.start_date = start_date;
            this.end_date = end_date;
            listOfTrainersIDs = new List<int>();
            listOfStudentsIDs = new List<int>();
            listOfAssignmentsIDs = new List<int>();
            this.ID = DataBase.newID(this);
            DataBase.addToDb(this);
        }

        public string Title
        {
            get
            {
                return title;
            }
            //set
            //{
            //    title = value;
            //}
        }
        public string Stream
        {
            get
            {
                return stream;
            }
            //set
            //{
            //    stream = value;
            //}
        }
        public string Type
        {
            get
            {
                return type;
            }
            //set
            //{
            //    type = value;
            //}
        }
        public DateTime StartDate
        {
            get
            {
                return start_date;
            }
            //set
            //{
            //    start_date = value;
            //}
        }
        public DateTime EndDate
        {
            get
            {
                return end_date;
            }
            //set
            //{
            //    end_date = value;
            //}
        }

        public void addToCourse<T>(T value)
        {
            addConnection(value, this.ID);
        }
        public override void showConnections(availableTypes typeToShow)
        {
            DataBase.show(this, "\nCourse: ");
            base.showConnections(typeToShow);
        }

        public void connectionsAnalysis()
        {
            DataBase.show(this, "\nCourse:");
            Console.WriteLine("\nTrainers:");
            if (Helper.checkListEmpty(listOfTrainersIDs))
                Console.WriteLine("\nThere are no Trainers in this Course yet.");
            else
            {
                foreach (int ID in listOfTrainersIDs)
                {
                    int counter = 1;
                    int positionInDb = ID - 1;
                    Trainer trainer = DataBase.getAllTrainers()[positionInDb];
                    DataBase.show(trainer, ("  " + counter + ". ").ToString());
                    counter++;
                }
            }
            Console.WriteLine("\nStudents:");
            if (Helper.checkListEmpty(listOfStudentsIDs))
                Console.WriteLine("\nThere are no Students in this Course yet.");
            else
            {
                foreach (int ID in listOfStudentsIDs)
                {
                    int counter = 1;
                    int positionInDb = ID - 1;
                    Student student = DataBase.getAllStudents()[positionInDb];
                    DataBase.show(student, ("  " + counter + ". ").ToString());
                    counter++;
                }
            }
            Console.WriteLine("\nAssignments:");
            if (Helper.checkListEmpty(listOfAssignmentsIDs))
                Console.WriteLine("\nThere are no Assignments in this Course yet.");
            else
            {
                foreach (int ID in listOfAssignmentsIDs)
                {
                    int counter = 1;
                    int positionInDb = ID - 1;
                    Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                    DataBase.show(assignment, ("  " + counter + ". ").ToString());
                    counter++;
                }
            }
        }
    }
}
