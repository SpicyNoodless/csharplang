using Newtonsoft.Json;
using RuleBasedLU;

namespace RuleBasedLUTest;

[TestClass]
public class LUGeneratorTest
{
    [TestMethod]
    public void TestGenerate()
    {
        var testSet = GetTestSet("en");
        foreach (var item in testSet)
        {
            var lu = LUGenerator.GenerateSemanticFrame(item.Text, "en");
            Assert.AreEqual(lu.Domain, item.Domain);
            Assert.AreEqual(lu.Intent, item.Intent);
        }
    }

    [TestMethod]
    public void TestReadFile()
    {
        var rawJson = File.ReadAllText("D:/Playground/csharplang/Assets/English.json");
        Assert.IsNotNull(rawJson);
        var testSet = JsonConvert.DeserializeObject<List<TestItem>>(rawJson);
        Assert.IsTrue(testSet?.Count != 0);
    }

    private static IEnumerable<TestItem> GetTestSet(string language="en")
    {
        var filePath = language switch {
            "en" => "D:/Playground/csharplang/Assets/English.json",
            "fr" => "D:/Playground/csharplang/Assets/French.json",
            "pt" => "D:/Playground/csharplang/Assets/Portuguese.json",
            _ => throw new ArgumentException("Invalid language", nameof(language))
        };

        var rawJson = File.ReadAllText(filePath);
        var testSet = JsonConvert.DeserializeObject<List<TestItem>>(rawJson);
        return testSet ?? Enumerable.Empty<TestItem>();
    }
}

class TestItem
{
    public string Text { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string Intent { get; set; } = null!;
}