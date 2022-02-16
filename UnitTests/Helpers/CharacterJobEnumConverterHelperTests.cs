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
    class CharacterJobEnumConverterHelperTests
    {
        [Test]
        public void CharacterJobEnumConvert_String_Should_Pass()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = "Frigate";
            var Result = myConverter.Convert(myObject, typeof(CharacterJobEnum), null, null);
            var Expected = "Frigate";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumConvert_Enum_Should_Pass()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = CharacterJobEnum.Frigate;
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = "Frigate";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumConvert_Other_Should_Skip()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = new CharacterModel();
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

            var myObject = 14;
            var Result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);
            var Expected = "Frigate";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumConvertBack_String_Should_Pass()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = "Frigate";
            var Result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);
            var Expected = CharacterJobEnum.Frigate;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumConvertBack_Enum_Should_Skip()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = CharacterJobEnum.Frigate;
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumConvertBack_Other_Should_Skip()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = new CharacterModel();
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new CharacterJobEnumConverter();

            var myObject = 14;
            var Result = myConverter.ConvertBack(myObject, typeof(CharacterJobEnum), null, null);
            var Expected = "Frigate";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

    }
}
