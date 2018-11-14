using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
	public string HorizontalAxisName;
	public string VerticalAxisName;

	private PlayerSelectionConfig _config;
	private QGBSelection _selection;

	public int Index { get; private set; }
	public bool Ready { get; private set; }
	public bool Complete { get; private set;  }

	private QuantumGyroBlade _selectedQGB
	{
		get { return _selection.GyroBlades[Index]; }
	}

	private Coroutine _moveLeft;
	private Coroutine _moveRight;

	public string PlayerPrefix;
	public GameObject Preview;
	public Text Name;
	public Text Power;
	public Text Resistance;
	public Text Acceleration;
	public GameObject ReadyArrow;
	public GameObject UnreadyArrow;

	private IEnumerator Start()
	{
		_config = PlayerSelectionConfig.Instance;
		_selection = QGBSelection.Instance;

		Index = 0;

		// Start control coroutines
		StartCoroutine(ReadyControl());
		StartCoroutine(SelectionControl(-1));
		StartCoroutine(SelectionControl(1));

		// Delay by a frame to allow `QGBSelection` to initialize before rendering
		yield return null;
		InitUI();
	}

	private void InitUI()
	{
		UpdatePreview();
		UpdateStats();
		
		ReadyArrow.SetActive(true);
		UnreadyArrow.SetActive(false);
	}

	private void UpdatePreview()
	{
		foreach (Transform child in Preview.transform)
		{
			Destroy(child.gameObject);
		}

		GameObject rendererPrefab = _selectedQGB.LoadRenderer(PlayerPrefix);

		if (rendererPrefab == null) rendererPrefab = _config.FallbackPrefab;

		GameObject playerRenderer = Instantiate(rendererPrefab, Preview.transform);
		playerRenderer.transform.localScale *= _config.RenderScale;

		// An `Image` works much better than a `Sprite` when it comes to UI. Switch preview to using image.
		Image image = playerRenderer.GetComponent<Image>();
		if (image == null)
		{
			image = playerRenderer.AddComponent<Image>();
			image.sprite = playerRenderer.GetComponent<SpriteRenderer>().sprite;
		}

		image.enabled = true;
		playerRenderer.GetComponent<SpriteRenderer>().enabled = false;
	}

	private void UpdateStats()
	{
		Name.text = _selectedQGB.Name;
		Power.text = _selectedQGB.Power.ToString(CultureInfo.CurrentCulture);
		Resistance.text = _selectedQGB.Resistance.ToString(CultureInfo.CurrentCulture);
		Acceleration.text = _selectedQGB.Acceleration.ToString(CultureInfo.CurrentCulture);
	}

	private IEnumerator ReadyControl()
	{
		while (true)
		{
			yield return new WaitUntil(() => !_selection.Password.isFocused && Input.GetAxisRaw(VerticalAxisName) > 0);

			Ready = true;
			Coroutine spin = StartCoroutine(ReadySpin()); // Will invoke `OnReadyChange`

			ReadyArrow.SetActive(false);
			UnreadyArrow.SetActive(true);

			yield return new WaitUntil(() => !_selection.Password.isFocused && Input.GetAxisRaw(VerticalAxisName) < 0);

			Ready = false;
			StopCoroutine(spin);
			Complete = false;

			Preview.transform.rotation = Quaternion.identity;
			ReadyArrow.SetActive(true);
			UnreadyArrow.SetActive(false);
		}
	}

	private IEnumerator ReadySpin()
	{
		float startTime = Time.time;

		while (Time.time - startTime < _config.BuildupTime)
		{
			float speed = Mathf.Lerp(0, _config.MaxSpeed, (Time.time - startTime) / _config.BuildupTime);
			Preview.transform.Rotate(Vector3.forward, speed * Time.deltaTime * 360);
			yield return null;
		}

		Complete = true;

		while (true)
		{
			Preview.transform.Rotate(Vector3.forward, _config.MaxSpeed * Time.deltaTime * 360);
			yield return null;
		}
	}

	private IEnumerator SelectionControl(int direction)
	{
		while (true)
		{
			yield return new WaitUntil(
				() => !Ready && !_selection.Password.isFocused && Input.GetAxisRaw(HorizontalAxisName) * direction > 0);

			Coroutine move = StartCoroutine(MoveRepeat(direction));

			yield return new WaitUntil(() =>
				Ready || !_selection.Password.isFocused && Input.GetAxisRaw(HorizontalAxisName) * direction <= 0);

			StopCoroutine(move);
		}
	}

	private IEnumerator MoveRepeat(int direction)
	{
		for (int index = 0;; index = Mathf.Min(index + 1, _config.IntervalCurve.Length - 1))
		{
			MoveSelection(direction);
			yield return new WaitForSeconds(_config.IntervalCurve[index]);
		}
	}

	private void MoveSelection(int direction)
	{
		do
		{
			Index += direction;
			if (Index < 0) Index += _selection.GyroBlades.Length;
			if (Index >= _selection.GyroBlades.Length) Index -= _selection.GyroBlades.Length;
		}
		while (!QGBSelection.IsUnlocked(_selectedQGB));

		UpdatePreview();
		UpdateStats();
	}
}