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

        [Fact]
        public void Challenge2()
        {
            var hex1 = "1c0111001f010100061a024b53535009181c";
            var hex2 = "686974207468652062756c6c277320657965";
            var expectedXor = "746865206b696420646f6e277420706c6179";

            var xor = Logic.Xor(hex1, hex2);

            Assert.Equal(expectedXor, xor);
        }
    }
}
