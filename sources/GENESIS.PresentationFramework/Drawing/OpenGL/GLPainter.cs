using System.Diagnostics;
using GENESIS.GPU.OpenGL;
using GENESIS.GPU.Shader;
using Silk.NET.OpenGL;
using Shader = GENESIS.GPU.Shader.Shader;

namespace GENESIS.PresentationFramework.Drawing.OpenGL {
	
	public class GLPainter : Painter {

		private readonly GLPlatform _platform;

		public GLPainter(GLPlatform platform) : base(platform) {
			_platform = platform;
		}

		public override int BeginDrawList(DrawList.ShapeType type = DrawList.ShapeType.Triangle) {
			Debug.Assert(CurrentDrawList == -1, "BeginDrawList() called without EndDrawList()");
			
			CurrentDrawList = DrawLists.Count;
			DrawLists.Add(new GLDrawList(_platform, type));
			return CurrentDrawList;
		}
		
		public override void EndDrawList() {
			Debug.Assert(CurrentDrawList != -1, "EndDrawList() called without BeginDrawList()");
			
			CurrentDrawList = -1;
			CurrentModel = null;
		}

		public override void Paint() {
			foreach(var drawList in DrawLists) {
				if(!drawList.Enabled) continue;
				
				drawList.Push();
				_platform.API.DrawArraysInstanced(
					((GLDrawList) drawList).GLShapeType,
					0,
					(uint) drawList.Vertices.Count,
					(uint) drawList.Matrices.Count
				);
			}
		}
	}
}
