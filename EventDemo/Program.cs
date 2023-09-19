namespace EventDemo;

class Program
{
    static void Main(string[] args)
    {
        CollegeClassModel history = new CollegeClassModel("History 101", 3);
        CollegeClassModel math = new CollegeClassModel("Calculus 201", 2);

        history.EnrollmentFull += CollegeClass_EnrollmentFull;

        history.SignUpStudent("uno").PrintToConsole();
        history.SignUpStudent("dos").PrintToConsole();
        history.SignUpStudent("tres").PrintToConsole();
        history.SignUpStudent("quatro").PrintToConsole();
        Console.WriteLine();

        math.EnrollmentFull += CollegeClass_EnrollmentFull;

        math.SignUpStudent("uno").PrintToConsole();
        math.SignUpStudent("dos").PrintToConsole();
        math.SignUpStudent("tres").PrintToConsole();
        math.SignUpStudent("quatro").PrintToConsole();
        Console.WriteLine();

        Console.ReadLine();
    }

    private static void CollegeClass_EnrollmentFull(object? sender, string e)
    {
        CollegeClassModel model = (CollegeClassModel)sender;

        Console.WriteLine();
        Console.WriteLine($"{model.CourseTitle}: Full");
        Console.WriteLine();
    }

    private static void History_EnrollmentFull(object? sender, string e)
    {
        Console.WriteLine("Enrollment is full for history.");
    }
}

public static class ConsoleHelpers
{
    public static void PrintToConsole(this string message)
    {
        Console.WriteLine(message);
    }
}

public class CollegeClassModel
{
    public event EventHandler<string> EnrollmentFull;

    private List<string> enrolledStudents = new List<string>();
    private List<string> waitingList = new List<string>();

    public string CourseTitle { get; private set; }
    public int MaxStudents { get; private set; }    

    public CollegeClassModel(string title, int maxStudents)
    {
        CourseTitle = title;
        MaxStudents = maxStudents;
    }

    public string SignUpStudent(string studentName)
    {
        string output = "";
        if (enrolledStudents.Count < MaxStudents)
        {
            enrolledStudents.Add(studentName);
            output = $"{studentName} was enrolled in {CourseTitle}.";
        }
        else
        {
            waitingList.Add(studentName);
            output = $"{studentName} was added to the wait list in {CourseTitle}.";

            EnrollmentFull?.Invoke(this, $"{CourseTitle} enrollment is full.");
        }

        return output;
    }
}