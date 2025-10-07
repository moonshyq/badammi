using BadammiAPI.Models;

namespace BadammiAPI.Services.Base;

public interface IJsonService
{
    public Fruit GetFruitById(int id);
    public List<Fruit> GetFruits();
    public void AddFruit(Fruit fruit);
    public void RemoveFruit(int id);
    public void UpdateFruit(int id, Fruit fruit);
}
