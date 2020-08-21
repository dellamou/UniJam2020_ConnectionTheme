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
    public bool torch;

    public Text ItemCollection;
    public Text MidScreenNotice;
    public Text TopScreenNotice;

    public GameObject Axe;
    public GameObject Torch;
    private GameObject PhysicalTorch;

    private void Start()
    {
        this.pizza = 0;
        this.axe = false;
        this.torch = false;
        PhysicalTorch = GameObject.Find("TorchModel");
        PhysicalTorch.SetActive(false);
    }

    public void pickUp(string item)
    {
        switch (item)
        {
            case "Pizza":
                this.pizza++;
                TopScreenNotice.text = "You picked up Pizza";
                break;
            case "Axe":
                this.axe = true;
                TopScreenNotice.text = "You picked up Axe";
                break;
            case "Torch":
                PhysicalTorch.SetActive(true);
                this.torch = true;
                TopScreenNotice.text = "You pakced up Torch";
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
            Axe.SetActive(true);
        }
        if (this.torch)
        {

            output += "Torch" + "\n";
            Torch.SetActive(true);
        }
        ItemCollection.text = output;
    }
}