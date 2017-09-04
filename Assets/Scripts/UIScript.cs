using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
	public Button playPauseButton; 		// play pause button
	public Sprite playImage;
	public Sprite pauseImage;
	Sprite currentImage;

//	public Scrollbar timelineScroll;
	Scrollbar timeline;					// slider 

	public Toggle toggleGI;				// checkbox for GI (= Global Illumination)

	bool isPlaying =  true;
	bool GIOn = true;

	GameObject lightObject;
	Light sunLight;						// sun

	// Use this for initialization
	void Start () 
	{
		playPauseButton.GetComponent<Image>().sprite = pauseImage;
		currentImage = pauseImage;

		// getting GameObjects in scene
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


	// updates text and disables/enables GI in scene on unchecking/checking the toggle
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

	// action to take on clicking the play/pause button
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
