using Xunit;
using ars.lib.Common.Utils;

namespace ars.tests
{
    public class Common_Util_Should
    {
        [Fact]
        public void GeneratedPasswordNotNull()
        {
            var pwdGen = new PasswordGenerator();

            var pwd = pwdGen.Generate();

            Assert.NotNull(pwd);
        }

        [Fact]
        public void GeneratedPasswordsNotEqual()
        {
            var pwdGen = new PasswordGenerator();

            var pwd1 = pwdGen.Generate();
            var pwd2 = pwdGen.Generate();

            Assert.NotEqual(pwd1, pwd2);
        }
    }
}
