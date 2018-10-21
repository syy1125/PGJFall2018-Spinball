using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class QGBSelectorManager : MonoBehaviour
{
	private const float RAD = 180 / Mathf.PI;

	public static QGBSelectorManager Instance;

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

	public GameObject FallbackRendererPrefab;
	public float RenderScale = 20;
	public QuantumGyroBlade[] GyroBlades;
	public QuantumGyroBlade Chonnole;

	public InputField PasswordInput;
	public Text UnlockQGBText;
	public float UnlockNotifyResetTime = 4;
	private string _baseUnlockText;
	private Coroutine _unlockNotification;

	public string P1HorizontalAxisName;
	public string P1VerticalAxisName;
	public string P2HorizontalAxisName;
	public string P2VerticalAxisName;
	public float Interval = 1;

	public float MaxReadySpeed = 1;
	public float ReadyBuildupTime = 1;
	public int CountdownLength = 5;

	public GameObject CountdownDisplay;

	private String _countdownBaseText;
	private bool _inCountdown;

	public GameObject P1Preview;
	public Text P1Name;
	public Text P1PowerDisplay;
	public Text P1ResistanceDisplay;
	public Text P1AccelerationDisplay;
	public GameObject P1ReadyArrow;
	public GameObject P1UnreadyArrow;

	public GameObject P2Preview;
	public Text P2Name;
	public Text P2PowerDisplay;
	public Text P2ResistanceDisplay;
	public Text P2AccelerationDisplay;
	public GameObject P2ReadyArrow;
	public GameObject P2UnreadyArrow;

	private class PlayerSelectionState
	{
		public int Index;
		public bool Ready;
		public float Speed;
		public bool Complete;
		public float LastLeft;
		public float LastRight;

		public PlayerSelectionState()
		{
			Index = 0;
			Ready = false;
			Speed = 0;
			Complete = false;
			LastLeft = Time.time;
			LastRight = Time.time;
		}
	}

	private PlayerSelectionState _p1State;
	private PlayerSelectionState _p2State;

	private void Start()
	{
		_p1State = new PlayerSelectionState();
		_p2State = new PlayerSelectionState();
		_baseUnlockText = UnlockQGBText.text;
		_countdownBaseText = CountdownDisplay.GetComponentInChildren<Text>().text;
		CountdownDisplay.SetActive(false);
		_inCountdown = false;

		if (GameStateManager.Instance.ChonnoleUnlocked)
		{
			UnlockQGB(Chonnole);
		}

		UpdateUI();
	}

	private void UpdatePlayerSelection(
		PlayerSelectionState state,
		string horizontalAxisName,
		string verticalAxisName
	)
	{
		if (PasswordInput.isFocused) return;

		if (state.Ready)
		{
			if (Input.GetAxisRaw(verticalAxisName) < 0)
			{
				state.Ready = false;
				UpdateUI();
			}
		}
		else
		{
			if (Input.GetAxisRaw(verticalAxisName) > 0)
			{
				state.Ready = true;
				UpdateUI();
				return;
			}

			if (Input.GetAxisRaw(horizontalAxisName) < 0 && Time.time - state.LastLeft > Interval)
			{
				state.Index--;
				state.LastLeft = Time.time;
				while (state.Index < 0) state.Index += GyroBlades.Length;
				UpdateUI();
			}
			else if (Input.GetAxisRaw(horizontalAxisName) > 0 && Time.time - state.LastRight > Interval)
			{
				state.Index++;
				state.LastRight = Time.time;
				while (state.Index >= GyroBlades.Length) state.Index -= GyroBlades.Length;
				UpdateUI();
			}
			else if (Math.Abs(Input.GetAxisRaw(horizontalAxisName)) < 0.01)
			{
				state.LastLeft = Time.time - Interval;
				state.LastRight = Time.time - Interval;
			}
		}
	}

	private void SetPreview(Transform preview, GameObject playerRendererPrefab)
	{
		foreach (Transform child in preview)
		{
			Destroy(child.gameObject);
		}

		if (playerRendererPrefab == null) playerRendererPrefab = FallbackRendererPrefab;

		GameObject playerRenderer = Instantiate(playerRendererPrefab, preview);
		playerRenderer.transform.localScale *= RenderScale;
	}

	private void UpdateUI()
	{
		QuantumGyroBlade p1QGB = GyroBlades[_p1State.Index];
		SetPreview(P1Preview.transform, p1QGB.P1RendererPrefab);
		P1Name.text = p1QGB.Name;
		P1PowerDisplay.text = p1QGB.Power.ToString(CultureInfo.CurrentCulture);
		P1ResistanceDisplay.text = p1QGB.Resistance.ToString(CultureInfo.CurrentCulture);
		P1AccelerationDisplay.text = p1QGB.Acceleration.ToString(CultureInfo.CurrentCulture);
		P1ReadyArrow.SetActive(!_p1State.Ready);
		P1UnreadyArrow.SetActive(_p1State.Ready);

		QuantumGyroBlade p2QGB = GyroBlades[_p2State.Index];
		SetPreview(P2Preview.transform, p2QGB.P2RendererPrefab);
		P2Name.text = p2QGB.Name;
		P2PowerDisplay.text = p2QGB.Power.ToString(CultureInfo.CurrentCulture);
		P2ResistanceDisplay.text = p2QGB.Resistance.ToString(CultureInfo.CurrentCulture);
		P2AccelerationDisplay.text = p2QGB.Acceleration.ToString(CultureInfo.CurrentCulture);
		P2ReadyArrow.SetActive(!_p2State.Ready);
		P2UnreadyArrow.SetActive(_p2State.Ready);
	}

	private void UpdateSpinState(PlayerSelectionState state, Transform t)
	{
		if (state.Ready)
		{
			state.Speed += Time.deltaTime / ReadyBuildupTime * MaxReadySpeed;
			if (state.Speed > MaxReadySpeed)
			{
				state.Complete = true;
				state.Speed = MaxReadySpeed;
			}

			t.Rotate(Vector3.forward, state.Speed * Mathf.PI * 2 * Time.deltaTime * RAD);
		}
		else
		{
			state.Speed = 0;
			state.Complete = false;
			t.rotation = Quaternion.identity;
		}
	}

	private IEnumerator CountdownSequence()
	{
		_inCountdown = true;
		CountdownDisplay.SetActive(true);

		float startTime = Time.time;

		while (Time.time < startTime + CountdownLength)
		{
			if (!_p1State.Ready || !_p2State.Ready)
			{
				CountdownDisplay.GetComponentInChildren<Text>().text = _countdownBaseText;
				CountdownDisplay.gameObject.SetActive(false);
				_inCountdown = false;
				yield break;
			}

			CountdownDisplay.GetComponentInChildren<Text>().text = _countdownBaseText + Mathf.CeilToInt(startTime + CountdownLength - Time.time);

			yield return new WaitForEndOfFrame();
		}
		
		GameStateManager.Instance.GoToCombat(GyroBlades[_p1State.Index], GyroBlades[_p2State.Index]);
	}

	private void Update()
	{
		UpdatePlayerSelection(_p1State, P1HorizontalAxisName, P1VerticalAxisName);
		UpdatePlayerSelection(_p2State, P2HorizontalAxisName, P2VerticalAxisName);

		UpdateSpinState(_p1State, P1Preview.transform);
		UpdateSpinState(_p2State, P2Preview.transform);

		if (_p1State.Complete && _p2State.Complete && !_inCountdown)
		{
			StartCoroutine(CountdownSequence());
		}
	}

	public void OnPasswordInput(string pass)
	{
		if (pass.Equals("7639"))
		{
			GameStateManager.Instance.ChonnoleUnlocked = true;
			UnlockQGB(Chonnole);
		}
		if(pass.Equals("omega"))
		{
			GameStateManager.Instance.GoToOmega(GyroBlades[0], GyroBlades[0]);
		}

		PasswordInput.text = "";
	}

	private void UnlockQGB(QuantumGyroBlade toUnlock)
	{
		QuantumGyroBlade[] newGyroBlades = new QuantumGyroBlade[GyroBlades.Length + 1];

		for (int i = 0; i < GyroBlades.Length; i++)
		{
			newGyroBlades[i] = GyroBlades[i];
		}

		newGyroBlades[GyroBlades.Length] = toUnlock;
		GyroBlades = newGyroBlades;

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