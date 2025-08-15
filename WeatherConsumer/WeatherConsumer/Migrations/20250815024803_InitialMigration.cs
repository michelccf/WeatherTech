using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherConsumer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currentWeatherUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    interval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    temperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    windspeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    winddirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weathercode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currentWeatherUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "curretWeather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    interval = table.Column<int>(type: "int", nullable: false),
                    temperature = table.Column<double>(type: "float", nullable: false),
                    windspeed = table.Column<double>(type: "float", nullable: false),
                    winddirection = table.Column<int>(type: "int", nullable: false),
                    is_day = table.Column<int>(type: "int", nullable: false),
                    weathercode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curretWeather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    generationtime_ms = table.Column<double>(type: "float", nullable: false),
                    utc_offset_seconds = table.Column<int>(type: "int", nullable: false),
                    timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timezone_abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    elevation = table.Column<double>(type: "float", nullable: false),
                    current_weather_unitsId = table.Column<int>(type: "int", nullable: false),
                    current_weatherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_weather_currentWeatherUnits_current_weather_unitsId",
                        column: x => x.current_weather_unitsId,
                        principalTable: "currentWeatherUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_weather_curretWeather_current_weatherId",
                        column: x => x.current_weatherId,
                        principalTable: "curretWeather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_weather_current_weather_unitsId",
                table: "weather",
                column: "current_weather_unitsId");

            migrationBuilder.CreateIndex(
                name: "IX_weather_current_weatherId",
                table: "weather",
                column: "current_weatherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weather");

            migrationBuilder.DropTable(
                name: "currentWeatherUnits");

            migrationBuilder.DropTable(
                name: "curretWeather");
        }
    }
}
