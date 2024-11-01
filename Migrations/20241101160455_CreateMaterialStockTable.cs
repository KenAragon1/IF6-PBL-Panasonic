using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class CreateMaterialStockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialStocks_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialStocks_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEEqCTSjxNYaPkK+iDNZ8MklTAoAvC4rIgIrVWZbTkg5wUgU6LYqrn7PsjYDMiBwgRg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEL+KSv/ea4PBm8fTxPpU1s48clJjSK5n8VHYls9eEbHt9IhrZzI2TeOe8PkNjbAu/Q==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJ57EKax7s5a2ecYr7TeKWxzOIW6O/V2MZx0WY0Y9Ei4097kBzK1SjsjemvitcYxeg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENMJx8Ek7bKVx2C67oT8DoUz7U/4S6tIQSAQT9yPIYu9djF+fNp787KW+odqyZTJZA==");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStocks_AreaId",
                table: "MaterialStocks",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStocks_MaterialId",
                table: "MaterialStocks",
                column: "MaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialStocks");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEI3IARaKq7rshW0wmdj0JAJwE4gJY8beLwh/0O7mIcPJI/VNSI25DvI+V4MndZObBA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEFGqIt1VeM5/5qKcEzIlf7qRrNJ9BipvXL864dSg32rHmkCS/nm1A8B6PxyUtmK/aA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDCl9ao2uP2wiBVpsi5W0bw+cnr240Hiaqm57/VmWYnUAhg0EMr2sK3krO+V0YIyNw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGgBnQMo3wDpLmuqH8NwUlpOAX8dYH0GI8k/JwQljjeU2o0iF5V7o1NfPy4sbnWB8A==");
        }
    }
}
