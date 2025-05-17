using SpiceSharp;
using SpiceSharp.Components;

class CircuitManager
{
    public static double variacRatio = 1;
    public Circuit ckt = [];
    readonly string ground = "0";

    static IComponent variac = new Variac("VAR", variacRatio);
    static IComponent threePhaseTrasformer = new ThreePhaseTransformer("TPT");
    static IComponent ammeter = new Ammeter("AM1");
    static IComponent voltmeter = new VoltMeter("VM1");
    static IComponent wattmeter = new Wattmeter("WM1");
    static IComponent rheostat = new Rheostat("RH", 10);

    Dictionary<string, IComponent> deviceNameToDeviceMap = new()
    {
        {"VAR", variac },
        {"TPT", threePhaseTrasformer},
        {"AM1", ammeter},
        {"VM1", voltmeter},
        {"WM1", wattmeter},
        {"RH", rheostat},
    };


    public void InitializeCircuit()
    {
        variac.Connect("VAR_1", "VAR_2", "VAR_3", ground);
        threePhaseTrasformer.Connect("TPT_P_1", "TPT_P_2", "TPT_P_3", "TPT_S_1", "TPT_S_2", "TPT_S_3", ground);
        ammeter.Connect("AM1_1", "AM1_2");
        voltmeter.Connect("VM1_1", "VM1_2");
        wattmeter.Connect("WM1_1", "WM1_2", "WM1_3");
        rheostat.Connect("RH_1", "RH_2", "RH_3");
    }

    public void ConnectWire(string wireName, string deviceName, string nodeName)
    {
        if (!ckt.Contains(wireName))
        {
            var newWire = new Wire(wireName).Connect(nodeName, $"unconnected_{wireName}_1");
            ckt.Add(newWire);
        }
        else
        {
            Wire wire = (Wire)ckt[wireName];
            var currentNodes = wire.Nodes;

            string[] newNodes = [.. currentNodes];

            if (newNodes[0].StartsWith("unconnected_"))
                newNodes[0] = nodeName;
            else if (newNodes[1].StartsWith("unconnected_"))
                newNodes[1] = nodeName;

            wire.Connect(newNodes);
        }

        if (!ckt.Contains(nodeName))
            ckt.Add(deviceNameToDeviceMap[deviceName]);
    }

    public void DisconnectWire(string wireName, string deviceName, string nodeName)
    {
        if (ckt.Contains(wireName))
        {
            Wire wire = (Wire)ckt[wireName];
            var currentNodes = wire.Nodes;
            string[] newNodes = [.. currentNodes];

            if (currentNodes[0] == nodeName)
                newNodes[0] = $"unconnected_{wireName}_0";
            else if (currentNodes[1] == nodeName)
                newNodes[1] = $"unconnected_{wireName}_1";

            // floating wire
            if (newNodes[0].StartsWith("unconnected_") && newNodes[1].StartsWith("unconnected_"))
                ckt.Remove(wire);
            else
                wire.Connect(newNodes);

            var device = deviceNameToDeviceMap[deviceName];
            var deviceNodes = device.Nodes;
            var allNodes = FindAllNodes();
            ckt.Remove(device);

            foreach (var node in deviceNodes)
            {
                if (allNodes.Contains(node))
                {
                    ckt.Add(device);
                    break;
                }
            }

        }

    }

    public HashSet<string> FindAllNodes()
    {
        var allNodes = new HashSet<string>();
        foreach (var entity in ckt)
        {
            if (entity is IComponent component)
            {
                foreach (string node in component.Nodes)
                {
                    allNodes.Add(node);
                }
            }

        }

        return allNodes;
    }


    public void TryRunningSimulation()
    {

    }

    public void CalculateRealPower(double voltage, double current)
    {

    }

    public void UpdateRheostatValue()
    {

    }


    public void UpdateVariacFactor(double factor)
    {

    }

}