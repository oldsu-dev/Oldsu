using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Oldsu.Utils
{
    public static class TokenGenerator
    {
        private static readonly object Lock = new object();
        
        public static string GenerateToken(int additionalByteCount)
        {
            lock (Lock)
            {
                long ticks = DateTime.Now.Ticks;

                using RandomNumberGenerator generator = RandomNumberGenerator.Create();
                using MemoryStream ms = new MemoryStream();
                using BinaryWriter writer = new BinaryWriter(ms);

                byte[] random = new byte[additionalByteCount];
                generator.GetBytes(random);

                writer.Write(random);
                writer.Write(DateTime.Now.Ticks);

                writer.Flush();

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}