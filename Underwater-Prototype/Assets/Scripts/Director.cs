using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Director : MonoBehaviour {
	private GameObject storyCanvas1, storyCanvas2, startCanvas;

	void Awake() {
		storyCanvas1 = GameObject.Find("StoryCanvas1");
		storyCanvas2 = GameObject.Find("StoryCanvas2");
		startCanvas = GameObject.Find("StartCanvas");
	}

	void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		storyCanvas1.SetActive(true);
		storyCanvas2.SetActive(false);
		startCanvas.SetActive(false);
	}

	public void switchToSecond(){
		storyCanvas1.SetActive(false);
		storyCanvas2.SetActive(true);
	}

	public void switchToStart(){
		storyCanvas2.SetActive(false);
		startCanvas.SetActive(true);
	}

}