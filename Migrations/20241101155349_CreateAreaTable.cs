using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class CreateAreaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specifier = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AreaTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "AreaTypeId", "Specifier" },
                values: new object[] { 1, 1, null });

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "ProductionLine");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAELNSlv0qgE+vO4XUgnt2qC0LX2Kt1uoOTbdt0nomh75SCTtZ3RXke7Kf58RiivMypg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAELRyA64K8IkdFATPKmKWerJmn4xa4n3LjWCxxpT5B/UkpsKbrsg/ajJ9GXDZPmtm0A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEINFf6xRwGsInibgljSXfLp8qv1ykObJcUuRbHAolF4w60Adz8+1YEg0vf6YD+Orbg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEM3RcLCc5asQZRwlJv8VzqtYVwMXpzzfxegHeFGt2avteQ26mDIjKihZeuuO8aXLxA==");

            migrationBuilder.CreateIndex(
                name: "IX_Area_Specifier",
                table: "Area",
                column: "Specifier",
                unique: true,
                filter: "[Specifier] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "Production Line");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPSPZYA33nPABys1cIqk5eIjhrKaq2iOMOov45OOWdrps0y/GiyanGRYn7dtm3+daA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEL3SjzhR/SRlBalqaQQ7kFK/e/aKTtntxLkii1Vtaso6XgEs+3xW34lUMizgk0PrUA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPwbpaXsSzumzoNstuELzg9fmvkJWcmqDZM9RtHDuhjiN92G9q2lWlJi+ELxhYCmyw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAED4aZ20LgOdi/AgXO4NXXoAUM715tLmDj4BCbd50TKM4ENAYrD0vp4Gz1mYd1WU23w==");
        }
    }
}
