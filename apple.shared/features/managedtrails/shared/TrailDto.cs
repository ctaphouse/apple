namespace apple.shared.features.managetrails.shared;

public class TrailDto {
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Location { get; set; } = default!;
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }
    public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();

    public class RouteInstruction {
        public int Stage { get; set; }
        public string Description { get; set; } = default!;
    }
}

