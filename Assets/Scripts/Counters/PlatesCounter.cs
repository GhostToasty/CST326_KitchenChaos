using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;


    private void Update()
    {
        //spawns a plate after a certain amount of time 
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            
            if (platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        
        }
    }


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty-handed
            if (platesSpawnAmount > 0)
            {
                //there's at least one plate on the counter
                //removes a plate from the count
                platesSpawnAmount--;

                //spawns KitchenObject in player's hand instead of just the visuals on the counter
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                //player has removed plate from the stack
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
