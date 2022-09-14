// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:33641,y:32712,varname:node_1,prsc:2|emission-9866-OUT,alpha-9840-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33117,y:32707,ptovrint:False,ptlb:node_2,ptin:_node_2,varname:node_1187,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44866e8daffa8df4c91019bb6edd5d25,ntxv:0,isnm:False|UVIN-3-UVOUT;n:type:ShaderForge.SFN_Panner,id:3,x:32904,y:32684,varname:node_3,prsc:2,spu:-2,spv:0|UVIN-9859-UVOUT;n:type:ShaderForge.SFN_Multiply,id:12,x:33296,y:32898,varname:node_12,prsc:2|A-2-RGB,B-13-OUT;n:type:ShaderForge.SFN_Vector1,id:13,x:32985,y:32988,varname:node_13,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:2132,x:33360,y:32587,ptovrint:False,ptlb:node_2132,ptin:_node_2132,varname:node_2132,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9866,x:33466,y:32744,varname:node_9866,prsc:2|A-2132-RGB,B-12-OUT;n:type:ShaderForge.SFN_TexCoord,id:9859,x:32663,y:32669,varname:node_9859,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9840,x:33449,y:33071,varname:node_9840,prsc:2|A-2132-A,B-2-A;proporder:2-2132;pass:END;sub:END;*/

Shader "Custom/daqiao_jiaxue" {
    Properties {
        _node_2 ("node_2", 2D) = "white" {}
        _node_2132 ("node_2132", Color) = (0.5,0.5,0.5,1)
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
            Name "FORWARD"
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
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform float4 _node_2132;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_5492 = _Time + _TimeEditor;
                float2 node_3 = (i.uv0+node_5492.g*float2(-2,0));
                float4 _node_2_var = tex2D(_node_2,TRANSFORM_TEX(node_3, _node_2));
                float3 emissive = (_node_2132.rgb*(_node_2_var.rgb*2.0));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_node_2132.a*_node_2_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
