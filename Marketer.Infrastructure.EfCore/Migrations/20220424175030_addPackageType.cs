using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketer.Infrastructure.EfCore.Migrations
{
    public partial class addPackageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EachBoxCount",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Products",
                newName: "PackageValue");

            migrationBuilder.AddColumn<long>(
                name: "PackageTypeId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PackageTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PackageTypeId",
                table: "Products",
                column: "PackageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PackageTypes_PackageTypeId",
                table: "Products",
                column: "PackageTypeId",
                principalTable: "PackageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PackageTypes_PackageTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_PackageTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackageTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PackageValue",
                table: "Products",
                newName: "Weight");

            migrationBuilder.AddColumn<int>(
                name: "EachBoxCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
