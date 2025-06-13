using GENESIS.GodotRenderer;
using GENESIS.PresentationFramework.Drawing;
using Godot;
using Window = GENESIS.GPU.Window;

namespace GENESIS.Visualisations {
	
	public class NBodySimulationViewer : GodotScene3D {

		public NBodySimulationViewer() : base("gd_nbody_sim_viewer") { }

		public override void Initialize(Window window) {
			base.Initialize(window);

			Camera = new Camera3D {
				Fov = 60
			};
			Root.AddChild(Camera);
			
			Root.AddChild(new Node3D {
				GlobalPosition = Vector3.Zero
			});
			
		}

		protected override void Paint(double delta) {
			
		}
	}
}
