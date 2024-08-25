namespace EventSagaDriven.Infrastructure.EntityFramework.InitialData;
using Domain.Entities.Genre;
using EventSagaDriven.Domain.Entities.Genre.ValueObjects;

internal static class InitialGenres
{
    private static readonly Guid ScienceFictionId = Guid.Parse("8217f508-c17d-431e-9cf0-05ca8984971b");
    private static readonly Guid FantasyId = Guid.Parse("e0007308-e1e3-4892-a5a7-883c02c6de22");
    private static readonly Guid MysteryId = Guid.Parse("c39c3b7178e94dcdbbbd35ac159f984b");
    private static readonly Guid RomanceId = Guid.Parse("3eb63894-c376-4eea-923a-ac1f3bfc6235");
    private static readonly Guid HorrorId = Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c");
    private static readonly Guid ThrillerId = Guid.Parse("9b862593-628a-4bc1-8cc4-038e01f34241");
    private static readonly Guid HistoricalFictionId = Guid.Parse("386a04f3-e4c4-4922-9e79-e75ac0fa3a6a");
    private static readonly Guid ContemporaryFictionid = Guid.Parse("102077ed-f0de-442c-8d97-fbb7dfd96d08");
    private static readonly Guid LiteraryFictionId = Guid.Parse("0de67652-5cc0-42ca-8005-aa41b3a41802");
    private static readonly Guid BiographyId = Guid.Parse("fc74ff91-3de6-4267-bce9-f390d3b0ca7c");
    private static readonly Guid AutobiographyId = Guid.Parse("7c9c3d92-40c1-4ac3-95dc-fd2916e1beda");
    private static readonly Guid HistoryId = Guid.Parse("ba6e7cff-854e-45a4-bba9-9d262e7a8e81");
    private static readonly Guid SelfHelpId = Guid.Parse("a08e0160-ac13-4eb4-b4d8-c119f06d3913");
    private static readonly Guid ScienceId = Guid.Parse("a2e70464-ad92-45e5-98a6-60d3169db78e");
    private static readonly Guid TechnologyId = Guid.Parse("96ca16f4-bdd9-49ef-b0a9-3037cfeb4e14");
    private static readonly Guid BusinessId = Guid.Parse("3d315b2a-a0f3-4f02-b7a6-f320359a256a");
    private static readonly Guid TravelId = Guid.Parse("254d662e-2c55-49bf-a5b9-1d21b1892ce3");
    private static readonly Guid CookingId = Guid.Parse("a9b598b0-ca8a-400f-aa97-a258f5e39135");
    private static readonly Guid ArtId = Guid.Parse("dc0a9d65-aa58-46a3-8e05-d44779563ad9");

    internal static IEnumerable<Genre> GetGenres()
    {
        return new List<Genre>
        {
            Genre.Build(ScienceFictionId, Name.Create("Science Fiction"), Description.Create("Imaginative stories involving advanced technology or space exploration")),
            Genre.Build(FantasyId, Name.Create("Fantasy"), Description.Create("Stories featuring magical elements and often set in imaginary worlds")),
            Genre.Build(MysteryId, Name.Create("Mystery"), Description.Create("Stories centered around solving a crime or unraveling a secret")),
            Genre.Build(RomanceId, Name.Create("Romance"), Description.Create("Stories focused on love and relationships")),
            Genre.Build(HorrorId, Name.Create("Horror"), Description.Create("Stories intended to frighten or disgust")),
            Genre.Build(ThrillerId, Name.Create("Thriller"), Description.Create("Suspenseful stories often involving crime or danger")),
            Genre.Build(HistoricalFictionId , Name.Create("Historical Fiction"), Description.Create("Fictional stories set in the past")),
            Genre.Build(ContemporaryFictionid, Name.Create("Contemporary Fiction"), Description.Create("Stories set in the present day")),
            Genre.Build(LiteraryFictionId, Name.Create("Literary Fiction"), Description.Create("Character-driven stories with complex themes")),
            Genre.Build(BiographyId , Name.Create("Biography"), Description.Create("The story of a person's life written by someone else")),
            Genre.Build(AutobiographyId, Name.Create("Autobiography"), Description.Create("The story of a person's life written by themselves")),
            Genre.Build(HistoryId, Name.Create("History"), Description.Create("Factual accounts of past events")),
            Genre.Build(SelfHelpId, Name.Create("SelfHelp"), Description.Create("Books offering advice or guidance on various life topics")),
            Genre.Build(ScienceId , Name.Create("Science"), Description.Create("Books explaining scientific concepts and theories")),
            Genre.Build(TechnologyId , Name.Create("Technology"), Description.Create("Books about technological advancements and applications")),
            Genre.Build(BusinessId, Name.Create("Business"), Description.Create("Books on business strategies, management, and economics")),
            Genre.Build(TravelId , Name.Create("Travel"), Description.Create("Books about exploring different places")),
            Genre.Build(CookingId, Name.Create("Cooking"), Description.Create("Books with recipes and culinary information")),
            Genre.Build(ArtId, Name.Create("Art"), Description.Create("Books about visual arts, such as painting, sculpture, and photography"))
        };
    }
}
