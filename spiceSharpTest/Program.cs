var wire1 = new Wire("wire1");
var wire2 = new Wire("wire2");
var wire3 = new Wire("wire3");
var wire4 = new Wire("wire4");
var wire5 = new Wire("wire5");
var wire6 = new Wire("wire6");
var wire7 = new Wire("wire7");

CircuitManager3.ConnectWire(wire1.Name, "VAR_1");
CircuitManager3.ConnectWire(wire1.Name, "VM1_1");
CircuitManager3.ConnectWire(wire2.Name, "VAR_2");
CircuitManager3.ConnectWire(wire2.Name, "VM1_2");
CircuitManager3.ConnectWire(wire3.Name, "VM1_1");
CircuitManager3.ConnectWire(wire3.Name, "WM1_1");
CircuitManager3.ConnectWire(wire4.Name, "VM1_2");
CircuitManager3.ConnectWire(wire4.Name, "WM1_3");
CircuitManager3.ConnectWire(wire5.Name, "WM1_2");
CircuitManager3.ConnectWire(wire5.Name, "AM1_1");
CircuitManager3.ConnectWire(wire6.Name, "AM1_2");
CircuitManager3.ConnectWire(wire6.Name, "RH_1");
CircuitManager3.ConnectWire(wire7.Name, "WM1_3");
CircuitManager3.ConnectWire(wire7.Name, "RH_2");

Console.WriteLine($"Voltage: {CircuitManager3.SimulationResult["VM1"]}");
Console.WriteLine($"Current: {CircuitManager3.SimulationResult["AM1"]}");
Console.WriteLine($"Power: {CircuitManager3.SimulationResult["WM1"]}");
