--- Address Table

CREATE TABLE [dbo].[Addresss] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Street]       NVARCHAR (MAX)   NULL,
    [City]         NVARCHAR (MAX)   NULL,
    [Country]      NVARCHAR (MAX)   NULL,
    [InstructorId] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Addresss] PRIMARY KEY CLUSTERED ([Id] ASC)
);

------ AdmissionTest Table

CREATE TABLE [dbo].[AdmissionTests] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [StartDateTime] DATETIME2 (7)    NULL,
    [EndDateTime]   DATETIME2 (7)    NULL,
    [TestFees]      FLOAT (53)       NULL,
    [CourseId]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_AdmissionTests] PRIMARY KEY CLUSTERED ([Id] ASC)
);


----- Course Table

CREATE TABLE [dbo].[Courses] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR (MAX)   NULL,
    [Fees]  FLOAT (53)       NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-----Instructors Table

CREATE TABLE [dbo].[Instructors] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (MAX)   NULL,
    [Email]    NVARCHAR (MAX)   NULL,
    [CourseId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Instructors] PRIMARY KEY CLUSTERED ([Id] ASC)
);

------ Phones Table

CREATE TABLE [dbo].[Phones] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Number]       NVARCHAR (MAX)   NULL,
    [Extension]    NVARCHAR (MAX)   NULL,
    [CountryCode]  NVARCHAR (MAX)   NULL,
    [InstructorId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED ([Id] ASC)
);

----- Session Table

CREATE TABLE [dbo].[Sessions] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [DurationInHour]    INT              NULL,
    [LearningObjective] NVARCHAR (MAX)   NULL,
    [TopicId]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

---- Topics Table

CREATE TABLE [dbo].[Topics] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (MAX)   NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [CourseId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED ([Id] ASC)
);

