public abstract class AbstractEntity
{
    protected AbstractEntity(string id)
    {
        this.Id = id;
    }

    public string Id { get; protected set; }

}
