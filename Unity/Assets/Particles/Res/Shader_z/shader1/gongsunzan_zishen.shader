// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:8540,x:32719,y:32712,varname:node_8540,prsc:2|emission-5967-OUT;n:type:ShaderForge.SFN_Tex2d,id:1984,x:32262,y:32560,ptovrint:False,ptlb:node_1984,ptin:_node_1984,varname:node_1984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:08b993ee37bbccb45af9a4c15864ec66,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4322,x:32262,y:32750,ptovrint:False,ptlb:node_4322,ptin:_node_4322,varname:node_4322,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b2292c2f7bde47145bb3549c6fd836f5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:1448,x:31355,y:32669,varname:node_1448,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4415,x:31554,y:32661,varname:node_4415,prsc:2,spu:0.1,spv:0.3|UVIN-1448-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8788,x:31753,y:32654,ptovrint:False,ptlb:node_8788,ptin:_node_8788,varname:node_8788,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:55a97670879396148baed3d57c386fdc,ntxv:0,isnm:False|UVIN-4415-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2132,x:31751,y:32922,ptovrint:False,ptlb:node_2132,ptin:_node_2132,varname:node_2132,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4cec7e336fdb1f5479dda24cbd8f163f,ntxv:0,isnm:False|UVIN-7468-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8525,x:31345,y:32922,varname:node_8525,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:7468,x:31525,y:32922,varname:node_7468,prsc:2,spu:0.1,spv:0.1|UVIN-8525-UVOUT;n:type:ShaderForge.SFN_Multiply,id:579,x:31925,y:32819,varname:node_579,prsc:2|A-8788-R,B-2132-R;n:type:ShaderForge.SFN_Multiply,id:8108,x:32118,y:32909,varname:node_8108,prsc:2|A-579-OUT,B-9660-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9660,x:31948,y:33095,ptovrint:False,ptlb:node_9660,ptin:_node_9660,varname:node_9660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:576,x:32500,y:32831,varname:node_576,prsc:2|A-4322-R,B-8661-OUT;n:type:ShaderForge.SFN_Color,id:5810,x:32230,y:33156,ptovrint:False,ptlb:node_5810,ptin:_node_5810,varname:node_5810,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:8661,x:32482,y:33001,varname:node_8661,prsc:2|A-8108-OUT,B-5810-RGB;n:type:ShaderForge.SFN_Add,id:5967,x:32500,y:32644,varname:node_5967,prsc:2|A-1984-RGB,B-576-OUT;proporder:1984-4322-8788-2132-9660-5810;pass:END;sub:END;*/

Shader "Unlit/gongsunzan_zishen" {
    Properties {
        _node_1984 ("node_1984", 2D) = "white" {}
        _node_4322 ("node_4322", 2D) = "white" {}
        _node_8788 ("node_8788", 2D) = "white" {}
        _node_2132 ("node_2132", 2D) = "white" {}
        _node_9660 ("node_9660", Float ) = 1
        _node_5810 ("node_5810", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform sampler2D _node_1984; uniform float4 _node_1984_ST;
            uniform sampler2D _node_4322; uniform float4 _node_4322_ST;
            uniform sampler2D _node_8788; uniform float4 _node_8788_ST;
            uniform sampler2D _node_2132; uniform float4 _node_2132_ST;
            uniform float _node_9660;
            uniform float4 _node_5810;
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
                float4 _node_1984_var = tex2D(_node_1984,TRANSFORM_TEX(i.uv0, _node_1984));
                float4 _node_4322_var = tex2D(_node_4322,TRANSFORM_TEX(i.uv0, _node_4322));
                float4 node_5551 = _Time + _TimeEditor;
                float2 node_4415 = (i.uv0+node_5551.g*float2(0.1,0.3));
                float4 _node_8788_var = tex2D(_node_8788,TRANSFORM_TEX(node_4415, _node_8788));
                float2 node_7468 = (i.uv0+node_5551.g*float2(0.1,0.1));
                float4 _node_2132_var = tex2D(_node_2132,TRANSFORM_TEX(node_7468, _node_2132));
                float3 emissive = (_node_1984_var.rgb+(_node_4322_var.r*(((_node_8788_var.r*_node_2132_var.r)*_node_9660)*_node_5810.rgb)));
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
