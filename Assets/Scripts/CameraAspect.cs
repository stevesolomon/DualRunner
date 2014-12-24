using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {

	public float orthographicSize = 8;
	public float aspect = 0.125f;

	private Camera camera;
	
	void Start()
	{
		camera = this.GetComponent<Camera>();

		camera.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			camera.nearClipPlane, camera.farClipPlane);
	}

	void Update() {
		camera.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			camera.nearClipPlane, camera.farClipPlane);

	}
}
