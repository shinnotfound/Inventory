using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private MouseFollower mouseFollower;


    List<Item> listOfItems = new List<Item>();

    public Sprite image;
    public int quantity;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
    }

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
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(Item obj)
    {
        
    }

    private void HandleBeginDrag(Item obj)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleItemSelection(Item item)
    {
        listOfItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        listOfItems[0].SetData(image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
