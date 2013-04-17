using UnityEngine;
using System.Collections;

public class JFButton : FButton
{
	Color downColor = new Color(.4f,.4f,.4f);
	
	public JFButton(string downElementName) : base(downElementName)
	{
		this.SignalPress += HandleHandleSignalPress;
		this.SignalRelease += HandleHandleSignalRelease;
		this.SignalReleaseOutside += HandleHandleSignalRelease;
	}
	
	override public void HandleSingleTouchMoved (FTouch touch)
	{
		base.HandleSingleTouchMoved(touch);
		Vector2 touchPos = _bg.GlobalToLocal(touch.position);
		
		//expand the hitrect so that it has more error room around the edges
		//this is what Apple does on iOS and it makes for better usability
		Rect expandedRect = _bg.textureRect.CloneWithExpansion(expansionAmount);
		
		if(expandedRect.Contains(touchPos))
		{
			this._bg.color = downColor;
		}
		else
		{
			this._bg.color = Color.white;
		}
	}

	void HandleHandleSignalRelease (FButton obj)
	{
		this._bg.color = Color.white;
	}

	void HandleHandleSignalPress (FButton obj)
	{
		this._bg.color = downColor;
	}
}

