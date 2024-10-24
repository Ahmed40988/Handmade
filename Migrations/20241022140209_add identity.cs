using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_User_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_User_ID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_User_ID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Signups_Roles_RoleId",
                table: "Signups");

            migrationBuilder.DropForeignKey(
                name: "FK_suppliers_Roles_RoleId",
                table: "suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_suppliers_Users_ID",
                table: "suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "_Users");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "_Roles");

            migrationBuilder.RenameTable(
                name: "Logins",
                newName: "_Logins");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "_Users",
                newName: "IX__Users_RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Users",
                table: "_Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Roles",
                table: "_Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Logins",
                table: "_Logins",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Users__Roles_RoleId",
                table: "_Users",
                column: "RoleId",
                principalTable: "_Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders__Users_User_ID",
                table: "Orders",
                column: "User_ID",
                principalTable: "_Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products__Users_User_ID",
                table: "Products",
                column: "User_ID",
                principalTable: "_Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews__Users_User_ID",
                table: "Reviews",
                column: "User_ID",
                principalTable: "_Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Signups__Roles_RoleId",
                table: "Signups",
                column: "RoleId",
                principalTable: "_Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_suppliers__Roles_RoleId",
                table: "suppliers",
                column: "RoleId",
                principalTable: "_Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_suppliers__Users_ID",
                table: "suppliers",
                column: "ID",
                principalTable: "_Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Users__Roles_RoleId",
                table: "_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders__Users_User_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products__Users_User_ID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews__Users_User_ID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Signups__Roles_RoleId",
                table: "Signups");

            migrationBuilder.DropForeignKey(
                name: "FK_suppliers__Roles_RoleId",
                table: "suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_suppliers__Users_ID",
                table: "suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Users",
                table: "_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Roles",
                table: "_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Logins",
                table: "_Logins");

            migrationBuilder.RenameTable(
                name: "_Users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "_Roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "_Logins",
                newName: "Logins");

            migrationBuilder.RenameIndex(
                name: "IX__Users_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "suppliers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins",
                table: "Logins",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_User_ID",
                table: "Orders",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_User_ID",
                table: "Products",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_User_ID",
                table: "Reviews",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Signups_Roles_RoleId",
                table: "Signups",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_suppliers_Roles_RoleId",
                table: "suppliers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_suppliers_Users_ID",
                table: "suppliers",
                column: "ID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
