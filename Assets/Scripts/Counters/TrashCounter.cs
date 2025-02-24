using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(Player player) {

        if (player.HasKitchenObject())
        {   
            player.GetKitchenObject().DestroySelf();
            player.isHolding = false;
        }

    }
}
