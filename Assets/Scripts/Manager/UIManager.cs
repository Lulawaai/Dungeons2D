using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance;
	public static UIManager Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError("UIManager is NULL.");
			}
			return _instance;
		}
	}

	[SerializeField] private Text _playerGemCount;
	[SerializeField] private Image _selectionImage;
	[SerializeField] private Text _playerGemCountUI;
	[SerializeField] private List<GameObject> _lifes = new List<GameObject>();
	[SerializeField] private GameObject _winningGO;
	[SerializeField] private GameObject _keyGO;
	[SerializeField] private GameObject _gameOverGO;

	private void Awake()
	{
		_instance = this;
	}

	private void OnEnable()
	{
		Shop.OnPurchaseKey += ActivateKey;
	}

	public void GemCount(int gems)
	{
		_playerGemCountUI.text = "" + gems;
	}

	public void OpenShop(int gemCount)
	{
		_playerGemCount.text = "Total GEMS: " + gemCount + "G";
	}

	public void UpdateShopSelection(int yPos)
	{
		_selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
	}

	public void RemoveLivesUI(int lifesRemaining)
	{
		_lifes[lifesRemaining].SetActive(false);
	}

	public void AddLifesUI(int lifesExtra)
	{
		_lifes[lifesExtra].SetActive(true);
	}

	public void WinningGame()
	{
		_winningGO.SetActive(true);
	}

	private void ActivateKey()
	{
		_keyGO.SetActive(true);
	}

	public void GameOver()
	{
		_gameOverGO.SetActive(true);
	}

	private void OnDisable()
	{
		Shop.OnPurchaseKey += ActivateKey;
	}
}
