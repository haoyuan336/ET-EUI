// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3906,x:32719,y:32712,varname:node_3906,prsc:2|emission-560-OUT,alpha-5891-OUT;n:type:ShaderForge.SFN_Tex2d,id:7621,x:31872,y:32529,ptovrint:False,ptlb:node_7621,ptin:_node_7621,varname:node_7621,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:adf6c0309ba91ff46ba5f302377a2327,ntxv:0,isnm:False|UVIN-8307-UVOUT;n:type:ShaderForge.SFN_Panner,id:8307,x:31673,y:32536,varname:node_8307,prsc:2,spu:0,spv:-1.2|UVIN-9198-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9198,x:31456,y:32524,varname:node_9198,prsc:2,uv:0;n:type:ShaderForge.SFN_Fresnel,id:5891,x:32276,y:33182,varname:node_5891,prsc:2|EXP-298-OUT;n:type:ShaderForge.SFN_ValueProperty,id:298,x:32082,y:33276,ptovrint:False,ptlb:node_298,ptin:_node_298,varname:node_298,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9831,x:32226,y:32745,varname:node_9831,prsc:2|A-5797-OUT,B-4143-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4143,x:32004,y:32856,ptovrint:False,ptlb:light,ptin:_light,varname:node_4143,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Power,id:5797,x:32053,y:32701,varname:node_5797,prsc:2|VAL-7621-R,EXP-8667-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8667,x:31849,y:32790,ptovrint:False,ptlb:power,ptin:_power,varname:node_8667,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:560,x:32442,y:32798,varname:node_560,prsc:2|A-3550-OUT,B-5272-RGB;n:type:ShaderForge.SFN_Color,id:5272,x:32330,y:32963,ptovrint:False,ptlb:node_5272,ptin:_node_5272,varname:node_5272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7855,x:32124,y:32489,ptovrint:False,ptlb:node_7855,ptin:_node_7855,varname:node_7855,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:66371ec7c7947d14286785d76b728bcc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3550,x:32365,y:32559,varname:node_3550,prsc:2|A-7855-RGB,B-9831-OUT;proporder:7621-298-4143-8667-5272-7855;pass:END;sub:END;*/

Shader "Unlit/chengong_fangyuzhao" {
    Properties {
        _node_7621 ("node_7621", 2D) = "white" {}
        _node_298 ("node_298", Float ) = 1
        _light ("light", Float ) = 2
        _power ("power", Float ) = 1
        _node_5272 ("node_5272", Color) = (1,1,1,1)
        _node_7855 ("node_7855", 2D) = "white" {}
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
            uniform sampler2D _node_7621; uniform float4 _node_7621_ST;
            uniform float _node_298;
            uniform float _light;
            uniform float _power;
            uniform float4 _node_5272;
            uniform sampler2D _node_7855; uniform float4 _node_7855_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _node_7855_var = tex2D(_node_7855,TRANSFORM_TEX(i.uv0, _node_7855));
                float4 node_664 = _Time + _TimeEditor;
                float2 node_8307 = (i.uv0+node_664.g*float2(0,-1.2));
                float4 _node_7621_var = tex2D(_node_7621,TRANSFORM_TEX(node_8307, _node_7621));
                float3 emissive = ((_node_7855_var.rgb*(pow(_node_7621_var.r,_power)*_light))*_node_5272.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_298));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
