namespace Core;

[AttributeUsage(AttributeTargets.Assembly)]
public class SolutionAssemblyAttribute(string puzzle, int year) : Attribute
{
    public String Puzzle = puzzle;
    public int Year = year;
}

[AttributeUsage(AttributeTargets.Class)]
public class SolutionAttribute(int day) : Attribute
{
    public int Day = day;
}

[AttributeUsage(AttributeTargets.Method)]
public class TaskAttribute(int index, int inputIndexOverride = -1, bool useTest = false) : Attribute
{
    public int Index = index;
    public int InputIndexOverride = inputIndexOverride;
    public bool UseTest = useTest;
}
