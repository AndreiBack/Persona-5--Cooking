using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour

{

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    private IKitchenObjectParent kitchenObjectParent;
    
    public KitchenObjectsSO GetKitchenObjectsSO() {
        return kitchenObjectsSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();

        }

        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("IKitchenObjectParent already has an object!!!");
        }
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject() ;
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }else
        {
            plateKitchenObject=null;
            return false;
        }
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectParent kitchenObjectParent) {
        if (kitchenObjectsSO == null)
        {
            Debug.LogError("KitchenObjectsSO está NULL ao tentar spawnar um objeto!");
            return null;
        }
        if (kitchenObjectsSO.prefab == null)
        {
            Debug.LogError("O prefab de KitchenObjectsSO está NULL! Defina no Inspector.");
            return null;
        }

        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }


}