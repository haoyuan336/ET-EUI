// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4548,x:32719,y:32712,varname:node_4548,prsc:2|emission-9348-OUT;n:type:ShaderForge.SFN_Tex2d,id:5363,x:31039,y:32936,ptovrint:False,ptlb:node_5363,ptin:_node_5363,varname:node_5363,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1e8d551dc4fbef64e9552e66ea0f0394,ntxv:0,isnm:False|UVIN-4685-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5970,x:30996,y:33194,ptovrint:False,ptlb:node_5970,ptin:_node_5970,varname:node_5970,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2fc431a899521674db6ac1d307bd29b3,ntxv:0,isnm:False|UVIN-6241-UVOUT;n:type:ShaderForge.SFN_Power,id:423,x:31210,y:33194,varname:node_423,prsc:2|VAL-5970-RGB,EXP-7662-OUT;n:type:ShaderForge.SFN_Vector1,id:7662,x:30968,y:33466,varname:node_7662,prsc:2,v1:1;n:type:ShaderForge.SFN_Panner,id:4685,x:30826,y:32898,varname:node_4685,prsc:2,spu:0.1,spv:0|UVIN-7747-UVOUT;n:type:ShaderForge.SFN_Panner,id:6241,x:30809,y:33194,varname:node_6241,prsc:2,spu:0,spv:0.3|UVIN-8874-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8874,x:30619,y:33211,varname:node_8874,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:7747,x:30611,y:32898,varname:node_7747,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4149,x:31667,y:32871,varname:node_4149,prsc:2|A-5363-RGB,B-423-OUT;n:type:ShaderForge.SFN_Color,id:2450,x:32005,y:32906,ptovrint:False,ptlb:node_2450,ptin:_node_2450,varname:node_2450,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9348,x:32309,y:32974,varname:node_9348,prsc:2|A-2450-RGB,B-3383-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3383,x:31712,y:33381,ptovrint:False,ptlb:node_3383,ptin:_node_3383,varname:node_3383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:6598,x:32135,y:33229,varname:node_6598,prsc:2|A-4149-OUT,B-3383-OUT;proporder:5363-5970-2450-3383;pass:END;sub:END;*/

Shader "Unlit/qiyuan" {
    Properties {
        _node_5363 ("node_5363", 2D) = "white" {}
        _node_5970 ("node_5970", 2D) = "white" {}
        _node_2450 ("node_2450", Color) = (0.5,0.5,0.5,1)
        _node_3383 ("node_3383", Float ) = 0
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _node_2450;
            uniform float _node_3383;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float3 emissive = (_node_2450.rgb*_node_3383);
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
