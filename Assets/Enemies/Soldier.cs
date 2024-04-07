using Godot;

namespace Assets.Enemies
{
    public partial class Soldier : CharacterBody2D
    {
        [Export]
        public float Speed = 500f;

        [Export]
        public int Health = 10;

        public bool IsAlive = true;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {

        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            PathFollow2D pathFollow2D = (PathFollow2D)GetParent();

            pathFollow2D.Progress += Speed * (float)delta;

            if (pathFollow2D.ProgressRatio == 1)
            {
                IsAlive = false;
                QueueFree();
            }
        }

        public void Hit(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                IsAlive = false;
                QueueFree();
            }
        }
    }
}


