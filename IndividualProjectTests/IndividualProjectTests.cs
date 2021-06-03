using System;
using Individual_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IndividualProjectTests
{
    [TestClass]
    public class DataBaseTests
    {
        [DataTestMethod]
        [DataRow("cb15", "c#", "Online", "2020-9-16", "2021-3-16", 7)]
        [DataRow("cb11", "c#", "Online", "2020-9-16", "2021-3-16", -1)]
        [DataRow("cb11", "PYTHON", "PARTIME", "2020-9-16", "2021-3-16", 8)]
        [DataRow("CB12", "Java", "partime", "2020-9-16", "2021-3-16", -1)]
        [DataRow("CB13", "Java", "online", "2020-9-16", "2021-3-16", -1)]
        [DataRow("cb09", "c#", "Online", "2020-9-16", "2021-3-16", 9)]
        [DataRow("cb11", "Javascript", "Online", "2020-9-16", "2021-3-16", 10)]
        [DataRow("CB13", "Java", "online", "2020-9-16", "2021-3-16", -1)]

        public void TestCourseNewID(string title, string stream, string type, string startdt, string enddt, int result)
        {
            //Tests if the DataBase Class gives correct IDs to Courses and prevents doubles
            InitializeDB();
            DateTime startDateTime = Convert.ToDateTime(startdt);
            DateTime endDateTime = Convert.ToDateTime(enddt);
            Course course = new Course(title, stream, type, startDateTime, endDateTime);

            Assert.AreEqual(result, course.ID);
        }
        void InitializeDB()
        {
            Course course1 = new Course("CB11", "C#", "online", new DateTime(2020, 9, 16), new DateTime(2021, 3, 16));//ID = 1
            Course course2 = new Course("CB11", "C#", "parttime", new DateTime(2020, 9, 16), new DateTime(2021, 3, 16));//ID = 2
            Course course3 = new Course("CB10", "Python", "online", new DateTime(2020, 3, 16), new DateTime(2020, 9, 16));//ID = 3
            Course course4 = new Course("CB12", "Java", "partime", new DateTime(2020, 3, 16), new DateTime(2020, 9, 16));//ID = 4
            Course course5 = new Course("CB15", "Python", "fulltime", new DateTime(2020, 3, 16), new DateTime(2020, 9, 16));//ID = 5
            Course course6 = new Course("CB13", "Java", "online", new DateTime(2020, 3, 16), new DateTime(2020, 9, 16));//ID = 6
        }
    }
    [TestClass]
    public class CourseTests
    {
        [DataTestMethod]
        [DataRow("George", "Malandris", "2020-9-16", 1500)]
        [DataRow("Tatiana", "Tsitsani", "2020-9-16", 1500)]
        [DataRow("John", "Rizos", "2020-9-16", 1500)]
        [DataRow("George", "Papanikolaou", "2020-9-16", 1500)]
        [DataRow("Bill", "Malandris", "2020-9-16", 1500)]
        [DataRow("Michael", "Zabetas", "2020-9-16", 1500)]

        public void TestAddToCourse(string firstname, string lastname, string DateOfBirth, double tuitionFees)
        {
            //Tests if the addToCourse Method updates the students listOfCoursesIDs
            Course course1 = new Course("CB11", "C#", "online", new DateTime(2020, 9, 16), new DateTime(2021, 3, 16));//ID = 1
            DateTime DateOfBirthdt = Convert.ToDateTime(DateOfBirth);
            Student student = new Student(firstname, lastname, DateOfBirthdt, tuitionFees);

            bool before = student.getConnections().getList(availableTypes.Course).Contains(course1.ID);
            course1.addToCourse(student);
            bool after = student.getConnections().getList(availableTypes.Course).Contains(course1.ID);
            bool output = (!before && after);

            Assert.IsTrue(output);
        }
    }
}
