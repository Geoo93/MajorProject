﻿using UnityEngine;
using System.Collections;

public class TransformerPickUp : MonoBehaviour {


	void OnCollisionEnter(Collision col){

		//print (gameObject.name + "  has collided with " + col.gameObject.name);

		if (col.gameObject.name == "Craft")
		{
			if (Items.finishItems.Count > 0) {


				//CharacterMeshComplete.tranformNum += 1;
				//Destroy (this.gameObject);
				Items.RemoveObjectFromList (this.gameObject, Items.transformerItems);
			}
		}

		//print ("transform: "+CharacterMeshComplete.TranformNum);
	}


}
