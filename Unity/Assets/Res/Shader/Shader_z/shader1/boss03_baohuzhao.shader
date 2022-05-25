// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6864,x:32719,y:32712,varname:node_6864,prsc:2|emission-8210-OUT,alpha-1818-OUT;n:type:ShaderForge.SFN_Tex2d,id:9342,x:31781,y:32434,ptovrint:False,ptlb:node_9342,ptin:_node_9342,varname:node_9342,prsc:2,ntxv:0,isnm:False|UVIN-4614-UVOUT;n:type:ShaderForge.SFN_Panner,id:4614,x:31542,y:32536,varname:node_4614,prsc:2,spu:0.2,spv:-0.8;n:type:ShaderForge.SFN_Tex2d,id:4089,x:32056,y:32847,ptovrint:False,ptlb:node_4089,ptin:_node_4089,varname:node_4089,prsc:2,tex:66371ec7c7947d14286785d76b728bcc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:7063,x:31930,y:32485,varname:node_7063,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:2562,x:32118,y:32425,varname:node_2562,prsc:2|A-9342-R,B-7063-OUT;n:type:ShaderForge.SFN_Vector1,id:9822,x:32046,y:33019,varname:node_9822,prsc:2,v1:1;n:type:ShaderForge.SFN_Power,id:1818,x:32314,y:32872,varname:node_1818,prsc:2|VAL-4089-R,EXP-9822-OUT;n:type:ShaderForge.SFN_Color,id:9375,x:32255,y:32672,ptovrint:False,ptlb:node_9375,ptin:_node_9375,varname:node_9375,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:7925,x:32292,y:32523,varname:node_7925,prsc:2|A-2562-OUT,B-1293-OUT;n:type:ShaderForge.SFN_Multiply,id:8210,x:32480,y:32570,varname:node_8210,prsc:2|A-7925-OUT,B-9375-RGB;n:type:ShaderForge.SFN_Slider,id:1293,x:31876,y:32634,ptovrint:False,ptlb:node_1293,ptin:_node_1293,varname:node_1293,prsc:2,min:0,cur:1.426744,max:3;proporder:9342-4089-9375-1293;pass:END;sub:END;*/

Shader "Unlit/boss03_baohuzhao" {
    Properties {
        _node_9342 ("node_9342", 2D) = "white" {}
        _node_4089 ("node_4089", 2D) = "white" {}
        _node_9375 ("node_9375", Color) = (0.5,0.5,0.5,1)
        _node_1293 ("node_1293", Range(0, 3)) = 1.426744
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
            uniform sampler2D _node_9342; uniform float4 _node_9342_ST;
            uniform sampler2D _node_4089; uniform float4 _node_4089_ST;
            uniform float4 _node_9375;
            uniform float _node_1293;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD1;
                #else
                    float3 shLight : TEXCOORD1;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_1043 = _Time + _TimeEditor;
                float2 node_4614 = (i.uv0+node_1043.g*float2(0.2,-0.8));
                float4 _node_9342_var = tex2D(_node_9342,TRANSFORM_TEX(node_4614, _node_9342));
                float3 emissive = (((_node_9342_var.r*2.0)*_node_1293)*_node_9375.rgb);
                float3 finalColor = emissive;
                float4 _node_4089_var = tex2D(_node_4089,TRANSFORM_TEX(i.uv0, _node_4089));
                return fixed4(finalColor,pow(_node_4089_var.r,1.0));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
