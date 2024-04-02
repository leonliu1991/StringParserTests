using NUnit.Framework;
using Shouldly;

namespace StringParser.UnitTests;

[TestFixture]
public class StringParserTests
{
    private Services.StringParser parser = new Services.StringParser();

    [Test]
    public void PostiveTests_01_TestStandardOutput()
    {
        parser.Parse("AAAc91%cWwWkLq$1ci3_848v3d__K").ShouldBe("Ac91%cWwWkLq£1c");
    }

    [Test]
    public void PostiveTests_02_TestMaximumLengthTruncation()
    {
        parser.Parse("12345678901234567890").ShouldBe("123567890123567");
    }

    [Test]
    public void PostiveTests_03_TestDollarSignReplacement()
    {
        parser.Parse("$$$").ShouldBe("£");
    }

    [Test]
    public void PostiveTests_04_TestCharacterDuplication()
    {
        parser.Parse("aaabbccAABBCC").ShouldBe("abcABC");
    }

    [Test]
    public void PostiveTests_05_TestRemovalOfUnderscoreAndNumber4()
    {
        parser.Parse("a__4b_4_c_").ShouldBe("abc");
    }

    [Test]
    public void PostiveTests_06_TestNonEnglishCharacters()
    {
        parser.Parse("ééé$$$ßß").ShouldBe("é£ß");
    }

    [Test]
    public void PostiveTests_07_TestSpecialCharacters()
    {
        parser.Parse("1234567890!!!!!!").ShouldBe("123567890!");
    }

    [Test]
    public void NegativeTests_01_TestEmptyInput()
    {
        parser.Parse(string.Empty).ShouldBe(null);
    }

    [Test]
    public void NegativeTests_02_TestNullInput()
    {
        parser.Parse(null).ShouldBe(null);
    }

    [Test]
    public void NegativeTests_03_TestStringOnlyHaveUnderscoresAndNumber4()
    {
        parser.Parse("_____444__4_").ShouldBe("");
    }

    [Test]
    public void NegativeTests_04_TestStringsOfJust15Duplicates()
    {
        parser.Parse("aaaaaaaaaaaaaaa").ShouldBe("a");
    }

    [Test]
    public void NegativeTests_05_TestAllExcludedCharacters()
    {
        parser.Parse("$_4_$$4__").ShouldBe("££");
    }

    [Test]
    public void NegativeTests_06_TestVeryLongString()
    {
        parser.Parse("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa").ShouldBe("a");
    }
}
