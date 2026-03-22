using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private Inventory inventoryController;
    public Transform player; // assign in Inspector

    void Start()
    {
        // define save location
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController= FindAnyObjectByType<Inventory>();

        Debug.Log("Save path: " + saveLocation);

        // delay loading so nothing overrides it
        Invoke(nameof(loadGame), 0.1f);
    }

    public void saveGame()
    {
        Debug.Log("GAME SAVED");

        SaveData saveData = new SaveData
        {
            playerPosition = player.position, //Fuck niggers
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
    }

    public void loadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.position = saveData.playerPosition;
                rb.linearVelocity = Vector2.zero; // stop movement after load
            }
            else
            {
                player.position = saveData.playerPosition;
            }

            inventoryController.SetInventoryItems(saveData.inventorySaveData);

            Debug.Log("LOADED POSITION: " + saveData.playerPosition);
        }
        else
        {
            Debug.Log("No save file found, creating new one.");
            saveGame();
        }
    }
}