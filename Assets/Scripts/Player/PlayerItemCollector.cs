using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private Inventory inventory;
    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item != null)
            {
                //add item inventory
                bool itemAdded = inventory.AddItem(collision.gameObject);
                if (itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
            }

        }
    }
}
