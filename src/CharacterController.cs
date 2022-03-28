using Godot;

namespace Freeblob {
    public class CharacterController : KinematicBody {
        Vector3 velocity;
        Vector2 look;

        Transform body;
        Transform head;

        public override void _Ready() {
            Input.SetMouseMode(Input.MouseMode.Captured);
            body = Transform;
            head = GetNode<Spatial>("Head").Transform;
        }

        public override void _Process(float delta) {
            body.basis = new Basis(new Vector3(0, look.x, 0));
            head.basis = new Basis(new Vector3(look.y, 0, 0));
        }

        public override void _Input(InputEvent eve) {
            base._Input(eve);

            if (eve is InputEventMouseMotion motion && Input.GetMouseMode() == Input.MouseMode.Captured) {
                look += motion.Relative;
            }
        }
    }
}