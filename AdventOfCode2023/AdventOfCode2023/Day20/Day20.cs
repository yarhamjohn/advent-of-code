namespace AdventOfCode2023.Day20;

public static class Day20
{
    public static long SumPulses(string[] input)
    {
        var broadcasterModule = CreateModules(input);

        var buttonModule = new ButtonModule();

        var pulses = new Queue<Pulse>();
        for (var i = 0; i < 1000; i++)
        {
            PressButton(buttonModule, broadcasterModule, pulses);
        }

        return pulses.Count(p => p.Type == PulseType.Low) * pulses.Count(p => p.Type == PulseType.High);
    }

    private static BroadcasterModule CreateModules(string[] input)
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

        return (BroadcasterModule)modules.Single(x => x is BroadcasterModule);
    }

    private static void PressButton(
        ButtonModule buttonModule,
        BroadcasterModule broadcasterModule,
        Queue<Pulse> pulses)
    {
        pulses.Enqueue(new Pulse(buttonModule, broadcasterModule, PulseType.Low));

        foreach (var pulse in broadcasterModule.HandlePulse(buttonModule, PulseType.Low))
        {
            pulses.Enqueue(pulse);
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

        public void AddSource(IModule module)
        {
            _previousPulses[module.GetName()] = PulseType.Low;
        }
    }

    private class UntypedModule : IModule
    {
        private string _name;
        
        public UntypedModule(string name)
        {
            _name = name;
        }

        public string GetName() => _name;

        public IEnumerable<Pulse> HandlePulse(IModule source, PulseType pulseType)
        {
            return [];
        }

        public void AddTarget(IModule module)
        {
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
