using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitColumnToMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHVXFgBzTtneKOha6XigIzHZrX+g2D+m4yTWjtZTmJbUmMN7uhtDua7ifbMAHwYKjg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBOC9M1/FynO5S3QP9sTyzufLiMxmVxEri3WYEuBcwg3TylxH39EaFXWMXLMsfHTKg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEE875wxQJ6bO92chPZie/AafQYW0c4XucYQa58PhFStUtg/MK+IqGebiTl/A64W1kA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEKdqzOcqM8C7OB93TUiiIfjfPLUggyw1ljmki2S9glHlFhosy2ZLKvxB0ZGxR9xlug==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Materials");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEEYRtuNNVTXSGVDtL22cBGXUdmwBYidsvzS5y4kvTLUZ3l1tLGHsi8L3UOlNspj4og==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENDKAdted6uSrYraKBnNylqq6OA/kWFkH3IXrmA/LEgncTtEW9jOYjcacQIDm4hxqw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBAz8XeHBQuk5IX+YQJGwRfvB944fcLJW5dIXrAwguQ5EtgSrJ+dwEfhLugnsphD9A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENI4CzClvpkkT1zzKLgbsYpFoYLnu8x7OGvowQPZdFIzHTZXrwEtkN+p5uVdJEKvDQ==");
        }
    }
}
