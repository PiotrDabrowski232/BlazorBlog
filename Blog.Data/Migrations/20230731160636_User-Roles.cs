using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000001-0000-0000-0000-000000000000"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersposts",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPost = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    postsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersposts", x => new { x.IdUser, x.IdPost });
                    table.ForeignKey(
                        name: "FK_usersposts_post_postsId",
                        column: x => x.postsId,
                        principalTable: "post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usersposts_users_usersId",
                        column: x => x.usersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "BasicUser" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "Admin" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "SuperUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_usersposts_postsId",
                table: "usersposts",
                column: "postsId");

            migrationBuilder.CreateIndex(
                name: "IX_usersposts_usersId",
                table: "usersposts",
                column: "usersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersposts");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
