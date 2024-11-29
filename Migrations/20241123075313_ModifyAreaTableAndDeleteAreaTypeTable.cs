using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class ModifyAreaTableAndDeleteAreaTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AreaTypes_AreaTypeId",
                table: "Areas");

            migrationBuilder.DropTable(
                name: "AreaTypes");

            migrationBuilder.DropIndex(
                name: "IX_Areas_AreaTypeId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_Specifier",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "Specifier",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "AreaTypeId",
                table: "Areas",
                newName: "Remark");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Remark", "Type" },
                values: new object[] { 0, "Store" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDmebvIsLhR6ZZt4vybuN6bikdNtBMV2QgdnO9v1eDQiUHqZp4ngMtQc7tsp5BaFQA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAELryv8eo6xkIYMoIV6LoNgErYV3uvct+lxCf109/lpAjXWkyHWcpfHxTfwhdM3Fj8w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEN+RBXGlLK9KgL5rmtBm3F8tQDVmDVvAT0IvmLHwo2SJiNKsPS2eLlzyW/c8Z1MYEQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEK8K90/BzOMWpOJtyJ4AkfvvKykUM4lMob2pkpgK9UPhTIbeVcMFagrHNqHRPXybIQ==");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Remark_Type",
                table: "Areas",
                columns: new[] { "Remark", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Areas_Remark_Type",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "Areas",
                newName: "AreaTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Specifier",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AreaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { 3, "Production Line", "ProductionLine" }
                });

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AreaTypeId", "Specifier" },
                values: new object[] { 1, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Areas_AreaTypeId",
                table: "Areas",
                column: "AreaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Specifier",
                table: "Areas",
                column: "Specifier",
                unique: true,
                filter: "[Specifier] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_AreaTypes_AreaTypeId",
                table: "Areas",
                column: "AreaTypeId",
                principalTable: "AreaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
