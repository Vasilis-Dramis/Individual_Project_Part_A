using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    class Trainer
    {
        string firstName;
        string lastName;
        public readonly string subject;
        public readonly int ID;
        Connections<Trainer> connections;

        public Trainer(string firstName, string lastName, string subject)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.subject = subject;
            connections = new Connections<Trainer>(this);
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

        public Connections<Trainer> getConnections()
        {
            return connections;
        }

        public void showConnections(availableTypes typeToShow, bool showContainerInfo)
        {
            if (showContainerInfo)
                DataBase.show(this, "\nTrainer: ");
            connections.showConnections(typeToShow);
        }
        public void connectionsAnalysis()
        {
            showConnections(availableTypes.Course, true);
            showConnections(availableTypes.Student, false);
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
