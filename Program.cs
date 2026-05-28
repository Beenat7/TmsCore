//module-1-session-1
using System;
using System.Collections.Generic;
using System.Linq;
//Null Safety in modern c#
//Because <Nullable>enable</Nullable> is turned on the compiler expects every normal string to contain actual text.
//string region = null; // Compiler warning CS8600
//Console.WriteLine(region.ToUpper()); //  Compiler warning CS8602


// The fix for the above issue 
// Declare the variable as nullable with '?'
// This tells the compiler: "I know this might be null. I accept responsibility."
string? region = null;
// Null-conditional operator '?.' — skip the call if null
// If region is null, ToUpper() never executes. No crash.
string? upperRegion = region?.ToUpper();
Console.WriteLine($"Region (conditional): {upperRegion}");
// Null-coalescing operator '??' — provide a fallback value
// If region is null, use "Unassigned" instead.
string displayRegion = region ?? "Unassigned";
Console.WriteLine($"Region (coalesced): {displayRegion}");
// Null-coalescing assignment '??=' — assign only if currently null
// Useful for lazy initialization.
region ??= "Addis Ababa";
Console.WriteLine($"Region (assigned): {region}");


// declare variables using core primitive types and date formatting
string studentName = "Abeba";
string studentId = "STU-001";
int enrollmentCount = 3;
decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
DateTime enrolledAt = DateTime.UtcNow;
string? campusRegion = null;
Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Courses: {enrollmentCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");


//resolve legacy double floating-point financial drift bug using precise decimal type
// Legacy implementation — the bug that caused the audit failure
//double grantPerStudent = 1999.99;
//double totalAllocation = grantPerStudent * 100_000;
//Console.WriteLine($"Total allocated (double): {totalAllocation}");
// Fixed implementation — exact financial math
decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;
Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");



// Legacy implementation — what the logging service did to the data
public class Enrollment
{
public string StudentId { get; set; } = string.Empty;
public string CourseCode { get; set; } = string.Empty;
public DateTime ProcessedAt { get; set; }
}
// Somewhere in the logging pipeline:
// enrollment.CourseCode = null; // ← No compiler error. Data silently corrupted.



// Immutable by design — the logging pipeline cannot corrupt this
// public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);
// var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
// Console.WriteLine(enrollment);
// // Try to mutate it — uncomment this line and see the compiler error:
// // enrollment.CourseCode = "HACKED"; // ERROR: init-only property
// // Non-destructive copy — creates a NEW record with one field changed
// var corrected = enrollment with { CourseCode = "CS-402" };
// Console.WriteLine(corrected);
// // Value equality — two records with the same data are equal

// var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
// Console.WriteLine($"Same data? {enrollment == duplicate}"); // True




// Legacy Pre-C# 14 Implementation (Verbose)
// public class Course
// {
// private int _capacity; // Manual backing field
// public int Capacity
// {
// get => _capacity;
// set
// {
// if (value <= 0)
// throw new ArgumentOutOfRangeException("Capacity must be positive.");
// _capacity = value;
// }
// }
// }


var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");
// Invalid capacity — should throw
try
{

course.Capacity = -5;
}
catch (ArgumentOutOfRangeException ex)
{
Console.WriteLine($"Caught: {ex.Message}");
}
// Invalid title — should throw
try
{
course.Title = "";
}
catch (ArgumentException ex)
{
Console.WriteLine($"Caught: {ex.Message}");
}


var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");




void PrintGradeReport(IEnumerable<IGradable> assessments)
{
Console.WriteLine("--- Grade Report ---");
foreach (var item in assessments)
{
Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
}
}
// Test it — one array holds two completely different types
IGradable[] cohortAssessments = [
new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 },
new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m }
];
PrintGradeReport(cohortAssessments);





//module-1-session-2

//Exercise 5
var service = new EnrollmentService();

// Test 1: Valid registration
var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
var result = service.ProcessRegistration(validStudent, validCourse);
Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

// Test 2: Null student should throw
try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Guard caught: {ex.ParamName}");
}

// Test 3: Full course should throw
var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 };
fullCourse.EnrolledCount = 1;
try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Business rule: {ex.Message}");
}






//Exercise 5
Console.WriteLine("\n=== Running Exercise 5: Analytics Dashboard ===");

// Step 1: Create Student Data using C# 12 Collection Expressions
List<Student> students = [
    new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m },
    new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m },
    new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
    new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
    new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
    new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
    new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
];

// Step 2: Build the Honors Leaderboard (Chained LINQ Queries)
List<string> leaderboard = students
    .Where(s => s.GPA >= 3.5m)                // TODO 1: Extract Honors
    .OrderByDescending(s => s.GPA)            // TODO 2: Sort by descending rank
    .Select(s => s.Name)                      // TODO 3: Project only the Name string
    .ToList();                                // TODO 4: Materialize the query instantly

Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
foreach (var name in leaderboard)
{
    Console.WriteLine($"- {name}");
}

// Step 3: Class Average
decimal averageGpa = students.Average(s => s.GPA); // TODO 5: Calculate Average
Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

// Step 4: Group by Academic Standing using GroupBy and Switch Expressions
var standingGroups = students.GroupBy(s => s.GPA switch
{
    >= 3.5m => "Honors",
    >= 2.5m => "Good Standing",
    >= 2.0m => "Probation",
    _ => "Academic Warning" // TODO 6: Cover fallback classification arm
});

Console.WriteLine("\n--- Academic Standing Report ---");
foreach (var group in standingGroups)
{
    Console.WriteLine($"\n{group.Key} ({group.Count()}):");
    foreach (var s in group)
    {
        Console.WriteLine($"  {s.Name} GPA: {s.GPA}");
    }
}

// Step 5: Collection Expressions with the Spread Operator
string[] backendCourses = ["C#", "ASP.NET Core"];
string[] frontendCourses = ["TypeScript", "Angular"];

// TODO 7: Combine elements seamlessly using the modern spread mechanics (..)
string[] allCourses = [.. backendCourses, .. frontendCourses, "Capstone"]; 
Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}");





