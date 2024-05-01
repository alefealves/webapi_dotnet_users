using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UsersManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    sobrenome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    senha_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    senha_salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    nivel_acesso = table.Column<int>(type: "integer", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_user_update = table.Column<int>(type: "integer", nullable: false),
                    data_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "ativo", "data_create", "email", "id_user_update", "nivel_acesso", "nome", "senha_hash", "senha_salt", "sobrenome", "data_update" },
                values: new object[] { 1, true, new DateTime(2024, 5, 1, 17, 17, 30, 577, DateTimeKind.Utc).AddTicks(4270), "admin@admin.com", 1, 1, "admin", new byte[] { 95, 116, 214, 215, 114, 121, 76, 169, 235, 162, 221, 181, 163, 145, 253, 0, 35, 61, 252, 249, 210, 118, 227, 125, 123, 128, 49, 27, 59, 196, 6, 51, 6, 243, 173, 145, 220, 107, 210, 76, 255, 195, 50, 106, 217, 120, 8, 172, 176, 126, 63, 153, 105, 51, 75, 51, 119, 97, 34, 75, 181, 129, 104, 128 }, new byte[] { 114, 105, 202, 182, 145, 44, 164, 110, 219, 89, 93, 25, 18, 34, 135, 85, 90, 3, 165, 144, 30, 51, 179, 70, 13, 169, 156, 73, 78, 228, 101, 85, 244, 25, 115, 118, 150, 81, 174, 196, 227, 162, 128, 70, 124, 9, 122, 125, 57, 189, 7, 199, 18, 161, 9, 135, 79, 17, 17, 145, 199, 19, 147, 226, 89, 56, 122, 10, 229, 37, 31, 72, 191, 223, 123, 130, 105, 122, 128, 162, 125, 234, 45, 179, 16, 89, 238, 225, 35, 32, 81, 216, 223, 191, 203, 177, 84, 12, 195, 123, 180, 163, 140, 1, 118, 197, 186, 27, 115, 209, 157, 175, 128, 251, 56, 9, 235, 108, 126, 31, 136, 119, 159, 130, 111, 127, 84, 0 }, "admin", new DateTime(2024, 5, 1, 17, 17, 30, 577, DateTimeKind.Utc).AddTicks(4270) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
