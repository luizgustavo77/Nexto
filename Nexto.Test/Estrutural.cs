using Nexto.Commom.Test;
using Xunit;

namespace Nexto.Test
{
    /// <summary>
    /// Caixa Branca
    /// </summary>
    public class Estrutural
    {
        [Theory]
        [InlineData("955.600.510-20")]
        [InlineData("868.548.460-05")]
        [InlineData("330.092.150-37")]
        public void ValidCPF(string cpf)
        {
            Assert.True(Valid.IsCpf(cpf));
        }

        [Theory]
        [InlineData("tm@hotmail.com")]
        [InlineData("tm@gmail.com")]
        [InlineData("tm@yahoo.com")]
        public void ValidEMail(string email)
        {
            Assert.True(Valid.IsEmail(email));
        }
        [Theory]
        [InlineData("955.600.510-666")]
        [InlineData("868.548.460-666")]
        [InlineData("330.092.150-666")]
        public void InvalidCPF(string cpf)
        {
            Assert.True(!Valid.IsCpf(cpf));
        }

        [Theory]
        [InlineData("tmhotmail.com")]
        [InlineData("tm77gmail.com")]
        [InlineData("tm@@yahoo.com")]
        public void InvalidEMail(string email)
        {
            Assert.True(!Valid.IsEmail(email));
        }
    }
}
