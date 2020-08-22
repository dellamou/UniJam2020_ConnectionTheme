using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;

public class Record
{
    public int pizza;
    public int axe;
    public int torch;
    public int photo;
    public bool be;
    public bool ge;
    public bool te;
}

public class permissionAskser : MonoBehaviour
{
    private Record record;
    private string path = "/record.json";
    // Start is called before the first frame update
    void Start()
    {
        LoadJson(path); ;
        path = Application.dataPath + path;
    }

    public void LoadJson(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                Debug.Log(json);
                this.record = JsonConvert.DeserializeObject<Record>(json);
            }
        }
        else
        {
            this.record = new Record();
            using (StreamWriter w = new StreamWriter(path))
            {
                w.WriteLine(JsonConvert.SerializeObject(record));
            }
        }
    }
}
