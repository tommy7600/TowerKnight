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
	FStage tilemapStage;
	
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
		if (Input.GetMouseButtonDown (0)) {

			int xPos = (int)(Input.mousePosition.x - Futile.stage.x *Futile.displayScale - Futile.screen.pixelWidth/2);
			int yPos = (int)(Futile.screen.pixelHeight - Input.mousePosition.y + Futile.stage.y*Futile.displayScale - Futile.screen.pixelHeight/2);
			int xTile = (int)(xPos / (fTileMap.tileWidth* Futile.displayScale));
			int yTile = (int)(yPos / (fTileMap.tileWidth*Futile.displayScale));
			int selectedTileFrame = fTileMap.getTileFrame(xTile, yTile);
			Debug.Log ("Tile Clicked: mouse("+xPos+","+yPos+")  tile(" + xTile + "," + yTile+") tiletype("+selectedTileFrame+")");
			FSprite selectedTile = fTileMap.getTile (xTile, yTile);
			if (isWalkable(selectedTileFrame))
				character.goToPos (new Vector2 (selectedTile.x, selectedTile.y));
		}
		const float speed = 5.0f;
		if (Input.GetKey(KeyCode.LeftArrow))
			character.x-=speed;
		if (Input.GetKey(KeyCode.RightArrow))
			character.x+=speed;
		 if (Input.GetKey(KeyCode.DownArrow))
			character.y -= speed;
		 if (Input.GetKey(KeyCode.UpArrow))
			character.y += speed;
		
		
		tilemapStage.x = -fCamera.x;
		tilemapStage.y = -fCamera.y;
	}
	
	private bool isWalkable(int tileFrame)
	{
		switch(tileFrame)
		{
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
}

