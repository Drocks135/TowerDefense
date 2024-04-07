using Assets.Enemies;
using Godot;
using System;
using System.Collections.Generic;

public partial class RedBullet : CharacterBody2D
{
    public int BulletSpeed = 2000;

    public Soldier Target;
    public string PathName;
    public int BulletDamage;

    public RedBullet() { }

    public RedBullet(Soldier target, Vector2 spawnPosition)
    {
        GlobalPosition = spawnPosition;
        Target = target;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!Target.IsAlive) QueueFree();

        Velocity = GlobalPosition.DirectionTo(Target.GlobalPosition) * BulletSpeed;
        LookAt(Target.GlobalPosition);

        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        if (collision != null)
        {
            if(collision.GetCollider().HasMethod("Hit"))
            {
                /*Soldier s = (Soldier) collision.GetCollider();
                s.Hit(BulletDamage);*/

                collision.GetCollider().CallDeferred("Hit", "damage, 5");
            }

            QueueFree();
        }
    }
}
