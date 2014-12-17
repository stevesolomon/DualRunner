using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class RoomLoader {

	private Dictionary<int, TileInfo> tileDictionary = new Dictionary<int, TileInfo>(128);

	public GameObject LoadRoom(string xmlText) {
		tileDictionary.Clear();

		var xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xmlText);

		var mapNode = xmlDoc.SelectSingleNode("//map");
		var tilesetNode = mapNode.SelectSingleNode("tileset");
		var roomNode = mapNode.SelectNodes("layer/data/tile");

		//Parse in our relevant map details.
		int width = int.Parse(mapNode.Attributes["width"].InnerText),
			height = int.Parse(mapNode.Attributes["height"].InnerText), 
			tileWidth = int.Parse(mapNode.Attributes["tilewidth"].InnerText), 
			tileHeight = int.Parse(mapNode.Attributes["tileheight"].InnerText),
			firstGid = int.Parse(tilesetNode.Attributes["firstgid"].InnerText),
			difficulty = int.Parse(mapNode.SelectSingleNode("properties/property[@name='difficulty']").Attributes["value"].InnerText);

		BuildTileInfo(tilesetNode.SelectNodes("tile"), firstGid);

		var room = GenerateRoom(roomNode, width, height, tileWidth, tileHeight, difficulty);

		return room;
	}

	private GameObject GenerateRoom(XmlNodeList roomTiles, int width, int height, int tileWidth, int tileHeight, int difficulty) {
		int i = -1;

		var room = GameObject.Instantiate(Resources.Load("Rooms/BlankRoom")) as GameObject;
		room.SetActive(false);

		var roomInfo = room.GetComponent<RoomInfo>();
		roomInfo.difficulty = difficulty;

		//Build up our prefabs one-by-one in the roomNode.
		foreach (XmlNode node in roomTiles) {
			i++;

			var tileId = int.Parse(node.Attributes["gid"].InnerText);

			if (tileId == 0) //empty tile
				continue;
			else if (!tileDictionary.ContainsKey(tileId)) {
				Debug.LogWarning("Could not find tile with id: " + tileId + " in tile dictionary!");
				continue;
			}

			//Yank the tile info from the map and instantiate it!
			var tileInfo = tileDictionary[tileId];
			var position = new Vector3((i % width) * tileWidth, (height - (i / width) - 1) * tileHeight, 0f);

			var gameObject = GameObject.Instantiate(Resources.Load(tileInfo.prefabName), position, Quaternion.identity) as GameObject;
			gameObject.transform.SetParent(room.transform);
		}

		return room;
	}

	private void BuildTileInfo(XmlNodeList tilesXml, int firstGid) {
		if (tilesXml == null) 
			return;

		//Loop through and build up our tile info.
		foreach (XmlNode tileXml in tilesXml) {
			var id = int.Parse(tileXml.Attributes["id"].InnerText);
			
			var prefabName = tileXml.SelectSingleNode("properties/property[@name='prefab']").Attributes["value"].InnerText;
			
			var tileInfo = new TileInfo(prefabName, id + firstGid);
			tileDictionary.Add(tileInfo.id, tileInfo);
		}

	}

}
