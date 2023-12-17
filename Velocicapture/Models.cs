namespace Velocicapture;

public class Sprint() 
{
    public string Name { get; set; }
    public int Points { get; set; }
    public decimal DevDays { get; set; }
};

public record SprintStats(Sprint Sprint, decimal DevDaysPerSprint)
{
    public decimal PointsPerDevDay => Sprint.Points / Sprint.DevDays;
    public decimal PotentialPoints => Math.Round(PointsPerDevDay * DevDaysPerSprint);
}

public record SprintPlan(DevPlan[] DevPlans);

public record DevPlan(string DevName) {
    public int DevDays { get; set; }
}

public class Team
{
    public string Name { get; set; } = "";
    public int SprintDays { get; set;} = 8;
    public List<TeamMember> Members { get; set; } = new ();
    
    public int SprintDevDays => SprintDays * Members.Count;

    public SprintPlan NewPlan() => new (Members
        .Select(x =>new DevPlan(x.Name) {DevDays = SprintDays})
        .ToArray());
}

public class TeamMember
{
    public TeamMember(string name) => Name = name;
    public string Name { get; set; }
};
