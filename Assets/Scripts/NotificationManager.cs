using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
	public GameObject NotificationPrefab;
	public float PersistentTime = 4;
	public float FadeTime = 1;
	public float Displacement;

	// Testing code
//	private IEnumerator Start()
//	{
//		yield return new WaitForSeconds(2);
//		
//		DisplayNotification("Hello world!");
//		DisplayNotification("Test text");
//		
//		yield return new WaitForSeconds(1);
//		
//		DisplayNotification("Boop");
//		
//		yield return new WaitForSeconds(6);
//		
//		DisplayNotification("All notifications should have disappeared by now.");
//	}

	private IEnumerator NotificationLifecycle(string message)
	{
		GameObject notification = Instantiate(
			NotificationPrefab,
			transform
		);
		Text text = notification.GetComponent<Text>();
		text.text = message;

		yield return new WaitForSeconds(PersistentTime);

		float fadeStartTime = Time.time;

		while (Time.time - fadeStartTime < FadeTime)
		{
			text.color = new Color(
				text.color.r, text.color.g, text.color.b,
				1 - (Time.time - fadeStartTime) / FadeTime
			);

			yield return new WaitForEndOfFrame();
		}
		
		Destroy(notification);
	}

	public void DisplayNotification(string message)
	{
		foreach (Transform child in transform)
		{
			child.localPosition += Vector3.down * Displacement;
		}

		StartCoroutine(NotificationLifecycle(message));
	}
}