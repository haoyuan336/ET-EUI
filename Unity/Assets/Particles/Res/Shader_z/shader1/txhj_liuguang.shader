// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.08780278,fgcg:0.2374955,fgcb:0.4117647,fgca:1,fgde:0.01,fgrn:10.49,fgrf:40.66,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4453,x:32719,y:32712,varname:node_4453,prsc:2|emission-8214-OUT;n:type:ShaderForge.SFN_Tex2d,id:2681,x:31683,y:33060,ptovrint:False,ptlb:node_2681,ptin:_node_2681,varname:node_2681,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e2e4156b398dd594da51ee794c29083f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6171,x:31569,y:32725,ptovrint:False,ptlb:node_6171,ptin:_node_6171,varname:node_6171,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a8b1973dfa81a014a8c7317f2903564f,ntxv:0,isnm:False|UVIN-8863-UVOUT;n:type:ShaderForge.SFN_Add,id:8214,x:32012,y:32815,varname:node_8214,prsc:2|A-36-OUT,B-2681-RGB;n:type:ShaderForge.SFN_TexCoord,id:910,x:30969,y:32877,varname:node_910,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:8863,x:31146,y:32877,varname:node_8863,prsc:2,spu:0,spv:-0.2|UVIN-910-UVOUT;n:type:ShaderForge.SFN_Multiply,id:36,x:31900,y:32617,varname:node_36,prsc:2|A-6171-RGB,B-3886-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3886,x:31630,y:32448,ptovrint:False,ptlb:node_3886,ptin:_node_3886,varname:node_3886,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:2681-6171-3886;pass:END;sub:END;*/

Shader "Unlit/txhj_liuguang" {
    Properties {
        _node_2681 ("node_2681", 2D) = "white" {}
        _node_6171 ("node_6171", 2D) = "white" {}
        _node_3886 ("node_3886", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2681; uniform float4 _node_2681_ST;
            uniform sampler2D _node_6171; uniform float4 _node_6171_ST;
            uniform float _node_3886;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_6611 = _Time + _TimeEditor;
                float2 node_8863 = (i.uv0+node_6611.g*float2(0,-0.2));
                float4 _node_6171_var = tex2D(_node_6171,TRANSFORM_TEX(node_8863, _node_6171));
                float4 _node_2681_var = tex2D(_node_2681,TRANSFORM_TEX(i.uv0, _node_2681));
                float3 emissive = ((_node_6171_var.rgb*_node_3886)+_node_2681_var.rgb);
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
