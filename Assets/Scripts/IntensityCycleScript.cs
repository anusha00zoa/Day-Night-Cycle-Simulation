using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// script to adjust the intensity and position of the sun as it moves from sunrise to sunset
// at the same time, moves the slider along
public class IntensityCycleScript : MonoBehaviour 
{
	public Vector3 dayRotateSpeed;
	[HideInInspector]
	public Vector3 nightRotateSpeed;
	[HideInInspector]
	public Vector3 rSpeed;

	Vector3 drs, nrs;

	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	//[HideInInspector]
	public Light mainLight;

	public Scrollbar scroll;

	// Use this for initialization
	void Start ()
	{
		//mainLight = transform;

		nightRotateSpeed = dayRotateSpeed * 5;
		drs = dayRotateSpeed;
		nrs = nightRotateSpeed;
	}

	// Update is called once per frame
	void Update () 
	{
		// gradually manipulates the intensity of the sun light
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

		// check for position of slider in scene that is moving as the sun progresses throught the day
		if (scroll.value >= 1f)
			scroll.value = 0f;

		// switches to night as slider has moved half the distance 
		if (scroll.value >= 0.5f)
			rSpeed = nightRotateSpeed;
		else
			rSpeed = dayRotateSpeed;

		// translate and rotate the sun from sunrise to sunset in each frame
		transform.Rotate (rSpeed * Time.deltaTime);
		scroll.value += rSpeed.x * Time.deltaTime / 360;
	}

	// method to restore the original speeds
	public void ResetRotateSpeed()
	{
		dayRotateSpeed = drs;
		nightRotateSpeed = nrs;
	}
}
