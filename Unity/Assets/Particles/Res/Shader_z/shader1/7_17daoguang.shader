// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:34364,y:32729,varname:node_1,prsc:2|emission-12-OUT,alpha-11-A;n:type:ShaderForge.SFN_Tex2d,id:2,x:32877,y:32682,ptovrint:False,ptlb:node_2,ptin:_node_2,varname:node_1683,prsc:2,ntxv:0,isnm:False|UVIN-4-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:32829,y:32930,ptovrint:False,ptlb:node_3,ptin:_node_3,varname:node_7429,prsc:2,ntxv:0,isnm:False|UVIN-5-UVOUT;n:type:ShaderForge.SFN_Panner,id:4,x:32698,y:32682,varname:node_4,prsc:2,spu:0.3,spv:1.4;n:type:ShaderForge.SFN_Panner,id:5,x:32635,y:32965,varname:node_5,prsc:2,spu:0,spv:2;n:type:ShaderForge.SFN_Multiply,id:6,x:33106,y:32768,varname:node_6,prsc:2|A-2-R,B-3-R;n:type:ShaderForge.SFN_Add,id:7,x:33419,y:32710,varname:node_7,prsc:2|A-9-UVOUT,B-6-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:33730,y:32917,ptovrint:False,ptlb:node_8,ptin:_node_8,varname:node_7611,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:9,x:33005,y:32502,varname:node_9,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:10,x:33646,y:32710,ptovrint:False,ptlb:node_10,ptin:_node_10,varname:node_3000,prsc:2,ntxv:0,isnm:False|UVIN-7-OUT;n:type:ShaderForge.SFN_VertexColor,id:11,x:33427,y:33009,varname:node_11,prsc:2;n:type:ShaderForge.SFN_Multiply,id:12,x:33944,y:32760,varname:node_12,prsc:2|A-10-RGB,B-11-RGB,C-8-R;proporder:2-3-8-10;pass:END;sub:END;*/

Shader "Shader Forge/7_17daoguang" {
    Properties {
        _node_2 ("node_2", 2D) = "white" {}
        _node_3 ("node_3", 2D) = "white" {}
        _node_8 ("node_8", 2D) = "white" {}
        _node_10 ("node_10", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            Cull Off
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
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform sampler2D _node_3; uniform float4 _node_3_ST;
            uniform sampler2D _node_8; uniform float4 _node_8_ST;
            uniform sampler2D _node_10; uniform float4 _node_10_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD1;
                #else
                    float3 shLight : TEXCOORD1;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_6215 = _Time + _TimeEditor;
                float2 node_4 = (i.uv0+node_6215.g*float2(0.3,1.4));
                float4 _node_2_var = tex2D(_node_2,TRANSFORM_TEX(node_4, _node_2));
                float2 node_5 = (i.uv0+node_6215.g*float2(0,2));
                float4 _node_3_var = tex2D(_node_3,TRANSFORM_TEX(node_5, _node_3));
                float2 node_7 = (i.uv0+(_node_2_var.r*_node_3_var.r));
                float4 _node_10_var = tex2D(_node_10,TRANSFORM_TEX(node_7, _node_10));
                float4 _node_8_var = tex2D(_node_8,TRANSFORM_TEX(i.uv0, _node_8));
                float3 emissive = (_node_10_var.rgb*i.vertexColor.rgb*_node_8_var.r);
                float3 finalColor = emissive;
                return fixed4(finalColor,i.vertexColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
