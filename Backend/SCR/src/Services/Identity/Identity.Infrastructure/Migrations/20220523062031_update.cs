using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infrastructure.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role_Permission");

            migrationBuilder.DropTable(
                name: "User_Company");

            migrationBuilder.DropTable(
                name: "User_Role");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RolePermissionRelation",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionRelation", x => new { x.RoleId, x.PermissionId });
                });

            migrationBuilder.CreateTable(
                name: "UserCompanyRelation",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanyRelation", x => new { x.CompanyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCompanyRelation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleRelation",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleRelation", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoleRelation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyRelation_UserId",
                table: "UserCompanyRelation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleRelation_UserId",
                table: "UserRoleRelation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "RolePermissionRelation");

            migrationBuilder.DropTable(
                name: "UserCompanyRelation");

            migrationBuilder.DropTable(
                name: "UserRoleRelation");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "User");

            migrationBuilder.CreateTable(
                name: "Role_Permission",
                columns: table => new
                {
                    PermissionsPermissionId = table.Column<int>(type: "integer", nullable: false),
                    RolesRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permission", x => new { x.PermissionsPermissionId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_Role_Permission_Permisson_PermissionsPermissionId",
                        column: x => x.PermissionsPermissionId,
                        principalTable: "Permisson",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Permission_Role_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Company",
                columns: table => new
                {
                    CompaniesCompanyId = table.Column<int>(type: "integer", nullable: false),
                    UsersUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Company", x => new { x.CompaniesCompanyId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_User_Company_Company_CompaniesCompanyId",
                        column: x => x.CompaniesCompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Company_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                columns: table => new
                {
                    RolesRoleId = table.Column<int>(type: "integer", nullable: false),
                    UsersUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role", x => new { x.RolesRoleId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_User_Role_Role_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_RolesRoleId",
                table: "Role_Permission",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Company_UsersUserId",
                table: "User_Company",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_UsersUserId",
                table: "User_Role",
                column: "UsersUserId");
        }
    }
}
