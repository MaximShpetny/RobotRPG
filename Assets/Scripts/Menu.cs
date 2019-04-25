using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour 
{

	public bool isOpened = false;

	public float volume = 0;

	public int quality = 0;

	public bool isFullscreen = false;

	public AudioMixer audioMixer;

	public Dropdown resolutionDropdown;

	private Resolution[] resolutions;

	private int currResolutionIndex = 0;

	void Start()
	{

		//Getting available screen resolutions to fill resolutionsDropdown in settings menu
		resolutionDropdown.ClearOptions();

		resolutions = Screen.resolutions;

		List<string> options = new List<string> ();

		for(int i = 0; i < resolutions.Length; i++) 
		{
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add(option);

			if(resolutions[i].Equals(Screen.currentResolution)) 
			{
				currResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);

		resolutionDropdown.value = currResolutionIndex;

		resolutionDropdown.RefreshShownValue();

	}


	void Update() 
	{
		if(Input.GetKey(KeyCode.Escape)) 
		{
			ShowHideMenu();
		}
	}

	public void GoToMain()
	{
		SceneManager.LoadScene("Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ShowHideMenu()
	{
		isOpened = !isOpened;
		GetComponent<Canvas> ().enabled = isOpened;
	}

	//New Game



	//Settings


	public void ChangeVolume(float val)
	{
		volume = val;
	}

	public void ChangeResolution(int index)
	{
		currResolutionIndex = index;
	}

	public void ChangeFullscreenMode(bool val)
	{
		isFullscreen = val;
	}

	public void ChangeQuality(int index)
	{
		quality = index;
	}

	public void SaveSettings()
	{
		audioMixer.SetFloat("MasterVolume", volume);
		QualitySettings.SetQualityLevel(quality);
		Screen.fullScreen = isFullscreen;
		Screen.SetResolution(Screen.resolutions[currResolutionIndex].width, Screen.resolutions[currResolutionIndex].height, isFullscreen);
	}


	//Load
	public void LoadGame()
	{
		
	}


	//Save
	public void SaveGame()
	{
		
	}



}
