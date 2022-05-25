// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33374,y:32704,varname:node_4013,prsc:2|alpha-7041-OUT,refract-3938-OUT;n:type:ShaderForge.SFN_Vector1,id:7041,x:33114,y:32985,varname:node_7041,prsc:2,v1:0;n:type:ShaderForge.SFN_VertexColor,id:7520,x:32863,y:33168,varname:node_7520,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3938,x:33071,y:33082,varname:node_3938,prsc:2|A-2022-OUT,B-7520-R,C-1435-OUT;n:type:ShaderForge.SFN_TexCoord,id:6405,x:31904,y:32959,varname:node_6405,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:7718,x:32066,y:32959,varname:node_7718,prsc:2,spu:0.1,spv:0.1|UVIN-6405-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5795,x:32229,y:32959,ptovrint:False,ptlb:node_5795,ptin:_node_5795,varname:node_5795,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:64e671d7ee7b78649a5fa4bbe0d828d0,ntxv:0,isnm:False|UVIN-7718-UVOUT;n:type:ShaderForge.SFN_Add,id:9506,x:32385,y:32959,varname:node_9506,prsc:2|A-5360-UVOUT,B-5795-R;n:type:ShaderForge.SFN_TexCoord,id:5360,x:32229,y:32799,varname:node_5360,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:970,x:32539,y:32959,ptovrint:False,ptlb:node_970,ptin:_node_970,varname:node_970,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:842bc94b697d5e9449aeac9ab3b1daf8,ntxv:0,isnm:False|UVIN-9506-OUT;n:type:ShaderForge.SFN_Multiply,id:2022,x:32863,y:32958,varname:node_2022,prsc:2|A-4498-OUT,B-970-R;n:type:ShaderForge.SFN_Vector3,id:4498,x:32677,y:32872,varname:node_4498,prsc:2,v1:0.3455882,v2:0.293154,v3:0;n:type:ShaderForge.SFN_Slider,id:1435,x:32794,y:33349,ptovrint:False,ptlb:node_1435,ptin:_node_1435,varname:node_1435,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:10;proporder:5795-970-1435;pass:END;sub:END;*/

Shader "Shader Forge/liubei01" {
    Properties {
        _node_5795 ("node_5795", 2D) = "white" {}
        _node_970 ("node_970", 2D) = "white" {}
        _node_1435 ("node_1435", Range(0, 10)) = 10
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_5795; uniform float4 _node_5795_ST;
            uniform sampler2D _node_970; uniform float4 _node_970_ST;
            uniform float _node_1435;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float4 node_2180 = _Time + _TimeEditor;
                float2 node_7718 = (i.uv0+node_2180.g*float2(0.1,0.1));
                float4 _node_5795_var = tex2D(_node_5795,TRANSFORM_TEX(node_7718, _node_5795));
                float2 node_9506 = (i.uv0+_node_5795_var.r);
                float4 _node_970_var = tex2D(_node_970,TRANSFORM_TEX(node_9506, _node_970));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((float3(0.3455882,0.293154,0)*_node_970_var.r)*i.vertexColor.r*_node_1435).rg;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,0.0),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
