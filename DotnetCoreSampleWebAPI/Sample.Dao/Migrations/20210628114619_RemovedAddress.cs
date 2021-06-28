using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Dao.Migrations
{
    public partial class RemovedAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_person_address_addressid",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_person_addressid",
                table: "person");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_person_addressid",
                table: "person",
                column: "addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_person_address_addressid",
                table: "person",
                column: "addressid",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
