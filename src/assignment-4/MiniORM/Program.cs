using MyMiniOrm;
using System.Collections;
using System.Text;


var adderss1 = new Address()
{
    Id = new Guid("6937ec6f-46a9-46c7-9c92-dff5b9eb1465"),
    // Id = Guid.NewGuid(),
    Street = "G L Roy Road",
    City = "Rangpur",
    Country = "Bangladesh"
};

// var adderss2 = new Address()
// {
//     Id = new Guid("59df9f68-5c40-4b41-8f22-1a3405d14d86"),
//     // Id = Guid.NewGuid(),
//     Street = "G L Roy Road",
//     City = "Rangpur",
//     Country = "Bangladesh"
// };

var sessions1 = new List<Session>()
{
    new Session()  {
        Id= new Guid("6b435dbb-e3ca-4bdd-915a-0cd574d13eed"),
        // Id = Guid.NewGuid(),
        DurationInHour = 5,
        LearningObjective = "noooooooo"},

    new Session()  {
        Id= new Guid("86f44f66-bedd-4f0a-9397-395f711d49de"), 
        // Id = Guid.NewGuid(),
        DurationInHour = 8,
        LearningObjective = "yessssssssss"}
};

var sessions2 = new List<Session>()
{
    new Session()  {
        Id= new Guid("989018f8-a52a-4d55-a6b5-536e765a36e6"),
        // Id = Guid.NewGuid(),
        DurationInHour = 5,
        LearningObjective = "important"},

    new Session()  {
        Id= new Guid("e7dfe44e-1bc8-40e9-a056-d8aa853d6502"), 
        // Id = Guid.NewGuid(),
        DurationInHour = 8,
        LearningObjective = "less iiiiiiiiiiiiii"}
};

var topic1 = new Topic()
{
    Id = new Guid("c482d3ff-2a38-4da6-b8f2-82715d301da0"),
    // Id = Guid.NewGuid(),
    Title = "Csharp",
    Description = "This is c# topic",
    Sessions = sessions1
};

var topic2 = new Topic()
{
    Id = new Guid("26ebaa06-2587-409f-9aca-8d2ec7d080bd"),
    // Id = Guid.NewGuid(),
    Title = "Java",
    Description = "This is java topic",
    Sessions = sessions2
};

var instructor1 = new Instructor
{
    Id = new Guid("f93437e3-1966-4d0b-8173-45b157ae6da0"),
    // Id = Guid.NewGuid(),
    Name = "Likhon",
    Email = "lsjdfkj@gmail.com",
    InstructorAddress = adderss1,
    // PermanentAddress = adderss2,
    PhoneNumbers = new List<Phone> {
            new Phone {
                Id= new Guid("3cb2e72e-860b-40ba-af00-a1a699b3242d"),
                // Id = Guid.NewGuid(),
                CountryCode ="111111111111",
                Extension="bbb",
                Number= "ccc"
            },

            new Phone {
            Id= new Guid("04afae75-b035-4bdb-a04c-241eb5f69d0a"), 
            // Id = Guid.NewGuid(),
            CountryCode ="xxx",
            Extension="yyy",
            Number= "zzz"
            }
        }
};

var admissionTest1 = new AdmissionTest()
{
    Id = new Guid("aabd3bb7-4f30-414c-8485-85fb01acaafd"),
    // Id = Guid.NewGuid(),
    StartDateTime = new DateTime(2022, 05, 09, 9, 15, 0),
    EndDateTime = new DateTime(2022, 05, 10, 9, 15, 0),
    TestFees = 888.87
};

var admissionTest2 = new AdmissionTest()
{
    Id = new Guid("b33738ae-742f-4d8b-b17b-bd96df834945"),
    // Id = Guid.NewGuid(),
    StartDateTime = new DateTime(2022, 05, 09, 9, 15, 0),
    EndDateTime = new DateTime(2022, 05, 10, 9, 15, 0),
    TestFees = 9999.87
};

var course = new Course
{
    Id = new Guid("79dd0913-acc2-4294-8f0b-aeb0f257cd0d"),
    // Id = Guid.NewGuid(),
    Title = "C sharp",
    Teacher = instructor1,
    Topics = new List<Topic> { topic1, topic2 },
    Fees = 500.55,
    Tests = new List<AdmissionTest> { admissionTest1, admissionTest2 },
};

// 1. For Update operation we need to set the primary keys.
//

var myORM = new MyORM<Guid, Topic>();

await myORM.Insert(topic1);

await myORM.GetById(new Guid("c482d3ff-2a38-4da6-b8f2-82715d301da0"));