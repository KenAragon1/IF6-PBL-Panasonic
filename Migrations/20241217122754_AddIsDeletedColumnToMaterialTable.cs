using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedColumnToMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Materials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAECZqTnlDkPefRFEgPsy1Fl+iRXbAWgb/LWA6heWrX1gbCVjBMWzPXorl9Yua44TNrQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENCau6nFPltbAdJooQQQpp01EjONBcVyuNywv8gjwNuhDMtaM6CYOTOkcpFkeHLskg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEG9YsqPsPW46w73FdQdCYOk0TBfeaWjnjvBxZ66DFyOqe4xnjePuxEYXQ5ygfkTVtA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPhmDBfCeBlrttqriKZSpf09+yz80U/4m3tAyFATTjohOeV0p8eLI1eIryjFo/M6wA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Materials");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEIlkukCWp7sLPZdO5qyMjqZSVjW6oD7CJcY96/Lxo47mEXQEqu2RVdV2y/HL/4nTVA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEN/AOLx/0X6O4TGXpaHxBVX2HDhX9ntmbmChI5jRxeXqUIXg+JnRuMkajzqo07EGWw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEG7UJdqXqCbcrKVuykGJVNTVyj9SuS/6Jrln1c6gEydbidU8uB52Pqxu9ZL2EfdWkw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEEidQTbqXWKzn3wU817I0GdE5unRxlhfB0ndqXxlH9+6iSVTkpPgcMC762ZsFQQcMw==");
        }
    }
}
