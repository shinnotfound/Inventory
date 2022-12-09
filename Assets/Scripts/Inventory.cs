using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<Item> listOfItems = new List<Item>();

    public void InitializeInventoryUI(int inventorysize)
    {
        for(int i = 0; i < inventorysize; i++)
        {
            Item _item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            _item.transform.SetParent(contentPanel);
            listOfItems.Add(_item);
            _item.OnClicked += HandleItemSelection;
            _item.OnItemBeginDrag += HandleBeginDrag;
            _item.OnItemDroppedOn += HandleSwap;
            _item.OnItemEndDrag += HandleEndDrag;
            _item.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(Item obj)
    {
        
    }

    private void HandleEndDrag(Item obj)
    {
        
    }

    private void HandleSwap(Item obj)
    {
        
    }

    private void HandleBeginDrag(Item obj)
    {
        
    }

    private void HandleItemSelection(Item obj)
    {
        Debug.Log(obj.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
