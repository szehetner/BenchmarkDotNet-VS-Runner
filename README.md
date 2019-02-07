# BenchmarkDotNet-VS-Runner
Visual Studio Extension for exploring and running [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) benchmarks.

It provides a Tool Window that displays all benchmarks in the current solution. From there you can run one or more benchmarks and view the results.

![Benchmark Runner Window](https://github.com/szehetner/BenchmarkDotNet-VS-Runner/raw/master/src/BenchmarkRunner/Sample.png)

## Installation

Download it from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=StephanZehetner.BenchmarkRunner).

## Requirements
When running a benchmark, the Benchmark Runner builds and runs the benchmark project, passing the right command line parameters. Therefore it is necessary that the **Benchmark 
project uses the BenchmarkSwitcher class in its main method** to make sure that BenchmarkDotNet can process the command line parameters, e.g.:


```csharp
class Program
{
    static void Main(string[] args)
    { 
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
```
It will **not** work if you explicitly start a Benchmark in the Main method, e.g. with `BenchmarkRunner.Run<MyClass>()`


## How to use it

* Open the "Benchmarks" Tool Window through View -> Other Windows -> Benchmarks
* Click the "Refresh" button in the Benchmarks Window
* Select a node in the tree representing the benchmarks you want to run. Depending on the current grouping you can select a project, namespace, category, class or method.
* Run the benchmarks via the toolbar or the context menu. This will build the project and start it.
* The console window with the BenchmarkDotNet output is displayed and will stay open until manually closed
* Results can be viewed by enabling the result section from the toolbar ("Results Right" or "Results Bottom").
* You can also open the results folder by selecting the folder icon in the toolbar.
* If you need additional diagnosers or command line parameters, you can specify them in the options dialog.
* You can navigate to the code of a benchmark class or method by double clicking it in the tree or selecting "Go To Code" in the context menu.

## Limitations / Known Issues:
* If you add/remove/rename benchmark classes/methods in the code, the changes won't be visible in the tree view immediately. You need to click "Refresh" to update the tree.
* If benchmarks of more than one class are run together, the results won't show up in the results section.
* You can only select one node in the tree view at once
