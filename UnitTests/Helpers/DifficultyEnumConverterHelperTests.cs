using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Game.Models;
using Game.Helpers;

namespace UnitTests.Helpers
{
    [TestFixture]
    class DifficultyEnumConverterHelperTests
    {
        [Test]
        public void DifficultyEnumConvert_String_Should_Pass()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = "Difficult";
            var Result = myConverter.Convert(myObject, typeof(DifficultyEnum), null, null);
            var Expected = "Difficult";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DifficultyEnumConvert_Enum_Should_Pass()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = DifficultyEnum.Difficult;
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = "Difficult";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DifficultyEnumConvert_Other_Should_Skip()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = new MonsterModel();
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        // Convert Back
        [Test]
        public void IntEnumConvertBack_Should_Skip()
        {
            var myConverter = new IntEnumConverter();

            var myObject = "Bogus";
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        // Convert Back
        [Test]
        public void IntEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new IntEnumConverter();

            var myObject = 16;
            var Result = myConverter.ConvertBack(myObject, typeof(DifficultyEnum), null, null);
            var Expected = "Difficult";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DifficultyEnumConvertBack_String_Should_Pass()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = "Difficult";
            var Result = myConverter.ConvertBack(myObject, typeof(DifficultyEnum), null, null);
            var Expected = DifficultyEnum.Difficult;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DifficultyEnumConvertBack_Enum_Should_Skip()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = DifficultyEnum.Difficult;
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DifficultyEnumConvertBack_Other_Should_Skip()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = new MonsterModel();
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DifficultyEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new DifficultyEnumConverter();

            var myObject = 16;
            var Result = myConverter.ConvertBack(myObject, typeof(DifficultyEnum), null, null);
            var Expected = "Difficult";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }
    }
}
