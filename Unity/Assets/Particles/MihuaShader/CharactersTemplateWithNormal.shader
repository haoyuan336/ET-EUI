Shader "JusGameShader/Characters/CharactersTemplateWithNormal" {
Properties {	
	_MainEmission ("Main Emission Power", Range(0.0,2.0)) = 0.45
	_EmissionP ("Emission Power", Range(0.0,4.0)) = 0.0
	HitPower("HitPower", Range(0.0,4.0)) = 0.0

	_Gamapow("Gamapow",Range(0.1,5.0))=1.1
	_GamapowExtra("GamapowExtra",Range(0.1,5.0))=0.25

	_SpecularP ("Specular Power", Range(0.0,4.0)) = 0.0
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125

	_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
	_RimColor ("Rim Color", Color) = (1.0,0.72,0.0,0.0)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_Illum ("Illumin", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_Mytsxf("Mytsxf",Range(0.0,4.0)) = 0.0

	_Mylight("light",Range(0.0,4.0)) = 0.48


	//石化使用
	   _MyShihuachengdu ("MyShihuachengdu", Range(1, 0)) = 0
        _SHColor ("SHColor", Color) = (0.5,0.5,0.5,1)
}
SubShader {
	Tags { "Queue"="Geometry+200" "RenderType"="Opaque" "Reflection" = "RenderReflectionOpaque"}
	LOD 200
	Cull Back
	AlphaTest Off
CGPROGRAM
#pragma surface surf CharactersLightMode nolightmap nodirlightmap 
//#pragma only_renderers gles d3d9
#pragma fragmentoption ARB_precision_hint_fastest
#pragma multi_compile IS_Other IS_Ios

struct CharactersLightModeOutput {
	half MyShihuachengdu;
	half3 SHColor;
	half3 Albedo;
	half3 Normal;
	half3 Emission;
	half Specular;
	half Alpha;
	half Gamapow;
	half RimPower;
	half3 RimColor;
	half HitPower;
	half SpecularP;
	half Mytsxf;
	half light;

};

inline fixed4 LightingCharactersLightMode (CharactersLightModeOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
{

	//float3 diffuseCol = pow(tex2D(diffTex,texCoord),2.2);
	half3 h = normalize (lightDir + viewDir);
	
	half diff = max (0, dot (s.Normal, lightDir));
	
	half nh = max (0, dot (s.Normal, h));
	half spec = pow (nh, s.Specular * 128.0) * s.SpecularP;

	fixed4 c;
	c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2);
	//c.a = s.Alpha;
	c.a = 1.0f;
	c.a = 1.0f;
	half Gama = 1.0h/s.Gamapow;

	half rim = 1.0 - saturate(dot (viewDir, s.Normal)) + 0.15f;

	half CurrentHitPower = clamp(s.HitPower, 0.0f, 1.0f);

	c.rgb = c.rgb + s.RimColor.rgb * pow (rim, s.RimPower) +  pow (rim, CurrentHitPower) * CurrentHitPower;
	#pragma target 3.0

	c.rgb = pow(c.rgb,Gama);

	return c;
}

sampler2D _MainTex;
sampler2D _Illum;

#if defined(IS_Other)
sampler2D _BumpMap;
#endif

half _Gamapow;
half _GamapowExtra;
half _MainEmission;
half _EmissionP;
half _SpecularP;
half _RimPower;
half3 _RimColor;
half _HitPower;
half _Shininess;
half _Mytsxf;
half _MyShihuachengdu;
half3 _SHColor;
half _Mylight;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout CharactersLightModeOutput o) {

	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);

	fixed4 Illum = tex2D(_Illum, IN.uv_MainTex);

		tex = pow(tex2D(_MainTex, IN.uv_MainTex),_Gamapow+_GamapowExtra);
	//天神下凡，默认Mytsxf = 0；
	if(_Mytsxf!=0.0f)
	{
		tex.r +=0.4f*_Mytsxf/4;
		tex.g += 0.4f*_Mytsxf/4;
		tex.b +=0.0f;
	}
	//石化输出
	if(_MyShihuachengdu!=0.0f)
	{
	float3 node_9097 = ((1.0f*_SHColor.rgb)*lerp(tex.rgb,dot(tex.rgb,float3(0.3,0.59,0.11)),_MyShihuachengdu));
	float3 SHemissive = lerp(tex.rgb,(node_9097*2.0),_MyShihuachengdu);
	tex.rgb = SHemissive;
	}


#if IS_Ios
	o.Emission = tex.rgb *(_Mylight+ _MainEmission) * Illum.g;
#else
	o.Emission = tex.rgb * (_Mylight+ _MainEmission) * Illum.b;
#endif

	o.SpecularP = Illum.r * _SpecularP;
	o.Specular = _Shininess;

	o.Albedo = (tex.rgb) * ( 1.0f + _EmissionP ) ;
	o.Alpha = 1.0f;
	o.RimPower = _RimPower;
	o.RimColor = _RimColor;
	o.HitPower = _HitPower;
	o.Gamapow  = _Gamapow;
	fixed3 normal;
#if IS_Ios
	normal.xy = Illum.ba * 2 - 1;
#else
	#if defined(SHADER_API_MOBILE)
		normal.xy = tex2D(_BumpMap, IN.uv_MainTex).xy * 2 - 1;
	#else
		normal.xy = tex2D(_BumpMap, IN.uv_MainTex).wy * 2 - 1;
	#endif
#endif
	normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
	o.Normal = normal;
}
ENDCG
}
FallBack "JusGameShader/Characters/CharactersTemplateFast"
}
