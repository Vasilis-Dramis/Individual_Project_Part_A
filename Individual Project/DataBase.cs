using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    static class DataBase
    {
        static List<Course> allCources = new List<Course>();
        static List<Trainer> allTrainers = new List<Trainer>();
        static List<Student> allStudents = new List<Student>();
        static List<Assignment> allAssignments = new List<Assignment>();

        public static IList<Course> getAllCourses()
        {
            IList<Course> readonlylist = allCources.AsReadOnly();
            return readonlylist;
        }
        public static IList<Trainer> getAllTrainers()
        {
            IList<Trainer> readonlylist = allTrainers.AsReadOnly();
            return readonlylist;
        }
        public static IList<Student> getAllStudents()
        {
            IList<Student> readonlylist = allStudents.AsReadOnly();
            return readonlylist;
        }
        public static IList<Assignment> getAllAssignments()
        {
            IList<Assignment> readonlylist = allAssignments.AsReadOnly();
            return readonlylist;
        }

        public static void addToDb<T>(T value)
        {
            switch (value)
            {
                case Course course:
                    if (course.ID != -1)
                    {
                        allCources.Add(course);
                        Console.WriteLine("The {0} was succefully added.", value.GetType().Name);
                    }
                    break;
                case Trainer trainer:
                    if (trainer.ID != -1)
                    {
                        allTrainers.Add(trainer);
                        Console.WriteLine("The {0} was succefully added.", value.GetType().Name);
                    }
                    break;
                case Student student:
                    if (student.ID != -1)
                    {
                        allStudents.Add(student);
                        Console.WriteLine("The {0} was succefully added.", value.GetType().Name);
                    }
                    break;
                case Assignment assignment:
                    if (assignment.ID != -1)
                    {
                        allAssignments.Add(assignment);
                        Console.WriteLine("The {0} was succefully added.", value.GetType().Name);
                    }
                    break;
                default:
                    Console.WriteLine("The DataBase doesn't support {0}.", value.GetType().Name);
                    break;
            }
        }
        public static int newID<T>(T value)
        {
            int newID = -1;
            switch (value)
            {
                case Course course:
                    if (!existsInDb(course))
                    {
                        newID = getAllCourses().Count() + 1;
                    }
                    break;
                case Trainer trainer:
                    if (!existsInDb(trainer))
                    {
                        newID = getAllTrainers().Count() + 1;
                    }
                    break;
                case Student student:
                    if (!existsInDb(student))
                    {
                        newID = getAllStudents().Count() + 1;
                    }
                    break;
                case Assignment assignment:
                    if (!existsInDb(assignment))
                    {
                        newID = getAllAssignments().Count() + 1;
                    }
                    break;
                default:
                    Console.WriteLine("The DataBase doesn't support {0}.", value.GetType().Name);
                    break;
            }
            return newID;
        }
        static bool existsInDb<T>(T value)
        {
            bool flag = false;
            switch (value)
            {
                case Course course:
                    foreach (Course item in getAllCourses())
                    {
                        if (item.Title.Equals(course.Title, StringComparison.OrdinalIgnoreCase) && item.Stream.Equals(course.Stream, StringComparison.OrdinalIgnoreCase) && item.Type.Equals(course.Type, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("The Course already exists and cannot be added again.");
                            flag = true;
                        }
                    }
                    break;
                case Trainer trainer:
                    foreach (Trainer item in getAllTrainers())
                    {
                        if (item.FirstName.Equals(trainer.FirstName, StringComparison.OrdinalIgnoreCase) && item.LastName.Equals(trainer.LastName, StringComparison.OrdinalIgnoreCase) && item.subject.Equals(trainer.subject, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("This Trainer already exists and cannot be added again.");
                            flag = true;
                        }
                        else if (item.FirstName.Equals(trainer.FirstName, StringComparison.OrdinalIgnoreCase) && item.LastName.Equals(trainer.LastName, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("There is already a Trainer with the same first and last name.\nAre you sure you want to add the Trainer? (y/n)");
                            flag = !Helper.yesInput();
                        }
                    }
                    break;
                case Student student:
                    foreach (Student item in getAllStudents())
                    {
                        if (item.FirstName.Equals(student.FirstName, StringComparison.OrdinalIgnoreCase) && item.LastName.Equals(student.LastName, StringComparison.OrdinalIgnoreCase) && item.DateOfBirth == student.DateOfBirth)
                        {
                            Console.WriteLine("There is already a Student with the same first,last name and date of birth.\nAre you sure you want to add the Student? (y/n)");
                            flag = !Helper.yesInput();
                        }
                    }
                    break;
                case Assignment assignment:
                    foreach (Assignment item in getAllAssignments())
                    {
                        if (item.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase) && item.Description.Equals(assignment.Description, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("There is already an Assignment with the same title and description.\nAre you sure you want to add the Assignment? (y/n)");
                            flag = !Helper.yesInput();
                        }
                    }
                    break;
            }
            return flag;
        }

        public static void showDbByType<T>(IList<T> value)
        {
            string valueT = value.GetType().ToString();
            valueT = valueT.Substring(valueT.LastIndexOf(".") + 1, valueT.Length - valueT.LastIndexOf(".") - 2);
            if (value.Count == 0)
                Console.WriteLine("\nThere are no {0}s yet.", valueT);
            else
            {
                Console.WriteLine("{0}s:", value[0].GetType().Name);
                int counter = 1;
                foreach (T item in value)
                {
                    show(item, ("  " + counter + ". ").ToString());
                    counter++;
                }
            }
        }
        public static void show<T>(T value, string prefix)
        {
            switch (value)
            {
                case Course course:
                    Console.WriteLine("{0}Title: {1}, Stream: {2}, Type: {3}, Start Date: {4}, End Date: {5}", prefix, course.Title, course.Stream, course.Type, course.StartDate.ToString("dddd, dd MMMM yyyy"), course.EndDate.ToString("dddd, dd MMMM yyyy"));
                    break;
                case Trainer trainer:
                    Console.WriteLine("{0}Last Name: {1}, First Name: {2}, Subject: {3}", prefix, trainer.LastName, trainer.FirstName, trainer.subject);
                    break;
                case Student student:
                    Console.WriteLine("{0}Last Name: {1}, First Name: {2}, Date of Birth: {3}, Tuition Fees: {4}", prefix, student.LastName, student.FirstName, student.DateOfBirth.ToString("dd/MM/yyyy"), student.Fees.ToString("N2"));
                    break;
                case Assignment assignment:
                    Console.WriteLine("{0}Title: {1}, Description: {2}, Sub Date: {3}, Oral Mark: {4}, Total Mark: {5}", prefix, assignment.Title, assignment.Description, assignment.SubDateTime.ToString("dddd, dd MMMM yyyy"), assignment.OralMark, assignment.TotalMark);
                    break;
                default:
                    Console.WriteLine("The DataBase doesn't support {0}.", value.GetType().Name);
                    break;
            }
        }

        public static void showStudentsWithMultipleCourses()
        {
            int counter = 0;
            Console.WriteLine("The students with multiple courses are:\n");
            foreach (Student item in allStudents)
            {
                if (item.getConnections().getList(availableTypes.Assignment).Count() > 1)
                {
                    counter++;
                    show(item, ("  " + counter + ". ").ToString());
                }
            }
            if (counter == 0)
                Console.WriteLine("None of the students has multiple courses.");
        }
    }
}
