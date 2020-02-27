using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	string scene;
	AudioSource audio;
	public SceneFader sceneManager;
	public GameObject tutorialCanvas;
	public GameObject creditsCanvas;
	public Sprite firstPage;
	public Sprite secondPage;

	void Start()
	{
		
		audio = this.GetComponent<AudioSource>();
		tutorialCanvas.SetActive(false);
		creditsCanvas.SetActive(false);
	}

	public void LoadGame()
	{
		scene = "Diabetes";
		audio.Play();
		sceneManager.FadeTo(scene);
	}	

	public void Credits()
	{
		audio.Play();
		if (!creditsCanvas.activeInHierarchy)
		{
			creditsCanvas.SetActive(true);
		}

		else
		{
			creditsCanvas.SetActive(false);
		}
	}	

	public void Tutorial()
	{	
		audio.Play();
		tutorialCanvas.GetComponentInChildren<Image>().sprite = firstPage;
		tutorialCanvas.SetActive(true);
	}	

	public void Next()
	{
		audio.Play();
		if (tutorialCanvas.GetComponentInChildren<Image>().sprite == firstPage)
		{
			tutorialCanvas.GetComponentInChildren<Image>().sprite = secondPage;
		}

		else if (tutorialCanvas.GetComponentInChildren<Image>().sprite == secondPage)
		{
			tutorialCanvas.SetActive(false);
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
