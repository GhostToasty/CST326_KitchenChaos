using System.Collections.Generic;
using UnityEngine;

//commented out so we don't create any more recipes in the game engine 
// [CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    //list of all possible recipes 
    public List<RecipeSO> recipeSOList;
}
