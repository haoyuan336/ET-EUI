Shader "Unlit/None"
{
	Properties
	{
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = fixed4(0,0,0,0);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return fixed4(0,0,0,0);
			}
			ENDCG
		}
	}
}
