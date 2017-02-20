using System;
using NUnit.Framework;
using TitanWcfService.Services.Parsers;

namespace Test.Parsers
{
    [TestFixture]
    public class MathParserTest
    {
        MathParser parser = new MathParser();
        [Test]
        public void ParseShouldEvaluateCorrectlySimpleOperations()
        {
            Assert.That(Convert.ToDouble(parser.Parse("=math(3*  1+3   *   3        +  7^1)")) == 19);
        }
        [Test]
        public void EmptyLineShouldntParse()
        {
            Assert.That(parser.Parse("=math()") == "=math()");
        }

        [Test]
        public void NumberShouldReturnNumber()
        {
            Assert.That(Convert.ToDouble(parser.Parse("=math(19)")) == 19);
        }
        
        [Test]
        public void SqrtShouldParsingGood()
        {
            Assert.That(Convert.ToDouble(parser.Parse("=math(4^(1/2))")) == 2);
        }
    }
}