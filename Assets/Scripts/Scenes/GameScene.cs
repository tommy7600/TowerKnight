using UnityEngine;
using System.Collections;

public class GameScene : BaseScene
{
	public GameScene ()
	{
		
	}
	
	float timeDelta = 0;
	float targetX = 0;
	float targetY = 0;
	FTmxMap room1;
	FTilemap fTileMap;
	FAnimatedSprite character;
	
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
		
		character = new FAnimatedSprite ("character");
		character.addAnimation (new FAnimation ("walkDown", new int[] {1,2}, 300, true));
		character.addAnimation (new FAnimation ("walkUp", new int[] {3,4}, 300, true));
		character.addAnimation (new FAnimation ("walkRight", new int[] {5,6}, 300, true));
		character.addAnimation (new FAnimation ("walkLeft", new int[] {7,8}, 300, true));
		character.play ("walkDown");
		
		AddChild (character);
		
		
		FNode objectLayer = room1.getLayerNamed ("PlayerSpawn");
		FSprite f = (FSprite)((FContainer)objectLayer).GetChildAt (0);
		targetX = f.x;
		targetY = f.y;
		
		character.x = f.x;
		character.y = f.y;
		
		
		fTileMap = (FTilemap)room1.getLayerNamed ("Tile Layer 1");
		
		
		base.Start ();
	}
	
	private void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			FSprite selectedTile = fTileMap.getTileAt (Input.mousePosition.x - Futile.stage.x - Futile.screen.halfWidth, Screen.height - Input.mousePosition.y + Futile.stage.y - Futile.screen.halfHeight);
			if (selectedTile != null)
			if (selectedTile.element.name.CompareTo ("tiles_1.png") == 0) {
				targetX = selectedTile.x;
				targetY = selectedTile.y;
			}
		}
		const float cameraSpeed = 5.0f;
		
		bool moving = false;
		
		if (character.x + cameraSpeed < targetX) {
			character.play ("walkRight");
			moving = true;
			character.x += cameraSpeed;
		} else if (character.x - cameraSpeed > targetX) {
			character.play ("walkLeft");
			moving = true;
			character.x -= cameraSpeed;
		} else
			Futile.stage.x = targetX;
		
		if (character.y + cameraSpeed < targetY) {
			if (!moving)
				character.play ("walkUp");
			moving = true;
			character.y += cameraSpeed;
		} else if (character.y - cameraSpeed > targetY) {
			if (!moving)
				character.play ("walkDown");
			moving = true;
			character.y -= cameraSpeed;
		} else
			character.y = targetY;
		if (!moving)
			character.play ("walkDown");
		Futile.stage.x = -character.x;
		Futile.stage.y = -character.y;
		
	}
	
	public override void AnimateOut (BaseScene nextScene)
	{
		base.AnimateOut (nextScene);
	}
}

