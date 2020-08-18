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
                break;
            case "Axe":
                this.axe = true;
                break;
        }
        
    }
}