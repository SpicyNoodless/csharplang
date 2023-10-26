using Newtonsoft.Json;

namespace RuleBasedLU;

public class Cat
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("age")]
    public int Age { get; set; }

    [JsonProperty("toys")]
    public string[] Toys { get; set; } = null!;

    [JsonProperty("bestFriend")]
    public Cat? BestFriend { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var tom = new Cat
        {
            Name = "Tom",
            Age = 3,
            Toys = new[] { "Mouse", "Ball" },
            BestFriend = new Cat
            {
                Name = "Jerry",
                Age = 2,
                Toys = new[] { "Ball" }
            }
        };

        var json = JsonConvert.SerializeObject(tom, Formatting.Indented);

        Console.WriteLine(json);
    }
}
