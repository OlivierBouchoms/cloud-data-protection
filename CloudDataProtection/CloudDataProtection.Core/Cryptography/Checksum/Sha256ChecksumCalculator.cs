using System;
using System.IO;
using System.Security.Cryptography;
using CloudDataProtection.Core.Entities;

namespace CloudDataProtection.Core.Cryptography.Checksum
{
    public class Sha256ChecksumCalculator : IChecksumCalculator
    {
        public ChecksumAlgorithm Algorithm => ChecksumAlgorithm.Sha256;

        public string Calculate(Stream stream)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(stream);

                stream.Position = 0;
                
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}