using Godot;

namespace td.tutorial2
{
    public partial class PathSpawner : Node2D
	{
		private PackedScene _path;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_path = GD.Load<PackedScene>("res://Assets/Maps/Stage_1/Stage_1_Path.tscn");
        }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void OnTimerTimeout()
		{
			AddChild(InstantiatePath());
		}

		private Path2D InstantiatePath()
		{
			return (Path2D)_path.Instantiate();
		}

    }

}