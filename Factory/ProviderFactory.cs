using System.Collections.Generic;

public class ProviderFactory : IProviderFactory
{
    public Provider Create(IEnumerable<string> args)
    {
        return GenericFactory.Create<Provider>(args, nameof(Provider));
    }
}