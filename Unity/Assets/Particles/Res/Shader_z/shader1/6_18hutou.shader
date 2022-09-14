// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-100-OUT,alpha-101-A;n:type:ShaderForge.SFN_Tex2d,id:4,x:33923,y:32439,ptlb:node_4,ptin:_node_4,tex:3044061a74a49704ab35852bdc2e1982,ntxv:0,isnm:False|UVIN-5-UVOUT;n:type:ShaderForge.SFN_Panner,id:5,x:34119,y:32622,spu:-0.2,spv:-0.3;n:type:ShaderForge.SFN_Panner,id:6,x:34003,y:32886,spu:0.3,spv:-0.7;n:type:ShaderForge.SFN_Multiply,id:7,x:33562,y:32744|A-139-OUT,B-217-R;n:type:ShaderForge.SFN_Multiply,id:100,x:32964,y:32797|A-231-OUT,B-101-RGB,C-242-OUT;n:type:ShaderForge.SFN_VertexColor,id:101,x:33208,y:32950;n:type:ShaderForge.SFN_Multiply,id:112,x:33357,y:32644|A-113-OUT,B-7-OUT;n:type:ShaderForge.SFN_Vector1,id:113,x:33520,y:32622,v1:3;n:type:ShaderForge.SFN_Vector1,id:137,x:33939,y:32622,v1:1;n:type:ShaderForge.SFN_Power,id:139,x:33715,y:32512|VAL-4-R,EXP-137-OUT;n:type:ShaderForge.SFN_Panner,id:153,x:33400,y:32238,spu:0,spv:-0.3;n:type:ShaderForge.SFN_Add,id:155,x:33176,y:32569|A-203-OUT,B-112-OUT;n:type:ShaderForge.SFN_Tex2d,id:194,x:33249,y:32348,ptlb:node_194,ptin:_node_194,tex:3044061a74a49704ab35852bdc2e1982,ntxv:0,isnm:False|UVIN-153-UVOUT;n:type:ShaderForge.SFN_Power,id:203,x:32972,y:32394|VAL-194-R,EXP-204-OUT;n:type:ShaderForge.SFN_Vector1,id:204,x:33126,y:32475,v1:1;n:type:ShaderForge.SFN_Tex2d,id:217,x:33752,y:32836,ptlb:node_217,ptin:_node_217,tex:f6392bcb65970f94eb288f66ec6a7400,ntxv:0,isnm:False|UVIN-6-UVOUT;n:type:ShaderForge.SFN_Vector3,id:230,x:33337,y:32813,v1:1,v2:0.6471065,v3:0.313365;n:type:ShaderForge.SFN_Multiply,id:231,x:32990,y:32638|A-155-OUT,B-230-OUT;n:type:ShaderForge.SFN_ValueProperty,id:242,x:33436,y:32934,ptlb:node_242,ptin:_node_242,glob:False,v1:0.5;proporder:4-194-217-242;pass:END;sub:END;*/

Shader "Custom/6_18hutou" {
    Properties {
        _node_4 ("node_4", 2D) = "white" {}
        _node_194 ("node_194", 2D) = "white" {}
        _node_217 ("node_217", 2D) = "white" {}
        _node_242 ("node_242", Float ) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_4; uniform float4 _node_4_ST;
            uniform sampler2D _node_194; uniform float4 _node_194_ST;
            uniform sampler2D _node_217; uniform float4 _node_217_ST;
            uniform float _node_242;
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
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_252 = _Time + _TimeEditor;
                float2 node_251 = i.uv0;
                float2 node_153 = (node_251.rg+node_252.g*float2(0,-0.3));
                float2 node_5 = (node_251.rg+node_252.g*float2(-0.2,-0.3));
                float2 node_6 = (node_251.rg+node_252.g*float2(0.3,-0.7));
                float4 node_101 = i.vertexColor;
                float3 emissive = (((pow(tex2D(_node_194,TRANSFORM_TEX(node_153, _node_194)).r,1.0)+(3.0*(pow(tex2D(_node_4,TRANSFORM_TEX(node_5, _node_4)).r,1.0)*tex2D(_node_217,TRANSFORM_TEX(node_6, _node_217)).r)))*float3(1,0.6471065,0.313365))*node_101.rgb*_node_242);
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,node_101.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
