// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:14,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1362,x:34927,y:32310,varname:node_1362,prsc:2|emission-8386-OUT;n:type:ShaderForge.SFN_Tex2d,id:3502,x:32080,y:32450,varname:_node_3502,prsc:2,ntxv:0,isnm:False|UVIN-784-UVOUT,TEX-5000-TEX;n:type:ShaderForge.SFN_Tex2d,id:4199,x:32080,y:32262,varname:_node_4199,prsc:2,ntxv:0,isnm:False|UVIN-1641-UVOUT,TEX-9217-TEX;n:type:ShaderForge.SFN_Panner,id:784,x:31801,y:32444,varname:node_784,prsc:2,spu:1,spv:0|UVIN-3537-UVOUT,DIST-3314-OUT;n:type:ShaderForge.SFN_TexCoord,id:3537,x:31562,y:32444,varname:node_3537,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1641,x:31801,y:32263,varname:node_1641,prsc:2,spu:1,spv:0|UVIN-7445-UVOUT,DIST-2978-OUT;n:type:ShaderForge.SFN_TexCoord,id:7445,x:31562,y:32112,varname:node_7445,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2850,x:32327,y:32339,varname:node_2850,prsc:2|A-4199-R,B-3502-R;n:type:ShaderForge.SFN_Tex2d,id:7162,x:33868,y:32138,varname:_node_7162,prsc:2,ntxv:0,isnm:False|UVIN-3482-OUT,TEX-8394-TEX;n:type:ShaderForge.SFN_Multiply,id:2035,x:32898,y:32701,varname:node_2035,prsc:2|A-8680-OUT,B-8686-OUT;n:type:ShaderForge.SFN_TexCoord,id:6862,x:32869,y:32391,varname:node_6862,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1702,x:33077,y:32391,varname:node_1702,prsc:2|A-6862-UVOUT,B-2035-OUT;n:type:ShaderForge.SFN_Multiply,id:8386,x:34610,y:32411,varname:node_8386,prsc:2|A-3907-OUT,B-7902-OUT;n:type:ShaderForge.SFN_Multiply,id:9851,x:34263,y:32229,varname:node_9851,prsc:2|A-9243-RGB,B-3232-OUT,C-5705-RGB;n:type:ShaderForge.SFN_Color,id:9243,x:34082,y:32082,ptovrint:False,ptlb:Color,ptin:_Color,varname:_node_9243,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7902,x:34276,y:32570,varname:node_7902,prsc:2|A-7162-A,B-8686-OUT,C-5705-A;n:type:ShaderForge.SFN_ValueProperty,id:8680,x:32698,y:32644,ptovrint:False,ptlb:raodongqiangdu,ptin:_raodongqiangdu,varname:node_8680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:9992,x:31364,y:32617,varname:node_9992,prsc:2|A-6364-OUT,B-3042-T;n:type:ShaderForge.SFN_RemapRange,id:3314,x:31562,y:32617,varname:node_3314,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9992-OUT;n:type:ShaderForge.SFN_Multiply,id:5023,x:31364,y:32390,varname:node_5023,prsc:2|A-7927-OUT,B-3042-T;n:type:ShaderForge.SFN_RemapRange,id:2978,x:31562,y:32273,varname:node_2978,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5023-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9217,x:31801,y:32617,ptovrint:False,ptlb:TexNoise01,ptin:_TexNoise01,varname:node_9217,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:5000,x:31801,y:32809,ptovrint:False,ptlb:TexNoise02,ptin:_TexNoise02,varname:node_5000,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:867,x:31798,y:33214,varname:node_867,prsc:2,spu:0,spv:1|UVIN-7327-UVOUT,DIST-2562-OUT;n:type:ShaderForge.SFN_TexCoord,id:7327,x:31558,y:33214,varname:node_7327,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:3957,x:31798,y:33033,varname:node_3957,prsc:2,spu:0,spv:1|UVIN-5354-UVOUT,DIST-2446-OUT;n:type:ShaderForge.SFN_TexCoord,id:5354,x:31558,y:32882,varname:node_5354,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1100,x:32324,y:33110,varname:node_1100,prsc:2|A-4577-R,B-5413-R;n:type:ShaderForge.SFN_Time,id:3042,x:31070,y:32896,varname:node_3042,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4094,x:31360,y:33388,varname:node_4094,prsc:2|A-3042-T,B-6364-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6364,x:31070,y:33101,ptovrint:False,ptlb:TexNoise02Val,ptin:_TexNoise02Val,varname:_node_4340_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_RemapRange,id:2562,x:31558,y:33388,varname:node_2562,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-4094-OUT;n:type:ShaderForge.SFN_Multiply,id:6431,x:31374,y:33043,varname:node_6431,prsc:2|A-7927-OUT,B-3042-T;n:type:ShaderForge.SFN_ValueProperty,id:7927,x:31070,y:32789,ptovrint:False,ptlb:TexNoise01Val,ptin:_TexNoise01Val,varname:_node_3542_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_RemapRange,id:2446,x:31558,y:33043,varname:node_2446,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6431-OUT;n:type:ShaderForge.SFN_Tex2d,id:5413,x:32060,y:33214,varname:node_5413,prsc:2,ntxv:0,isnm:False|UVIN-867-UVOUT,TEX-5000-TEX;n:type:ShaderForge.SFN_Tex2d,id:4577,x:32060,y:33029,varname:node_4577,prsc:2,ntxv:0,isnm:False|UVIN-3957-UVOUT,TEX-9217-TEX;n:type:ShaderForge.SFN_ToggleProperty,id:587,x:32310,y:32758,ptovrint:False,ptlb:U,ptin:_U,varname:node_587,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_ToggleProperty,id:3067,x:32310,y:32937,ptovrint:False,ptlb:V,ptin:_V,varname:node_3067,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Multiply,id:2751,x:32502,y:32724,varname:node_2751,prsc:2|A-8428-OUT,B-587-OUT;n:type:ShaderForge.SFN_Multiply,id:1181,x:32502,y:32937,varname:node_1181,prsc:2|A-3067-OUT,B-3927-OUT;n:type:ShaderForge.SFN_Add,id:8686,x:32698,y:32806,varname:node_8686,prsc:2|A-2751-OUT,B-1181-OUT;n:type:ShaderForge.SFN_VertexColor,id:5705,x:34004,y:32671,varname:node_5705,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3907,x:34453,y:32229,varname:node_3907,prsc:2|A-9851-OUT,B-1004-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1004,x:34263,y:32396,ptovrint:False,ptlb:ColorVal,ptin:_ColorVal,varname:node_1004,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:3482,x:33432,y:32178,varname:node_3482,prsc:2|A-1702-OUT,B-5963-OUT;n:type:ShaderForge.SFN_Subtract,id:5550,x:33436,y:32443,varname:node_5550,prsc:2|A-1702-OUT,B-5963-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5963,x:33077,y:32317,ptovrint:False,ptlb:HueSeparation,ptin:_HueSeparation,varname:node_5963,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2dAsset,id:8394,x:33432,y:32009,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8394,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6282,x:33868,y:32284,varname:node_6282,prsc:2,ntxv:0,isnm:False|UVIN-1702-OUT,TEX-8394-TEX;n:type:ShaderForge.SFN_Tex2d,id:7783,x:33868,y:32418,varname:node_7783,prsc:2,ntxv:0,isnm:False|UVIN-5550-OUT,TEX-8394-TEX;n:type:ShaderForge.SFN_Append,id:3232,x:34082,y:32250,varname:node_3232,prsc:2|A-7162-R,B-6282-G,C-7783-B;n:type:ShaderForge.SFN_Multiply,id:3693,x:32327,y:32184,varname:node_3693,prsc:2|A-4199-RGB,B-3502-RGB,C-2564-RGB,D-525-OUT;n:type:ShaderForge.SFN_Lerp,id:8428,x:32568,y:32318,varname:node_8428,prsc:2|A-2850-OUT,B-3693-OUT,T-4505-OUT;n:type:ShaderForge.SFN_Slider,id:4505,x:32248,y:32499,ptovrint:False,ptlb:TexNoise01R-RGB,ptin:_TexNoise01RRGB,varname:node_4505,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:8930,x:32324,y:33272,varname:node_8930,prsc:2|A-4577-RGB,B-5413-RGB,C-3335-RGB,D-8142-OUT;n:type:ShaderForge.SFN_Lerp,id:3927,x:32582,y:33220,varname:node_3927,prsc:2|A-1100-OUT,B-8930-OUT,T-5258-OUT;n:type:ShaderForge.SFN_Slider,id:5258,x:32245,y:33494,ptovrint:False,ptlb:TexNoise02R-RGB,ptin:_TexNoise02RRGB,varname:node_5258,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:3335,x:32060,y:33383,ptovrint:False,ptlb:TexNoise02Color,ptin:_TexNoise02Color,varname:node_3335,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:8142,x:32060,y:33568,ptovrint:False,ptlb:TexNoise02ColorVal,ptin:_TexNoise02ColorVal,varname:node_8142,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:2564,x:32080,y:32045,ptovrint:False,ptlb:TexNoise01Color,ptin:_TexNoise01Color,varname:node_2564,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:525,x:32080,y:32203,ptovrint:False,ptlb:TexNoise01ColorVal,ptin:_TexNoise01ColorVal,varname:node_525,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:9243-1004-8394-2564-525-4505-9217-3335-8142-5258-5000-587-3067-8680-7927-6364-5963;pass:END;sub:END;*/

Shader "effect/DistortBlendAdd" {
    Properties {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        _ColorVal ("ColorVal", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        _TexNoise01Color ("TexNoise01Color", Color) = (1,1,1,1)
        _TexNoise01ColorVal ("TexNoise01ColorVal", Float ) = 1
        _TexNoise01RRGB ("TexNoise01R-RGB", Range(0, 1)) = 0
        _TexNoise01 ("TexNoise01", 2D) = "white" {}
        _TexNoise02Color ("TexNoise02Color", Color) = (1,1,1,1)
        _TexNoise02ColorVal ("TexNoise02ColorVal", Float ) = 1
        _TexNoise02RRGB ("TexNoise02R-RGB", Range(0, 1)) = 0
        _TexNoise02 ("TexNoise02", 2D) = "white" {}
        [MaterialToggle] _U ("U", Float ) = 0
        [MaterialToggle] _V ("V", Float ) = 0
        _raodongqiangdu ("raodongqiangdu", Float ) = 0.2
        _TexNoise01Val ("TexNoise01Val", Float ) = 0.2
        _TexNoise02Val ("TexNoise02Val", Float ) = 0.5
        _HueSeparation ("HueSeparation", Float ) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _raodongqiangdu;
            uniform sampler2D _TexNoise01; uniform float4 _TexNoise01_ST;
            uniform sampler2D _TexNoise02; uniform float4 _TexNoise02_ST;
            uniform float _TexNoise02Val;
            uniform float _TexNoise01Val;
            uniform fixed _U;
            uniform fixed _V;
            uniform float _ColorVal;
            uniform float _HueSeparation;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _TexNoise01RRGB;
            uniform float _TexNoise02RRGB;
            uniform float4 _TexNoise02Color;
            uniform float _TexNoise02ColorVal;
            uniform float4 _TexNoise01Color;
            uniform float _TexNoise01ColorVal;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_3042 = _Time;
                float2 node_1641 = (i.uv0+((_TexNoise01Val*node_3042.g)*2.0+-1.0)*float2(1,0));
                float4 _node_4199 = tex2D(_TexNoise01,TRANSFORM_TEX(node_1641, _TexNoise01));
                float2 node_784 = (i.uv0+((_TexNoise02Val*node_3042.g)*2.0+-1.0)*float2(1,0));
                float4 _node_3502 = tex2D(_TexNoise02,TRANSFORM_TEX(node_784, _TexNoise02));
                float node_2850 = (_node_4199.r*_node_3502.r);
                float2 node_3957 = (i.uv0+((_TexNoise01Val*node_3042.g)*2.0+-1.0)*float2(0,1));
                float4 node_4577 = tex2D(_TexNoise01,TRANSFORM_TEX(node_3957, _TexNoise01));
                float2 node_867 = (i.uv0+((node_3042.g*_TexNoise02Val)*2.0+-1.0)*float2(0,1));
                float4 node_5413 = tex2D(_TexNoise02,TRANSFORM_TEX(node_867, _TexNoise02));
                float node_1100 = (node_4577.r*node_5413.r);
                float3 node_8686 = ((lerp(float3(node_2850,node_2850,node_2850),(_node_4199.rgb*_node_3502.rgb*_TexNoise01Color.rgb*_TexNoise01ColorVal),_TexNoise01RRGB)*_U)+(_V*lerp(float3(node_1100,node_1100,node_1100),(node_4577.rgb*node_5413.rgb*_TexNoise02Color.rgb*_TexNoise02ColorVal),_TexNoise02RRGB)));
                float3 node_1702 = (float3(i.uv0,0.0)+(_raodongqiangdu*node_8686));
                float3 node_3482 = (node_1702+_HueSeparation);
                float4 _node_7162 = tex2D(_MainTex,TRANSFORM_TEX(node_3482, _MainTex));
                float4 node_6282 = tex2D(_MainTex,TRANSFORM_TEX(node_1702, _MainTex));
                float3 node_5550 = (node_1702-_HueSeparation);
                float4 node_7783 = tex2D(_MainTex,TRANSFORM_TEX(node_5550, _MainTex));
                float3 emissive = (((_Color.rgb*float3(_node_7162.r,node_6282.g,node_7783.b)*i.vertexColor.rgb)*_ColorVal)*(_node_7162.a*node_8686*i.vertexColor.a));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
