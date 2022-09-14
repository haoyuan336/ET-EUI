// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:8034,x:32719,y:32712,varname:node_8034,prsc:2|emission-1364-OUT;n:type:ShaderForge.SFN_Tex2d,id:8923,x:31356,y:32800,ptovrint:False,ptlb:node_8923,ptin:_node_8923,varname:node_8923,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:26515ba28811dee4eb2d4c4ca4c10918,ntxv:0,isnm:False|UVIN-4444-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2211,x:31355,y:32543,ptovrint:False,ptlb:node_2211,ptin:_node_2211,varname:node_2211,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:742faf48242549644be32f2a148ff8f2,ntxv:0,isnm:False|UVIN-5723-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9338,x:32027,y:32750,ptovrint:False,ptlb:node_9338,ptin:_node_9338,varname:node_9338,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7577cf76dc352a943bba8fd0113ed812,ntxv:0,isnm:False|UVIN-2058-OUT;n:type:ShaderForge.SFN_TexCoord,id:5660,x:30900,y:32543,varname:node_5660,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:5723,x:31121,y:32543,varname:node_5723,prsc:2,spu:0.3,spv:0.3|UVIN-5660-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2241,x:30906,y:32803,varname:node_2241,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4444,x:31120,y:32803,varname:node_4444,prsc:2,spu:-0.2,spv:0.1|UVIN-2241-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:5068,x:31758,y:32633,varname:node_5068,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3699-OUT;n:type:ShaderForge.SFN_Add,id:3699,x:31550,y:32643,varname:node_3699,prsc:2|A-2211-R,B-8923-R;n:type:ShaderForge.SFN_Add,id:2058,x:31850,y:32807,varname:node_2058,prsc:2|A-5068-OUT,B-8338-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6653,x:31356,y:33017,varname:node_6653,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:8338,x:31575,y:33017,varname:node_8338,prsc:2,spu:0,spv:0.1|UVIN-6653-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:9244,x:32216,y:32989,varname:node_9244,prsc:2|EXP-3589-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3589,x:31995,y:33147,ptovrint:False,ptlb:node_3589,ptin:_node_3589,varname:node_3589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.3;n:type:ShaderForge.SFN_Multiply,id:1364,x:32486,y:32780,varname:node_1364,prsc:2|A-1195-OUT,B-6948-RGB,C-9244-OUT,D-7686-OUT;n:type:ShaderForge.SFN_Multiply,id:220,x:32151,y:32471,varname:node_220,prsc:2|A-136-OUT,B-9338-R;n:type:ShaderForge.SFN_ValueProperty,id:136,x:31861,y:32456,ptovrint:False,ptlb:liang,ptin:_liang,varname:node_136,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:6948,x:32233,y:32832,ptovrint:False,ptlb:node_6948,ptin:_node_6948,varname:node_6948,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7261,x:31930,y:33317,ptovrint:False,ptlb:node_7261,ptin:_node_7261,varname:node_7261,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:66371ec7c7947d14286785d76b728bcc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:7686,x:32236,y:33361,varname:node_7686,prsc:2|VAL-7261-R,EXP-1672-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1672,x:32073,y:33539,ptovrint:False,ptlb:node_1672,ptin:_node_1672,varname:node_1672,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:3549,x:32463,y:32169,ptovrint:False,ptlb:node_3549,ptin:_node_3549,varname:node_3549,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:34c9ce9304139304eb67f49ee2a12312,ntxv:0,isnm:False|UVIN-3636-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9253,x:32041,y:32169,varname:node_9253,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:3636,x:32215,y:32169,varname:node_3636,prsc:2,spu:0,spv:0.8|UVIN-9253-UVOUT;n:type:ShaderForge.SFN_Add,id:1195,x:32529,y:32525,varname:node_1195,prsc:2|A-2313-OUT,B-220-OUT;n:type:ShaderForge.SFN_Multiply,id:2313,x:32357,y:32333,varname:node_2313,prsc:2|A-3549-R,B-1266-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1266,x:32062,y:32380,ptovrint:False,ptlb:xian,ptin:_xian,varname:node_1266,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:8923-2211-9338-3589-136-6948-7261-1672-3549-1266;pass:END;sub:END;*/

Shader "Unlit/xuchu_zhaozi" {
    Properties {
        _node_8923 ("node_8923", 2D) = "white" {}
        _node_2211 ("node_2211", 2D) = "white" {}
        _node_9338 ("node_9338", 2D) = "white" {}
        _node_3589 ("node_3589", Float ) = 2.3
        _liang ("liang", Float ) = 1
        _node_6948 ("node_6948", Color) = (0.5,0.5,0.5,1)
        _node_7261 ("node_7261", 2D) = "white" {}
        _node_1672 ("node_1672", Float ) = 1
        _node_3549 ("node_3549", 2D) = "white" {}
        _xian ("xian", Float ) = 1
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_8923; uniform float4 _node_8923_ST;
            uniform sampler2D _node_2211; uniform float4 _node_2211_ST;
            uniform sampler2D _node_9338; uniform float4 _node_9338_ST;
            uniform float _node_3589;
            uniform float _liang;
            uniform float4 _node_6948;
            uniform sampler2D _node_7261; uniform float4 _node_7261_ST;
            uniform float _node_1672;
            uniform sampler2D _node_3549; uniform float4 _node_3549_ST;
            uniform float _xian;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_8740 = _Time + _TimeEditor;
                float2 node_3636 = (i.uv0+node_8740.g*float2(0,0.8));
                float4 _node_3549_var = tex2D(_node_3549,TRANSFORM_TEX(node_3636, _node_3549));
                float2 node_5723 = (i.uv0+node_8740.g*float2(0.3,0.3));
                float4 _node_2211_var = tex2D(_node_2211,TRANSFORM_TEX(node_5723, _node_2211));
                float2 node_4444 = (i.uv0+node_8740.g*float2(-0.2,0.1));
                float4 _node_8923_var = tex2D(_node_8923,TRANSFORM_TEX(node_4444, _node_8923));
                float2 node_2058 = ((_node_2211_var.r+_node_8923_var.r).r+(i.uv0+node_8740.g*float2(0,0.1)));
                float4 _node_9338_var = tex2D(_node_9338,TRANSFORM_TEX(node_2058, _node_9338));
                float4 _node_7261_var = tex2D(_node_7261,TRANSFORM_TEX(i.uv0, _node_7261));
                float3 emissive = (((_node_3549_var.r*_xian)+(_liang*_node_9338_var.r))*_node_6948.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_3589)*pow(_node_7261_var.r,_node_1672));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
