using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
	[SerializeField] GameObject _shopGO;
	private int _selectedItem;
	private int _priceItem;
	private Player _player;

	[SerializeField] int _priceMusic;
	[SerializeField] int _priceExtraLifes;
	[SerializeField] int _priceKey;

	[SerializeField] Text _priceMusicText;
	[SerializeField] Text _priceExtraLifesText;
	[SerializeField] Text _priceKeyText;

	[SerializeField] private bool _purchasedMusic = false;

	public static event Action<int> OnPurchaseItem;
	public static event Action OnPurchaseKey;
	public static event Action OnPurchaseMusic;
	public static event Action OnPurchaseLifes;

	private void Start()
	{
		_selectedItem = 2;
		_priceItem = _priceKey;

		_priceMusicText.text = "" + _priceMusic + "G";
		_priceExtraLifesText.text = "" + _priceExtraLifes + "G";
		_priceKeyText.text = "" + _priceKey + "G";
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_player = other.GetComponent<Player>();

			if (_player != null)
			{
				UIManager.Instance.OpenShop(_player.gemsCollected);
			}

			_shopGO.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_shopGO.SetActive(false);
		}
	}

	public void SelectedItem(int item)
	{
		switch (item)
		{
			case 0:
				_selectedItem = 0;
				_priceItem = _priceMusic;
				UIManager.Instance.UpdateShopSelection(139);
				break;

			case 1:
				_selectedItem = 1;
				_priceItem = _priceExtraLifes;
				UIManager.Instance.UpdateShopSelection(26);

				break;

			case 2:
				_selectedItem = 2;
				_priceItem = _priceKey;
				UIManager.Instance.UpdateShopSelection(-71);
				break;
		}
	}

	public void BuyItem()
	{
		if (_player != null)
		{
			if (_priceItem <= _player.gemsCollected)
			{

				switch (_selectedItem)
				{
					case 0:
						if (_purchasedMusic == false)
						{
							_purchasedMusic = true;
							OnPurchaseMusic?.Invoke();

							OnPurchaseItem?.Invoke(-_priceItem);
						}
						break;

					case 1:
						if (_player.Health < 4)
						{
							OnPurchaseLifes?.Invoke();
							OnPurchaseItem?.Invoke(-_priceItem);
						}
						
						break;

					case 2:
						if (GameManager.Instance.HasKeyToCastel == false)
						{
							OnPurchaseKey?.Invoke();
							GameManager.Instance.HasKeyToCastel = true;
							OnPurchaseItem?.Invoke(-_priceItem);
						}
						
						break;
				}
			}
			else
				return;
		}
	}
}

