using SpiceSharp.Simulations;


var cktManager = new CircuitManager();
cktManager.InitializeCircuit();
var ckt = cktManager.ckt;

// simulate wire connection
cktManager.ConnectWire("wire1", "VAR", "VAR_1");
cktManager.ConnectWire("wire2", "TPT", "TPT_P_1");


var tran = new Transient("tran1", 0.001, 0.1);

List<double> times = new();
List<double> input = new();
List<double> output = new();

try
{

    foreach (var _ in tran.Run(ckt))
    {
        times.Add(tran.Time);
        input.Add(tran.GetVoltage("TPT_P_1"));
        output.Add(tran.GetVoltage("TPT_S_1"));
    }

}
catch (ValidationFailedException ex)
{
    Console.WriteLine($"Validation failed with {ex.Rules.ViolationCount} violations:");
    foreach (var violation in ex.Rules.Violations)
    {
        Console.WriteLine($"- {violation.GetType().Name}: {violation}");
    }
}

Plotter.Plot(times, input, output);
