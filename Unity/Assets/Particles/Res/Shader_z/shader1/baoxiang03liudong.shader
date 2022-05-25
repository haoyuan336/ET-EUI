// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5074,x:32719,y:32712,varname:node_5074,prsc:2|emission-2084-OUT,clip-8225-A;n:type:ShaderForge.SFN_Tex2d,id:9878,x:30931,y:32979,ptovrint:False,ptlb:node_9878,ptin:_node_9878,varname:node_9878,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6327,x:31222,y:32628,ptovrint:False,ptlb:node_6327,ptin:_node_6327,varname:node_6327,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cb329efbd8297bf40a12ba3673144390,ntxv:0,isnm:False|UVIN-4588-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6896,x:30881,y:32523,varname:node_6896,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4588,x:31047,y:32543,varname:node_4588,prsc:2,spu:-0.1,spv:-0.2|UVIN-6896-UVOUT;n:type:ShaderForge.SFN_Add,id:2084,x:32318,y:33048,varname:node_2084,prsc:2|A-8889-OUT,B-6812-RGB;n:type:ShaderForge.SFN_Tex2d,id:6812,x:32041,y:33244,ptovrint:False,ptlb:node_6812,ptin:_node_6812,varname:node_6812,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c41ba9128b88e8b48b3926c828fce82a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:4639,x:31406,y:32839,varname:node_4639,prsc:2|A-6327-B,B-9878-R;n:type:ShaderForge.SFN_Color,id:8225,x:31847,y:33532,ptovrint:False,ptlb:node_8225,ptin:_node_8225,varname:node_8225,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:8889,x:32016,y:33030,varname:node_8889,prsc:2|A-726-OUT,B-8225-RGB;n:type:ShaderForge.SFN_Multiply,id:726,x:31579,y:33034,varname:node_726,prsc:2|A-4639-OUT,B-3425-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3425,x:31302,y:33230,ptovrint:False,ptlb:node_3425,ptin:_node_3425,varname:node_3425,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:9878-6327-6812-8225-3425;pass:END;sub:END;*/

Shader "Unlit/baoxiang03liudong" {
    Properties {
        _node_9878 ("node_9878", 2D) = "white" {}
        _node_6327 ("node_6327", 2D) = "white" {}
        _node_6812 ("node_6812", 2D) = "white" {}
        _node_8225 ("node_8225", Color) = (0.5,0.5,0.5,1)
        _node_3425 ("node_3425", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            uniform sampler2D _node_9878; uniform float4 _node_9878_ST;
            uniform sampler2D _node_6327; uniform float4 _node_6327_ST;
            uniform sampler2D _node_6812; uniform float4 _node_6812_ST;
            uniform float4 _node_8225;
            uniform float _node_3425;
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
                clip(_node_8225.a - 0.5);
////// Lighting:
////// Emissive:
                float4 node_9983 = _Time + _TimeEditor;
                float2 node_4588 = (i.uv0+node_9983.g*float2(-0.1,-0.2));
                float4 _node_6327_var = tex2D(_node_6327,TRANSFORM_TEX(node_4588, _node_6327));
                float4 _node_9878_var = tex2D(_node_9878,TRANSFORM_TEX(i.uv0, _node_9878));
                float4 _node_6812_var = tex2D(_node_6812,TRANSFORM_TEX(i.uv0, _node_6812));
                float3 emissive = ((((_node_6327_var.b*_node_9878_var.r)*_node_3425)*_node_8225.rgb)+_node_6812_var.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _node_8225;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip(_node_8225.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
