using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    class Assignment
    {
        string title;
        string description;
        DateTime subDateTime;
        int oralMark;
        int totalMark;
        public readonly int ID;
        Connections<Assignment> connections;

        public Assignment(string title, string description, DateTime subDateTime, int oralMark, int totalMark)
        {
            this.title = title;
            this.description = description;
            this.subDateTime = subDateTime;
            this.oralMark = oralMark;
            this.totalMark = totalMark;
            connections = new Connections<Assignment>(this);
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
        public string Description
        {
            get
            {
                return description;
            }
            //set
            //{
            //    description = value;
            //}
        }
        public DateTime SubDateTime
        {
            get
            {
                return subDateTime;
            }
            //set
            //{
            //    subDateTime = value;
            //}
        }
        public int OralMark
        {
            get
            {
                return oralMark;
            }
            //set
            //{
            //    oralMark = value;
            //}
        }
        public int TotalMark
        {
            get
            {
                return totalMark;
            }
            //set
            //{
            //    totalMark = value;
            //}
        }

        public Connections<Assignment> getConnections()
        {
            return connections;
        }

        public void showConnections(availableTypes typeToShow, bool showContainerInfo)
        {
            if (showContainerInfo)
                DataBase.show(this, "\nAssignment: ");
            connections.showConnections(typeToShow);
        }
        public void connectionsAnalysis()
        {
            showConnections(availableTypes.Course, true);
            showConnections(availableTypes.Trainer, false);
            showConnections(availableTypes.Student, false);
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
