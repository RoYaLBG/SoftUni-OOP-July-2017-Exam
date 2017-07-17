using System;
using System.Linq;

namespace Minedraft
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Common;

    class MindeDraft
    {
        static void Main(string[] args)
        {
            var reader = InitializeInputReader(args);
            var writer = InitializeOutputWriter(args);

            var line = reader.ReadLine();
            var harvesterFactory = new HarvesterFactory();
            var providerFactory = new ProviderFactory();
            var dm = new DraftManager(harvesterFactory, providerFactory);

            while (line != Constants.EndOfInput)
            {
                var tokens = line?.Split();
                var methodName = tokens?[0];
                var method = dm.GetType().GetMethod(methodName??Constants.EndOfInput, BindingFlags.Public | BindingFlags.Instance);
                var result = (string)method.Invoke(
                    dm, 
                    method.GetParameters().Length == 0 
                    ? new object [] {} 
                    : new object [] { tokens?.Skip(1).ToList() }
                );
                writer.WriteLine(result);

                line = reader.ReadLine();
            }

            writer.WriteLine(dm.ShutDown());
        }

        private static TextReader InitializeInputReader(IReadOnlyList<string> args)
        {
            return args.Count > 0
                ? (TextReader)Activator.CreateInstance(Type.GetType(args[0]))
                : Console.In;
        }

        private static TextWriter InitializeOutputWriter(IReadOnlyList<string> args)
        {
            return args.Count > 1
                ? (TextWriter)Activator.CreateInstance(Type.GetType(args[1]))
                : Console.Out;
        }
    }
}
