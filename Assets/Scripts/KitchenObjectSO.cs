using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    //info that's attached to each kitchen object 
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
