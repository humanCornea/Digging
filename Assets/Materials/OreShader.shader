Shader "Custom/OreShader" {
	Properties{
		_BaseColor ("Base Color", Color) = (1,1,1,1)
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
		
		fixed4 _BaseColor;// = fixed4(0.6f, 0.7f, 0.4f, 1);

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _BaseColor.rgb;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}