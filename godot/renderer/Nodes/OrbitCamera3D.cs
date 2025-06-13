using System;
using GENESIS.LanguageExtensions;
using GENESIS.PresentationFramework;
using Godot;
using Hexa.NET.ImGui;

namespace GENESIS.GodotRenderer.Nodes {
	
	[Tool]
	[GlobalClass]
	public partial class OrbitCamera3D : Camera3D {

		public float Sensitivity { get; set; } = 1.0f;
		
		public bool AllowZooming { get; set; } = true;
		public bool AllowPanning { get; set; } = true;
		public bool AllowRotating { get; set; } = true;

		private Node3D _target;
		public Node3D Target {
			get => _target;
			set {
				_target = value;
				GetParent<Node3D>().Reparent(value);
			}
		}
		
		public float Distance { get; set; } = 10.0f;

		public override void _Ready() {
		#if DEBUG
			DebugScene.DebugInfoSlots["orbit_camera"] = _ => {
				ImGui.Text($"Distance: {Distance}");
				ImGui.Text($"Target: {Target}");
				ImGui.Text($"Position: {GlobalPosition}");
				ImGui.Text($"Rotation deg: {RotationDegrees}");
			};
		#endif
		}

		public override void _Input(InputEvent @event) {
			if(@event is InputEventMouseButton mouseButtonEvent) {
				if(mouseButtonEvent.ButtonIndex is not
				   (MouseButton.WheelUp or MouseButton.WheelDown)) return;
				if(!AllowZooming) return;

				int multiplier = 1;
				if(Input.IsKeyPressed(Key.Shift)) multiplier = 10;
				if(Input.IsKeyPressed(Key.Ctrl)) multiplier = 50;
				if(Input.IsKeyPressed(Key.Alt)) multiplier = 100;
				
				Distance -= (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp ? 1 : -1)
				            * multiplier;
				Distance = MathF.Max(Distance, 1);
			} else if(@event is InputEventMouseMotion mouseMotionEvent) {
				var deltaX = mouseMotionEvent.Relative.X * Sensitivity;
				var deltaY = mouseMotionEvent.Relative.Y * Sensitivity;

				var gizmo = GetParent<Node3D>();
				
				// rotation
				if(AllowRotating && Input.IsMouseButtonPressed(MouseButton.Left)) {
					gizmo.Rotation = gizmo.Rotation with {
						X = gizmo.Rotation.X - deltaY.ToRadians(),
						Y = gizmo.Rotation.Y - deltaX.ToRadians()
					};
				}

				// translation X/Z
				if(AllowPanning && Input.IsMouseButtonPressed(MouseButton.Right)) {
					float prevY = gizmo.GlobalPosition.Y;
					
					gizmo.GlobalTranslate(-(GlobalTransform.Basis.X
					                       * (deltaX * (Sensitivity / 50.0f))));
					gizmo.GlobalTranslate(-(-GlobalTransform.Basis.Z
					                        * (deltaY * (Sensitivity / 50.0f))));

					gizmo.GlobalPosition = gizmo.GlobalPosition with {
						Y = prevY
					};
				}

				// translation Y
				if(AllowPanning && Input.IsMouseButtonPressed(MouseButton.Middle)) {
					gizmo.GlobalTranslate(new Vector3(0, -(deltaY * (Sensitivity / 50.0f)), 0));
				}
			}
		}

		public override void _Process(double delta) {
			var offset = new Vector3(
				Distance * MathF.Cos(Rotation.X) * MathF.Sin(Rotation.Y),
				Distance * MathF.Sin(Rotation.X),
				Distance * MathF.Cos(Rotation.X) * MathF.Cos(Rotation.Y)
			);
			
			Position = offset;
		}
	}
}
