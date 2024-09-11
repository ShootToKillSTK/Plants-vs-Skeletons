using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections;

public class PlantPlacement : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] plantPrefabs;
    public int[] plantCosts; // Add plant costs array
    public Text messageText; // Reference to the message Text UI element

    private GameObject selectedPlant;
    private int selectedPlantCost;
    private Camera mainCamera;
    private Coroutine messageCoroutine;

    void Start()
    {
        mainCamera = Camera.main;
        messageText.text = ""; // Ensure the message text is initially empty
    }

    void Update()
    {
        if (selectedPlant != null && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mousePos);

            if (tilemap.GetTile(cellPos) != null)
            {
                if (RainManager.instance.SpendRain(selectedPlantCost))
                {
                    Instantiate(selectedPlant, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
                    selectedPlant = null;
                }
                // BROKEN, spams it even if click rain, need bunch rewriting/rework
                //else
                //{
                //    ShowMessage("Not Enough Rain!");
                //}
            }
        }
    }

    public void SelectPlant(int plantIndex)
    {
        selectedPlant = plantPrefabs[plantIndex];
        selectedPlantCost = plantCosts[plantIndex];
    }

    private void ShowMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        messageCoroutine = StartCoroutine(DisplayMessage(message));
    }

    private IEnumerator DisplayMessage(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(1f); // Display the message for 1 second
        messageText.text = "";
        messageCoroutine = null;
    }
}
