using SpiceSharp.Simulations;


var cktManager = new CircuitManager();
cktManager.InitializeCircuit();
var ckt = cktManager.ckt;

// simulate wire connection
var wire1 = new Wire("wire1");
var wire2 = new Wire("wire2");
var wire3 = new Wire("wire3");
var wire4 = new Wire("wire4");
var wire5 = new Wire("wire5");
var wire6 = new Wire("wire6");
var wire7 = new Wire("wire7");
var wire8 = new Wire("wire8");

cktManager.ConnectWire(wire1.Name, "VAR_1");
cktManager.ConnectWire(wire2.Name, "VAR_2");
cktManager.ConnectWire(wire3.Name, "VAR_3");

cktManager.ConnectWire(wire1.Name, "AM1_1");
cktManager.ConnectWire(wire4.Name, "AM1_2");

cktManager.ConnectWire(wire4.Name, "TPT_P_1");
cktManager.ConnectWire(wire2.Name, "TPT_P_2");
cktManager.ConnectWire(wire3.Name, "TPT_P_3");

cktManager.ConnectWire(wire5.Name, "TPT_S_1");
cktManager.ConnectWire(wire6.Name, "TPT_S_2");
cktManager.ConnectWire(wire7.Name, "TPT_S_3");

cktManager.ConnectWire(wire5.Name, "VM1_1");
cktManager.ConnectWire(wire6.Name, "VM1_2");

var tran = new Transient("tran1", 0.001, 0.1);

List<double> times = new();
List<double> input = new();
List<double> output = new();

try
{

    foreach (var _ in tran.Run(ckt))
    {
        times.Add(tran.Time);
        var voltage1 = tran.GetVoltage("TPT_P_1", "TPT_P_2");
        var voltage2 = tran.GetVoltage("VM1_1", "VM1_2");

        input.Add(voltage1);
        output.Add(voltage2);
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

Console.WriteLine(input.Max());
Console.WriteLine(output.Max());
