using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelter.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Breed = table.Column<string>(maxLength: 150, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Sex = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Breed = table.Column<string>(maxLength: 150, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Sex = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "Breed", "Name", "Sex" },
                values: new object[,]
                {
                    { 1, 6, "American Shorthair", "Alphred", "Female" },
                    { 2, 7, "Bombay", "Unfried", "Male" },
                    { 3, 8, "Cyprus", "Hans", "Female" },
                    { 4, 9, "Devon Rex", "Gilbert", "Male" },
                    { 5, 10, "Egyptian Mau", "Heathrow", "Female" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Age", "Breed", "Name", "Sex" },
                values: new object[,]
                {
                    { 1, 6, "Akita", "Higgins", "Male" },
                    { 2, 7, "Belgian Shepherd", "Basil", "Female" },
                    { 3, 8, "Chilean Terrier", "Oliver", "Male" },
                    { 4, 9, "Dingo", "Hammond", "Female" },
                    { 5, 10, "Estonian Hound", "Sigfried", "Male" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "Dogs");
        }
    }
}
