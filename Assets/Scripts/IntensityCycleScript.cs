using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

		if (scroll.value >= 1f)
			scroll.value = 0f;

		if (scroll.value >= 0.5f)
			rSpeed = nightRotateSpeed;
		else
			rSpeed = dayRotateSpeed;
		
		transform.Rotate (rSpeed * Time.deltaTime);
		scroll.value += rSpeed.x * Time.deltaTime / 360;
	}

	public void ResetRotateSpeed()
	{
		dayRotateSpeed = drs;
		nightRotateSpeed = nrs;
	}
}
