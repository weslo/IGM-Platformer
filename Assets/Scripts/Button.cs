using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public Sprite idleSprite;
	public Sprite hoverSprite;

	private SpriteRenderer sr;

	// Use this for initialization
	protected virtual void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Fires when the button is clicked. Override for effect.
	/// </summary>
	protected virtual void OnClick() {
	}

	/// <summary>
	/// Raises the mouse enter event.
	/// </summary>
	void OnMouseEnter() {
		sr.sprite = hoverSprite;
	}

	/// <summary>
	/// Raises the mouse exit event.
	/// </summary>
	void OnMouseExit() {
		sr.sprite = idleSprite;
	}

	/// <summary>
	/// Raises the mouse up event.
	/// </summary>
	void OnMouseUp() {
		OnClick ();
	}
}
