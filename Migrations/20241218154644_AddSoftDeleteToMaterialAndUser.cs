using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToMaterialAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HashedPassword", "IsDeleted" },
                values: new object[] { "AQAAAAIAAYagAAAAEJht/xBO7emsecjK9YpeQlzyJ1iSNZHEd3neYVKyfasOq+aYSEH6tq7FZyqSF3JgIQ==", false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HashedPassword", "IsDeleted" },
                values: new object[] { "AQAAAAIAAYagAAAAEPNq87+P3d0ZAOu94ViMaDaCqWqK7eEuXDSUSJiTQ/5ZudwVxR4ZK4TcPhwqj01g/A==", false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HashedPassword", "IsDeleted" },
                values: new object[] { "AQAAAAIAAYagAAAAEMmgWFx2pQmP6r1sTo0a5msSNnNyKuceAwEkeME2YlPdMCMdaGgFf9yfL06vdYdx5A==", false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HashedPassword", "IsDeleted" },
                values: new object[] { "AQAAAAIAAYagAAAAEMAx+EdA32TXxgcjKoigrA8MjOWIdfJ+HuzOXc5IleJcqBrfYRkLyizeS7KVr03uKg==", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

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
    }
}
