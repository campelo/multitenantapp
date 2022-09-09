using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenant.Repository.Migrations
{
    public partial class addinghierarchicaltenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_TenantId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tenants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Tenants",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentTenantId",
                table: "Tenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentTenantKey",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantKey",
                table: "Locations",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tenants_Code",
                table: "Tenants",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Code",
                table: "Tenants",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ParentTenantId",
                table: "Tenants",
                column: "ParentTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TenantKey",
                table: "Locations",
                column: "TenantKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Tenants_ParentTenantId",
                table: "Tenants",
                column: "ParentTenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Tenants_ParentTenantId",
                table: "Tenants");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tenants_Code",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_Code",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_ParentTenantId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Locations_TenantKey",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ParentTenantId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ParentTenantKey",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "TenantKey",
                table: "Locations");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Locations",
                type: "varchar(12)",
                unicode: false,
                maxLength: 12,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TenantId",
                table: "Locations",
                column: "TenantId");
        }
    }
}
