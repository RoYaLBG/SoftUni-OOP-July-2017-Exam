using System;
using Minedraft.Common;

public class Provider : AbstractEntity
{
    private double energyOutput;

    public Provider(string id, double energyOutput)
        : base(id)
    {
        this.EnergyOutput = energyOutput;
    }

    public double EnergyOutput
    {
        get { return this.energyOutput; }
        protected set
        {
            if (value < 0 || value > Constants.MaxProviderEnergy)
            {
                throw new ArgumentException(string.Format(Messages.PropertyViolation, nameof(Provider), nameof(EnergyOutput)));
            }

            this.energyOutput = value;
        }
    }

    public override string ToString()
    {
        return $"{this.GetType().ToString().Replace(nameof(Provider), "")} {nameof(Provider)} - {this.Id}\r\nEnergy Output: {this.EnergyOutput}";
    }
}