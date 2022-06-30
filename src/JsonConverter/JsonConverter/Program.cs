using JsonConverter;
using System.Collections;
using System.Text;


var adderss = new Address()
{
    Street = "G L Roy Road",
    City = "Rangpur",
    Country = "Bangladesh"
};

var sessions = new List<Session>()
{
    new Session()  { DurationInHour = 5, LearningObjective = "important"},
    new Session()  {DurationInHour = 8, LearningObjective = "less importart"}
};

var topic = new Topic()
{
    Title = "Java",
    Description = "This is java topic",
    Sessions = sessions
};

var instructor = new Instructor
{
    Name = "Likhon",
    Email = "lsjdfkj@gmail.com",
    PresentAddress = adderss,
    PermanentAddress = adderss,
    PhoneNumbers = new List<Phone> { new Phone { CountryCode ="xxx", Extension="yyy", Number= "zzz" },
                                     new Phone { CountryCode ="xxx", Extension="yyy", Number= "zzz" }
                                    }
};

double[] str = { 1, 2, 3, 4, 5, 6, 7, 8 };

var ex = new Example
{
    Name = "Akash",
    Roll = 40.56f,
    //Subject = new List<string>() { "sldkj", "sldjfkld", "sldfjkdf" }
    //Subject = new List<int> { 4, 4, 2},
    Subject = str,
    IsCheck = true
};

var listInstructor = new List<Instructor>();
listInstructor.Add(instructor);
listInstructor.Add(instructor);

var admissionTest = new AdmissionTest()
{
    StartDateTime = DateTime.Now,
    EndDateTime = DateTime.Now,
    TestFees = 888.87
};

var course = new Course
{
    Title = "C sharp",
    Teacher = instructor,
    Topics = new List<Topic> { topic, topic },
    Fees = 500.55,
    Tests = new List<AdmissionTest> {admissionTest, admissionTest },
    Example = ex
};

var courseList = new List<Course>() { course, course };
Course[] courseArray = courseList.ToArray();
 



Console.WriteLine(JsonFormatter.Convert(courseList));
