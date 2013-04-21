using UnityEngine;
using System.Collections;

public class GameScene : BaseScene
{
	public GameScene ()
	{
		
	}
	
	FTmxMap room1;
	FTilemap fTileMap;
	
	FCamObject fCamera;
	
	Player character;
	
	public override void HandleAddedToStage ()
	{
		Futile.instance.SignalUpdate += Update;
		
		base.HandleAddedToStage ();
	}
	
	public override void HandleRemovedFromStage ()
	{
		Futile.instance.SignalUpdate -= Update;
		base.HandleRemovedFromStage ();
	}
	
	public override void Start ()
	{
		room1 = new FTmxMap ();
		room1.LoadTMX ("CSVs/roomOne");
		
		AddChild (room1);
		
		FNode objectLayer = room1.getLayerNamed ("PlayerSpawn");
		FSprite f = (FSprite)((FContainer)objectLayer).GetChildAt (0);
		
		
		
		fTileMap = (FTilemap)room1.getLayerNamed ("Tile Layer 1");
		
		
		character = new Player(fTileMap);
		
		fCamera = new FCamObject();
		fCamera.follow(character);
		AddChild(fCamera);
		
		AddChild (character);
		
		character.x = f.x;
		character.y = f.y;
		
		base.Start ();
	}
	
	private void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			FSprite selectedTile = fTileMap.getTileAt (Input.mousePosition.x - Futile.stage.x - Futile.screen.halfWidth, Screen.height - Input.mousePosition.y + Futile.stage.y - Futile.screen.halfHeight);
			if (selectedTile != null)
			if (selectedTile.element.name.CompareTo ("tiles_2.png") != 0) {
				character.goToPos(new Vector2(selectedTile.x, selectedTile.y));
			}
		}
		
	}
	
	public override void AnimateOut (BaseScene nextScene)
	{
		base.AnimateOut (nextScene);
	}
}

