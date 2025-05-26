using SpiceSharp;
using SpiceSharp.Components;


public class Bjt : Subcircuit {
    public Bjt(string name)
        : base(name, new SubcircuitDefinition(new Circuit(
        new BipolarJunctionTransistorModel("NPN_model"),
        new BipolarJunctionTransistor("Q1", "collector", "base", "emitter", "substrate", "NPN_model")
        ), "base", "collector", "emitter", "substrate")) {
    }
}