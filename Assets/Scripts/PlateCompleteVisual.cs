using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject {

        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject GameObject;

    }


    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;


    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        
            foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
            {
                    kitchenObjectSO_GameObject.GameObject.SetActive(false);
            }

        }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
      foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
        {
            if(kitchenObjectSO_GameObject.kitchenObjectsSO == e.kitchenObjectsSO)
            {
                kitchenObjectSO_GameObject.GameObject.SetActive(true);
            }
        }
    }
}
