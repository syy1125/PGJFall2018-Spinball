using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QGBSelection : MonoBehaviour
{
	private const float RAD = 180 / Mathf.PI;

	public static QGBSelection Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public TextAsset QGBManifest;
	public QuantumGyroBlade[] GyroBlades { get; private set; }
	public bool ClearUnlockOnLaunch;

	[FormerlySerializedAs("PasswordInput")]
	public InputField Password;

	public Text UnlockQGBText;
	public float UnlockNotifyResetTime = 4;
	private string _baseUnlockText;
	private Coroutine _unlockNotification;

	public int CountdownLength = 5;

	public GameObject CountdownDisplay;

	public PlayerSelection P1Selection;
	public PlayerSelection P2Selection;
	private String _countdownBaseText;
	private bool _inCountdown;

	private void Start()
	{
		_baseUnlockText = UnlockQGBText.text;
		_countdownBaseText = CountdownDisplay.GetComponentInChildren<Text>().text;
		CountdownDisplay.SetActive(false);
		_inCountdown = false;

		GyroBlades = JsonUtility.FromJson<QGBManifest>(QGBManifest.text).QGBs;
		if (ClearUnlockOnLaunch) PlayerPrefs.DeleteKey("Unlocks");
	}

	private IEnumerator CountdownSequence()
	{
		_inCountdown = true;
		CountdownDisplay.SetActive(true);

		float startTime = Time.time;

		while (Time.time < startTime + CountdownLength)
		{
			if (!P1Selection.Ready || !P2Selection.Ready)
			{
				CountdownDisplay.GetComponentInChildren<Text>().text = _countdownBaseText;
				CountdownDisplay.gameObject.SetActive(false);
				_inCountdown = false;
				yield break;
			}

			CountdownDisplay.GetComponentInChildren<Text>().text =
				_countdownBaseText + Mathf.CeilToInt(startTime + CountdownLength - Time.time);

			yield return null;
		}

		GameStateManager.Instance.GoToCombat(GyroBlades[P1Selection.Index], GyroBlades[P2Selection.Index]);
	}

	private void Update()
	{
		if (P1Selection.Complete && P2Selection.Complete && !_inCountdown)
		{
			StartCoroutine(CountdownSequence());
		}
	}

	public void OnPasswordInput(string pass)
	{
		foreach (QuantumGyroBlade qgb in GyroBlades)
		{
			if (qgb.Secret != null && qgb.Secret.Equals(pass))
			{
				UnlockQGB(qgb);
				return;
			}
		}

		if (pass.Equals("omega"))
		{
			GameStateManager.Instance.GoToOmega(GyroBlades[0], GyroBlades[0]);
		}

		Password.text = "";
	}

	public static bool IsUnlocked(QuantumGyroBlade qgb)
	{
		if (qgb.Secret == null) return true;

		string[] unlocks = PlayerPrefs.GetString("Unlocks", "").Split(':');
		return Array.BinarySearch(unlocks, qgb.Name) >= 0;
	}

	private void UnlockQGB(QuantumGyroBlade toUnlock)
	{
		string[] unlocks = PlayerPrefs.GetString("Unlocks", "").Split(':');

		Array.Resize(ref unlocks, unlocks.Length + 1);
		unlocks[unlocks.Length - 1] = toUnlock.Name;

		Array.Sort(unlocks);
		PlayerPrefs.SetString("Unlocks", string.Join(":", unlocks));

		if (_unlockNotification != null)
		{
			StopCoroutine(_unlockNotification);
		}

		_unlockNotification = StartCoroutine(NotifyUnlock(toUnlock.Name));
	}

	private IEnumerator NotifyUnlock(string qgbName)
	{
		UnlockQGBText.text = "Unlocked: " + qgbName;

		yield return new WaitForSeconds(UnlockNotifyResetTime);

		UnlockQGBText.text = _baseUnlockText;
		_unlockNotification = null;
	}
}