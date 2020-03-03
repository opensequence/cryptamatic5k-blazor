using System;
using Newtonsoft.Json;
public class SecretItemJson
{
    public int Version { get; set; }
    public byte[] Message { get; set; }
    public bool FileAttached { get; set; } = false;
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public DateTime FileDate { get; set; }
    public string FileType { get; set; }
    public byte[] File { get; set; }
    public string Key { get; set; }
    private string IV { get; set; }
    public SecretItemJson(byte[] message, string key, bool filesattached)
	{
        Message = message;
        Key = key;
        FileAttached = filesattached;
	}

    //TODO-NOW: Design a method to turn the contents of this object into json
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
