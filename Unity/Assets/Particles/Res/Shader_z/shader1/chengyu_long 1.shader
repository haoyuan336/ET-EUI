// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4201,x:32719,y:32712,varname:node_4201,prsc:2|emission-7884-OUT;n:type:ShaderForge.SFN_TexCoord,id:6674,x:30850,y:32633,varname:node_6674,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:9913,x:31090,y:32634,varname:node_9913,prsc:2,spu:-0.5,spv:0|UVIN-6674-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5103,x:31238,y:32962,ptovrint:False,ptlb:node_5103,ptin:_node_5103,varname:node_5103,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f6392bcb65970f94eb288f66ec6a7400,ntxv:0,isnm:False|UVIN-101-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2219,x:31274,y:32646,ptovrint:False,ptlb:node_2219,ptin:_node_2219,varname:node_2219,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:18256d133dc84a3469ae30745ca3f053,ntxv:0,isnm:False|UVIN-9913-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1936,x:30768,y:32932,varname:node_1936,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:101,x:30977,y:32907,varname:node_101,prsc:2,spu:-0.2,spv:0.3|UVIN-1936-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:1125,x:32016,y:33029,varname:node_1125,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5336-OUT;n:type:ShaderForge.SFN_Tex2d,id:4276,x:32455,y:32967,ptovrint:False,ptlb:node_4276,ptin:_node_4276,varname:node_4276,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:164ce07f3d181074993094022ad31286,ntxv:0,isnm:False|UVIN-67-OUT;n:type:ShaderForge.SFN_Add,id:7594,x:31465,y:32795,varname:node_7594,prsc:2|A-2219-R,B-5103-R;n:type:ShaderForge.SFN_TexCoord,id:2622,x:31647,y:33295,varname:node_2622,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:67,x:32055,y:33315,varname:node_67,prsc:2|A-1125-OUT,B-2622-UVOUT;n:type:ShaderForge.SFN_Power,id:5336,x:31657,y:33013,varname:node_5336,prsc:2|VAL-7594-OUT,EXP-3381-OUT;n:type:ShaderForge.SFN_Vector1,id:3381,x:31465,y:33082,varname:node_3381,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:7075,x:32363,y:33297,ptovrint:False,ptlb:node_7075,ptin:_node_7075,varname:node_7075,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:7884,x:32544,y:33140,varname:node_7884,prsc:2|A-4276-R,B-7075-RGB;proporder:5103-2219-4276-7075;pass:END;sub:END;*/

Shader "Unlit/chengyu_long 1" {
    Properties {
        _node_5103 ("node_5103", 2D) = "white" {}
        _node_2219 ("node_2219", 2D) = "white" {}
        _node_4276 ("node_4276", 2D) = "white" {}
        _node_7075 ("node_7075", Color) = (0.5,0.5,0.5,1)
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
            Blend One One
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
            uniform sampler2D _node_5103; uniform float4 _node_5103_ST;
            uniform sampler2D _node_2219; uniform float4 _node_2219_ST;
            uniform sampler2D _node_4276; uniform float4 _node_4276_ST;
            uniform float4 _node_7075;
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
                float4 node_2288 = _Time + _TimeEditor;
                float2 node_9913 = (i.uv0+node_2288.g*float2(-0.5,0));
                float4 _node_2219_var = tex2D(_node_2219,TRANSFORM_TEX(node_9913, _node_2219));
                float2 node_101 = (i.uv0+node_2288.g*float2(-0.2,0.3));
                float4 _node_5103_var = tex2D(_node_5103,TRANSFORM_TEX(node_101, _node_5103));
                float2 node_67 = (pow((_node_2219_var.r+_node_5103_var.r),2.0).r*i.uv0);
                float4 _node_4276_var = tex2D(_node_4276,TRANSFORM_TEX(node_67, _node_4276));
                float3 emissive = (_node_4276_var.r*_node_7075.rgb);
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
