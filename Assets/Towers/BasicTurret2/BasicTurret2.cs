using Assets.Enemies;
using Godot;
using Godot.Collections;
using System.Diagnostics;
using System.Net;
using System.Xml.XPath;

namespace td.tutorial2.assets
{
    public partial class BasicTurret2 : StaticBody2D
    {
        private PackedScene _bullet;

        private Array<Soldier> _availableTargets;

        private Soldier _currentTarget;

        private PathFollow2D _currentTargetPath;

        private int _fireRate = 500;

        private Stopwatch _stopwatch = Stopwatch.StartNew();

        [Export]
        public int BulletDamage = 5;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _bullet = GD.Load<PackedScene>("res://Assets/Towers/BasicTurret2/RedBullet.tscn");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            Area2D area = (Area2D)GetNode("Tower");

            _availableTargets = new Array<Soldier>();

            foreach (Node2D nodeInArea in area.GetOverlappingBodies())
            {
                if (nodeInArea.Name.Equals("Soldier"))
                {
                    _availableTargets.Add((Soldier)nodeInArea);
                }
            }

            if (_availableTargets.Count == 0)
            {
                _currentTarget = null;
                return;
            }
            else
            {
                _currentTarget = _availableTargets[0];
            }

            foreach (Soldier soldier in _availableTargets)
            {
                PathFollow2D currentEnemyPath = (PathFollow2D)_currentTarget.GetParent();
                PathFollow2D newEnemyPath = (PathFollow2D)soldier.GetParent();

                if (newEnemyPath.Progress >= currentEnemyPath.Progress)
                {
                    _currentTarget = soldier;
                    _currentTargetPath = newEnemyPath;
                }
            }

            LookAt(_currentTarget.GlobalPosition);
            Fire(_currentTarget);
        }

        private void FindFirstTarget() { }

        /*public void OnTowerBodyEntered(Node2D node)
        {
            if(!CanShoot())
            {
                return;
            }

            if (node.Name.Equals("Soldier"))
            {
                _availableTargets = new Array<Soldier>();

                Area2D area = (Area2D)GetNode("Tower");

                foreach (Node2D nodeInArea in area.GetOverlappingBodies())
                {
                    if (nodeInArea.Name.Equals("Soldier"))
                    {
                        _availableTargets.Add((Soldier)nodeInArea);
                    }
                }

                if (_availableTargets.Count == 0)
                {
                    _currentTarget = null;
                    return;
                }
                else
                {
                    _currentTarget = _availableTargets[0];
                }

                foreach (Soldier soldier in _availableTargets)
                {
                    PathFollow2D currentEnemyPath = (PathFollow2D)_currentTarget.GetParent();
                    PathFollow2D newEnemyPath = (PathFollow2D)soldier.GetParent();

                    if (newEnemyPath.Progress >= currentEnemyPath.Progress)
                    {
                        _currentTarget = soldier;
                        _currentTargetPath = newEnemyPath;
                    }
                }

                Rotation = _currentTarget.GlobalRotation;
                Fire(_currentTarget);

                //TODO: Figure out if godot.collections.Array is worth it or to use standard c# collections.
                *//*currentTargets = area.GetOverlappingBodies()
                    .Where(node => node.Name.Equals("Soldier"))
                    .ToList();*//*
            }
        }*/

        public void OnTowerBodyExited(Node2D node)
        {

        }

        public void Fire(Soldier target)
        {
            if (!CanShoot())
            {
                return;
            }

            RedBullet bullet = (RedBullet)_bullet.Instantiate();
            bullet.GlobalPosition = ((Node2D)GetNode("Aim")).GlobalPosition;
            bullet.Target = target;

            GetTree().Root.AddChild(bullet);
        }

        private bool CanShoot()
        {
            if (_stopwatch.ElapsedMilliseconds > _fireRate)
            {
                _stopwatch.Restart();

                return true;
            }

            return false;
        }
    }
}


