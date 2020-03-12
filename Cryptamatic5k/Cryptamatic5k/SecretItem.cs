using System;
using System.IO;
using System.Security.Cryptography;

namespace opensequence.Cryptamatic5k
{
    public class SecretItem
    {
        public int Version { get; set; }
        public string PlaintextMessage { get; set; }
        public bool FileAttached { get; set; } = false;
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime FileDate { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }
        public byte[] Key { get; set; }
        //TODO-NOW Take this IV Secret out
        private byte[] IV { get; set; } = System.Text.Encoding.ASCII.GetBytes("/oa4SbceDr5miR4ReRb1GQ==");

        public string Encrypt(string plainText)
        {
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {

                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);


                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        // return result
                        return Convert.ToBase64String(ms.ToArray());
                    }

                }
            }

        }
        public string Decrypt(byte[] cipherText)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}

