using System;
using System.Collections.Generic;
using System.Linq;

public static class GenericFactory
{
    public static T Create<T>(IEnumerable<string> args, string suffix = null)
    {
        var type = args.First();
        var objectType = Type.GetType(type + suffix);
        var construtorParams = objectType.GetConstructors().First().GetParameters();
        var nonParsedArgs = args.Skip(1).Take(construtorParams.Length).ToArray();
        var parsedArgs = new object[nonParsedArgs.Length];
        for (var i = 0; i < construtorParams.Length; i++)
        {
            var curentParam = construtorParams[i];
            var paramType = curentParam.ParameterType;
            parsedArgs[i] = Convert.ChangeType(nonParsedArgs[i], paramType);
        }

        return (T)Activator.CreateInstance(objectType, parsedArgs);
    }
}
