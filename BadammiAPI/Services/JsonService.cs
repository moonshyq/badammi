using BadammiAPI.Models;
using BadammiAPI.Services.Base;
using System.Text.Json;

namespace BadammiAPI.Services;

public class JsonService : IJsonService
{
    private readonly string path = "Resourse/Json/Fruits.json";

    public void AddFruit(Fruit fruit)
    {
        var json = File.ReadAllText(path);
        if(json == string.Empty)
        {
            var list = new List<Fruit>()
            {
                fruit
            };
            var toaddjson = JsonSerializer.Serialize(list);
            File.WriteAllText(path, toaddjson);
            return;
        }
        var listtoadd = JsonSerializer.Deserialize<List<Fruit>>(json);
        listtoadd.Add(fruit);
        var addjson = JsonSerializer.Serialize(listtoadd);
        File.WriteAllText(path, addjson);
    }

    public Fruit GetFruitById(int id)
    {
        var json = File.ReadAllText(path);
        if (json == string.Empty)
        {
            return new Fruit();
        }
        var list = JsonSerializer.Deserialize<List<Fruit>>(json);

        var result = list.Find(x => x.Id == id);
        return result;
    }

    public List<Fruit> GetFruits()
    {
        var json = File.ReadAllText(path);
        if(json == string.Empty)
        {
            return new List<Fruit>();
        }
        var result = JsonSerializer.Deserialize<List<Fruit>>(json);
        return result;
    }

    public void RemoveFruit(int id)
    {
        var json = File.ReadAllText(path);
        var listtoedit = JsonSerializer.Deserialize<List<Fruit>>(json);

        var todelete = listtoedit.Find(x => x.Id == id);
        listtoedit.Remove(todelete);

        var towrite = JsonSerializer.Serialize(listtoedit);
        File.WriteAllText(path,towrite);
    }

    public void UpdateFruit(int id, Fruit fruit)
    {
        var json = File.ReadAllText(path);
        var listtoedit = JsonSerializer.Deserialize<List<Fruit>>(json);

        var toedit = listtoedit.Find(x => x.Id==id);
        var index = listtoedit.IndexOf(toedit);
        fruit.PhotoPath = toedit.PhotoPath;
        listtoedit.Remove(toedit);
        listtoedit.Insert(index, fruit);
        var editedjson = JsonSerializer.Serialize(listtoedit);
        File.WriteAllText(path, editedjson);
    }
}
