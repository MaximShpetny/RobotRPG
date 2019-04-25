using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour 
{
	public GameObject fullscreenToggle;
	public GameObject resolutionSlider;
	public GameObject qualitySlider;

	public GameObject saveButton;
	public GameObject cancelButton;


	public bool isFullScreen;

	// Use this for initialization
	void Start() 
	{
		isFullScreen = Screen.fullScreen;
	}
	
	// Update is called once per frame
	void Update() 
	{

		saveButton.GetComponent<Button>().onClick.AddListener(SaveSettings);
		cancelButton.GetComponent<Button>().onClick.AddListener(HideSettings);

	}


	void ToggleFullscreen()
	{
		isFullScreen = !isFullScreen;

	}




	void SaveSettings()
	{

		Screen.fullScreen = isFullScreen;

		HideSettings();
	}

	void HideSettings()
	{
		GetComponent<Canvas>().enabled = false;
	}

}
