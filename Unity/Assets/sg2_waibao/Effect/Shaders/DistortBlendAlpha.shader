// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1362,x:34051,y:32099,varname:node_1362,prsc:2|emission-3907-OUT,alpha-2705-OUT;n:type:ShaderForge.SFN_Tex2d,id:3502,x:32080,y:32450,varname:_node_3502,prsc:2,tex:74a5869b32b885043aa60b3943385579,ntxv:0,isnm:False|UVIN-784-UVOUT,TEX-5000-TEX;n:type:ShaderForge.SFN_Tex2d,id:4199,x:32080,y:32262,varname:_node_4199,prsc:2,tex:bd2e0887544ccb34693ddef076f571a9,ntxv:0,isnm:False|UVIN-1641-UVOUT,TEX-9217-TEX;n:type:ShaderForge.SFN_Panner,id:784,x:31801,y:32444,varname:node_784,prsc:2,spu:1,spv:0|UVIN-3537-UVOUT,DIST-3314-OUT;n:type:ShaderForge.SFN_TexCoord,id:3537,x:31562,y:32444,varname:node_3537,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1641,x:31801,y:32263,varname:node_1641,prsc:2,spu:1,spv:0|UVIN-7445-UVOUT,DIST-2978-OUT;n:type:ShaderForge.SFN_TexCoord,id:7445,x:31562,y:32112,varname:node_7445,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2850,x:32327,y:32339,varname:node_2850,prsc:2|A-4199-R,B-3502-R;n:type:ShaderForge.SFN_Tex2d,id:7162,x:33280,y:32321,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_node_7162,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74a5869b32b885043aa60b3943385579,ntxv:0,isnm:False|UVIN-1702-OUT;n:type:ShaderForge.SFN_Multiply,id:2035,x:32914,y:32599,varname:node_2035,prsc:2|A-8680-OUT,B-8686-OUT;n:type:ShaderForge.SFN_TexCoord,id:6862,x:31843,y:31833,varname:node_6862,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1702,x:33094,y:32475,varname:node_1702,prsc:2|A-7225-OUT,B-2035-OUT;n:type:ShaderForge.SFN_Multiply,id:9851,x:33537,y:32201,varname:node_9851,prsc:2|A-9243-RGB,B-7162-RGB,C-5705-RGB;n:type:ShaderForge.SFN_Color,id:9243,x:33252,y:32133,ptovrint:False,ptlb:Color,ptin:_Color,varname:_node_9243,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7902,x:33549,y:32634,varname:node_7902,prsc:2|A-7162-A,B-8686-OUT,C-5705-A;n:type:ShaderForge.SFN_ValueProperty,id:8680,x:32714,y:32542,ptovrint:False,ptlb:raodongqiangdu,ptin:_raodongqiangdu,varname:node_8680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:9992,x:31364,y:32617,varname:node_9992,prsc:2|A-6364-OUT,B-3042-T;n:type:ShaderForge.SFN_RemapRange,id:3314,x:31562,y:32617,varname:node_3314,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9992-OUT;n:type:ShaderForge.SFN_Multiply,id:5023,x:31358,y:32273,varname:node_5023,prsc:2|A-7927-OUT,B-3042-T;n:type:ShaderForge.SFN_RemapRange,id:2978,x:31562,y:32273,varname:node_2978,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5023-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9217,x:31801,y:32617,ptovrint:False,ptlb:TexNoise01,ptin:_TexNoise01,varname:node_9217,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bd2e0887544ccb34693ddef076f571a9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:5000,x:31801,y:32809,ptovrint:False,ptlb:TexNoise02,ptin:_TexNoise02,varname:node_5000,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74a5869b32b885043aa60b3943385579,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:867,x:31798,y:33214,varname:node_867,prsc:2,spu:0,spv:1|UVIN-7327-UVOUT,DIST-2562-OUT;n:type:ShaderForge.SFN_TexCoord,id:7327,x:31558,y:33214,varname:node_7327,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:3957,x:31798,y:33033,varname:node_3957,prsc:2,spu:0,spv:1|UVIN-5354-UVOUT,DIST-2446-OUT;n:type:ShaderForge.SFN_TexCoord,id:5354,x:31558,y:32882,varname:node_5354,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1100,x:32324,y:33110,varname:node_1100,prsc:2|A-4577-R,B-5413-R;n:type:ShaderForge.SFN_Time,id:3042,x:31070,y:32896,varname:node_3042,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4094,x:31377,y:33388,varname:node_4094,prsc:2|A-3042-T,B-6364-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6364,x:31070,y:33098,ptovrint:False,ptlb:TexNoise02Val,ptin:_TexNoise02Val,varname:_node_4340_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_RemapRange,id:2562,x:31558,y:33388,varname:node_2562,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-4094-OUT;n:type:ShaderForge.SFN_Multiply,id:6431,x:31369,y:33046,varname:node_6431,prsc:2|A-7927-OUT,B-3042-T;n:type:ShaderForge.SFN_ValueProperty,id:7927,x:31070,y:32789,ptovrint:False,ptlb:TexNoise01Val,ptin:_TexNoise01Val,varname:_node_3542_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_RemapRange,id:2446,x:31558,y:33046,varname:node_2446,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6431-OUT;n:type:ShaderForge.SFN_Tex2d,id:5413,x:32060,y:33214,varname:node_5413,prsc:2,tex:74a5869b32b885043aa60b3943385579,ntxv:0,isnm:False|UVIN-867-UVOUT,TEX-5000-TEX;n:type:ShaderForge.SFN_Tex2d,id:4577,x:32060,y:33029,varname:node_4577,prsc:2,tex:bd2e0887544ccb34693ddef076f571a9,ntxv:0,isnm:False|UVIN-3957-UVOUT,TEX-9217-TEX;n:type:ShaderForge.SFN_ToggleProperty,id:587,x:32326,y:32656,ptovrint:False,ptlb:U,ptin:_U,varname:node_587,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_ToggleProperty,id:3067,x:32326,y:32835,ptovrint:False,ptlb:V,ptin:_V,varname:node_3067,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Multiply,id:2751,x:32518,y:32622,varname:node_2751,prsc:2|A-2850-OUT,B-587-OUT;n:type:ShaderForge.SFN_Multiply,id:1181,x:32518,y:32835,varname:node_1181,prsc:2|A-3067-OUT,B-1100-OUT;n:type:ShaderForge.SFN_Add,id:8686,x:32714,y:32704,varname:node_8686,prsc:2|A-2751-OUT,B-1181-OUT;n:type:ShaderForge.SFN_VertexColor,id:5705,x:33252,y:32537,varname:node_5705,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3907,x:33727,y:32201,varname:node_3907,prsc:2|A-9851-OUT,B-1004-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1004,x:33537,y:32368,ptovrint:False,ptlb:ColorVal,ptin:_ColorVal,varname:node_1004,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7924,x:33537,y:32493,ptovrint:False,ptlb:AlphaVal,ptin:_AlphaVal,varname:node_7924,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2705,x:33692,y:32519,varname:node_2705,prsc:2|A-7924-OUT,B-7902-OUT;n:type:ShaderForge.SFN_Panner,id:3413,x:32376,y:31855,varname:node_3413,prsc:2,spu:0,spv:1|UVIN-9269-OUT,DIST-7747-OUT;n:type:ShaderForge.SFN_Multiply,id:7747,x:32169,y:31996,varname:node_7747,prsc:2|A-3042-T,B-635-OUT;n:type:ShaderForge.SFN_Relay,id:7191,x:32058,y:31807,varname:node_7191,prsc:2|IN-6862-U;n:type:ShaderForge.SFN_Relay,id:4995,x:32058,y:31941,varname:node_4995,prsc:2|IN-6862-V;n:type:ShaderForge.SFN_Append,id:9269,x:32169,y:31855,varname:node_9269,prsc:2|A-7191-OUT,B-4995-OUT;n:type:ShaderForge.SFN_Panner,id:5050,x:32376,y:31996,varname:node_5050,prsc:2,spu:1,spv:0|UVIN-9269-OUT,DIST-7747-OUT;n:type:ShaderForge.SFN_Lerp,id:7225,x:32624,y:31921,varname:node_7225,prsc:2|A-3413-UVOUT,B-5050-UVOUT,T-5063-OUT;n:type:ShaderForge.SFN_Slider,id:5063,x:32219,y:32164,ptovrint:False,ptlb:Speed(U\V0~1),ptin:_SpeedUV01,varname:node_5063,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:635,x:32012,y:32062,ptovrint:False,ptlb:MainTexSpeed,ptin:_MainTexSpeed,varname:node_635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:9243-1004-7924-7162-5063-635-9217-5000-587-3067-8680-7927-6364;pass:END;sub:END;*/

Shader "effect/DistortBlendAlpha" {
    Properties {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        _ColorVal ("ColorVal", Float ) = 1
        _AlphaVal ("AlphaVal", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        _SpeedUV01 ("Speed(U\V0~1)", Range(0, 1)) = 0
        _MainTexSpeed ("MainTexSpeed", Float ) = 0
        _TexNoise01 ("TexNoise01", 2D) = "white" {}
        _TexNoise02 ("TexNoise02", 2D) = "white" {}
        [MaterialToggle] _U ("U", Float ) = 0
        [MaterialToggle] _V ("V", Float ) = 0
        _raodongqiangdu ("raodongqiangdu", Float ) = 0.2
        _TexNoise01Val ("TexNoise01Val", Float ) = 0.2
        _TexNoise02Val ("TexNoise02Val", Float ) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            //#pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _raodongqiangdu;
            uniform sampler2D _TexNoise01; uniform float4 _TexNoise01_ST;
            uniform sampler2D _TexNoise02; uniform float4 _TexNoise02_ST;
            uniform float _TexNoise02Val;
            uniform float _TexNoise01Val;
            uniform fixed _U;
            uniform fixed _V;
            uniform float _ColorVal;
            uniform float _AlphaVal;
            uniform float _SpeedUV01;
            uniform float _MainTexSpeed;
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
                float node_7747 = (node_3042.g*_MainTexSpeed);
                float2 node_9269 = float2(i.uv0.r,i.uv0.g);
                float2 node_3413 = (node_9269+node_7747*float2(0,1));
                float2 node_5050 = (node_9269+node_7747*float2(1,0));
                float2 node_1641 = (i.uv0+((_TexNoise01Val*node_3042.g)*2.0+-1.0)*float2(1,0));
                float4 _node_4199 = tex2D(_TexNoise01,TRANSFORM_TEX(node_1641, _TexNoise01));
                float2 node_784 = (i.uv0+((_TexNoise02Val*node_3042.g)*2.0+-1.0)*float2(1,0));
                float4 _node_3502 = tex2D(_TexNoise02,TRANSFORM_TEX(node_784, _TexNoise02));
                float2 node_3957 = (i.uv0+((_TexNoise01Val*node_3042.g)*2.0+-1.0)*float2(0,1));
                float4 node_4577 = tex2D(_TexNoise01,TRANSFORM_TEX(node_3957, _TexNoise01));
                float2 node_867 = (i.uv0+((node_3042.g*_TexNoise02Val)*2.0+-1.0)*float2(0,1));
                float4 node_5413 = tex2D(_TexNoise02,TRANSFORM_TEX(node_867, _TexNoise02));
                float node_8686 = (((_node_4199.r*_node_3502.r)*_U)+(_V*(node_4577.r*node_5413.r)));
                float2 node_1702 = (lerp(node_3413,node_5050,_SpeedUV01)+(_raodongqiangdu*node_8686));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1702, _MainTex));
                float3 emissive = ((_Color.rgb*_MainTex_var.rgb*i.vertexColor.rgb)*_ColorVal);
                float3 finalColor = emissive;
                return fixed4(finalColor,(_AlphaVal*(_MainTex_var.a*node_8686*i.vertexColor.a)));
            }
            ENDCG
        }
        //Pass {
        //    Name "ShadowCaster"
        //    Tags {
        //        "LightMode"="ShadowCaster"
        //    }
        //    Offset 1, 1
       //     Cull Off
       //     
       //     CGPROGRAM
       //     #pragma vertex vert
       //     #pragma fragment frag
       //     #define UNITY_PASS_SHADOWCASTER
       //     #include "UnityCG.cginc"
       //     #include "Lighting.cginc"
       //     #pragma fragmentoption ARB_precision_hint_fastest
      //      #pragma multi_compile_shadowcaster
       //     #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
       //     #pragma target 3.0
       //     struct VertexInput {
      //          float4 vertex : POSITION;
       //     };
       //     struct VertexOutput {
       //         V2F_SHADOW_CASTER;
       //     };
       //     VertexOutput vert (VertexInput v) {
       //         VertexOutput o = (VertexOutput)0;
       //         o.pos = UnityObjectToClipPos( v.vertex );
       //         TRANSFER_SHADOW_CASTER(o)
       //         return o;
       //     }
       //     float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
       //         float isFrontFace = ( facing >= 0 ? 1 : 0 );
       //         float faceSign = ( facing >= 0 ? 1 : -1 );
       //         SHADOW_CASTER_FRAGMENT(i)
       //     }
      //      ENDCG
      //  }
    }

}