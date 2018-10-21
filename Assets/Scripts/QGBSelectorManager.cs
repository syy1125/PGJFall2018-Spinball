using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QGBSelectorManager : MonoBehaviour
{
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

	[FormerlySerializedAs("Fallback")] public Sprite FallbackSprite;
	public QuantumGyroBlade[] GyroBlades;
	public QuantumGyroBlade Chonnole;
	public InputField PasswordInput;

	public string P1HorizontalAxisName;
	public string P1VerticalAxisName;
	public string P2HorizontalAxisName;
	public string P2VerticalAxisName;
	public float Interval = 1;

	public Image P1Image;
	public Text P1Name;
	public Text P1Stats;
	public Image P1Background;

	public Image P2Image;
	public Text P2Name;
	public Text P2Stats;
	public Image P2Background;

	private readonly Regex _numberRegex = new Regex(@"\D");

	private class PlayerSelectionState
	{
		public int Index;
		public bool Ready;
		public float LastLeft;
		public float LastRight;

		public PlayerSelectionState()
		{
			Index = 0;
			Ready = false;
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

		UpdateUI();
	}

	private void UpdatePlayerSelection(
		PlayerSelectionState state,
		string horizontalAxisName,
		string verticalAxisName
	)
	{
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

	private string FormatStats(QuantumGyroBlade qgb)
	{
		return "Power: " + qgb.Power + "\n"
		       + "Resistance: " + qgb.Resistance + "\n"
		       + "Acceleration: " + qgb.Acceleration;
	}

	private Sprite GetSprite(GameObject prefab)
	{
		if (prefab == null) return FallbackSprite;
		
		SpriteRenderer playerSpriteRenderer = prefab.GetComponent<SpriteRenderer>();

		if (playerSpriteRenderer != null && playerSpriteRenderer.sprite != null)
		{
			return playerSpriteRenderer.sprite;
		}

		return FallbackSprite;
	}

	private void UpdateUI()
	{
		QuantumGyroBlade p1QGB = GyroBlades[_p1State.Index];
		P1Image.sprite = GetSprite(p1QGB.P1RendererPrefab);
		P1Name.text = p1QGB.Name;
		P1Stats.text = FormatStats(p1QGB);
		P1Background.color = _p1State.Ready ? Color.green : Color.white;

		QuantumGyroBlade p2QGB = GyroBlades[_p2State.Index];
		P2Image.sprite = GetSprite(p2QGB.P2RendererPrefab);
		P2Name.text = p2QGB.Name;
		P2Stats.text = FormatStats(p2QGB);
		P2Background.color = _p2State.Ready ? Color.green : Color.white;
	}

	private void Update()
	{
		UpdatePlayerSelection(_p1State, P1HorizontalAxisName, P1VerticalAxisName);
		UpdatePlayerSelection(_p2State, P2HorizontalAxisName, P2VerticalAxisName);
	}

	public void OnPasswordInputChange(string pass)
	{
		PasswordInput.text = _numberRegex.Replace(pass, "");

		if (PasswordInput.text.Equals("7639"))
		{
			UnlockQGB(Chonnole);
		}

		if (PasswordInput.text.Length >= 4)
		{
			PasswordInput.text = "";
		}
	}

	private void UnlockQGB(QuantumGyroBlade toUnlock)
	{
		Debug.Log("Unlocking " + toUnlock.Name);

		QuantumGyroBlade[] newGyroBlades = new QuantumGyroBlade[GyroBlades.Length + 1];

		for (int i = 0; i < GyroBlades.Length; i++)
		{
			newGyroBlades[i] = GyroBlades[i];
		}

		newGyroBlades[GyroBlades.Length] = toUnlock;
		GyroBlades = newGyroBlades;
	}
}