namespace EventSagaDriven.Infrastructure.Models.AppSettings;

public class Databases
{
    internal const string DatabasesName = "Databases";
    public bool Cosmos { get; set; }
    public bool Sqlite { get; set; }
}
