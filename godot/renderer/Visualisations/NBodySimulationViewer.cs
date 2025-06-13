using GENESIS.Simulation.NBody;
using Godot;

namespace GENESIS.GodotRenderer.Visualisations {
	
	public partial class NBodySimulationViewer : Node3D {

		public const float RENDER_SCALE = 1 / 1_000_000f;
		
		public NBodySimulation Simulation { get; set; }
		
	}
}
