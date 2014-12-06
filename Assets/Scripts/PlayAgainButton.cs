using UnityEngine;
using System.Collections;

public class PlayAgainButton : Button {

	private GameScreen screen;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		screen = transform.parent.GetComponent<GameScreen>();
	}

	/// <summary>
	/// Fires when the button is clicked.
	/// </summary>
	protected override void OnClick ()
	{
		base.OnClick ();

		screen.LoadOtherScreen(0);
	}
}
