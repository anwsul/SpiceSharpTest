using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;


var ckt = new Circuit(
    new VoltageSource("V_bias", "Vcc", "0", 12.0),
    new VoltageSource("Vin", "input", "0", new Sine(0.0, 0.1, 50)),
    new Capacitor("C1", "input", "base", 10e-6),
    new Resistor("Rb1", "Vcc", "base", 47e3),
    new Resistor("Rb2", "base", "0", 10e3),
    new Bjt("Bjt1").Connect("base", "collector", "emitter", "0"),
    new Resistor("Rc", "Vcc", "collector", 2.2e3),
    new Resistor("Re", "emitter", "0", 1e3),
    new Capacitor("Ce", "emitter", "0", 100e-6),
    new Capacitor("C2", "collector", "output", 10e-6),
    new Resistor("Rl", "output", "0", 10e3)
);

var tran = new Transient("Tran1", 0.01, 0.1);

var timePoints = new List<double>();
var input = new List<double>();
var output = new List<double>();

foreach (var _ in tran.Run(ckt)) {
    timePoints.Add(tran.Time);
    input.Add(tran.GetVoltage("input"));
    output.Add(tran.GetVoltage("output"));
}

Plotter.Plot(timePoints, [input, output]);