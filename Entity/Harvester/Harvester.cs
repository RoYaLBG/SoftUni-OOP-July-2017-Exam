using System;
using Minedraft.Common;

public abstract class Harvester : AbstractEntity
{
    private double oreOutput;
    private double energyRequirement;

    protected Harvester(string id, double oreOutput, double energyRequirement) 
        : base(id)
    {
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
    }

    public double OreOutput
    {
        get { return this.oreOutput; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format(Messages.PropertyViolation, nameof(Harvester), nameof(OreOutput)));
            }

            this.oreOutput = value;
        }
    }

    public double EnergyRequirement
    {
        get { return this.energyRequirement; }
        protected set
        {
            if (value < 0 || value > Constants.MaxHarvesterEnergy)
            {
                throw new ArgumentException(string.Format(Messages.PropertyViolation, nameof(Harvester), nameof(EnergyRequirement)));
            }

            this.energyRequirement = value;
        }
    }

    public override string ToString()
    {
        return $"{this.GetType().ToString().Replace(nameof(Harvester), "")} {nameof(Harvester)} - {this.Id}\r\nOre Output: {this.OreOutput}\r\nEnergy Requirement: {this.EnergyRequirement}";
    }
}
