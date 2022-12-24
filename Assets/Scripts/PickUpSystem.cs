using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Items _item = collider.GetComponent<Items>();
        if(_item != null)
        {
            //Debug.Log("Collision Detected");
            int reminder = inventoryData.AddItem(_item.InventoryItem, _item.Quantity);
            if (reminder == 0)
                _item.DestroyItem();
            else
                _item.Quantity = reminder;
        }
    }
}
