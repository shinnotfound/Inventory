using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionPanel : MonoBehaviour
{
    [SerializeField] private GameObject btnPrefab;

    public void AddBTN(string name, Action onClickAction)
    {
        GameObject btn = Instantiate(btnPrefab, transform);
        btn.GetComponent<Button>().onClick.AddListener(() => onClickAction());
        btn.GetComponentInChildren<TMPro.TMP_Text>().text = name;
    }

    internal void Toggle(bool val)
    {
        if (val == true)
        {
            RemoveOldBTN();
        }
        gameObject.SetActive(val);
    }

    public void RemoveOldBTN()
    {
        foreach (Transform transformChildObjects in transform)
        {
            Destroy(transformChildObjects.gameObject);
        }
    }
}
