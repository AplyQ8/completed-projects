using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	
	public void ChangeScene(int scene)
	{
		Debug.Log("Changing!");
		SceneManager.LoadScene(scene);
	}
	public void Exit()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void X3Button(GameObject button)
	{
		button.GetComponent<ThreeStrategy>().enabled = true;
	}

	public void WallButton(GameObject button)
	{
		button.GetComponent<WallStrategy>().enabled = true;
	}

	public void ColumnButton(GameObject button)
	{
		button.GetComponent<ColumnStrategy>().enabled = true;
	}

	public void BackButton()
	{
		GameObject.Find("Strategy").GetComponent<Context>().strategy = null;
		Destroy(GameObject.Find("Menu"));
	}

	public void BackFromBattle()
	{
		Destroy(GameObject.Find("Menu"));
		Destroy(GameObject.Find("Canvas"));
	}
}
