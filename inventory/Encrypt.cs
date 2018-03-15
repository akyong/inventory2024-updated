using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace inventory
{
    class Encrypt
    {        
            //// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
            //// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
            //private const string initVector = "pemgail9uzpgzl88";
            //// This constant is used to determine the keysize of the encryption algorithm
            //private const int keysize = 256;
            ////Encrypt
            //public static string EncryptString(string plainText, string passPhrase)
            //{
            //    byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            //    byte[] keyBytes = password.GetBytes(keysize / 8);
            //    RijndaelManaged symmetricKey = new RijndaelManaged();
            //    symmetricKey.Mode = CipherMode.CBC;
            //    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            //    MemoryStream memoryStream = new MemoryStream();
            //    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            //    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            //    cryptoStream.FlushFinalBlock();
            //    byte[] cipherTextBytes = memoryStream.ToArray();
            //    memoryStream.Close();
            //    cryptoStream.Close();
            //    return Convert.ToBase64String(cipherTextBytes);
            //}
            ////Decrypt
            //public static string DecryptString(string cipherText, string passPhrase)
            //{
            //    byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            //    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            //    byte[] keyBytes = password.GetBytes(keysize / 8);
            //    RijndaelManaged symmetricKey = new RijndaelManaged();
            //    symmetricKey.Mode = CipherMode.CBC;
            //    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            //    MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            //    CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            //    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            //    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            //    memoryStream.Close();
            //    cryptoStream.Close();
            //    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            //}

            //public static string EncodePasswordToBase64(string password)
            //{
            //    byte[] bytes = Encoding.Unicode.GetBytes(password);
            //    byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            //    return Convert.ToBase64String(inArray);
            //}

        public static string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
        
    }
}
