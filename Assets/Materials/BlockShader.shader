Shader "Custom/BlockShader" {
	Properties
	{
		_Color ("Color", Color) = (0.9, 0.9, 0.9, 1)
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade 
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _Color;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}