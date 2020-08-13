using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfWhenCollide : MonoBehaviour
{
    public ItemCollectionManager ItemCollection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            ItemCollection.itemCounter++;
        }
    }
}