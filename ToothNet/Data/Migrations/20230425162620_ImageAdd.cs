using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToothNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PatientPhotos",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "PatientPhotos",
                newName: "Description");
        }
    }
}
