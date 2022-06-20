using System.Security.Cryptography;
using System.Text;

namespace Oldsu.Utils.Crypto;

public class MD5Hash
{
    public static string Compute(byte[] data)
    {
        data = MD5.HashData(data);

        char[] str = new char[data.Length * 2];
        for (int i = 0; i < data.Length; i++)
            data[i].ToString("x2").CopyTo(0, str, i * 2, 2);

        return new string(str);
    }
    
    public static string Compute(string data)
    {
        return Compute(Encoding.UTF8.GetBytes(data));
    }
}