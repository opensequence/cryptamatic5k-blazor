using System;
using Newtonsoft.Json;
namespace opensequence.Cryptamatic5k
{
    public class SecretItemJson
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
        private byte[] IV { get; set; }
        public SecretItemJson()
        {
        }

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static SecretItemJson ConvertFromJson(string json)
        {
            //Deserialize Object into this instance
            return JsonConvert.DeserializeObject<SecretItemJson>(json);

        }
    }
}
