using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    public class Student
    {
        string firstName;
        string lastName;
        DateTime dateOfBirth;
        double tuitionFees;
        public readonly int ID;
        Connections<Student> connections;

        public Student(string firstName, string lastName, DateTime dateOfBirth, double tuitionFees)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.tuitionFees = tuitionFees;
            connections = new Connections<Student>(this);
            this.ID = DataBase.newID(this);

            DataBase.addToDb(this);
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            //set
            //{
            //    firstName = value;
            //}
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            //set
            //{
            //    lastName = value;
            //}
        }
        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            //set
            //{
            //    dateOfBirth = value;
            //}
        }
        public double Fees
        {
            get
            {
                return tuitionFees;
            }
            //set
            //{
            //    tuitionFees = value;
            //}
        }

        public Connections<Student> getConnections()
        {
            return connections;
        }

        public void showConnections(availableTypes typeToShow, bool showContainerInfo)
        {
            if (showContainerInfo)
                DataBase.show(this, "\nStudent: ");
            connections.showConnections(typeToShow);
        }
        public void connectionsAnalysis()
        {
            showConnections(availableTypes.Course, true);
            showConnections(availableTypes.Trainer, false);
            showConnections(availableTypes.Assignment, false);
            Console.WriteLine("\nConnection Analysis:");
            Console.WriteLine("- - - - -");
            foreach (int ID in connections.getList(availableTypes.Course))
            {
                int positionInDb = ID - 1;
                Course course = DataBase.getAllCourses()[positionInDb];
                course.connectionsAnalysis();
            }
        }
    }
}
