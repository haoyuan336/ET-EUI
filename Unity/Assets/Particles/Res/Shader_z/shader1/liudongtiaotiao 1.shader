// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33264,y:32921,varname:node_4013,prsc:2|emission-5220-OUT,alpha-1304-A;n:type:ShaderForge.SFN_Tex2d,id:170,x:32717,y:32878,ptovrint:False,ptlb:node_170,ptin:_node_170,varname:node_170,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:71bb32570d038ef4aae88948fafad599,ntxv:0,isnm:False|UVIN-6263-UVOUT;n:type:ShaderForge.SFN_Panner,id:6263,x:32552,y:32878,varname:node_6263,prsc:2,spu:0,spv:1|UVIN-8069-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8069,x:32399,y:32878,varname:node_8069,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:5978,x:32650,y:33067,ptovrint:False,ptlb:node_5978,ptin:_node_5978,varname:node_5978,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ccfbe011e9ac443a6e8475ebc2833b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1197,x:32881,y:32944,varname:node_1197,prsc:2|A-170-RGB,B-5978-RGB;n:type:ShaderForge.SFN_Multiply,id:5220,x:33037,y:33020,varname:node_5220,prsc:2|A-1197-OUT,B-1304-RGB;n:type:ShaderForge.SFN_VertexColor,id:1304,x:32791,y:33241,varname:node_1304,prsc:2;proporder:170-5978;pass:END;sub:END;*/

Shader "Shader Forge/liudongtiaotiao" {
    Properties {
        _node_170 ("node_170", 2D) = "white" {}
        _node_5978 ("node_5978", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_170; uniform float4 _node_170_ST;
            uniform sampler2D _node_5978; uniform float4 _node_5978_ST;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_2184 = _Time + _TimeEditor;
                float2 node_6263 = (i.uv0+node_2184.g*float2(0,1));
                float4 _node_170_var = tex2D(_node_170,TRANSFORM_TEX(node_6263, _node_170));
                float4 _node_5978_var = tex2D(_node_5978,TRANSFORM_TEX(i.uv0, _node_5978));
                float3 emissive = ((_node_170_var.rgb*_node_5978_var.rgb)*i.vertexColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,i.vertexColor.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
