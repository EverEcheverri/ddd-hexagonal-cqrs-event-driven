using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventSagaDriven.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialsqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Mobile = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    AccountType = table.Column<int>(type: "INTEGER", nullable: false),
                    CityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountGenres",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GenreId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountGenres", x => new { x.AccountId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AccountGenres_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0de67652-5cc0-42ca-8005-aa41b3a41802"), "Character-driven stories with complex themes", "Literary Fiction" },
                    { new Guid("102077ed-f0de-442c-8d97-fbb7dfd96d08"), "Stories set in the present day", "Contemporary Fiction" },
                    { new Guid("254d662e-2c55-49bf-a5b9-1d21b1892ce3"), "Books about exploring different places", "Travel" },
                    { new Guid("386a04f3-e4c4-4922-9e79-e75ac0fa3a6a"), "Fictional stories set in the past", "Historical Fiction" },
                    { new Guid("3d315b2a-a0f3-4f02-b7a6-f320359a256a"), "Books on business strategies, management, and economics", "Business" },
                    { new Guid("3eb63894-c376-4eea-923a-ac1f3bfc6235"), "Stories focused on love and relationships", "Romance" },
                    { new Guid("5ebf0600-c390-4b16-945d-eb0e734cf51c"), "Stories intended to frighten or disgust", "Horror" },
                    { new Guid("7c9c3d92-40c1-4ac3-95dc-fd2916e1beda"), "The story of a person's life written by themselves", "Autobiography" },
                    { new Guid("8217f508-c17d-431e-9cf0-05ca8984971b"), "Imaginative stories involving advanced technology or space exploration", "Science Fiction" },
                    { new Guid("96ca16f4-bdd9-49ef-b0a9-3037cfeb4e14"), "Books about technological advancements and applications", "Technology" },
                    { new Guid("9b862593-628a-4bc1-8cc4-038e01f34241"), "Suspenseful stories often involving crime or danger", "Thriller" },
                    { new Guid("a08e0160-ac13-4eb4-b4d8-c119f06d3913"), "Books offering advice or guidance on various life topics", "SelfHelp" },
                    { new Guid("a2e70464-ad92-45e5-98a6-60d3169db78e"), "Books explaining scientific concepts and theories", "Science" },
                    { new Guid("a9b598b0-ca8a-400f-aa97-a258f5e39135"), "Books with recipes and culinary information", "Cooking" },
                    { new Guid("ba6e7cff-854e-45a4-bba9-9d262e7a8e81"), "Factual accounts of past events", "History" },
                    { new Guid("c39c3b71-78e9-4dcd-bbbd-35ac159f984b"), "Stories centered around solving a crime or unraveling a secret", "Mystery" },
                    { new Guid("dc0a9d65-aa58-46a3-8e05-d44779563ad9"), "Books about visual arts, such as painting, sculpture, and photography", "Art" },
                    { new Guid("e0007308-e1e3-4892-a5a7-883c02c6de22"), "Stories featuring magical elements and often set in imaginary worlds", "Fantasy" },
                    { new Guid("fc74ff91-3de6-4267-bce9-f390d3b0ca7c"), "The story of a person's life written by someone else", "Biography" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountGenres_GenreId",
                table: "AccountGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountGenres");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
