using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddExpDateToAreaMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpirationDate",
                table: "AreaMaterials",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEOtP+4mH3HOMm/Swi8Zp6YtUsJyk8ivunN887olkV6IpeFo9PgpYvZ1U9TQKO8EWoA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGbMLuMd9JC8YfEyzl2soeNvfwNv85a6UAdFeQFJnpAl8CShvmxNd16LfQv+BWH6bQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAED7CFdfU+RElPoZfQrW1dUTteEC22lKDx75OMxsfa4JuIWQazC7NmagaoEcbW71C1w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEN2PTaGFIPPWhwdDUxIr8JOU2ppe9ajRLjkq2UdYhuRMhQk9+ikBh6wTIUXCY7O+1w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "AreaMaterials");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBXQbqCJzcIdnH7e+6VP411fPof2pKGReSsnl4kv3aXwZontOOVEcD7PC4ppXsr5FQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBRMMVc88TZ4gT1xL8JkOFr7gD+4QC6rkXGgxtmr7ERn5C/2CkAOw3g0iY1vbebLsA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBO0PGQ+cv+IM+pCpEQNzIVHsDTVb1yKmRD6j3n6yyF8Mkixmb8rnWS8QkJ+nz9X+A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGkYtcBiRWGOre3X/chN7sKrSWiv9LUKqmlvXGmpScrLUy/JTqrt58J88Thmi4Ty8w==");
        }
    }
}
