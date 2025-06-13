using GENESIS.PresentationFramework;
using GENESIS.PresentationFramework.Drawing;
using Godot;
using Window = GENESIS.GPU.Window;

namespace GENESIS.GodotRenderer {
	
	public abstract class GodotScene3D : Scene {

		public Node3D Root { get; private set; }
		public Camera3D? Camera { get; protected set; }
		
		public GodotScene3D(string id) : base(default, id) { }

		public override void Render(double delta) { }
		protected override void Paint(double delta) { }

		public override void Initialize(Window window) {
			base.Initialize(window);
			DefaultScene.GlobalInstance.Clear();
			Root = DefaultScene.GlobalInstance;
		}

		public override void Deinitialize(Window window) {
			base.Deinitialize(window);
			DefaultScene.GlobalInstance.Clear();
		}
	}
}
