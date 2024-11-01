using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class CreateAreaTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AreaTypes",
                columns: new[] { "Id", "Label", "Type" },
                values: new object[,]
                {
                    { 1, "Storage", "Storage" },
                    { 2, "Preperation Room", "PreperationRoom" },
                    { 3, "Production Line", "Production Line" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaTypes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENZvTtpQS9YNrjS8DrERvQwnB1UZZdCoypj+NJmh1kdPhJP2GQcSM6d4Oc2HzLQbBw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDF/hMhxzT1ZT2t1k1oSIgqqV26ULVUzGKpIwP0J1P+nNqDaKNhS84oCT5s52tpkfA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJGWWaTGM4QfYWiui2CUQDA3D6dJs+n9kDn+zdmT/9wkiI+JqFP8ClASo47snCiqXw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJ7U8tqI4k+VIUJ1aFTXYrSjRmF2ZlauCNK7VOYJbEtl1MTcerhZ8TqCcmNTAO3W2A==");
        }
    }
}
