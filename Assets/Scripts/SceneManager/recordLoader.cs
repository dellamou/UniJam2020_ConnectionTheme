using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;

using TMPro;
using UnityEngine.UI;

public class recordLoader : MonoBehaviour
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

    private string path = "record.json";
    public Record record = new Record();
    public Button badEnd;
    public Button GoodEnd;
    public Button TrueEnd;
    public TextMeshProUGUI count;

    // Start is called before the first frame update
    void Start()
    {
        LoadJson(this.path);
        badEnd = GameObject.Find("badEnd").GetComponent<Button>();
        GoodEnd = GameObject.Find("GoodEnd").GetComponent<Button>();
        TrueEnd = GameObject.Find("TrueEnd").GetComponent<Button>();
        count = GameObject.Find("count").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        count.text = record.pizza + "\n" +
                     record.axe + "\n" +
                     record.torch + "\n" +
                     record.photo + "\n";
        badEnd.interactable = record.be;
        GoodEnd.interactable = record.ge;
        TrueEnd.interactable = record.te;
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
}

