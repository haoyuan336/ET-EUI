// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1202,x:32719,y:32712,varname:node_1202,prsc:2|emission-5253-OUT;n:type:ShaderForge.SFN_Tex2d,id:7287,x:31958,y:32905,ptovrint:False,ptlb:node_7287,ptin:_node_7287,varname:node_7287,prsc:2,tex:ae1a7b68edb2b1e4f95a14e58e29d87c,ntxv:0,isnm:False|UVIN-4581-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5646,x:32047,y:32651,ptovrint:False,ptlb:node_5646,ptin:_node_5646,varname:node_5646,prsc:2,tex:6f872a7620b38c142b658998c6324b71,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7653,x:32377,y:32849,varname:node_7653,prsc:2|A-5646-A,B-6935-OUT;n:type:ShaderForge.SFN_Panner,id:4581,x:31694,y:32922,varname:node_4581,prsc:2,spu:0,spv:-0.3;n:type:ShaderForge.SFN_Slider,id:6707,x:31969,y:32482,ptovrint:False,ptlb:node_6707,ptin:_node_6707,varname:node_6707,prsc:2,min:0,cur:2.153846,max:5;n:type:ShaderForge.SFN_Multiply,id:5253,x:32551,y:32595,varname:node_5253,prsc:2|A-7653-OUT,B-745-RGB;n:type:ShaderForge.SFN_Color,id:745,x:32354,y:32523,ptovrint:False,ptlb:node_745,ptin:_node_745,varname:node_745,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6935,x:32183,y:32966,varname:node_6935,prsc:2|A-7287-R,B-6707-OUT;proporder:7287-5646-6707-745;pass:END;sub:END;*/

Shader "Unlit/ui_zhenrong_lizi_zi" {
    Properties {
        _node_7287 ("node_7287", 2D) = "white" {}
        _node_5646 ("node_5646", 2D) = "white" {}
        _node_6707 ("node_6707", Range(0, 5)) = 2.153846
        _node_745 ("node_745", Color) = (0.5,0.5,0.5,1)
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
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_7287; uniform float4 _node_7287_ST;
            uniform sampler2D _node_5646; uniform float4 _node_5646_ST;
            uniform float _node_6707;
            uniform float4 _node_745;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _node_5646_var = tex2D(_node_5646,TRANSFORM_TEX(i.uv0, _node_5646));
                float4 node_1494 = _Time + _TimeEditor;
                float2 node_4581 = (i.uv0+node_1494.g*float2(0,-0.3));
                float4 _node_7287_var = tex2D(_node_7287,TRANSFORM_TEX(node_4581, _node_7287));
                float3 emissive = ((_node_5646_var.a*(_node_7287_var.r*_node_6707))*_node_745.rgb);
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
