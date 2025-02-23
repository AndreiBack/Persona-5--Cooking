using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {


    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject())
        {
            // There is no Kitchen object here

            if (player.HasKitchenObject())
            {   //Player is carring something
                player.GetKitchenObject().SetKitchenObjectParent(this);
                player.isHolding = false;
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {   // There is a Kitchen object here
            if (player.HasKitchenObject())
            {   //Player is carring something

            }
            else
            {
                //player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }


    }
}