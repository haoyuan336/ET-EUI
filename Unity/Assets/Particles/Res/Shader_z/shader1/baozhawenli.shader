// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33423,y:32700,varname:node_4013,prsc:2|emission-3180-OUT,clip-2127-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32768,y:32655,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:308,x:32481,y:32818,ptovrint:False,ptlb:node_308,ptin:_node_308,varname:node_308,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7328,x:32376,y:33228,ptovrint:False,ptlb:node_7328,ptin:_node_7328,varname:node_7328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2467,x:32768,y:32851,varname:node_2467,prsc:2|A-308-RGB,B-3991-RGB;n:type:ShaderForge.SFN_VertexColor,id:3991,x:32523,y:33027,varname:node_3991,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3180,x:33083,y:32774,varname:node_3180,prsc:2|A-1304-RGB,B-2467-OUT,C-6820-OUT;n:type:ShaderForge.SFN_Add,id:4597,x:32799,y:33129,varname:node_4597,prsc:2|A-308-R,B-7328-R;n:type:ShaderForge.SFN_Multiply,id:2127,x:33059,y:33090,varname:node_2127,prsc:2|A-3991-A,B-4597-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6820,x:32958,y:32934,ptovrint:False,ptlb:node_6820,ptin:_node_6820,varname:node_6820,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:1304-308-7328-6820;pass:END;sub:END;*/

Shader "Shader Forge/baozhawenli" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _node_308 ("node_308", 2D) = "white" {}
        _node_7328 ("node_7328", 2D) = "white" {}
        _node_6820 ("node_6820", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _node_308; uniform float4 _node_308_ST;
            uniform sampler2D _node_7328; uniform float4 _node_7328_ST;
            uniform float _node_6820;
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
                float4 _node_308_var = tex2D(_node_308,TRANSFORM_TEX(i.uv0, _node_308));
                float4 _node_7328_var = tex2D(_node_7328,TRANSFORM_TEX(i.uv0, _node_7328));
                clip((i.vertexColor.a*(_node_308_var.r+_node_7328_var.r)) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_Color.rgb*(_node_308_var.rgb*i.vertexColor.rgb)*_node_6820);
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
            uniform sampler2D _node_308; uniform float4 _node_308_ST;
            uniform sampler2D _node_7328; uniform float4 _node_7328_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _node_308_var = tex2D(_node_308,TRANSFORM_TEX(i.uv0, _node_308));
                float4 _node_7328_var = tex2D(_node_7328,TRANSFORM_TEX(i.uv0, _node_7328));
                clip((i.vertexColor.a*(_node_308_var.r+_node_7328_var.r)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
