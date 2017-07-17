using Minedraft.Common;

public class HammerHarvester : Harvester
{
    public HammerHarvester(string id, 
        double oreOutput, 
        double energyRequirement) : base(id, oreOutput, energyRequirement)
    {
        this.OreOutput = oreOutput + oreOutput * Constants.HammerOreFactor;
        this.EnergyRequirement = energyRequirement + energyRequirement * Constants.HammerEnergyFactor;
    }

}