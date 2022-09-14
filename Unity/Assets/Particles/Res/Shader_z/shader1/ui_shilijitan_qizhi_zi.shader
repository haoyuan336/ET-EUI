// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:300,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3304,x:32719,y:32712,varname:node_3304,prsc:2|emission-5859-OUT,clip-3644-A;n:type:ShaderForge.SFN_Tex2d,id:3644,x:32022,y:32687,ptovrint:False,ptlb:node_3644,ptin:_node_3644,varname:node_3644,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5001ec74f0bf1dd4d892f02d021d4779,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6553,x:32022,y:33118,ptovrint:False,ptlb:node_6553,ptin:_node_6553,varname:node_6553,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e9d27787ce0126747a42f178b22330ae,ntxv:0,isnm:False|UVIN-2047-UVOUT;n:type:ShaderForge.SFN_Panner,id:2047,x:31831,y:32920,varname:node_2047,prsc:2,spu:0,spv:0.25|UVIN-6209-UVOUT,DIST-6868-OUT;n:type:ShaderForge.SFN_TexCoord,id:6209,x:31581,y:32961,varname:node_6209,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:6868,x:31711,y:33227,ptovrint:False,ptlb:node_6868,ptin:_node_6868,varname:node_6868,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:4694,x:32022,y:32920,ptovrint:False,ptlb:node_4694,ptin:_node_4694,varname:node_4694,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5859,x:32479,y:32860,varname:node_5859,prsc:2|A-1058-OUT,B-7423-OUT,C-3644-RGB;n:type:ShaderForge.SFN_ValueProperty,id:7423,x:32250,y:33349,ptovrint:False,ptlb:node_7423,ptin:_node_7423,varname:node_7423,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1058,x:32229,y:33080,varname:node_1058,prsc:2|A-4694-RGB,B-6553-R;proporder:3644-6553-6868-4694-7423;pass:END;sub:END;*/

Shader "Unlit/ui_shilijitan_qizhi_zi" {
    Properties {
        _node_3644 ("node_3644", 2D) = "white" {}
        _node_6553 ("node_6553", 2D) = "white" {}
        _node_6868 ("node_6868", Float ) = 1
        _node_4694 ("node_4694", Color) = (0.5,0.5,0.5,1)
        _node_7423 ("node_7423", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+300"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _node_3644; uniform float4 _node_3644_ST;
            uniform sampler2D _node_6553; uniform float4 _node_6553_ST;
            uniform float _node_6868;
            uniform float4 _node_4694;
            uniform float _node_7423;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _node_3644_var = tex2D(_node_3644,TRANSFORM_TEX(i.uv0, _node_3644));
                clip(_node_3644_var.a - 0.5);
////// Lighting:
////// Emissive:
                float2 node_2047 = (i.uv0+_node_6868*float2(0,0.25));
                float4 _node_6553_var = tex2D(_node_6553,TRANSFORM_TEX(node_2047, _node_6553));
                float3 emissive = ((_node_4694.rgb*_node_6553_var.r)*_node_7423*_node_3644_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _node_3644; uniform float4 _node_3644_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _node_3644_var = tex2D(_node_3644,TRANSFORM_TEX(i.uv0, _node_3644));
                clip(_node_3644_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
