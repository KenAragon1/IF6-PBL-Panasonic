using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJC3Q7++iLw3ESXrJlw8Eob/o1HjrN05NlIe+baxYj/hfPLXhJCgrWKGnOrJuDGcUA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGO9R/V4KIaddziNdoy0BhhRYmIuf0B1dXRowZfSqlgIWhILVdq34Nf/1eC6rWGl+w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDWGuGTQtb1RnCThp52Kc8PP9VRBn+TKb6grMJYjkPWMjmeJ7WCvV2LVeCwsKMAqfg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEO4lNZ8QBSD33wPxgr/kLXy9rDU7nLDLFcNAiv02mrRflKU31Lv30eqCttS5SboZWg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJ3FC2c74C+Xhqx/qlBpZFSFDRdZTjoSzgQxeOLaxp7XFsBO+T8LpvHOOIxkygh/mA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPXcmBoEfcJkny1YX+9IaQsLJohC5dqOImo84zoLdZBrMZfw7s6X8GbGrRWnsQ/WYA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBCVxgxxdSRwO47D6lx4XalJgDkgbGihLdEgjRNdBdQ9pcMv72sw9/6F6amvoQImDA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAELVIGutIdZqPsVdt5/IVOe+vWo8l+FRkX0gQ4vwxW9W2LkpWAgcksFgDfZTLLlq2hg==");
        }
    }
}
