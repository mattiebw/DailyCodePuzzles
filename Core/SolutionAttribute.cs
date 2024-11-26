namespace Core;

[AttributeUsage(AttributeTargets.Assembly)]
public class SolutionAssemblyAttribute(int year) : Attribute
{
    public int Year = year;
}

[AttributeUsage(AttributeTargets.Class)]
public class SolutionAttribute(int day) : Attribute
{
    public int Day = day;
}

[AttributeUsage(AttributeTargets.Method)]
public class TaskAttribute(int index) : Attribute
{
    public int Index = index;
}
