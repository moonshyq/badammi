namespace BadammiAPI.Dtos
{
    public class FruitDto
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public List<string> ChildPhotoPath { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        public List<string> Benefits { get; set; } = new List<string>();
    }
}
