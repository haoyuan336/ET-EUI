// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:34114,y:32712,varname:node_1,prsc:2|emission-100-OUT,alpha-101-A;n:type:ShaderForge.SFN_Tex2d,id:3,x:33161,y:32875,ptovrint:False,ptlb:node_3,ptin:_node_3,varname:node_3356,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9b2b92ddb4b229945a097e92b44e6a3b,ntxv:0,isnm:False|UVIN-6-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4,x:32980,y:32439,ptovrint:False,ptlb:node_4,ptin:_node_4,varname:node_2458,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3044061a74a49704ab35852bdc2e1982,ntxv:0,isnm:False|UVIN-5-UVOUT;n:type:ShaderForge.SFN_Panner,id:5,x:32784,y:32622,varname:node_5,prsc:2,spu:-0.2,spv:-0.3|UVIN-4551-UVOUT;n:type:ShaderForge.SFN_Panner,id:6,x:32900,y:32886,varname:node_6,prsc:2,spu:0,spv:-0.7|UVIN-2074-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7,x:33341,y:32744,varname:node_7,prsc:2|A-139-OUT,B-3-R;n:type:ShaderForge.SFN_Multiply,id:100,x:33947,y:32825,varname:node_100,prsc:2|A-155-OUT,B-101-RGB,C-8500-RGB;n:type:ShaderForge.SFN_VertexColor,id:101,x:33742,y:32902,varname:node_101,prsc:2;n:type:ShaderForge.SFN_Multiply,id:112,x:33546,y:32644,varname:node_112,prsc:2|A-113-OUT,B-7-OUT;n:type:ShaderForge.SFN_Vector1,id:113,x:33383,y:32622,varname:node_113,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:137,x:32964,y:32622,varname:node_137,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:139,x:33188,y:32512,varname:node_139,prsc:2|VAL-4-R,EXP-137-OUT;n:type:ShaderForge.SFN_Panner,id:153,x:33436,y:32363,varname:node_153,prsc:2,spu:0.2,spv:-0.3|UVIN-8584-UVOUT;n:type:ShaderForge.SFN_Add,id:155,x:33797,y:32636,varname:node_155,prsc:2|A-203-OUT,B-112-OUT;n:type:ShaderForge.SFN_Tex2d,id:194,x:33647,y:32407,ptovrint:False,ptlb:node_194,ptin:_node_194,varname:node_5846,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3044061a74a49704ab35852bdc2e1982,ntxv:0,isnm:False|UVIN-153-UVOUT;n:type:ShaderForge.SFN_Power,id:203,x:33931,y:32394,varname:node_203,prsc:2|VAL-194-R,EXP-204-OUT;n:type:ShaderForge.SFN_Vector1,id:204,x:33797,y:32513,varname:node_204,prsc:2,v1:2;n:type:ShaderForge.SFN_TexCoord,id:4551,x:32635,y:32822,varname:node_4551,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:2074,x:32677,y:32980,varname:node_2074,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:8584,x:33193,y:32305,varname:node_8584,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:8500,x:33358,y:33178,ptovrint:False,ptlb:node_8500,ptin:_node_8500,varname:node_8500,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:3-4-194-8500;pass:END;sub:END;*/

Shader "Custom/6_17dunpai" {
    Properties {
        _node_3 ("node_3", 2D) = "white" {}
        _node_4 ("node_4", 2D) = "white" {}
        _node_194 ("node_194", 2D) = "white" {}
        _node_8500 ("node_8500", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
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
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_3; uniform float4 _node_3_ST;
            uniform sampler2D _node_4; uniform float4 _node_4_ST;
            uniform sampler2D _node_194; uniform float4 _node_194_ST;
            uniform float4 _node_8500;
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
////// Lighting:
////// Emissive:
                float4 node_9123 = _Time + _TimeEditor;
                float2 node_153 = (i.uv0+node_9123.g*float2(0.2,-0.3));
                float4 _node_194_var = tex2D(_node_194,TRANSFORM_TEX(node_153, _node_194));
                float2 node_5 = (i.uv0+node_9123.g*float2(-0.2,-0.3));
                float4 _node_4_var = tex2D(_node_4,TRANSFORM_TEX(node_5, _node_4));
                float2 node_6 = (i.uv0+node_9123.g*float2(0,-0.7));
                float4 _node_3_var = tex2D(_node_3,TRANSFORM_TEX(node_6, _node_3));
                float3 emissive = ((pow(_node_194_var.r,2.0)+(1.0*(pow(_node_4_var.r,2.0)*_node_3_var.r)))*i.vertexColor.rgb*_node_8500.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,i.vertexColor.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
