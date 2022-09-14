// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-6269-OUT,alpha-4151-OUT;n:type:ShaderForge.SFN_Fresnel,id:2147,x:32728,y:33100,varname:node_2147,prsc:2|EXP-9916-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9916,x:32525,y:33164,ptovrint:False,ptlb:touming,ptin:_touming,varname:node_9916,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Tex2d,id:1359,x:32492,y:32720,ptovrint:False,ptlb:node_1359,ptin:_node_1359,varname:node_1359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cd57958ce8c5ddc47b6fdc887cc5f8c9,ntxv:0,isnm:False|UVIN-2241-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4151,x:33033,y:33003,varname:node_4151,prsc:2|A-1359-A,B-7547-OUT;n:type:ShaderForge.SFN_Multiply,id:7547,x:33006,y:33191,varname:node_7547,prsc:2|A-2147-OUT,B-1493-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1493,x:32769,y:33291,ptovrint:False,ptlb:liang,ptin:_liang,varname:node_1493,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:934,x:32006,y:32700,varname:node_934,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:2241,x:32258,y:32666,varname:node_2241,prsc:2,spu:0,spv:0.2|UVIN-934-UVOUT;n:type:ShaderForge.SFN_Color,id:8354,x:32593,y:32915,ptovrint:False,ptlb:node_8354,ptin:_node_8354,varname:node_8354,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6269,x:32938,y:32708,varname:node_6269,prsc:2|A-1359-RGB,B-8354-RGB;proporder:9916-1359-1493-8354;pass:END;sub:END;*/

Shader "Shader Forge/pangtong_bingdun" {
    Properties {
        _touming ("touming", Float ) = 2
        _node_1359 ("node_1359", 2D) = "white" {}
        _liang ("liang", Float ) = 1
        _node_8354 ("node_8354", Color) = (0.5,0.5,0.5,1)
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
            uniform float _touming;
            uniform sampler2D _node_1359; uniform float4 _node_1359_ST;
            uniform float _liang;
            uniform float4 _node_8354;
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
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 node_4552 = _Time + _TimeEditor;
                float2 node_2241 = (i.uv0+node_4552.g*float2(0,0.2));
                float4 _node_1359_var = tex2D(_node_1359,TRANSFORM_TEX(node_2241, _node_1359));
                float3 finalColor = (_node_1359_var.rgb*_node_8354.rgb);
                fixed4 finalRGBA = fixed4(finalColor,(_node_1359_var.a*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_touming)*_liang)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
