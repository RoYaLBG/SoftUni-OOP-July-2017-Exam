using System.Collections.Generic;

public class HarvesterFactory : IHarvesterFactory
{
    public Harvester Create(IEnumerable<string> args)
    {
        return GenericFactory.Create<Harvester>(args, nameof(Harvester));
    }
}
