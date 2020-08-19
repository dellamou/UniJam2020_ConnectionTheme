using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollectionManager : MonoBehaviour
{
    // the number of quest items player got
    public int pizza;
    public bool axe;

    public Text ItemCollection;
    public Text MidScreenNotice;
    public Text TopScreenNotice;

    private void Start()
    {
        this.pizza = 0;
        this.axe = false;
    }

    public void pickUp(string item)
    {
        switch (item)
        {
            case "Pizza":
                this.pizza++;
                TopScreenNotice.text = "You got the Pizza";
                break;
            case "Axe":
                this.axe = true;
                TopScreenNotice.text = "You got the Axe";
                break;
        }
        
    }

    private void Update()
    {
        string output = "Collected Items: \n";
        if (this.pizza > 0)
        {
            output += "Pizza " + pizza + "\n";
        }
        if (this.axe)
        {
            output += "Axe" + "\n";
        }
        ItemCollection.text = output;
    }
}