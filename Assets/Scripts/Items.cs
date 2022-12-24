using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
   [field: SerializeField] public ItemSO InventoryItem { get; private set; }

   [field: SerializeField] public int Quantity { get; set; } = 1;

   [SerializeField] private float duration = 0.3f;
    
   private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickUp());
    }

    private IEnumerator AnimateItemPickUp()
    {
        
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;//Scale the item down until it's zero
        float currentTime = 0;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
