using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gpuviewer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GraphicsCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Manufacturer = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Architecture = table.Column<string>(type: "text", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VramGb = table.Column<double>(type: "double precision", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    MemoryBusWidth = table.Column<int>(type: "integer", nullable: false),
                    CoreClockMhz = table.Column<double>(type: "double precision", nullable: false),
                    BoostClockMhz = table.Column<double>(type: "double precision", nullable: false),
                    TdpWatts = table.Column<int>(type: "integer", nullable: false),
                    ShaderUnits = table.Column<int>(type: "integer", nullable: true),
                    RtCores = table.Column<int>(type: "integer", nullable: true),
                    TensorCores = table.Column<int>(type: "integer", nullable: true),
                    BenchmarkScore = table.Column<double>(type: "double precision", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GraphicsCards");
        }
    }
}
