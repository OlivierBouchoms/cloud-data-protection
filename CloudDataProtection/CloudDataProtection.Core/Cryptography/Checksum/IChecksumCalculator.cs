using System.IO;
using CloudDataProtection.Core.Entities;

namespace CloudDataProtection.Core.Cryptography.Checksum
{
    public interface IChecksumCalculator
    {
        string Calculate(Stream stream);
        
        ChecksumAlgorithm Algorithm { get; }
    }
}