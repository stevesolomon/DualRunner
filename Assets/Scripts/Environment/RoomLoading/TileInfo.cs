using UnityEngine;
using System.Collections;

public class TileInfo {

	public string prefabName;

	public int id;

	public TileInfo(string prefabName, int id) {
		this.prefabName = prefabName;
		this.id = id;
	}
}
