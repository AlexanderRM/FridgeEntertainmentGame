//Code has written by Przemyslaw Zaworski, 07.01.2018

Shader "Unlit See Through"
{
	Properties
	{
		surface("Color Map", 2D) = "white" {}
		mask("Mask", 2D) = "white" {}
		[KeywordEnum(Desaturation,Grain,NoName)] filters("Filters", Float) = 0
	}
	Subshader
	{	
		Pass
		{
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment pixel_shader
			#pragma target 3.0

			struct structure
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D surface,mask;
					
			structure vertex_shader (float4 vertex:POSITION, float2 uv:TEXCOORD0) 
			{
				structure vs;
				vs.vertex = UnityObjectToClipPos (vertex);
				vs.uv = uv;
				return vs;
			}

			float4 pixel_shader (structure ps) : COLOR
			{			
				float2 mask_uv = ps.vertex.xy / _ScreenParams.xy;
				float4 color = tex2D(mask,mask_uv);
				if (color.x>0.1) 
					discard;
				return tex2D(surface,ps.uv.xy);
			}
			ENDCG
		}
		
		GrabPass { "image" }
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment pixel_shader
			#pragma target 3.0

			sampler2D mask, image;
			float filters;
			
			float4 vertex_shader (float4 vertex:position) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}
			
			float4 pixel_shader (float4 vertex:SV_POSITION) : SV_TARGET
			{ 
				float2 mask_uv = vertex.xy / _ScreenParams.xy;
				float4 color = tex2D(mask,mask_uv);
				return tex2D(image,mask_uv);
			}

			ENDCG
		}
	}
}