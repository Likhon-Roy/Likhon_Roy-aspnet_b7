// See https://aka.ms/new-console-template for more information
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

var ex = new Example
{
    Name = "Akash",
    Roll = 40.56f,
    //Subject = new List<string>() { "sldkj", "sldjfkld", "sldfjkdf" }
    Subject = new List<bool> { true, false, true},
    //IsCheck = true
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

//Console.WriteLine(course);
 

Console.WriteLine(JsonFormatter.Convert(topic));


//var sb = new StringBuilder("likhon");

//Console.WriteLine(sb.Remove(sb.Length - 2, 1));

//var ch = sb[sb.Length - 1];
//Console.WriteLine(sb[4]);