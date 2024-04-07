using Godot;

namespace td.tutorial2.assets.Towers
{
    public partial class BasicTurret1 : Sprite2D
    {
        [Export]
        float range = 1f;


        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            CollisionShape2D collisionShape2D = new()
            {
                Shape = new CircleShape2D() { Radius = range }
            };
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}

