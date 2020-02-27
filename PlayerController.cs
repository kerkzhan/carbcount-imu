using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	public Image bloodSugarImage;

	public float moveSpeed;
	public Vector2 playerPosition;
	public float maxBloodSugarLevel;
	public float currentBloodSugarLevel;
	public GameManager gameManager;
	public Button candyButton;
	public int counter;
	AudioSource audio;
	bool used = false;

	float scl = 1;

	// Use this for initialization
	void Start () 
	{
		counter = 0;
		audio = gameManager.GetComponent<AudioSource>();
		playerPosition = this.transform.position;
		maxBloodSugarLevel = 90.0f;
		currentBloodSugarLevel = 60f;
		StartCoroutine(UpdateBloodSugarLevel());
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = playerPosition;

		bloodSugarImage.fillAmount = GetSugarLevel();

		if (Input.GetKey(KeyCode.Space))
		{
			if (!used)
			{
				CandyPress();
			}
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			playerPosition.x -= moveSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			playerPosition.x += moveSpeed * Time.deltaTime;
		}

		if (this.transform.position.x >= gameManager.rightBorder - scl/2)
		{
			playerPosition.x = gameManager.rightBorder - scl/2;
			if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				playerPosition.x = gameManager.rightBorder - scl/2 - 0.000001f;
			}
		}

		if (this.transform.position.x <= gameManager.leftBorder + scl/2)
		{
			playerPosition.x = gameManager.leftBorder + scl/2;
			if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				playerPosition.x = gameManager.leftBorder + scl/2 + 0.000001f;
			}
		}

		if (currentBloodSugarLevel < 0 || currentBloodSugarLevel == 0)
		{
			currentBloodSugarLevel = 0;
			counter++;

		}

		if (currentBloodSugarLevel > maxBloodSugarLevel || currentBloodSugarLevel == maxBloodSugarLevel)
		{
			currentBloodSugarLevel = maxBloodSugarLevel;
			counter++;
		}

		if (currentBloodSugarLevel < maxBloodSugarLevel && currentBloodSugarLevel > 0)
		{
			counter = 0;
		}

	}

	IEnumerator UpdateBloodSugarLevel()
	{
		while (gameManager.gameState == GameManager.GameState.Running)
		{
			currentBloodSugarLevel -= 15.0f;
			yield return new WaitForSeconds(2);
		}
	}

	public float GetSugarLevel()
	{
		return currentBloodSugarLevel/maxBloodSugarLevel;
	}

	void CandyPress()
	{
		audio.Play();
		used = true;
		currentBloodSugarLevel += 15f;
		candyButton.gameObject.SetActive(false);
	}
}
