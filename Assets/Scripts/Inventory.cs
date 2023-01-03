using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class Inventory : MonoBehaviour

{
    [SerializeField] private Item itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private MouseFollower mouseFollower;
    [SerializeField] private ActionPanel actionPanel;


    List<Item> listOfItems = new List<Item>();

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private int currentlyDraggedIndex = -1;

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

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if(listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string Description)
    {
        DeselectAllItems();
        listOfItems[itemIndex].Select();
    }

    private void HandleShowItemActions(Item item)
    {
        int index = listOfItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(Item item)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(Item item)
    {
        int index = listOfItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedIndex, index);
        HandleItemSelection(item);

    }

    private void HandleBeginDrag(Item item)
    {
        int index = listOfItems.IndexOf(item);
        if (index == -1)
            return;
        currentlyDraggedIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);

    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(Item item)
    {
        int index = listOfItems.IndexOf(item);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddBTN(actionName, performAction);
    }

    public void ShowActionPanel(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfItems[itemIndex].transform.position;
    }

    public void HideActionPanel(int itemIndex)
    {
        actionPanel.Toggle(false);
    }

    public void ResetSelection()
    {
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach(Item item in listOfItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedIndex = -1;
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
