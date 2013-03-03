using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ItsaWeb.Infrastructure;
using NUnit.Framework;

namespace ItsaWebTests.Infrastructure
{
    [TestFixture]
    class CookieEncryptionTests
    {
        [Test]
        public void WhenDecryptingAValue_ThenTheValueCannotBNull()
        {
            byte[] ciperText = null;
            Action act = () => ciperText.Decrypt("", "");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenDecryptingAValue_ThenTheValueCannotBeEmpty()
        {
            byte[] ciperText = new byte[0];
            Action act = () => ciperText.Decrypt("", "");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenEncryptingAValue_ThenTheValueCannotBNull()
        {
            string ciperText = null;
            Action act = () => ciperText.Encrypt("saltsalt", "");
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenEncryptingAValue_ThenTheValueCannotBeEmpty()
        {
            string ciperText = "";
            Action act = () => ciperText.Encrypt("saltsalt", "");
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
