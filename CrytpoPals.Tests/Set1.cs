using System;
using Xunit;

namespace CrytpoPals.Tests
{
    public class Set1
    {
        [Fact]
        public void Challenge1()
        {
            var hex = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            var expectedBase64 = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

            var base64 = Converter.Convert(hex);

            Assert.Equal(expectedBase64, base64);
        }
    }
}
