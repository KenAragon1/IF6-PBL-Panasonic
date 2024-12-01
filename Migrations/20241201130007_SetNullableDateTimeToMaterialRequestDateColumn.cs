using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class SetNullableDateTimeToMaterialRequestDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Guest",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "ShiftLeader");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VerifiedAt",
                table: "MaterialRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedAt",
                table: "MaterialRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEKNf7WVC1/iSigaKyEBA0Rxg2MxWPuJc9OcR6NtLSXbQ2VrwyeKtGlOuLvubKfV4ww==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJN79dFQ9aNUoCrU7dngwEyphBTJ2fFuDlrHnkOQn6FgkBOHEcP74IQLeBUI+YN3PQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHSAi69r7CwrWpYLOF2nHSa4D79GHmnEyRj0wzrPGuklVHaJoYPsmIJJTplc5omfMA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPFpZkoB3EX8UZxBY+Q5HsbGv+bfRsVV945h0+juF7m5s1uLoaeInS6Tk4Dt9RDWFQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "ShiftLeader",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Guest");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VerifiedAt",
                table: "MaterialRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedAt",
                table: "MaterialRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEMQ08kcbDEEDxKH6pqsAW7f3l8RISHam4K5jY4SKzS5CcziK3DMeKYpZNOputm5qSQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPqjgvN/WJZk7oXjan7JMGEqUWbODvjof0Z5M1DUE3gaCMJXpNLTZabIDCp8d3i8Uw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEOARSloRmwjd4u7iAEB7bpwuNkDUvu8o8te6LwgDp/Hk0zMw58toKr/Z2D961Zehtg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEP1DlQMubeBSeQjqcxTnXd3fITsJer9RNcCkTFCwWPYdVaTPVm24OkN+ytU9/mDqSQ==");
        }
    }
}
