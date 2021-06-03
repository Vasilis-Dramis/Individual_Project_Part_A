using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    public class Connections<T>
    {
        string typeOfConnections;

        protected List<int> listOfCoursesIDs;
        protected List<int> listOfTrainersIDs;
        protected List<int> listOfStudentsIDs;
        protected List<int> listOfAssignmentsIDs;

        public Connections(T value)
        {
            typeOfConnections = value.GetType().Name;
            switch (value)
            {
                case Trainer trainer:
                    listOfCoursesIDs = new List<int>();
                    listOfTrainersIDs = null;
                    listOfStudentsIDs = new List<int>();
                    listOfAssignmentsIDs = new List<int>();
                    break;
                case Student student:
                    listOfCoursesIDs = new List<int>();
                    listOfTrainersIDs = new List<int>();
                    listOfStudentsIDs = null;
                    listOfAssignmentsIDs = new List<int>();
                    break;
                case Assignment assignment:
                    listOfCoursesIDs = new List<int>();
                    listOfTrainersIDs = new List<int>();
                    listOfStudentsIDs = new List<int>();
                    listOfAssignmentsIDs = null;
                    break;
                default:
                    Console.WriteLine("The type {0}, does not support connections.", typeOfConnections);
                    break;
            }
        }
        protected Connections()
        {
            listOfCoursesIDs = null;
            listOfTrainersIDs = new List<int>();
            listOfStudentsIDs = new List<int>();
            listOfAssignmentsIDs = new List<int>();
        }

        public IList<int> getList(availableTypes typeToGet)
        {
            IList<int> readOnlyList = null;
            switch (typeToGet)
            {
                case availableTypes.Course:
                    readOnlyList = listOfCoursesIDs;
                    break;
                case availableTypes.Trainer:
                    readOnlyList = listOfTrainersIDs;
                    break;
                case availableTypes.Student:
                    readOnlyList = listOfStudentsIDs;
                    break;
                case availableTypes.Assignment:
                    readOnlyList = listOfAssignmentsIDs;
                    break;
            }
            return readOnlyList;
        }

        protected void addConnection<J>(J value, int courseID)
        {
            switch (value)
            {
                case Trainer trainer:
                    if (!exists(trainer) && DataBase.getAllTrainers().Contains(trainer))
                    {
                        listOfTrainersIDs.Add(trainer.ID);
                        trainer.getConnections().listOfCoursesIDs.Add(courseID);
                        UpdateConnections(trainer);
                        UpdateParallelConnections(trainer);
                        Console.WriteLine("The Trainer was succefully added to the Course.");
                    }
                    else
                        Console.WriteLine("This Course allready has this Trainer.");
                    break;
                case Student student:
                    if (!exists(student) && DataBase.getAllStudents().Contains(student))
                    {
                        listOfStudentsIDs.Add(student.ID);
                        student.getConnections().listOfCoursesIDs.Add(courseID);
                        UpdateConnections(student);
                        UpdateParallelConnections(student);
                        Console.WriteLine("The Student was succefully added to the Course.");
                    }
                    else
                        Console.WriteLine("This Course allready has this Student.");
                    break;
                case Assignment assignment:
                    if (!exists(assignment) && DataBase.getAllAssignments().Contains(assignment))
                    {
                        listOfAssignmentsIDs.Add(assignment.ID);
                        assignment.getConnections().listOfCoursesIDs.Add(courseID);
                        UpdateConnections(assignment);
                        UpdateParallelConnections(assignment);
                        Console.WriteLine("The Assignment was succefully added to the Course.");
                    }
                    else
                        Console.WriteLine("This Course allready has this Assignment.");
                    break;
                default:
                    Console.WriteLine("The Course cannot contain {0}.", value.GetType().Name);
                    break;
            }
        }
        void UpdateConnections<J>(J value)
        {
            switch (value)
            {
                case Trainer trainer:
                    foreach (int ID in listOfStudentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Student student = DataBase.getAllStudents()[positionInDb];
                        if (!trainer.getConnections().listOfStudentsIDs.Contains(student.ID))
                            trainer.getConnections().listOfStudentsIDs.Add(student.ID);
                    }
                    foreach (int ID in listOfAssignmentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                        if (!trainer.getConnections().listOfAssignmentsIDs.Contains(assignment.ID))
                            trainer.getConnections().listOfAssignmentsIDs.Add(assignment.ID);
                    }
                    break;
                case Student student:
                    foreach (int ID in listOfAssignmentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                        if (!student.getConnections().listOfAssignmentsIDs.Contains(assignment.ID))
                            student.getConnections().listOfAssignmentsIDs.Add(assignment.ID);
                    }
                    foreach (int ID in listOfTrainersIDs)
                    {
                        int positionInDb = ID - 1;
                        Trainer trainer = DataBase.getAllTrainers()[positionInDb];
                        if (!student.getConnections().listOfTrainersIDs.Contains(trainer.ID))
                            student.getConnections().listOfTrainersIDs.Add(trainer.ID);
                    }
                    break;
                case Assignment assignment:
                    foreach (int ID in listOfTrainersIDs)
                    {
                        int positionInDb = ID - 1;
                        Trainer trainer = DataBase.getAllTrainers()[positionInDb];
                        if (!assignment.getConnections().listOfTrainersIDs.Contains(trainer.ID))
                            assignment.getConnections().listOfTrainersIDs.Add(trainer.ID);
                    }
                    foreach (int ID in listOfStudentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Student student = DataBase.getAllStudents()[positionInDb];
                        if (!assignment.getConnections().listOfStudentsIDs.Contains(student.ID))
                            assignment.getConnections().listOfStudentsIDs.Add(student.ID);
                    }
                    break;
                default:
                    Console.WriteLine("Cannot update {0} connections.", value.GetType().Name);
                    break;
            }
        }
        void UpdateParallelConnections<J>(J value)
        {
            switch (value)
            {
                case Trainer trainer:
                    foreach (int ID in listOfStudentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Student student = DataBase.getAllStudents()[positionInDb];
                        if (!student.getConnections().listOfTrainersIDs.Contains(trainer.ID))
                            student.getConnections().listOfTrainersIDs.Add(trainer.ID);
                    }
                    foreach (int ID in listOfAssignmentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                        if (!assignment.getConnections().listOfTrainersIDs.Contains(trainer.ID))
                            assignment.getConnections().listOfTrainersIDs.Add(trainer.ID);
                    }
                    break;
                case Student student:
                    foreach (int ID in listOfAssignmentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                        if (!assignment.getConnections().listOfStudentsIDs.Contains(student.ID))
                            assignment.getConnections().listOfStudentsIDs.Add(student.ID);
                    }
                    foreach (int ID in listOfTrainersIDs)
                    {
                        int positionInDb = ID - 1;
                        Trainer trainer = DataBase.getAllTrainers()[positionInDb];
                        if (!trainer.getConnections().listOfStudentsIDs.Contains(student.ID))
                            trainer.getConnections().listOfStudentsIDs.Add(student.ID);
                    }
                    break;
                case Assignment assignment:
                    foreach (int ID in listOfTrainersIDs)
                    {
                        int positionInDb = ID - 1;
                        Trainer trainer = DataBase.getAllTrainers()[positionInDb];
                        if (!trainer.getConnections().listOfAssignmentsIDs.Contains(assignment.ID))
                            trainer.getConnections().listOfAssignmentsIDs.Add(assignment.ID);
                    }
                    foreach (int ID in listOfStudentsIDs)
                    {
                        int positionInDb = ID - 1;
                        Student student = DataBase.getAllStudents()[positionInDb];
                        if (!student.getConnections().listOfAssignmentsIDs.Contains(assignment.ID))
                            student.getConnections().listOfAssignmentsIDs.Add(assignment.ID);
                    }
                    break;
                default:
                    Console.WriteLine("Cannot update {0} parallel connections.", value.GetType().Name);
                    break;
            }
        }
        bool exists<J>(J value)
        {
            bool flag = false;
            switch (value)
            {
                case Trainer trainer:
                    flag = listOfTrainersIDs.Contains(trainer.ID) ? true : false;
                    break;
                case Student student:
                    flag = listOfStudentsIDs.Contains(student.ID) ? true : false;
                    break;
                case Assignment assignment:
                    flag = listOfAssignmentsIDs.Contains(assignment.ID) ? true : false;
                    break;
            }
            return flag;
        }

        public virtual void showConnections(availableTypes typeToShow)
        {
            int counter = 1;
            switch (typeToShow)
            {
                case availableTypes.Course:
                    if (listOfCoursesIDs != null)
                    {
                        Console.WriteLine("\nCourses:");
                        if (Helper.checkListEmpty(listOfCoursesIDs))
                            Console.WriteLine("\nThe {0} has no Courses yet.", typeOfConnections);
                        else
                        {
                            foreach (int ID in listOfCoursesIDs)
                            {
                                int positionInDb = ID - 1;
                                Course course = DataBase.getAllCourses()[positionInDb];
                                DataBase.show(course, ("  " + counter + ". ").ToString());
                                counter++;
                            }
                        }
                    }
                    break;
                case availableTypes.Trainer:
                    if (listOfTrainersIDs != null)
                    {
                        Console.WriteLine("\nTrainers:");
                        if (Helper.checkListEmpty(listOfTrainersIDs))
                            Console.WriteLine("\nThe {0} has no Trainers yet.", typeOfConnections);
                        else
                        {
                            foreach (int ID in listOfTrainersIDs)
                            {
                                int positionInDb = ID - 1;
                                Trainer trainer = DataBase.getAllTrainers()[positionInDb];
                                DataBase.show(trainer, ("  " + counter + ". ").ToString());
                                counter++;
                            }
                        }
                    }
                    break;
                case availableTypes.Student:
                    if (listOfStudentsIDs != null)
                    {
                        Console.WriteLine("\nStudents:");
                        if (Helper.checkListEmpty(listOfStudentsIDs))
                            Console.WriteLine("\nThe {0} has no Students yet.", typeOfConnections);
                        else
                        {
                            foreach (int ID in listOfStudentsIDs)
                            {
                                int positionInDb = ID - 1;
                                Student student = DataBase.getAllStudents()[positionInDb];
                                DataBase.show(student, ("  " + counter + ". ").ToString());
                                counter++;
                            }
                        }
                    }
                    break;
                case availableTypes.Assignment:
                    if (listOfAssignmentsIDs != null)
                    {
                        Console.WriteLine("\nAssignments:");
                        if (Helper.checkListEmpty(listOfAssignmentsIDs))
                            Console.WriteLine("\nThe {0} has no Assignments yet.", typeOfConnections);
                        else
                        {
                            foreach (int ID in listOfAssignmentsIDs)
                            {
                                int positionInDb = ID - 1;
                                Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                                DataBase.show(assignment, ("  " + counter + ". ").ToString());
                                counter++;
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("The {0} doesn't support connections.", typeToShow);
                    break;
            }
        }
    }
}
