Shader "Mobile/Diffuse_MonsterDead" {
Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Alpha("Alpha",Range(0,1)) = 1
	}

	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		half4 _Color;
		half _Alpha;

		struct Input {
			float2 uv_MainTex;
			};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Emission = c.rgb;
			o.Alpha = _Alpha;
		}

		ENDCG
	}

Fallback "Mobile/VertexLit"
}

//Properties
//{
//     _Color ("_Color", Color) = (1,1,1,0.5)
//     _MainTex ("Base (RGB)", 2D) = "white" {}
//     _Mask ("Culling Mask", 2D) = "white" {}
//     _Cutoff ("Alpha cutoff", Range (0,0.5)) = 0.5
//}
//SubShader
//{
// Tags {"Queue"="Geometry  -400"}   
//   ZWrite On
//   Blend SrcAlpha OneMinusSrcAlpha
// ZTest Always
           
//   Pass
//   {
//      SetTexture [_Mask] {combine texture}
//      SetTexture [_MainTex]
//      {
//      	constantColor [_Color]
//      	combine texture*constant, previous*constant
//      }
//   }
//}
//}