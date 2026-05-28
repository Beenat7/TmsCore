using System;

public class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
    {
        // TODO 1: Guard Clauses (Fail Fast)
        if (student is null) 
            throw new ArgumentNullException(nameof(student));

        if (course is null) 
            throw new ArgumentNullException(nameof(course));

        if (course.Capacity - course.EnrolledCount <= 0)
            throw new InvalidOperationException("System constraint: Course capacity is full.");

        // TODO 2: Switch Expression for Academic Standing
        string standing = student.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "Good Standing",
            _ => "Academic Warning" // Fallback arm to protect against unhandled ranges
        };

        Console.WriteLine($"{student.Name} is in {standing}.");

        // Increment enrolled count as a business rule tracking update
        course.EnrolledCount++;

        // TODO 3: Return materialized record
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}