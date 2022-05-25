// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:3,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33271,y:32698,varname:node_4013,prsc:2|emission-6295-OUT,alpha-5013-A;n:type:ShaderForge.SFN_Color,id:1304,x:32765,y:32526,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:8583,x:32524,y:32722,ptovrint:False,ptlb:node_8583,ptin:_node_8583,varname:node_8583,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44644919a1c0b5b46a50e41198149846,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:591,x:32706,y:32906,ptovrint:False,ptlb:node_591,ptin:_node_591,varname:node_591,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:64e671d7ee7b78649a5fa4bbe0d828d0,ntxv:0,isnm:False|UVIN-9204-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3741,x:32202,y:32891,varname:node_3741,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:9204,x:32524,y:32906,varname:node_9204,prsc:2,spu:-0.2,spv:-0.2|UVIN-347-OUT;n:type:ShaderForge.SFN_Multiply,id:317,x:32979,y:32831,varname:node_317,prsc:2|A-5319-OUT,B-5218-OUT;n:type:ShaderForge.SFN_RemapRange,id:347,x:32368,y:32899,varname:node_347,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:3|IN-3741-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5352,x:32685,y:32671,varname:node_5352,prsc:2|A-7381-OUT,B-8583-A;n:type:ShaderForge.SFN_ValueProperty,id:7381,x:32514,y:32508,ptovrint:False,ptlb:node_7381,ptin:_node_7381,varname:node_7381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_Multiply,id:5319,x:32950,y:32696,varname:node_5319,prsc:2|A-1304-RGB,B-5352-OUT;n:type:ShaderForge.SFN_Multiply,id:5218,x:32902,y:32990,varname:node_5218,prsc:2|A-591-R,B-4014-RGB;n:type:ShaderForge.SFN_Color,id:4014,x:32678,y:33091,ptovrint:False,ptlb:node_4014,ptin:_node_4014,varname:node_4014,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.6559837,c3:0.2205882,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5013,x:32979,y:33058,varname:node_5013,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6295,x:33115,y:32905,varname:node_6295,prsc:2|A-317-OUT,B-5013-RGB;proporder:1304-8583-591-7381-4014;pass:END;sub:END;*/

Shader "Shader Forge/ui01" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _node_8583 ("node_8583", 2D) = "bump" {}
        _node_591 ("node_591", 2D) = "white" {}
        _node_7381 ("node_7381", Float ) = 8
        _node_4014 ("node_4014", Color) = (1,0.6559837,0.2205882,1)
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
            Blend SrcAlpha SrcAlpha
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
            uniform float4 _Color;
            uniform sampler2D _node_8583; uniform float4 _node_8583_ST;
            uniform sampler2D _node_591; uniform float4 _node_591_ST;
            uniform float _node_7381;
            uniform float4 _node_4014;
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
                float4 _node_8583_var = tex2D(_node_8583,TRANSFORM_TEX(i.uv0, _node_8583));
                float4 node_1309 = _Time + _TimeEditor;
                float2 node_9204 = ((i.uv0*4.0+-1.0)+node_1309.g*float2(-0.2,-0.2));
                float4 _node_591_var = tex2D(_node_591,TRANSFORM_TEX(node_9204, _node_591));
                float3 emissive = (((_Color.rgb*(_node_7381*_node_8583_var.a))*(_node_591_var.r*_node_4014.rgb))*i.vertexColor.rgb);
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
