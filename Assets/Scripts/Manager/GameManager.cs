using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError("GameManager is NULL.");
			}
			return _instance;
		}
	}

	public bool HasKeyToCastel { get; set; }
	public bool GameOver { get; set; }

	private void Awake()
	{
		_instance = this;
		GameOver = false;
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}

/*
 * -------------------------Events and Listeners
 * 
 *    PLAYER      | PLAYERANIM |        SHOP         | FALLINGZONE   | EXTRAJUMP          | AudioManager| DIAMOND		 |    END     |  UIManager
 *  CollectGems   |            |  OnPurchaseItem(Int)|			     |					  |			    |				 |            | 
 *				  |            |  OnPurchaseKey      |		 	     |					  |             |				 |            | ActivateKey
 *	 AddLifes     |            |  OnPurchaseLifes    |			     |					  |             |				 |            |
 *				  |            |  OnPurchaseMusic    |			     |					  | PlayAudio   |				 |            |  
 *----------------------------------------------------------------------------------------------------------------------------------------------
 * OnMove(Float)  |  Move      |                     |			     |					  |             |				 |            |
 * OnJumping(Bool)|  Jump      |                     |			     |					  |             |				 |            |
 * OnAttacking	  |  Attack    |                     |			     |					  |             |				 |            |
 * Onhit 		  |  Hit       |                     |			     |					  |             |				 |            |
 * OnDeath(Bool)  |  Death     |                     |			     |					  |             |				 |            |
 -----------------------------------------------------------------------------------------------------------------------------------------------
 * Fallen         |            |                     | OnFallingZone |					  |			    | OnGemCollect   |            |       
 -----------------------------------------------------------------------------------------------------------------------------------------------
 * CollectGems	  |            |					 |  			 |			          |			    |                |            |					 
 -----------------------------------------------------------------------------------------------------------------------------------------------
 * HigherJump     |            |					 |               | OnExtraJump(Float) |			    |                |   	      |
 * NormalJump	  |			   |					 |				 |                    |             | 	             |            |
 -----------------------------------------------------------------------------------------------------------------------------------------------
 *          	  |	GameOver   |					 |				 |                    |             | 	             | OnGameOver |
 *
 */