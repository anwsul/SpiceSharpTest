using ConsolePlot;

class Plotter
{


    public static void Plot(List<double> times, List<double> input, List<double> output)
    {
        Console.WriteLine("\n\n");
        Plot plot = new Plot(80, 22);
        plot.AddSeries(times, input);
        plot.AddSeries(times, output);
        plot.Draw();
        plot.Render();

        Console.WriteLine("\n\n");
    }
}