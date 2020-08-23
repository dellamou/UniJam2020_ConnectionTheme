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

    private string path = "/record.json";
    public Record record = new Record();
    public Button badEnd;
    public Button GoodEnd;
    public Button TrueEnd;
    public TextMeshProUGUI count;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + path;
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
        record = new Record();
        record.pizza = PlayerPrefs.GetInt("pizza");
        record.axe = PlayerPrefs.GetInt("axe");
        record.torch = PlayerPrefs.GetInt("torch");
        record.photo = PlayerPrefs.GetInt("photo");
        record.be = PlayerPrefs.GetInt("be") == 1 ? true : false;
        record.ge = PlayerPrefs.GetInt("ge") == 1 ? true : false;
        record.te = PlayerPrefs.GetInt("te") == 1 ? true : false;
    }
}

