namespace BadammiAPI.Models;

public class Fruit
{
    public int Id { get; set; }
    public string PhotoPath {  get; set; }
    public List<string> ChildPhotoPath { get; set; }
    public List<string> ChildPhotoText {  get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public string Composition { get; set; }
    public List<string> Benefits { get; set; } = new List<string>();
}
