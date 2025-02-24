using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectsSO> validkitchenObjectsSOList;

    private List<KitchenObjectsSO> kitchenObjectsSOList;

    private void Awake() {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO) {
        if (!validkitchenObjectsSOList.Contains(kitchenObjectsSO)) 
        { 
            return false;
        }
        if (kitchenObjectsSOList.Contains(kitchenObjectsSO))
        {
            return false;
        }
        kitchenObjectsSOList.Add(kitchenObjectsSO);
        return true;
    }
}
