using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{

    private void Awake()
    {
        //clears all static listeners || can find list in SoundManager.cs
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }

}
