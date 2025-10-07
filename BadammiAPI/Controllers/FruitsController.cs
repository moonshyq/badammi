using BadammiAPI.Dtos;
using BadammiAPI.Models;
using BadammiAPI.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BadammiAPI.Controllers;


[AllowAnonymous]
public class FruitsController : Controller
{
    private readonly IJsonService _jsonService;
    private readonly IWebHostEnvironment _env;

    public FruitsController(IJsonService jsonService,IWebHostEnvironment env)
    {
        _jsonService = jsonService;
        _env = env;
    }

    [HttpPost]
    public async Task<IActionResult> AddFruits([FromForm] FruitDto fruit)
    {
        Console.WriteLine(fruit.Name);
        Console.WriteLine(fruit.Composition);
        Console.WriteLine(fruit.Benefits[1]);

        var random = new Random();
        var id = random.Next(10000000, 99999999);

        if (Request.Form.Files.Count > 0)
        {
            var file = Request.Form.Files[0];
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "Resource", "Photo");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, id.ToString() + extension);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fruit.PhotoPath = Path.Combine("Resource", "Photo", id.ToString() + extension);
            }
        }
        Console.WriteLine(fruit.PhotoPath);
        fruit.Id = id;
        _jsonService.AddFruit(new Fruit()
        {
            Name = fruit.Name,
            Benefits = fruit.Benefits,
            Composition = fruit.Composition,
            Id = id,
            PhotoPath = fruit.PhotoPath,
        });
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAllFruits()
    {
        Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "Resourse", "Photo"));
        var fruits = _jsonService.GetFruits();

        return Ok(fruits);
    }

    [HttpDelete]
    public IActionResult DeleteFruit(int id)
    {
        _jsonService.RemoveFruit(id);

        return Ok(new { message = "Fruit deleted" });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFruit(int id, [FromForm] Fruit fruit, IFormFile? newPhoto, string? BenefitsString)
    {
        Console.WriteLine(fruit.PhotoPath);
        if (Request.Form.Files.Count > 0)
        {
            var file = Request.Form.Files[0];
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "Resource", "Photo");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, id.ToString() + extension);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fruit.PhotoPath = Path.Combine("Resource", "Photo", id.ToString() + extension);
            }
        }
        fruit.Benefits = BenefitsString.Split(",").ToList();
        _jsonService.UpdateFruit(id, fruit);
        return Ok("ok");
    }

    [HttpGet]
    [ActionName("search")]
    public async Task<IActionResult> ShopSearch(string name)
    {
        var result = _jsonService.GetFruits();
        var toshow = result
        .Where(x => x.Name.ToLower().Contains(name.ToLower()));
        return Ok(toshow);
    }

    [HttpGet]
    public async Task<IActionResult> ShopOne(string id)
    {
        var int_id = int.Parse(id);
        var result = _jsonService.GetFruits();
        var toshow = result.First(x => x.Id == int_id);
        return Ok(toshow);
    }

}
