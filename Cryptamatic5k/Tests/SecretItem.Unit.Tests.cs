using Xunit;
using opensequence.Cryptamatic5k;
using System;

namespace opensequence.Cryptamatic5k.Unit.Tests
{
    public class TestSecretItem
    {
        public SecretItem SutSecretItem;
        public TestSecretItem()
        {
            SutSecretItem = new SecretItem();
        }
        
        [Fact]
        public void EncryptShouldReturnString()
        {
            Assert.IsType<string>(SutSecretItem.Encrypt("plaintext","password"));
            
        }
        [Fact]
        public void DecryptShouldReturnString()
        {
            string encryptedMessage = "FwX7Z/wEIrRHIG3rPHXsS0gyJXlb0yC6r+DqKnGDbxG+QPGY2jl5rbGI7ZBQ5QZ/BJzToNy4Elv1UYGOVhMEw03yvqlRt5mUFaEay4VJJZ0u55GNQwNZMxsMv2aYk7n8rHvduWSn0pGFvKiK4SbFxNamF0+6UMPOms6nZc7/Y9M0sVss6nJSQPm9DbEoHsuCZf9t9Ia8zdLkGqMUxQ2tJuqLEoAw+wiwHVL3ZEkC+v8=";
            string password = "yoohoo";
            Assert.IsType<string>(SutSecretItem.Encrypt("plaintext", "password"));

        }
    }
}
