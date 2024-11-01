using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class FixAreaTypeInAreaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEIsOteUhmFUTTnKUToXbE3nZmPow1BI2uVWhgKGVrTitABEvmy25Lf6jatsfSuZ/Bg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEI07YOrrkNd6VZ1Hc01f3lTqdvZh4f2xHgIDyhqMNXyOUm5B1U/2m2ywldimQHI7Wg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAECO1EqB84HL4AmrPmBUi3hDmFKFZdkqdvdfPoFFIw/qPXE2E1hfG4WmhDUJ1nfsX8w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBXz/vpTLd6k7vOabt21ZNYS+NSKO5H4p3GT9EE5gkBjGgp8/pBVOBIldQRo1b8CNw==");

            migrationBuilder.CreateIndex(
                name: "IX_Area_AreaTypeId",
                table: "Area",
                column: "AreaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_AreaTypes_AreaTypeId",
                table: "Area",
                column: "AreaTypeId",
                principalTable: "AreaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_AreaTypes_AreaTypeId",
                table: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Area_AreaTypeId",
                table: "Area");

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
        }
    }
}
