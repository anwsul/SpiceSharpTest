using ConsolePlot;

class Plotter {
    public static void Plot(List<double> times, List<List<double>> inputs) {
        Console.WriteLine("\n\n");
        Plot plot = new Plot(80, 22);

        foreach (var input in inputs) {
            plot.AddSeries(times, input);
        }

        plot.Draw();
        plot.Render();

        Console.WriteLine("\n\n");
    }
}