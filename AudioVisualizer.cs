/*
 * Brendan Voelz
 * 29 November 2016
 * Audio Visualizer script
 */

using UnityEngine;
using System.Collections;

public class AudioVisualizer : MonoBehaviour {
	public ParticleSystem particles;
	public GameObject prefab;
	public int numObjects = 20;
	public float radius = 5f;
	public GameObject[] cubes;


	void Start() {
		for (int i = 0; i < numObjects; i++) {
			float angle = i * Mathf.PI * 2 / numObjects;
			Vector3 pos = new Vector3 (Mathf.Cos (angle), 0, Mathf.Sin (angle)) * radius;
			Instantiate (prefab, pos, Quaternion.identity);
		}
		cubes = GameObject.FindGameObjectsWithTag ("Cubes");
	}

	// Update is called once per frame
	void Update () {
		float[] spectrum = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		for (int j = 0; j < numObjects; j++) {
			Vector3 previousScale = cubes [j].transform.localScale;
			previousScale.y = spectrum [j] * 125;
			cubes [j].transform.localScale = previousScale;
			if ((spectrum [j] * 100) > .75) {
				particles.startColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
				GameObject tempParticles = Instantiate(particles, cubes[j].transform.position, Quaternion.identity) as GameObject;
				Destroy (tempParticles, 1f);

			}
		}
	}
}
