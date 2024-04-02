using NUnit.Framework;
using Shouldly;
using Moq;
using StringParser.Abstractions;
using StringParser.Services;

namespace StringParser.UnitTests;

[TestFixture]
public class StringCollectionParserTests
{
    private Mock<IStringParser> _stringParserMock = new Mock<IStringParser>();
    private StringCollectionParser _collectionParser;

    public StringCollectionParserTests()
    {
        _collectionParser = new StringCollectionParser(_stringParserMock.Object);
    }

    [Test]
    public void PostiveTests_01_TestSingleObjectInTheCollection()
    {
        string testData = "123abc";
        _stringParserMock.Setup(x => x.Parse(testData)).Returns(testData);

        List<string> testInput = new List<string>();
        testInput.Add(testData);

        _collectionParser.Parse(testInput).ShouldBe(testInput);
    }

    [Test]
    public void PostiveTests_02_TestMultipleObjectsInTheCollection()
    {
        string testData = "123abc";
        _stringParserMock.Setup(x => x.Parse(testData)).Returns(testData);

        List<string> testInput = new List<string>();
        testInput.Add(testData);
        testInput.Add(testData);

        _collectionParser.Parse(testInput).ShouldBe(testInput);
    }

    [Test]
    public void NegativeTests_01_TestEmptyResults()
    {
        string testData = "_4_4";
        _stringParserMock.Setup(x => x.Parse(testData)).Returns(string.Empty);

        List<string> testInput = new List<string>();
        testInput.Add(testData);
        testInput.Add(testData);

        List<string> expectedOutput = new List<string>();
        expectedOutput.Add("");
        expectedOutput.Add("");

        _collectionParser.Parse(testInput).ShouldBe(expectedOutput);
    }

    [Test]
    public void NegativeTests_02_TestNullResults()
    {
        string testData = "";
        _stringParserMock.Setup(x => x.Parse(testData)).Returns(value:null);

        List<string> testInput = new List<string>();
        testInput.Add(testData);
        testInput.Add(testData);

        List<string> expectedOutput = new List<string>();
        expectedOutput.Add(null);
        expectedOutput.Add(null);

        _collectionParser.Parse(testInput).ShouldBe(expectedOutput);
    }
}
