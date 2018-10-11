using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace scrimp.Migrations
{
    public partial class AddHttpRequestMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HttpRequestId",
                table: "Errors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HttpRequestMeta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    PathBase = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpRequestMeta", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Errors_HttpRequestId",
                table: "Errors",
                column: "HttpRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Errors_HttpRequestMeta_HttpRequestId",
                table: "Errors",
                column: "HttpRequestId",
                principalTable: "HttpRequestMeta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errors_HttpRequestMeta_HttpRequestId",
                table: "Errors");

            migrationBuilder.DropTable(
                name: "HttpRequestMeta");

            migrationBuilder.DropIndex(
                name: "IX_Errors_HttpRequestId",
                table: "Errors");

            migrationBuilder.DropColumn(
                name: "HttpRequestId",
                table: "Errors");
        }
    }
}
