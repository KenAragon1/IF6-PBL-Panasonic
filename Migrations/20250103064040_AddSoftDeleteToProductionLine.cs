using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToProductionLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductionLines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEKdFiUwPOGg9qOZsbDhf1SU/Lw5wlxwtVz0R+U1JDiCVRBWVodG9Wc72hcVgyIDXrA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEOd3PBL5s++yMkYEqOxGFp2Q9kGnl6Uog3E9mTvTrQOu2nG2ctC21ZZtKny0xlZFVg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHnOBAxo5/rZp+kHdHhs/rUobGKrL3NpRGWFLeZ8t2fFxl3fK/frW8/G8T1g3CfBgw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBLX2QYj+NzOZ6F4eA+henXg+c88MtCVaEShKIaKjZY0IjjS/sXXTxwTWm8hNx8PKQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductionLines");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHpohhG05RCc0tIq3eABWX4D2Y5I5l2g583icsZSBFCnjylTVA2CLwqTVl8CJy+K+Q==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEAZ59CYSrjhHgaQ8ZXjXd+euMvP6w2xnuwBAhi/DAF14yLmPXHGC1SfXh5HPUwxbLw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBLmL9ADK+4Dz7F6Gr2rm8OHxaE8394lcFyK3bYRAC2bw3HUQH0csNn6lszVSZCnYg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEH+bIwc41HjXlokKPNGm9cSSzolF9aZYKgBDnrwz5OIQHYYE86j+E0cHkua2nSt/hg==");
        }
    }
}
