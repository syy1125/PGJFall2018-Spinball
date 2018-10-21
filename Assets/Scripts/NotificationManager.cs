using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
	public GameObject NotificationPrefab;
	public float PersistentTime = 4;
	public float FadeTime = 1;
	public float Displacement;
	public float LerpStrength = 0.8f;

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
		StartCoroutine(NotificationLifecycle(message));
	}

	private void Update()
	{
		for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
		{
			Vector3 currentPosition = transform.GetChild(childIndex).localPosition;

			transform.GetChild(childIndex).localPosition = Vector3.Lerp(
				currentPosition,
				Vector3.down * (transform.childCount - childIndex - 1) * Displacement,
				LerpStrength * Time.deltaTime
			);
		}
	}
}