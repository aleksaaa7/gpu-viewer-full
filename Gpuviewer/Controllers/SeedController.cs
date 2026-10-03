using Microsoft.AspNetCore.Mvc;
using Gpuviewer.Data;
using Gpuviewer.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Globalization;

namespace Gpuviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportGpus()
        {
            var existingCount = await _context.GraphicsCards.CountAsync();
            if (existingCount > 0)
            {
                return Ok(new { message = $"Database have {existingCount} cards. Didnt imported." });
            }

            var files = new[] { "nvidia.json", "amd.json", "intel.json" };
            int totalImported = 0;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            foreach (var fileName in files)
            {
                var path = Path.Combine(_env.ContentRootPath, "SeedData", fileName);

                if (!System.IO.File.Exists(path))
                    continue;

                var json = await System.IO.File.ReadAllTextAsync(path);
                var gpuList = JsonSerializer.Deserialize<List<GpuJsonDto>>(json, options);

                if (gpuList == null)
                    continue;

                foreach (var dto in gpuList)
                {
                    if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Manufacturer))
                        continue;

                    DateTime? releaseDate = null;
                    if (DateTime.TryParse(dto.ReleaseDate, CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                            out var parsedDate))
                    {
                        releaseDate = parsedDate;
                    }

                    var card = new GraphicsCard
                    {
                        Manufacturer = dto.Manufacturer,
                        Model = dto.Name,
                        Architecture = dto.Architecture,
                        ReleaseDate = releaseDate,
                        VramGb = dto.MemorySize ?? 0,
                        MemoryType = dto.MemoryType ?? string.Empty,
                        MemoryBusWidth = dto.MemoryBus ?? 0,
                        CoreClockMhz = dto.BaseClock ?? 0,
                        BoostClockMhz = dto.BoostClock ?? 0,
                        TdpWatts = dto.Tdp ?? 0,
                        ShaderUnits = dto.Shaders,
                        RtCores = dto.RtCores,
                        TensorCores = dto.TensorCores,
                        BenchmarkScore = null,
                        Price = null
                    };

                    _context.GraphicsCards.Add(card);
                    totalImported++;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Added {totalImported} gpu " });
        }
    }
}