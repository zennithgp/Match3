using UnityEngine;
using System.Collections;

public class ThreeInputManagerScript : InputManagerScript {

	public override void SelectToken(){
		if(Input.GetMouseButtonDown(0)){
			//when you click, check where on the screen you're clicking
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//use a 2D raycast to see what you're clicking on
			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			if(collider != null){
				//if you click on something...
				if(selected == null){
					//if we haven't yet selected a token, select this token and remember it
					selected = collider.gameObject;				
				} else {
					//if we HAVE already selected a token, calculate the distance between this token (which we're currently clicking on)
					//and that one (which we clicked on last time)
					Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
					Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

					//CHANGE:
					//If they're exactly three spaces away horizontally, vertically, or diagonally, swap them
					if(
						//Check if they're exactly three spaces apart horizontally
						((Mathf.Abs(pos1.x - pos2.x)) == 3 && Mathf.Abs(pos1.y - pos2.y) == 0)
						//Check if they're exactly three spaces apart vertically
						|| ((Mathf.Abs(pos1.x - pos2.x) == 0) && (Mathf.Abs(pos1.y - pos2.y) == 3))
						//Check if they're exactly three spaces apart diagonally
						|| ((Mathf.Abs(pos1.x - pos2.x) == 3) && (Mathf.Abs(pos1.y - pos2.y) == 3))
					)
					{
						moveManager.SetupTokenExchange (selected, pos1, collider.gameObject, pos2, true);
					}

//
//					//if they're next to each other, swap them
//					if(Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y) == 1){
//						moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
//					}


					//then deselect our current token (because we're about to destroy or forget it)
					selected = null;
				}
			}
		}

	}




}
