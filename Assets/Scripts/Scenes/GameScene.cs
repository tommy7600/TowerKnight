using UnityEngine;
using System.Collections;

public class GameScene : BaseScene , FMultiTouchableInterface
{
	public GameScene ()
	{
		
	}
	
	FTmxMap room1;
	FTilemap fTileMap;
	FCamObject fCamera;
	Player character;
	FStage tilemapStage;
	
	public override void HandleAddedToStage ()
	{
		Futile.instance.SignalUpdate += Update;
		Futile.touchManager.AddMultiTouchTarget (this);
		
		base.HandleAddedToStage ();
	}
	
	public override void HandleRemovedFromStage ()
	{
		Futile.instance.SignalUpdate -= Update;
		Futile.touchManager.RemoveMultiTouchTarget (this);
		base.HandleRemovedFromStage ();
	}
	
	public override void Start ()
	{
		
		tilemapStage = new FStage ("tilemap");
		room1 = new FTmxMap ();
		room1.LoadTMX ("CSVs/roomOne");
		
		Futile.AddStage (tilemapStage);
		Futile.AddStageAtIndex (tilemapStage, 0);
		tilemapStage.AddChild (room1);
		
		FNode objectLayer = room1.getLayerNamed ("PlayerSpawn");
		FSprite f = (FSprite)((FContainer)objectLayer).GetChildAt (0);
		f.RemoveFromContainer ();
		
		fTileMap = (FTilemap)room1.getLayerNamed ("Tile Layer 1");
		
		character = new Player (fTileMap);
		
		fCamera = new FCamObject ();
		fCamera.follow (character);
		AddChild (fCamera);
		
		AddChild (character);
		
		character.x = f.x;
		character.y = f.y;
		
		base.Start ();
	}
	
	private void Update ()
	{
		const float speed = 1.0f;
		
		if (Input.GetKey (KeyCode.LeftArrow))
			character.x -= speed;
		if (Input.GetKey (KeyCode.RightArrow))
			character.x += speed;
		if (Input.GetKey (KeyCode.DownArrow))
			character.y -= speed;
		if (Input.GetKey (KeyCode.UpArrow))
			character.y += speed;
		
		tilemapStage.x = -(int)fCamera.x;
		tilemapStage.y = -(int)fCamera.y;
	}
	
	private bool isWalkable (int tileFrame)
	{
		switch (tileFrame) {
		case 1:
		case 3:
			return true;
		default:
			return false;
		}
	}
	
	public override void AnimateOut (BaseScene nextScene)
	{
		base.AnimateOut (nextScene);
	}
	
	public void HandleMultiTouch (FTouch[] touches)
	{
		if (touches.Length > 0 && touches [0].phase == TouchPhase.Ended) {
			int xPos = (int)(touches [0].position.x - Futile.stage.x);
			int yPos = -(int)(touches [0].position.y - Futile.stage.y);
			int xTile = (int)(xPos / (fTileMap.tileWidth * Futile.displayScale));
			int yTile = (int)(yPos / (fTileMap.tileWidth * Futile.displayScale));
			int selectedTileFrame = fTileMap.getTileFrame (xTile, yTile);
			
			Debug.Log ("Tile Clicked: mouse(" + xPos + "," + yPos + ")  tile(" + xTile + "," + yTile + ") tiletype(" + selectedTileFrame + ")");
			FSprite selectedTile = fTileMap.getTile (xTile, yTile);
			if (isWalkable (selectedTileFrame))
				character.goToPos (new Vector2 (selectedTile.x, selectedTile.y));
		}
	}
}

