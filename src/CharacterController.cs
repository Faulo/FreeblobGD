using Godot;

namespace Freeblob {
    public class CharacterController : KinematicBody {
        Vector3 velocity;
        Vector2 movement;
        Vector2 look;

        [Export]
        Vector2 inputGain = Vector2.One;

        Spatial body;
        Spatial head;

        public override void _Ready() {
            Input.SetMouseMode(Input.MouseMode.Captured);
            body = this;
            head = GetNode<Spatial>("Head");

            look.x = body.RotationDegrees.y;
            look.y = head.RotationDegrees.x;
        }

        public override void _Process(float delta) {
            movement = GetMovement();

            body.RotationDegrees = new Vector3(0, look.x, 0);
            head.RotationDegrees = new Vector3(look.y, 0, 0);

            velocity.x = movement.x;
            velocity.z = movement.y;

            MoveAndSlide(velocity);
        }

        Vector2 GetMovement() => new Vector2 {
            x = Input.GetActionStrength("avatar_walk_right") - Input.GetActionStrength("avatar_walk_left"),
            y = Input.GetActionStrength("avatar_walk_forward") - Input.GetActionStrength("avatar_walk_back")
        };

        public override void _Input(InputEvent eve) {
            base._Input(eve);

            if (eve is InputEventMouseMotion motion && Input.GetMouseMode() == Input.MouseMode.Captured) {
                look -= motion.Relative * inputGain;
                look.y = Mathf.Clamp(look.y, -90, 90);
            }
        }
    }
}