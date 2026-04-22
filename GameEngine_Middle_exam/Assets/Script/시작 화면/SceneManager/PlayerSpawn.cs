using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject grapePrefab;
    public GameObject icePrefab;
    public GameObject vegetablePrefab;
    public GameObject meatPrefab;

    void Start()
    {
        switch (GameManager.selectedIngredient)
        {
            case "Grape":
                Instantiate(grapePrefab);
                break;

            case "Ice":
                Instantiate(icePrefab);
                break;

            case "Vegetable":
                Instantiate(vegetablePrefab);
                break;

            case "Meat":
                Instantiate(meatPrefab);
                break;
        }
    }
}