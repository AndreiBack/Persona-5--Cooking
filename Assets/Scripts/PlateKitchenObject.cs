using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler <OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectsSO kitchenObjectsSO;
    }

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

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectsSO = kitchenObjectsSO
        });
        return true;
    }

    public List<KitchenObjectsSO> GetKitchenObjectsSOList() {
        return kitchenObjectsSOList;
    }


}
