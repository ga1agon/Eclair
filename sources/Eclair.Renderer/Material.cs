using System.Numerics;
using System.Runtime.InteropServices;
using Eclair.Extensions.CSharp;
using ILGPU.Runtime.CPU;

namespace Eclair.Renderer {
	
	[StructLayout(LayoutKind.Explicit)]
	public struct Material() {
		
		[FieldOffset(0)] public Vector4 Albedo = Vector4.One;
		[FieldOffset(16)] public float Roughness = 0.5f;
		[FieldOffset(20)] public float Metallic = 0.5f;
		[FieldOffset(24)] public TextureType UseTextures = 0;
		[FieldOffset(28)] private float _padding0 = 0;

		public static implicit operator Material(System.Drawing.Color color)
			=> new Material { Albedo = color.ToVector4() };
		
		public static implicit operator Material(SixLabors.ImageSharp.Color color)
			=> new Material { Albedo = (Vector4) color };
		
		public override string ToString() {
			return $"[Albedo={Albedo}]";
		}

		[Flags]
		public enum TextureType {
			
			None = 0,
			Diffuse = (1 << 0),
			Normal = (1 << 1),
			Roughness = (1 << 3),
			Metallic = (1 << 2),
		}

		/*public static Material Create(Color albedoColor, float metallic = 0.5f, float roughness = 0.5f)
			=> new Material {
				Albedo = albedoColor.ToVector4(),
				Metallic = metallic,
				Roughness = roughness,
				UseTextures = 0
			};

		public static Material Create(
			Texture diffuseTexture,
			Texture? normalTexture = null,
			Texture? roughnessTexture = null,
			Texture? metallicTexture = null,
			Color? albedoColor = null,
			float metallic = 0.5f,
			float roughness = 0.5f
		) {
			albedoColor ??= Color.White;

			var material = new Material {
				Albedo = albedoColor.Value.ToVector4(),
				Metallic = metallic,
				Roughness = roughness
			};

			material.UseTextures |= (1 << 0);
			if(normalTexture is not null) material.UseTextures |= (1 << 1);
			if(roughnessTexture is not null) material.UseTextures |= (1 << 2);
			if(metallicTexture is not null) material.UseTextures |= (1 << 3);

			return material;
		}*/
	}
}
