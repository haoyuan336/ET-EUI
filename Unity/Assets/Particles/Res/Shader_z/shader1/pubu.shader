// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:34129,y:32717,varname:node_1,prsc:2|emission-28-OUT,alpha-11-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33119,y:32870,ptovrint:False,ptlb:node_2,ptin:_node_2,varname:node_1838,prsc:2,ntxv:0,isnm:False|UVIN-6-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:33135,y:32692,ptovrint:False,ptlb:node_3,ptin:_node_3,varname:node_360,prsc:2,tex:f70f1e1970289814aa4217c002113a05,ntxv:0,isnm:False|UVIN-5-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4,x:33196,y:33064,ptovrint:False,ptlb:node_4,ptin:_node_4,varname:node_9622,prsc:2,tex:753675c49623e3d4a91e06b1d39b3083,ntxv:0,isnm:False|UVIN-8-UVOUT;n:type:ShaderForge.SFN_Panner,id:5,x:32946,y:32634,varname:node_5,prsc:2,spu:0,spv:0.8;n:type:ShaderForge.SFN_Panner,id:6,x:32908,y:32837,varname:node_6,prsc:2,spu:0,spv:0.5;n:type:ShaderForge.SFN_Add,id:7,x:33327,y:32919,varname:node_7,prsc:2|A-3-R,B-2-R;n:type:ShaderForge.SFN_Panner,id:8,x:32961,y:33042,varname:node_8,prsc:2,spu:0,spv:1;n:type:ShaderForge.SFN_Add,id:9,x:33491,y:33007,varname:node_9,prsc:2|A-7-OUT,B-4-R;n:type:ShaderForge.SFN_Tex2d,id:10,x:33755,y:33095,ptovrint:False,ptlb:node_10,ptin:_node_10,varname:node_1960,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:11,x:33865,y:32913,varname:node_11,prsc:2|A-9-OUT,B-10-R;n:type:ShaderForge.SFN_Multiply,id:24,x:33569,y:32744,varname:node_24,prsc:2|A-2-RGB,B-26-RGB;n:type:ShaderForge.SFN_VertexColor,id:26,x:33431,y:32799,varname:node_26,prsc:2;n:type:ShaderForge.SFN_Vector3,id:27,x:33694,y:32536,varname:node_27,prsc:2,v1:0.7279412,v2:0.8649088,v3:1;n:type:ShaderForge.SFN_Multiply,id:28,x:33950,y:32640,varname:node_28,prsc:2|A-27-OUT,B-49-OUT;n:type:ShaderForge.SFN_Multiply,id:49,x:33743,y:32698,varname:node_49,prsc:2|A-50-OUT,B-24-OUT;n:type:ShaderForge.SFN_Vector1,id:50,x:33499,y:32622,varname:node_50,prsc:2,v1:1;proporder:4-10-2-3;pass:END;sub:END;*/

Shader "Custom/pubu" {
    Properties {
        _node_4 ("node_4", 2D) = "white" {}
        _node_10 ("node_10", 2D) = "white" {}
        _node_2 ("node_2", 2D) = "white" {}
        _node_3 ("node_3", 2D) = "white" {}
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform sampler2D _node_3; uniform float4 _node_3_ST;
            uniform sampler2D _node_4; uniform float4 _node_4_ST;
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
                UNITY_FOG_COORDS(1)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_585 = _Time + _TimeEditor;
                float2 node_6 = (i.uv0+node_585.g*float2(0,0.5));
                float4 _node_2_var = tex2D(_node_2,TRANSFORM_TEX(node_6, _node_2));
                float3 emissive = (float3(0.7279412,0.8649088,1)*(1.0*(_node_2_var.rgb*i.vertexColor.rgb)));
                float3 finalColor = emissive;
                float2 node_5 = (i.uv0+node_585.g*float2(0,0.8));
                float4 _node_3_var = tex2D(_node_3,TRANSFORM_TEX(node_5, _node_3));
                float2 node_8 = (i.uv0+node_585.g*float2(0,1));
                float4 _node_4_var = tex2D(_node_4,TRANSFORM_TEX(node_8, _node_4));
                float4 _node_10_var = tex2D(_node_10,TRANSFORM_TEX(i.uv0, _node_10));
                fixed4 finalRGBA = fixed4(finalColor,(((_node_3_var.r+_node_2_var.r)+_node_4_var.r)*_node_10_var.r));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
