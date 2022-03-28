using Godot;
using MonoCustomResourceRegistry;

namespace Freeblob {
	[RegisteredType(nameof(GameManager))]
	public class GameManager : Node {
		[Export]
		float speed = 0.5f;
		[Export]
		float deltaTime = 0;
		[Export]
		string fps = "?";

		// Called when the node enters the scene tree for the first time.
		public override void _Ready() {
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta) {
			deltaTime = Mathf.Lerp(deltaTime, delta, speed);
			fps = Mathf.RoundToInt(1 / deltaTime).ToString();
			GD.Print($"{deltaTime}: {fps}");
		}
	}
}
