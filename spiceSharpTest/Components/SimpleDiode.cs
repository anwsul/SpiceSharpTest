using SpiceSharp;
using SpiceSharp.Components;


public class SimpleDiode : Subcircuit {
    public SimpleDiode(string name)
        : base(name, new SubcircuitDefinition(new Circuit(
            new DiodeModel("D_model"),
            new Diode("D1", "anode", "cathode", "D_model")
        ), "anode", "cathode")) {
    }
}