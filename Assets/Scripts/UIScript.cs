using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
	public Button playPauseButton; 
	public Sprite playImage;
	public Sprite pauseImage;
	Sprite currentImage;

//	public Scrollbar timelineScroll;
	Scrollbar timeline;

	public Toggle toggleGI;

	bool isPlaying =  true;
	bool GIOn = true;

	GameObject lightObject;
	Light sunLight;

	// Use this for initialization
	void Start () 
	{
		playPauseButton.GetComponent<Image>().sprite = pauseImage;
		currentImage = pauseImage;

		lightObject = GameObject.FindWithTag ("GILayer");
		timeline = lightObject.GetComponent<IntensityCycleScript> ().scroll;
		sunLight = lightObject.GetComponent<IntensityCycleScript> ().mainLight;

		if (timeline == null)
			print ("Cannot find scrollbar.");
		else
			print (timeline.name);
	}
	
	// Update is called once per frame
	void Update () 
	{
		float markerX = Input.GetAxis ("Mouse X");
		float scaleFactor = 5f;

		if (!isPlaying)
		{
			lightObject.GetComponent<IntensityCycleScript> ().dayRotateSpeed = new Vector3(markerX * scaleFactor, 0, 0);	//timelineScroll.enabled = false;
			lightObject.GetComponent<IntensityCycleScript>().nightRotateSpeed = lightObject.GetComponent<IntensityCycleScript>().dayRotateSpeed * 4;
		}
		else
			lightObject.GetComponent<IntensityCycleScript> ().ResetRotateSpeed ();	//timelineScroll.enabled = true;
	}

	public void GIDisabled()
	{
		if (GIOn) 
		{
			toggleGI.GetComponentInChildren<Text> ().text = "GI Off";
			sunLight.enabled = false;
		} 
		else 
		{
			toggleGI.GetComponentInChildren<Text> ().text = "GI On";
			sunLight.enabled = true;
		}
		
		GIOn = !GIOn;
	}


	public void PlayPause()
	{
		if (isPlaying)
			currentImage = playImage;
		else
			currentImage = pauseImage;

		playPauseButton.GetComponent<Image>().sprite = currentImage;
		isPlaying = !isPlaying;
	}
}
