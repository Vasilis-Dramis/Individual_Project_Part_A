using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project
{
    class Synthetic
    {
        public static bool created = false;

        public void createSyntheticData()
        {
            Course[] syntheticCourses = createSyntheticCourses();
            Trainer[] syntheticTrainers = createSyntheticTrainer();
            Student[] syntheticStudents = createSyntheticStudent();
            Assignment[] syntheticAssignments = createSyntheticAssignment();

            assignSyntheticData(syntheticCourses, syntheticTrainers, syntheticStudents, syntheticAssignments);
            created = true;

            Console.WriteLine("\nPlease press any key to continue.");
            Console.ReadKey();
        }
        void assignSyntheticData(Course[] courses, Trainer[] trainers, Student[] students, Assignment[] assignments)
        {
            courses[0].addToCourse(students[0]);
            courses[0].addToCourse(students[1]);
            courses[0].addToCourse(trainers[0]);
            courses[0].addToCourse(assignments[0]);
            courses[0].addToCourse(assignments[2]);

            courses[1].addToCourse(students[1]);
            courses[1].addToCourse(trainers[0]);
            courses[1].addToCourse(trainers[1]);
            courses[1].addToCourse(assignments[1]);

            for (int i = 0; i < 3; i++)
            {
                courses[2].addToCourse(students[i]);
                courses[2].addToCourse(trainers[i]);
            }
            courses[2].addToCourse(assignments[2]);
        }

        Course[] createSyntheticCourses()
        {
            Course course1 = new Course("CB11", "C#", "online", new DateTime(2020, 9, 16), new DateTime(2021, 3, 16));
            Course course2 = new Course("CB11", "C#", "parttime", new DateTime(2020, 9, 16), new DateTime(2021, 3, 16));
            Course course3 = new Course("CB10", "Python", "online", new DateTime(2020, 3, 16), new DateTime(2020, 9, 16));

            Course[] syntheticCourses = new Course[] { course1, course2, course3 };

            return syntheticCourses;
        }
        Trainer[] createSyntheticTrainer()
        {
            Trainer trainer1 = new Trainer("Michalis", "Chamilos", "Academic Associate");
            Trainer trainer2 = new Trainer("George", "Pasparakis", "Academic Lead");
            Trainer trainer3 = new Trainer("Aliki", "Tsirozidi", "Academic Assistant");

            Trainer[] syntheticTrainers = new Trainer[] { trainer1, trainer2, trainer3 };

            return syntheticTrainers;
        }
        Student[] createSyntheticStudent()
        {
            Student student1 = new Student("George", "Malandris", new DateTime(1988, 6, 6), 1800);
            Student student2 = new Student("Tatiana", "Tsitsani", new DateTime(1991, 8, 14), 2000);
            Student student3 = new Student("John", "Rizos", new DateTime(1998, 2, 23), 1500.50);

            Student[] syntheticStudents = new Student[] { student1, student2, student3 };

            return syntheticStudents;
        }
        Assignment[] createSyntheticAssignment()
        {
            Assignment assignment1 = new Assignment("IndividualProject", "Project", new DateTime(2020, 9, 22), 50, 100);
            Assignment assignment2 = new Assignment("TeamProject", "Project", new DateTime(2020, 12, 11), 50, 100);
            Assignment assignment3 = new Assignment("Exercise 1", "Assignment", new DateTime(2020, 10, 18), 50, 100);

            Assignment[] syntheticAssignments = new Assignment[] { assignment1, assignment2, assignment3 };

            return syntheticAssignments;
        }
    }
}
