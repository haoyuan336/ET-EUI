// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5707,x:32719,y:32712,varname:node_5707,prsc:2|emission-1696-OUT;n:type:ShaderForge.SFN_Tex2d,id:3082,x:31946,y:32513,ptovrint:False,ptlb:node_3082,ptin:_node_3082,varname:node_3082,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:26515ba28811dee4eb2d4c4ca4c10918,ntxv:0,isnm:False|UVIN-2967-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8517,x:31946,y:32723,ptovrint:False,ptlb:node_8517,ptin:_node_8517,varname:node_8517,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:64e671d7ee7b78649a5fa4bbe0d828d0,ntxv:0,isnm:False|UVIN-4985-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:975,x:31587,y:32520,varname:node_975,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:2967,x:31750,y:32499,varname:node_2967,prsc:2,spu:0,spv:-0.5|UVIN-975-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2231,x:31596,y:32723,varname:node_2231,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4985,x:31775,y:32723,varname:node_4985,prsc:2,spu:0,spv:-0.2|UVIN-2231-UVOUT;n:type:ShaderForge.SFN_Power,id:7333,x:32436,y:32660,varname:node_7333,prsc:2|VAL-2179-OUT,EXP-4439-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4439,x:32174,y:32955,ptovrint:False,ptlb:power,ptin:_power,varname:node_4439,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:7752,x:32382,y:32955,ptovrint:False,ptlb:node_7752,ptin:_node_7752,varname:node_7752,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1696,x:32540,y:32804,varname:node_1696,prsc:2|A-7333-OUT,B-7752-RGB,C-7512-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7512,x:32350,y:32507,ptovrint:False,ptlb:liang,ptin:_liang,varname:node_7512,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2179,x:32216,y:32615,varname:node_2179,prsc:2|A-3082-R,B-8517-R;proporder:3082-8517-4439-7752-7512;pass:END;sub:END;*/

Shader "Unlit/chongwu_xisui1" {
    Properties {
        _node_3082 ("node_3082", 2D) = "white" {}
        _node_8517 ("node_8517", 2D) = "white" {}
        _power ("power", Float ) = 1
        _node_7752 ("node_7752", Color) = (0.5,0.5,0.5,1)
        _liang ("liang", Float ) = 1
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
            uniform sampler2D _node_3082; uniform float4 _node_3082_ST;
            uniform sampler2D _node_8517; uniform float4 _node_8517_ST;
            uniform float _power;
            uniform float4 _node_7752;
            uniform float _liang;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_2773 = _Time + _TimeEditor;
                float2 node_2967 = (i.uv0+node_2773.g*float2(0,-0.5));
                float4 _node_3082_var = tex2D(_node_3082,TRANSFORM_TEX(node_2967, _node_3082));
                float2 node_4985 = (i.uv0+node_2773.g*float2(0,-0.2));
                float4 _node_8517_var = tex2D(_node_8517,TRANSFORM_TEX(node_4985, _node_8517));
                float3 emissive = (pow((_node_3082_var.r*_node_8517_var.r),_power)*_node_7752.rgb*_liang);
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
