using UnityEngine;
using System.Collections;

public class Player : FAnimatedSprite
{
	FTilemap currentTilemap;
	
	public Player (FTilemap tilemap) : base("character")
	{
		this.currentTilemap = tilemap;
		addAnimation (new FAnimation ("walkDown", new int[] {1,2}, 300, true));
		addAnimation (new FAnimation ("walkUp", new int[] {3,4}, 300, true));
		addAnimation (new FAnimation ("walkRight", new int[] {5,6}, 300, true));
		addAnimation (new FAnimation ("walkLeft", new int[] {7,8}, 300, true));
		play ("walkDown");
	}
	
	public void goToPos (Vector2 position)
	{
		Go.killAllTweensWithTarget (this);
		Go.to (this, 1.0f, new TweenConfig ()
			.floatProp ("x", position.x)
			.floatProp ("y", position.y)
			.onComplete (HandleGoToPosComplete));
		
		if (position.x < x)
			play ("walkLeft");
		else if (position.x > x)
			play ("walkRight");
		else if (position.y < y)
			play ("walkDown");
		else if (position.y > y)
			play ("walkUp");
		
		Debug.Log ("Walk started from: " + (x/currentTilemap.tileWidth) + "," + (-y/currentTilemap.tileWidth) + " type: " + currentTilemap.getTileFrame((int)(x/currentTilemap.tileWidth), (int)(-y/currentTilemap.tileWidth)));
		
	}
	
	private void HandleGoToPosComplete (AbstractTween at)
	{
		Debug.Log ("Walk finished at: " + (x/currentTilemap.tileWidth) + "," + (-y/currentTilemap.tileWidth) + " type: " + currentTilemap.getTileFrame((int)(x/currentTilemap.tileWidth), (int)(-y/currentTilemap.tileWidth)));
		play ("walkDown");
	}
}

