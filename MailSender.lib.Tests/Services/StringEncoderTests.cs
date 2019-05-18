using MailSender.lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Services
{
    [TestClass]
    public class StringEncoderTests
    {
        [TestMethod]
        public void EncodeTest()
        {
            const string str = "asd";
            const int key = 1;
            const string expected_str = "bte";

            var actual_str = StringEncoder.Encode(str, key);

            Assert.AreEqual(expected_str, actual_str);
        }

        [TestMethod]
        public void DecodeTest()
        {
            const string str = "bte";
            const int key = 1;
            const string expected_str = "asd";

            var actual_str = StringEncoder.Decode(str, key);

            Assert.AreEqual(expected_str, actual_str);

            //StringAssert.Matches();
            //CollectionAssert.IsSubsetOf();
        }
    }
}