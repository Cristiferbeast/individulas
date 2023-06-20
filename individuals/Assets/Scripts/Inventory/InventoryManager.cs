using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //External Varibables
    public GameObject inventoryUI;

    //Internal Varianles
    private bool isInventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInventoryOpen)
            {
                InventoryController(true);
            }
            else
            {
                InventoryController(false);
            }
        }
    }

    //Internal Functions
    void InventoryController (bool state){
        //Activates Inventory, and Sets the Bool for Inventory to Specified State
        inventoryUI.SetActive(state);
        isInventoryOpen = state; 
        if (state){
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }
}
