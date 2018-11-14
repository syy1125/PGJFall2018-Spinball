using System;
using UnityEngine;

[Serializable]
public class QuantumGyroBlade
{
	public string Name = "Unknown";
	public string RendererAlias;
	public float Power = 1;
	public float Resistance = 1;
	public float Acceleration = 1;
	public string Secret;

	public GameObject LoadRenderer(string playerPrefix)
	{
		GameObject renderer = Resources.Load<GameObject>(
			"PlayerRenderers/" + playerPrefix + (RendererAlias != null ? RendererAlias : Name)
		);
		return renderer != null ? renderer : Resources.Load<GameObject>("PlayerRenderer/Fallback");
	}
}

[Serializable]
public class QGBManifest
{
	public QuantumGyroBlade[] QGBs;
}