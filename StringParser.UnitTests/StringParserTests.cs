using NUnit.Framework;
using Shouldly;

namespace StringParser.UnitTests;

[TestFixture]
public class StringParserTests
{
    private Services.StringParser parser = new Services.StringParser();

    //Positive Test 01: Test a standard output
    [Test]
    public void PositiveTests_01_TestStandardOutput()
    {
        parser.Parse("AAAc91%cWwWkLq$1ci3_848v3d__K").ShouldBe("Ac91%cWwWkLq£1c");
    }

    //Positive Test 02: Test maximum length truncaton (15 characters) in the result
    [Test]
    public void PositiveTests_02_TestMaximumLengthTruncation()
    {
        parser.Parse("12345678901234567890").ShouldBe("123567890123567");
    }

    //Positive Test 03: Test dollar sign replacement
    [Test]
    public void PositiveTests_03_TestDollarSignReplacement()
    {
        parser.Parse("$$$").ShouldBe("£");
    }

    //Positive Test 04: Test character duplication
    [Test]
    public void PositiveTests_04_TestCharacterDuplication()
    {
        parser.Parse("aaabbccAABBCC").ShouldBe("abcABC");
    }

    //Positive Test 05: Test the removal of underscore and number 4
    [Test]
    public void PositiveTests_05_TestRemovalOfUnderscoreAndNumber4()
    {
        parser.Parse("a__4b_4_c_").ShouldBe("abc");
    }

    //Positive Test 06: Test non-english characters
    [Test]
    public void PositiveTests_06_TestNonEnglishCharacters()
    {
        parser.Parse("ééé$$$ßß").ShouldBe("é£ß");
    }

    //Positive Test 07: Test specical characters
    [Test]
    public void PositiveTests_07_TestSpecialCharacters()
    {
        parser.Parse("1234567890!!!!!!").ShouldBe("123567890!");
    }

    //Negative Test 01: Test empty input
    [Test]
    public void NegativeTests_01_TestEmptyInput()
    {
        parser.Parse(string.Empty).ShouldBe(null);
    }

    //Negative Test 02: Test null input
    [Test]
    public void NegativeTests_02_TestNullInput()
    {
        parser.Parse(null).ShouldBe(null);
    }

    //Negative Test 03: Test input only having underscores and number 4
    [Test]
    public void NegativeTests_03_TestStringOnlyHaveUnderscoresAndNumber4()
    {
        parser.Parse("_____444__4_").ShouldBe("");
    }

    //Negative Test 04: Test all excluded characters
    [Test]
    public void NegativeTests_04_TestAllExcludedCharacters()
    {
        parser.Parse("$_4_$$4__").ShouldBe("££");
    }

    //Negative Test 05: Test very long string
    [Test]
    public void NegativeTests_05_TestVeryLongString()
    {
        parser.Parse("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa").ShouldBe("a");
    }
}
