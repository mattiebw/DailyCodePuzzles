using System.Diagnostics;
using System.Reflection;
using System.Resources;
using Spectre.Console;

namespace Core;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.WriteLine("Finding Solutions...");
        
        var dlls = Directory.GetFiles(args[0], "*.dll");
        var assemblies = dlls.Select(Assembly.LoadFile);
        var solutionAssemblies = assemblies.Where(a => a.GetCustomAttribute<SolutionAssemblyAttribute>() != null);
        var solutionAssemblyArray = solutionAssemblies as Assembly[] ?? solutionAssemblies.ToArray();
        var types = solutionAssemblyArray.SelectMany(a => a.GetTypes());
        var solutions = types.Where(t => t.GetCustomAttribute<SolutionAttribute>() != null);
        
        // Load required inputs
        Dictionary<Assembly, ResourceManager> inputs = new();
        foreach (var solutionAssembly in solutionAssemblyArray)
        {
            ResourceManager resources = new ResourceManager($"{solutionAssembly.GetName().Name}.Inputs", solutionAssembly);
            inputs.Add(solutionAssembly, resources);
        }
        
        AnsiConsole.WriteLine($"Found {solutionAssemblyArray.Length} solution assemblies!");
        foreach (var solutionAssembly in solutionAssemblyArray)
        {
            AnsiConsole.WriteLine(
                
                $"    {solutionAssembly.GetName().Name} - {solutionAssembly.GetCustomAttribute<SolutionAssemblyAttribute>()!.Year}");
        }
        AnsiConsole.WriteLine();

        var solutionArray = solutions as Type[] ?? solutions.ToArray();
        
        while (true)
        {
            var solution = AnsiConsole.Prompt(new SelectionPrompt<Type>()
                .Title("Select a solution:")
                .PageSize(20)
                .MoreChoicesText("Move up and down to see all solutions")
                .AddChoices(solutionArray)
                .AddChoices([null!])
                .UseConverter(solution =>
                {
                    if (solution is null)
                        return "Exit";
                    return $"{solution.Assembly.GetCustomAttribute<SolutionAssemblyAttribute>()!.Year} - Day {solution.GetCustomAttribute<SolutionAttribute>()!.Day}";
                }));

            if (solution is null)
                break;
        
            var tasks = solution.GetMethods().Where(m => m.GetCustomAttribute<TaskAttribute>() != null);
            var taskArray = tasks as MethodInfo[] ?? tasks.ToArray();
        
            foreach (var task in taskArray)
            {
                var ind = task.GetCustomAttribute<TaskAttribute>()!.Index;
                var inputOverride = task.GetCustomAttribute<TaskAttribute>()!.InputIndexOverride;
                var useTest = task.GetCustomAttribute<TaskAttribute>()!.UseTest;
                var inputIndex = inputOverride == -1 ? ind : inputOverride;
                
                AnsiConsole.WriteLine($"Running task {ind}");
                AnsiConsole.WriteLine("-----------------------");
                var day = task.DeclaringType!.GetCustomAttribute<SolutionAttribute>()!.Day;
                object?[] param = [inputs[task.DeclaringType!.Assembly].GetString($"Day{day}-Task{inputIndex}{(useTest ? "-Test" : "")}")];
                Stopwatch sw = new();
                sw.Restart();
                task.Invoke(null, param);
                sw.Stop();
                AnsiConsole.WriteLine("-----------------------");
                AnsiConsole.WriteLine($"Task {ind} took {sw.ElapsedMilliseconds}ms");
                AnsiConsole.WriteLine("-----------------------");
            }
        }
    }
}
