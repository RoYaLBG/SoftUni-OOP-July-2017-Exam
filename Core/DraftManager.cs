using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Minedraft.Common;

public class DraftManager
{
    public DraftManager(IHarvesterFactory harvesterFactory, 
        IProviderFactory providerFactory)
    {
        this.Harversters = new Dictionary<string, Harvester>();
        this.Providers = new Dictionary<string, Provider>();
        this.TotalStoredEnergy = 0.00;
        this.TotalMinedOre = 0.00;
        this.EnergyFactor = 1.00;
        this.OreFactor = 1.00;

        this.HarvesterFactory = harvesterFactory;
        this.ProviderFactory = providerFactory;
    }

    public DraftManager()
        : this(new HarvesterFactory(), new ProviderFactory())
    {
    }

    private Dictionary<string, Harvester> Harversters { get; }

    private Dictionary<string, Provider> Providers { get; }

    private double TotalStoredEnergy { get; set; }

    private double TotalMinedOre { get; set; }

    private double EnergyFactor { get; set; }

    private double OreFactor { get; set; }

    private IProviderFactory ProviderFactory { get; }
    private IHarvesterFactory HarvesterFactory { get; }

    public string RegisterHarvester(List<string> arguments)
    {
        return this.RegisterEntity(this.HarvesterFactory, this.Harversters, arguments, nameof(Harvester));
    }
    public string RegisterProvider(List<string> arguments)
    {
        return this.RegisterEntity(this.ProviderFactory, this.Providers, arguments, nameof(Provider));
    }
    public string Day()
    {
        var summedEnergyOutput = this.Providers.Values.Sum(p => p.EnergyOutput);
        this.TotalStoredEnergy += summedEnergyOutput;

        var summedEnergyRequirement = this.Harversters.Values.Sum(p => p.EnergyRequirement * this.EnergyFactor);
        if (summedEnergyRequirement > this.TotalStoredEnergy)
        {
            return string.Format(Messages.DaySuccess, summedEnergyOutput, 0.00);
        }

        this.TotalStoredEnergy -= summedEnergyRequirement;

        var minedOre = this.Harversters.Values.Sum(h => h.OreOutput * this.OreFactor);
        this.TotalMinedOre += minedOre;

        return string.Format(Messages.DaySuccess, summedEnergyOutput, minedOre);
    }

    public string Mode(List<string> arguments)
    {
        var type = arguments[0];
        switch (type)
        {
            case "Full":
                this.EnergyFactor = 1;
                this.OreFactor = 1;
                break;
            case "Half":
                this.EnergyFactor = 0.6;
                this.OreFactor = 0.5;
                break;
            default:
                this.EnergyFactor = 0.0;
                this.OreFactor = 0.0;
                break;
        }

        return string.Format(Messages.ModeSuccess, type);
    }
    public string Check(List<string> arguments)
    {
        var id = arguments[0];
        if (this.Harversters.ContainsKey(id))
        {
            return this.Harversters[id].ToString();
        }

        return this.Providers.ContainsKey(id) 
            ? this.Providers[id].ToString() 
            : string.Format(Messages.CheckFail, id);
    }
    public string ShutDown()
    {
        return string.Format(Messages.ShutdownSuccess, this.TotalStoredEnergy, this.TotalMinedOre);
    }

    private string RegisterEntity<T>(IFactory<T> factory, 
        IDictionary<string, T> registrar, 
        IList<string> arguments,
        string entityName
        ) where T : AbstractEntity
    {
        try
        {
            var entity = factory.Create(arguments);
            registrar[entity.Id] = entity;

            return string.Format(Messages.RegisterSuccess, arguments[0], entityName, entity.Id);
        }
        catch (ArgumentException e)
        {
            return e.Message;
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException?.Message;
        }
    }

}


