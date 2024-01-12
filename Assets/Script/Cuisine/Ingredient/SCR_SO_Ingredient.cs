using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/So_Ingredient", order = 1)]
public class SCR_SO_Ingredient : ScriptableObject
{
    public enumAllIgredient myEnumIngredient;
    public Sprite mySpriteSO;
    public int stockSO;
}
