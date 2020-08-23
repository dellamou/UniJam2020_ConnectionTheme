using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        record = new Record();
        record.pizza = PlayerPrefs.GetInt("pizza");
        record.axe = PlayerPrefs.GetInt("axe");
        record.torch = PlayerPrefs.GetInt("torch");
        record.photo = PlayerPrefs.GetInt("photo");
        record.be = PlayerPrefs.GetInt("be")==1 ? true : false; 
        record.ge = PlayerPrefs.GetInt("ge") == 1 ? true : false;
        record.te = PlayerPrefs.GetInt("te") == 1 ? true : false;
    }

    public void SaveJson(string ending)
    {
        PlayerPrefs.SetInt("pizza", record.pizza += items.pizza);
        //record.pizza += items.pizza;
        if (items.axe)
        {
            PlayerPrefs.SetInt("axe", record.axe += 1);
            //record.axe++;
        }
        if (items.torch)
        {
            PlayerPrefs.SetInt("torch", record.torch += 1);
            //record.torch++;
        }
        if (items.photo)
        {
            PlayerPrefs.SetInt("photo", record.photo += 1);
            //record.photo++;
        }
        switch (ending)
        {
            case "bad":
                PlayerPrefs.SetInt("be", 1);
                //record.be = true;
                break;
            case "good":
                PlayerPrefs.SetInt("ge", 1);
                //record.ge = true;
                break;
            case "true":
                PlayerPrefs.SetInt("te", 1);
                //record.te = true;
                break;
            case "":
                break;
        }
    }
}
