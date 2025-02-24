using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;


    public override void Interact(Player player) {
        if (!player.HasKitchenObject())
        {  //player not carrying anything
            Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
            KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);
        }
    }
}
