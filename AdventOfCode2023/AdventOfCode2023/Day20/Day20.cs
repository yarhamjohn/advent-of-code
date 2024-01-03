namespace AdventOfCode2023.Day20;

public static class Day20
{
    public static long SumPulses(string[] input)
    {
        var (broadcasterModule, untypedModule) = CreateModules(input);

        var buttonModule = new ButtonModule();

        var pulses = new Queue<Pulse>();
        for (var i = 0; i < 1000; i++)
        {
            PressButton(buttonModule, broadcasterModule, pulses);
        }

        return pulses.Count(p => p.Type == PulseType.Low) * pulses.Count(p => p.Type == PulseType.High);
    }

    public static long CountButtonPresses(string[] input)
    {
        var (broadcasterModule, untypedModule) = CreateModules(input);

        var buttonModule = new ButtonModule();
        
        var pulses = new Queue<Pulse>();
        var count = 0;
        while (!untypedModule.turnedOn)
        {
            PressButton(buttonModule, broadcasterModule, pulses);
            count++;
        }
        
        return count;
    }

    private static (BroadcasterModule, UntypedModule) CreateModules(string[] input)
    {
        List<IModule> modules = [];
        Dictionary<string, IEnumerable<string>> mappings = [];

        foreach (var line in input)
        {
            if (line.StartsWith("broadcaster"))
            {
                var targets = line.Split(" -> ")[1].Split(",").Select(x => x.Trim()).ToList();
                modules.Add(new BroadcasterModule());
                mappings["broadcaster"] = targets;
            }
            
            if (line.StartsWith("%"))
            {
                var targets = line.Split(" -> ")[1].Split(",").Select(x => x.Trim()).ToList();
                var name = line.Split(" -> ")[0].Replace("%", "");
                modules.Add(new FlipFlopModule(name));
                mappings[name] = targets;
            }
            
            if (line.StartsWith("&"))
            {
                var targets = line.Split(" -> ")[1].Split(",").Select(x => x.Trim()).ToList();
                var name = line.Split(" -> ")[0].Replace("&", "");
                modules.Add(new ConjunctionModule(name));
                mappings[name] = targets;
            }
        }
        
        // Add any "untyped modules"
        var untypedNames = mappings.SelectMany(x => x.Value).Distinct().Where(x => !mappings.Keys.Contains(x));
        foreach (var name in untypedNames)
        {
            modules.Add(new UntypedModule(name));
        }

        foreach (var name in mappings.Keys)
        {
            var module = modules.Single(x => x.GetName() == name);
            foreach (var target in mappings[name])
            {
                var targetModule = modules.Single(x => x.GetName() == target);
                module.AddTarget(targetModule);
            }
        }

        var conjunctionModuleNames = modules.Where(x => x is ConjunctionModule).Select(x => x.GetName());
        foreach (var name in conjunctionModuleNames)
        {
            var module = (ConjunctionModule) modules.Single(x => x.GetName() == name);

            var sources = mappings.Where(x => x.Value.Contains(name)).Select(x => x.Key);
            foreach (var source in sources)
            {
                module.AddSource(modules.Single(x => x.GetName() == source));
            }
        }

        return ((BroadcasterModule)modules.Single(x => x is BroadcasterModule),(UntypedModule)modules.Single(x => x is UntypedModule));
    }

    private static void PressButton(
        ButtonModule buttonModule,
        BroadcasterModule broadcasterModule,
        Queue<Pulse> pulses)
    {
        var x = new Queue<(IModule source, IModule target, PulseType pulseType)>();
        x.Enqueue((buttonModule, broadcasterModule, PulseType.Low));

        while (x.Count != 0)
        {
            var current = x.Dequeue();
            pulses.Enqueue(new Pulse(current.source, current.target, current.pulseType));

            var next = current.target.GetNext(current.source, current.pulseType);

            foreach (var t in next)
            {
                x.Enqueue(t);
            }
        }
    }

    private record Pulse(IModule Source, IModule Target, PulseType Type)
    {
        public override string ToString()
        {
            return $"{Source.GetName()} -{Enum.GetName(Type)}-> {Target.GetName()}";
        }
    }

    private interface IModule
    {
        string GetName();
        
        IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType);

        void AddTarget(IModule module);

        List<(IModule, IModule, PulseType)> GetNext(IModule source, PulseType pulseType);
    }

    private class ButtonModule : IModule
    {
        public ModuleType Type = ModuleType.Button;

        public string GetName() => "button";

        public IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType)
        {
            return [];
        }

        public void AddTarget(IModule module)
        {
        }

        public List<(IModule, IModule, PulseType)> GetNext(IModule source, PulseType pulseType)
        {
            return [];
        }
    }

    private class BroadcasterModule : IModule
    {
        public ModuleType Type = ModuleType.Broadcaster;

        private List<IModule> _targets = [];

        public string GetName() => "broadcaster";

        public IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType)
        {
            var modules = _targets
                .Select(target => new Pulse(this, target, pulseType))
                .ToList();
            
            foreach (var target in _targets)
            {
                modules.AddRange(target.HandlePulse(this, pulseType));
            }

            
            return modules;
        }

        public void AddTarget(IModule module)
        {
            _targets.Add(module);
        }

        public List<(IModule, IModule, PulseType)> GetNext(IModule source, PulseType pulseType)
        {
            return _targets.Select(x => ((IModule) this, x, pulseType)).ToList();
        }
    }

    private class FlipFlopModule : IModule
    {
        public ModuleType Type = ModuleType.FlipFlop;

        private List<IModule> _targets = [];
        private bool isOn;
        private string _name;
        
        public FlipFlopModule(string name)
        {
            _name = name;
        }

        public string GetName() => _name;

        public IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType)
        {
            if (pulseType is PulseType.High)
            {
                return [];
            }

            isOn = !isOn;

            var modules = new List<Pulse>();
            
            if (isOn)
            {
                foreach (var target in _targets)
                {
                    modules.Add(new Pulse(this, target, PulseType.High));
                }
                
                foreach (var target in _targets)
                {
                    modules.AddRange(target.HandlePulse(this, PulseType.High));
                }
            }
            else
            {
                foreach (var target in _targets)
                {
                    modules.Add(new Pulse(this, target, PulseType.Low));
                }

                foreach (var target in _targets)
                {
                    modules.AddRange(target.HandlePulse(this, PulseType.Low));
                }
            }
            
            return modules;
        }

        public void AddTarget(IModule module)
        {
            _targets.Add(module);
        }

        public List<(IModule, IModule, PulseType)> GetNext(IModule source, PulseType pulseType)
        {
            if (pulseType is PulseType.High)
            {
                return [];
            }
            
            isOn = !isOn;

            if (isOn)
            {
                return _targets.Select(x => ((IModule)this, x, PulseType.High)).ToList();
            }
            else
            {
                return _targets.Select(x => ((IModule)this, x, PulseType.Low)).ToList();
            }
        }
    }

    private class ConjunctionModule : IModule
    {
        public ModuleType Type = ModuleType.Conjunction;
        
        private List<IModule> _targets = [];
        private string _name;
        private Dictionary<string, PulseType> _previousPulses = [];

        public ConjunctionModule(string name)
        {
            _name = name;
        }

        public string GetName() => _name;

        public IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType)
        {
            _previousPulses[source.GetName()] = pulseType;
            
            var modules = new List<Pulse>();

            if (_previousPulses.Values.All(x => x == PulseType.High))
            {
                foreach (var target in _targets)
                {
                    modules.Add(new Pulse(this, target, PulseType.Low));
                }
                
                foreach (var target in _targets)
                {
                    modules.AddRange(target.HandlePulse(this, PulseType.Low));
                }
            }
            else
            {
                foreach (var target in _targets)
                {
                    modules.Add(new Pulse(this, target, PulseType.High));
                }
                
                foreach (var target in _targets)
                {
                    modules.AddRange(target.HandlePulse(this, PulseType.High));
                }
            }

            return modules;
        }
        
        public void AddTarget(IModule module)
        {
            _targets.Add(module);
        }

        public List<(IModule, IModule, PulseType)> GetNext(IModule source, PulseType pulseType)
        {
            _previousPulses[source.GetName()] = pulseType;

            if (_previousPulses.Values.All(x => x == PulseType.High))
            {
                return _targets.Select(x => ((IModule)this, x, PulseType.Low)).ToList();
            }
            else
            {
                return _targets.Select(x => ((IModule)this, x, PulseType.High)).ToList();
            }
        }

        public void AddSource(IModule module)
        {
            _previousPulses[module.GetName()] = PulseType.Low;
        }
    }

    private class UntypedModule : IModule
    {
        private string _name;
        public bool turnedOn;
        
        public UntypedModule(string name)
        {
            _name = name;
        }

        public string GetName() => _name;

        public IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType)
        {
            turnedOn = pulseType == PulseType.Low;
            return [];
        }

        public void AddTarget(IModule module)
        {
        }

        public List<(IModule, IModule, PulseType)> GetNext(IModule source, PulseType pulseType)
        {
            turnedOn = pulseType == PulseType.Low;
            return [];
        }
    }

    private enum PulseType
    {
        Low,
        High
    };

    private enum ModuleType
    {
        Button,
        Broadcaster,
        FlipFlop,
        Conjunction
    }
}
