using Godot;

namespace Freeblob {
    public class CharacterController : KinematicBody {
        Vector3 velocity;
        Vector2 look;

        [Export]
        Vector2 inputGain = Vector2.One;

        Spatial body;
        Spatial head;

        public override void _Ready() {
            Input.SetMouseMode(Input.MouseMode.Captured);
            body = this;
            head = GetNode<Spatial>("Head");
        }

        public override void _Process(float delta) {
            body.RotationDegrees = new Vector3(0, look.x, 0);
            head.RotationDegrees = new Vector3(look.y, 0, 0);
        }

        public override void _Input(InputEvent eve) {
            base._Input(eve);

            if (eve is InputEventMouseMotion motion && Input.GetMouseMode() == Input.MouseMode.Captured) {
                look -= motion.Relative * inputGain;
                look.y = Mathf.Clamp(look.y, -90, 90);
            }
        }
    }
}