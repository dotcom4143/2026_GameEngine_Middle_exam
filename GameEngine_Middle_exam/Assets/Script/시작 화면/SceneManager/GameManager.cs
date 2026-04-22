using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string selectedIngredient;

    public static void SetIngredient(string ingredient)
    {
        selectedIngredient = ingredient;
    }
}