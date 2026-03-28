using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    public float minDropDistance = 2f;
    public float maxDropDistance = 3f;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;        //Save old parent
        transform.SetParent(transform.root);      //Above other canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;                 //make transparency at 0.6 durgin drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; //follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //enable raycasts
        canvasGroup.alpha = 1f;            //No longer transparent

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        

        if (dropSlot == null)
        {
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot= item.GetComponentInParent<Slot>();
            }
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                //swap items
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }

            //Move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            //if were dropping is not within the inventory
            if (!isWithinInventory(eventData.position))
            {
                //drop out item
                DropItem(originalSlot);

            }
            else
            {
                //snap back to og slot
                transform.SetParent(originalParent);
            }
            //No slot under drop point
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //Center
    }
    bool isWithinInventory(Vector2 mousePosition)
    {
       RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }
    void DropItem(Slot orignalSlot)
    {
       orignalSlot.currentItem = null;
        //find player
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if(playerTransform == null)
        {
            Debug.LogError("missing player tag");
            return;
        }
        //random drop position
        Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
        Vector2 dropPosition = (Vector2)playerTransform.position + dropOffset;

        //instantie drop item
        Instantiate(gameObject, dropPosition, Quaternion.identity);

        //destroy the UI one
        Destroy(gameObject);
    }
}
