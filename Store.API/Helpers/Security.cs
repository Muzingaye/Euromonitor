using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Store.API.Helpers
{
    public class Security
    {
        private readonly TripleDESCryptoServiceProvider _mDes = new TripleDESCryptoServiceProvider();
        private readonly byte[] _mIv;
        private readonly byte[] _mKey;
        private readonly UTF8Encoding _mUtf8 = new UTF8Encoding();

        public Security(byte[] key, byte[] iv)
        {
            _mKey = key;
            _mIv = iv;
        }

        public Security()
        {
            var key = new byte[]
                { 1, 7, 3, 4, 5, 6, 7, 8, 19, 15, 11, 12, 19, 14, 15, 16, 11, 18, 34, 20, 21, 25, 23, 27 };
            var iv = new byte[] { 1, 7, 2, 5, 9, 3, 2, 1 };
            _mKey = key;
            _mIv = iv;
        }

        public string Encrypt(string text)
        {
            byte[] input = _mUtf8.GetBytes(text);
            byte[] output = Transform(input, _mDes.CreateEncryptor(_mKey, _mIv));
            return Convert.ToBase64String(output);
        }

        private byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
        {
            var memStream = new MemoryStream();
            var cryptStream = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write);

            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();

            memStream.Position = 0;
            var result = new byte[((Int32)(memStream.Length - 1)) + 1];
            memStream.Read(result, 0, result.Length);

            memStream.Close();
            cryptStream.Close();

            return result;
        }
    }
}