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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
