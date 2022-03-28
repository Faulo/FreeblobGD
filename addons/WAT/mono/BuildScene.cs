using Godot;

public class BuildScene : Node {
    public override void _Ready() {
        GetTree().Quit();
    }
}
