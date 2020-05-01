using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
 
  private Inventory inventory;
  public GameObject itemButton;

  private void Start(){
      inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); 
  }

  void OnTriggerEnter(Collider other){
      if(other.CompareTag("Player")){
          Debug.Log("HIT!");
          for (int i = 0; i < inventory.slots.Length; i++)
          {
              if (inventory.isFull[i] == false){
                  //Add item to inventory
                  inventory.isFull[i] = true;
                  Instantiate(itemButton, inventory.slots[i].transform, false);
                  Destroy(gameObject);
                  break;
              } 
          }
      }
  }
}
