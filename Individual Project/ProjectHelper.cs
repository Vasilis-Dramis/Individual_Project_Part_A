using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    public enum availableTypes { Course = 1, Trainer = 2, Student = 3, Assignment = 4 };
    static class ProjectHelper
    {
        public static void showCourseInfo(availableTypes typeToShow)
        {
            do
            {
                Console.WriteLine("\nPlease choose the Course you want to see its {0}s.\n", typeToShow);
                Course course = chooseCourse();
                course.showConnections(typeToShow);
                Console.WriteLine("\nWould you like to see the {0}s of another Course? (y/n)", typeToShow);
            } while (Helper.yesInput());
        }
        public static void showStudentInfo(availableTypes typeToShow)
        {
            if (Helper.checkListEmpty(DataBase.getAllStudents()))
            {
                Console.WriteLine("You don't have any students yet.\nWould you like to add a new student? (y/n)");
                if (Helper.yesInput())
                {
                    addNew(availableTypes.Student);
                }
            }
            else
            {
                do
                {
                    Console.WriteLine("\nPlease choose the student you want to see his {0}.", typeToShow);
                    Student student = chooseStudent();
                    student.showConnections(typeToShow, true);
                    Console.WriteLine("\nWould you like to see the {0}s of another Student? (y/n)", typeToShow);
                } while (Helper.yesInput());
            }
        }
        public static void addToCourse(availableTypes typeToAdd)
        {

            Console.WriteLine("\nPlease choose the course you want to add the {0}.\n", typeToAdd);
            Course course = chooseCourse();
            bool emptyListFlag = true;
            switch (typeToAdd)
            {
                case availableTypes.Trainer:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllTrainers());
                    break;
                case availableTypes.Student:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllStudents());
                    break;
                case availableTypes.Assignment:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllAssignments());
                    break;
            }
            if (emptyListFlag)
            {
                Console.WriteLine("You don't have any {0} yet.\nWould you like to add a new {1}? (y/n)", typeToAdd, typeToAdd);
                if (Helper.yesInput())
                {
                    addNewLoop(typeToAdd, course);
                }
            }
            else
            {
                Console.WriteLine("\nPlease choose the {0} you want to add the Course.\n", typeToAdd);
                do
                {
                    switch (typeToAdd)
                    {
                        case availableTypes.Trainer:
                            Trainer trainer = chooseTrainer();
                            course.addToCourse(trainer);
                            break;
                        case availableTypes.Student:
                            Student student = chooseStudent();
                            course.addToCourse(student);
                            break;
                        case availableTypes.Assignment:
                            Assignment assignment = chooseAssignment();
                            if (assignment.SubDateTime < course.StartDate || assignment.SubDateTime > course.EndDate)
                            {
                                Console.WriteLine("The submit date of the assignment is out of range of the dates of the course.\nAre you sure you want to add the assignment? (y/n)");
                                if (Helper.yesInput())
                                    course.addToCourse(assignment);
                            }
                            else
                            {
                                course.addToCourse(assignment);
                            }
                            break;
                    }
                    Console.WriteLine("\nWould you like to add another {0} to the course? (y/n)", typeToAdd);
                } while (Helper.yesInput());
            }
        }
        public static void addNew(availableTypes typeToAdd)
        {
            if (typeToAdd == availableTypes.Course)
            {
                do
                {
                    Course newcourse = createCourse();
                    Console.WriteLine();
                    Console.WriteLine("\nWould you like to add another course? (y/n)");
                } while (Helper.yesInput());
            }
            else
            {
                Console.WriteLine("Every {0} must belong to at least one Course.\nPlease choose the Course you want to add the {1}", typeToAdd, typeToAdd);
                Course course = chooseCourse();
                addNewLoop(typeToAdd, course);
            }
        }
        static void addNewLoop(availableTypes typeToAdd, Course course)
        {
            do
            {
                switch (typeToAdd)
                {
                    case availableTypes.Trainer:
                        Trainer newTrainer = createTrainer();
                        course.addToCourse(newTrainer);
                        break;
                    case availableTypes.Student:
                        Student newStudent = createStudent();
                        course.addToCourse(newStudent);
                        break;
                    case availableTypes.Assignment:
                        Assignment newAssignment = createAssignment(course);
                        course.addToCourse(newAssignment);
                        break;
                    default:
                        Console.WriteLine("You cannot add {0} to the DataBase.", typeToAdd);
                        break;
                }
                Console.WriteLine("\nWould you like to add another {0} to this Course? (y/n)", typeToAdd);
            } while (Helper.yesInput());
        }
        public static Course createCourse()
        {
            Console.WriteLine("Please give the Title of the course.");
            string title = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Stream of the course.");
            string stream = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Type of the course.");
            string type = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Start Date of the course.");
            DateTime startDate = Helper.validDateTimeInput();
            Console.WriteLine("Please give the End Date of the course.");
            DateTime endDate = Helper.validDateTimeInput(startDate);

            Course course = new Course(title, stream, type, startDate, endDate);
            return course;
        }
        public static Trainer createTrainer()
        {
            Console.WriteLine("Please give the First Name of the trainer.");
            string firstName = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Last Name of the trainer.");
            string lastName = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Subject of the trainer.");
            string subject = Helper.noEmptyStringInputChecker();

            Trainer trainer = new Trainer(firstName, lastName, subject);
            return trainer;
        }
        public static Student createStudent()
        {
            Console.WriteLine("Please give the First Name of the student.");
            string firstName = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Last Name of the student.");
            string lastName = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the birth date of the student.");
            DateTime birthDate = Helper.validDateTimeInput(new DateTime(2020 - 120, 01, 01), DateTime.Now);
            Console.WriteLine("Please give the tuition fees of the student.");
            double tuitionFees = Helper.doubleInput(0);

            Student student = new Student(firstName, lastName, birthDate, tuitionFees);
            return student;
        }
        public static Assignment createAssignment(Course course)
        {
            Console.WriteLine("Please give the Title of the assignment.");
            string title = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the Description of the assignment.");
            string description = Helper.noEmptyStringInputChecker();
            Console.WriteLine("Please give the SubDate of the assignment.");
            DateTime subDate = Helper.validDateTimeInput(course.StartDate, course.EndDate, true);
            Console.WriteLine("Please give the oralMark of the assignment, as an integer between 1 and 100.");
            int oralMark = Helper.intInput(1, 100);
            Console.WriteLine("Please give the totalMark of the assignment, as an integer between 1 and 100.");
            int totalMark = Helper.intInput(1, 100);

            Assignment assignment = new Assignment(title, description, subDate, oralMark, totalMark);
            return assignment;
        }
        public static Course chooseCourse()
        {
            DataBase.showDbByType(DataBase.getAllCourses());
            int positionInDb = Helper.menuSelectionChecker(DataBase.getAllCourses().Count, false) - 1;
            Course course = DataBase.getAllCourses()[positionInDb];
            return course;
        }
        public static Trainer chooseTrainer()
        {
            DataBase.showDbByType(DataBase.getAllTrainers());
            int positionInDb = Helper.menuSelectionChecker(DataBase.getAllTrainers().Count, false) - 1;
            Trainer trainer = DataBase.getAllTrainers()[positionInDb];
            return trainer;
        }
        public static Student chooseStudent()
        {
            DataBase.showDbByType(DataBase.getAllStudents());
            int positionInDb = Helper.menuSelectionChecker(DataBase.getAllStudents().Count, false) - 1;
            Student student = DataBase.getAllStudents()[positionInDb];
            return student;
        }
        public static Assignment chooseAssignment()
        {
            DataBase.showDbByType(DataBase.getAllAssignments());
            int positionInDb = Helper.menuSelectionChecker(DataBase.getAllAssignments().Count, false) - 1;
            Assignment assignment = DataBase.getAllAssignments()[positionInDb];
            return assignment;
        }

        public static void showAssignmentDates()
        {
            do
            {
                List<int> assignmentsToSubmit = new List<int>();
                Console.WriteLine("Please give a date to see if there are any assignments to be submitted.");
                DateTime dt = Helper.validDateTimeInput();
                DateTime startDt = Helper.startOfCalendarWeek(dt);
                DateTime endDt = startDt.AddDays(6);
                foreach (Assignment assignment in DataBase.getAllAssignments())
                {
                    if (assignment.SubDateTime >= startDt && assignment.SubDateTime <= endDt)
                        assignmentsToSubmit.Add(assignment.ID);
                }
                Console.WriteLine("\nAssignments to be submitted between {0} and {1} :\n", startDt.ToString("dd/MM/yyyy"), endDt.ToString("dd/MM/yyyy"));
                if (assignmentsToSubmit.Count() == 0)
                {
                    Console.WriteLine("None assignment.");
                }
                else
                {
                    int assignmentCounter = 1;
                    foreach (int ID in assignmentsToSubmit)
                    {
                        int studentCounter = 1;
                        int positionInDb = ID - 1;
                        Assignment assignment = DataBase.getAllAssignments()[positionInDb];
                        DataBase.show(assignment, assignmentCounter + ". ");
                        Console.WriteLine("\nStudents:");
                        foreach (int studentID in assignment.getConnections().getList(availableTypes.Student))
                        {
                            positionInDb = studentID - 1;
                            Student student = DataBase.getAllStudents()[studentID - 1];
                            DataBase.show(student, "  " + studentCounter + ". ");
                            studentCounter++;
                        }
                        assignmentCounter++;
                    }
                }
                Console.WriteLine("\nWould you like to check another Date? (y/n)");
            } while (Helper.yesInput());

        }
        public static void showAllConnections(availableTypes typeToShow)
        {
            bool emptyListFlag = true;
            switch (typeToShow)
            {
                case availableTypes.Course:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllCourses());
                    break;
                case availableTypes.Trainer:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllTrainers());
                    break;
                case availableTypes.Student:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllStudents());
                    break;
                case availableTypes.Assignment:
                    emptyListFlag = Helper.checkListEmpty(DataBase.getAllAssignments());
                    break;
            }
            if (emptyListFlag)
                Console.WriteLine("\nThere are no {0}s yet.", typeToShow);
            switch (typeToShow)
            {
                case availableTypes.Course:
                    foreach (Course course in DataBase.getAllCourses())
                    {
                        course.connectionsAnalysis();
                        Console.WriteLine(". . . . . . . . . . . . . . . . . . . .");
                    }
                    break;
                case availableTypes.Trainer:
                    foreach (Trainer trainer in DataBase.getAllTrainers())
                    {
                        trainer.connectionsAnalysis();
                        Console.WriteLine(". . . . . . . . . . . . . . . . . . . .");
                    }
                    break;
                case availableTypes.Student:
                    foreach (Student student in DataBase.getAllStudents())
                    {
                        student.connectionsAnalysis();
                        Console.WriteLine(". . . . . . . . . . . . . . . . . . . .");
                    }
                    break;
                case availableTypes.Assignment:
                    foreach (Assignment assignment in DataBase.getAllAssignments())
                    {
                        assignment.connectionsAnalysis();
                        Console.WriteLine(". . . . . . . . . . . . . . . . . . . .");
                    }
                    break;
                default:
                    Console.WriteLine("The DataBase doesn't support {0} connections.", typeToShow);
                    break;
            }
        }
    }
}
