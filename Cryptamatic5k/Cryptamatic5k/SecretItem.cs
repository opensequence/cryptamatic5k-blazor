using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace opensequence.Cryptamatic5k
{
    public class SecretItem
    {
        public int Version { get; set; } = 2;
        public string PlaintextMessage { get; set; }
        public bool FileAttached { get; set; } = false;
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public DateTime FileDate { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }
        private byte[] Key { get; set; }
        private byte[] IV { get; set; }


        public string Encrypt(string plainText, string password)
        {
            //TODO-NOW Redesign the Key and IV derivation (derive from password with salt?)
            //Derive Key from Password
            Key = CreateHash(password);
            //Derive IV from Password        
            IV = CreateHash(password + "/oa4SbceDr5miR4ReRb1GQ==", 16);
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
        public string Decrypt(byte[] cipherText, string password)
        {
            string plaintext = null;
            //Derive Key from Password
            Key = CreateHash(password);
            //Derive IV from Password        
            IV = CreateHash(password + "/oa4SbceDr5miR4ReRb1GQ==", 16);
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

        private static byte[] CreateHash(string text, int length = 32)
        {
            byte[] hash = new byte[length];
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                hash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            }
            if (hash.Length != length)
            {
                Array.Resize(ref hash, length);
            }
            return hash;
            
        }
    }
}

