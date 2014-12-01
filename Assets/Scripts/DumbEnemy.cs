using UnityEngine;
using System.Collections;

public class DumbEnemy : Enemy {
    /// <summary>
    /// Case 0: An enemy has spawned sufficiently far enough on the left side of the screen to move steadily in a down-right direction.
    /// Case 1: An enemy will move straight down. It does not matter where an enemy has spawned.
    /// Case 2: An enemy has spawned sufficiently far enough on the right side of the screen to move steadily in a down-left direction.
    /// </summary>
    /// <param name="atm">Acronym for "Amount to move," which is how far the enemy is to move every frame update.</param>
	public override void MoveEnemy(float atm)
	{
		switch (Path)
		{
			case 0:
				transform.Translate(new Vector3(0.5f, 0f, -1f) * atm, Space.World);
				break;
			case 1:				
                transform.Translate(Vector3.back * atm, Space.World);
				break;
			case 2:
				transform.Translate(new Vector3(-0.5f, 0f, -1f) * atm, Space.World);
				break;
		}
	}
 
    
}
