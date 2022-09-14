// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33841,y:32712,varname:node_1,prsc:2|emission-254-OUT,alpha-253-A;n:type:ShaderForge.SFN_Slider,id:6,x:32648,y:32754,ptovrint:False,ptlb:node_6,ptin:_node_6,varname:node_6798,prsc:2,min:-0.5,cur:0.5756773,max:1;n:type:ShaderForge.SFN_Panner,id:39,x:32986,y:32602,varname:node_39,prsc:2,spu:1,spv:0|DIST-6-OUT;n:type:ShaderForge.SFN_Tex2d,id:70,x:33116,y:32808,ptovrint:False,ptlb:node_70,ptin:_node_70,varname:node_6162,prsc:2,tex:a1cbd7fe477c5f34898f4d1d1ec1fa71,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:87,x:33178,y:32551,ptovrint:False,ptlb:node_87,ptin:_node_87,varname:node_9820,prsc:2,tex:61948c0d7c2ec9d43b1d5930b6f81d0f,ntxv:0,isnm:False|UVIN-39-UVOUT;n:type:ShaderForge.SFN_Multiply,id:184,x:33322,y:32678,varname:node_184,prsc:2|A-87-R,B-70-R;n:type:ShaderForge.SFN_VertexColor,id:253,x:33294,y:32937,varname:node_253,prsc:2;n:type:ShaderForge.SFN_Multiply,id:254,x:33635,y:32755,varname:node_254,prsc:2|A-274-OUT,B-253-RGB,C-263-RGB;n:type:ShaderForge.SFN_Color,id:263,x:33472,y:32839,ptovrint:False,ptlb:node_263,ptin:_node_263,varname:node_7732,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:274,x:33522,y:32569,varname:node_274,prsc:2|A-275-OUT,B-184-OUT;n:type:ShaderForge.SFN_Vector1,id:275,x:33337,y:32574,varname:node_275,prsc:2,v1:3;proporder:6-70-87-263;pass:END;sub:END;*/

Shader "Custom/7_17dilie" {
    Properties {
        _node_6 ("node_6", Range(-0.5, 1)) = 0.5756773
        _node_70 ("node_70", 2D) = "white" {}
        _node_87 ("node_87", 2D) = "white" {}
        _node_263 ("node_263", Color) = (1,1,1,1)
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
            uniform float _node_6;
            uniform sampler2D _node_70; uniform float4 _node_70_ST;
            uniform sampler2D _node_87; uniform float4 _node_87_ST;
            uniform float4 _node_263;
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
                float2 node_39 = (i.uv0+_node_6*float2(1,0));
                float4 _node_87_var = tex2D(_node_87,TRANSFORM_TEX(node_39, _node_87));
                float4 _node_70_var = tex2D(_node_70,TRANSFORM_TEX(i.uv0, _node_70));
                float3 emissive = ((3.0*(_node_87_var.r*_node_70_var.r))*i.vertexColor.rgb*_node_263.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,i.vertexColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
