using UnityEngine;
using System.Collections;

public class GameScene : BaseScene
{
	public GameScene ()
	{
		
	}
	
	float timeDelta=0;
	
	float targetX = 0;
	float targetY = 0;
	
	FTmxMap room1;
	FTilemap fTileMap;
	
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
		Futile.stage.x = targetX = -f.x;
		Futile.stage.y = targetY = -f.y;
		
		
		fTileMap = (FTilemap)room1.getLayerNamed ("Tile Layer 1");
		
		
		base.Start ();
	}
	
	private void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			FSprite selectedTile = fTileMap.getTileAt (Input.mousePosition.x - Futile.stage.x - Futile.screen.halfWidth, Screen.height - Input.mousePosition.y + Futile.stage.y - Futile.screen.halfHeight);
			if (selectedTile != null)
			if (selectedTile.element.name.CompareTo ("tiles_1.png") == 0) {
				targetX = -selectedTile.x;
				targetY = -selectedTile.y;
			}
		}
		const float cameraSpeed = 5.0f;
		
		if(Futile.stage.x+cameraSpeed < targetX)
			Futile.stage.x += cameraSpeed;
		else
		if(Futile.stage.x-cameraSpeed > targetX)
			Futile.stage.x -= cameraSpeed;
		else
			Futile.stage.x = targetX;
		
		if(Futile.stage.y+cameraSpeed < targetY)
			Futile.stage.y += cameraSpeed;
		else
			if(Futile.stage.y-cameraSpeed > targetY)
				Futile.stage.y -= cameraSpeed;
		else
			Futile.stage.y = targetY;
		
	}
	
	public override void AnimateOut (BaseScene nextScene)
	{
		base.AnimateOut (nextScene);
	}
}

