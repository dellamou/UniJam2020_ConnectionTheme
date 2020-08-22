using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json;

public class recordSaver : MonoBehaviour
{
    public class Record
    {
        public int pizza = 0;
        public int axe = 0;
        public int torch = 0;
        public int photo = 0;
        public bool be = false;
        public bool ge = false;
        public bool te = false;
    }

    public ItemCollectionManager items;
    private Record record;
    private string path = "/record.json";

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + path;
        items = GameObject.Find("Player").GetComponent<ItemCollectionManager>();
        LoadJson(path);
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

        }
    }

    public void SaveJson(string ending)
    {
        record.pizza += items.pizza;
        if (items.axe)
        {
            record.axe++;
        }
        if (items.torch)
        {
            record.torch++;
        }
        if (items.photo)
        {
            record.photo++;
        }
        switch (ending)
        {
            case "bad":
                record.be = true;
                break;
            case "good":
                record.ge = true;
                break;
            case "true":
                record.te = true;
                break;
            case "":
                break;
        }
        using (StreamWriter w = new StreamWriter(path))
        {
            w.WriteLine(JsonConvert.SerializeObject(record));
        }
    }
}
