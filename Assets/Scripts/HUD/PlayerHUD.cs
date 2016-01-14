using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	public GameController gameController;
	public Text heartRateIndicator;

	private HeartRateTracker heartRateTracker;
	private Color healthyColor = new Color(170f / 255, 1f, 73f / 255);
	private Color deadColor = new Color(88f / 255, 0, 38f / 255);
	private int startingFontSize;


	void Awake()
	{
		heartRateTracker = gameController.heartRateTracker;
		startingFontSize = heartRateIndicator.fontSize;
	}


	void OnEnable()
	{
		heartRateTracker.OnHeartRateUpdated += UpdateHeartRate;
	}


	void OnDisable()
	{
		heartRateTracker.OnHeartRateUpdated -= UpdateHeartRate;
	}


	void UpdateHeartRate(HeartRateTracker hrt, int newBPM)
	{
		StopAllCoroutines();
		StartCoroutine(CoLerpBPM(hrt, newBPM));
	}


	// TODO: Rewrite this 'cause this animation looks pretty gross.
	IEnumerator CoLerpBPM(HeartRateTracker hrt, int newBPM)
	{
		int oldBPM = System.Int32.Parse(heartRateIndicator.text);


		if(oldBPM < newBPM)
		{
			// Find out how big a hit it was.
			int iterations = (newBPM - oldBPM) / 4;


			// Enlarge the text proportionally by the strength of the hit.
			heartRateIndicator.fontSize = startingFontSize + iterations;


			for(int i = 1; i <= iterations; i ++)
			{
				// Drop the size by a bit.
				heartRateIndicator.fontSize--;


				// Reset the count.
				heartRateIndicator.text = (oldBPM + i).ToString();


				// Reset the color.
				UpdateColor(hrt, oldBPM + i);


				yield return new WaitForSeconds(Time.fixedDeltaTime);
			}
		}


		UpdateColor(hrt, newBPM);
		heartRateIndicator.fontSize = startingFontSize;
		heartRateIndicator.text = newBPM.ToString();


		yield return null;
	}


	void UpdateColor(HeartRateTracker hrt, int newBPM)
	{
		float closenessToDead = (float)(newBPM - hrt.startingHeartRate) / (hrt.lethalHeartRate - hrt.startingHeartRate);
		heartRateIndicator.color = Color.Lerp(healthyColor, deadColor, closenessToDead);
	}
}
