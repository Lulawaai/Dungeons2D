using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject _menuPanelGO;

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void MenuInstructions()
	{
		_menuPanelGO.SetActive(true);
	}

	public void BackMainMenu()
	{
		_menuPanelGO.SetActive(false);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
