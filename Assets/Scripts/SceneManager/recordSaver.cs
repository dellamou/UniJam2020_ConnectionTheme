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
        public int pizza;
        public int axe;
        public int torch;
        public int photo;
        public bool be;
        public bool ge;
        public bool te;
    }

    public ItemCollectionManager items;
    private Record record;
    private string path = "record.json";

    // Start is called before the first frame update
    void Start()
    {
        items = GameObject.Find("Player").GetComponent<ItemCollectionManager>();
        LoadJson(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadJson(string path)
    {
        using (StreamReader r = new StreamReader(path))
        {
            string json = r.ReadToEnd();
            Debug.Log(json);
            this.record = JsonConvert.DeserializeObject<Record>(json);
        }
    }

    public void SaveJson()
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
        using (StreamWriter w = new StreamWriter(path))
        {
            w.WriteLine(JsonConvert.SerializeObject(record));
        }
    }
}
