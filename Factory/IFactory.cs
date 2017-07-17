using System.Collections.Generic;

public interface IFactory<out T> where T : AbstractEntity
{
    T Create(IEnumerable<string> args);
}