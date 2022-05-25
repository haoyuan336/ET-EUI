// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2925,x:32719,y:32712,varname:node_2925,prsc:2|emission-9648-OUT;n:type:ShaderForge.SFN_Tex2d,id:5982,x:31970,y:32926,ptovrint:False,ptlb:node_5982,ptin:_node_5982,varname:node_5982,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:64e671d7ee7b78649a5fa4bbe0d828d0,ntxv:0,isnm:False|UVIN-8031-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3997,x:31986,y:32715,ptovrint:False,ptlb:node_3997,ptin:_node_3997,varname:node_3997,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b84e6b6dbcf58614198d545865e8ab57,ntxv:0,isnm:False|UVIN-3867-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6722,x:31604,y:32668,varname:node_6722,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:3867,x:31789,y:32668,varname:node_3867,prsc:2,spu:0,spv:-0.25|UVIN-6722-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5965,x:31600,y:32878,varname:node_5965,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:8031,x:31785,y:32852,varname:node_8031,prsc:2,spu:0,spv:-0.1|UVIN-5965-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6694,x:32206,y:32767,varname:node_6694,prsc:2|A-3997-R,B-5982-R;n:type:ShaderForge.SFN_Color,id:7868,x:32189,y:33030,ptovrint:False,ptlb:node_7868,ptin:_node_7868,varname:node_7868,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9648,x:32527,y:32876,varname:node_9648,prsc:2|A-141-OUT,B-7868-RGB;n:type:ShaderForge.SFN_Multiply,id:141,x:32442,y:32538,varname:node_141,prsc:2|A-3228-OUT,B-6694-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3228,x:32250,y:32476,ptovrint:False,ptlb:liangdu,ptin:_liangdu,varname:node_3228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:5982-3997-7868-3228;pass:END;sub:END;*/

Shader "Unlit/ui_chenxing1" {
    Properties {
        _node_5982 ("node_5982", 2D) = "white" {}
        _node_3997 ("node_3997", 2D) = "white" {}
        _node_7868 ("node_7868", Color) = (0.5,0.5,0.5,1)
        _liangdu ("liangdu", Float ) = 1
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
            uniform sampler2D _node_5982; uniform float4 _node_5982_ST;
            uniform sampler2D _node_3997; uniform float4 _node_3997_ST;
            uniform float4 _node_7868;
            uniform float _liangdu;
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
                float4 node_5863 = _Time + _TimeEditor;
                float2 node_3867 = (i.uv0+node_5863.g*float2(0,-0.25));
                float4 _node_3997_var = tex2D(_node_3997,TRANSFORM_TEX(node_3867, _node_3997));
                float2 node_8031 = (i.uv0+node_5863.g*float2(0,-0.1));
                float4 _node_5982_var = tex2D(_node_5982,TRANSFORM_TEX(node_8031, _node_5982));
                float3 node_9648 = ((_liangdu*(_node_3997_var.r*_node_5982_var.r))*_node_7868.rgb);
                float3 emissive = node_9648;
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
