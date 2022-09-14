// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:34646,y:32712,varname:node_1,prsc:2|emission-76-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33332,y:32792,ptovrint:False,ptlb:node_2,ptin:_node_2,varname:node_3400,prsc:2,tex:3044061a74a49704ab35852bdc2e1982,ntxv:0,isnm:False|UVIN-7-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:33365,y:32612,ptovrint:False,ptlb:node_3,ptin:_node_3,varname:node_8867,prsc:2,tex:13ba8f97a2e116f48a2a57d76a615c4a,ntxv:0,isnm:False|UVIN-6-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4,x:33358,y:32997,ptovrint:False,ptlb:node_4,ptin:_node_4,varname:node_8587,prsc:2,ntxv:0,isnm:False|UVIN-8-UVOUT;n:type:ShaderForge.SFN_Add,id:5,x:33612,y:32629,varname:node_5,prsc:2|A-3-R,B-2-R,C-4-R;n:type:ShaderForge.SFN_Panner,id:6,x:33092,y:32599,varname:node_6,prsc:2,spu:0,spv:0.1|UVIN-3952-UVOUT;n:type:ShaderForge.SFN_Panner,id:7,x:33092,y:32776,varname:node_7,prsc:2,spu:0,spv:0.1|UVIN-3952-UVOUT;n:type:ShaderForge.SFN_Panner,id:8,x:33144,y:32982,varname:node_8,prsc:2,spu:0,spv:0.2|UVIN-3952-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9,x:33746,y:32230,ptovrint:False,ptlb:node_9,ptin:_node_9,varname:node_9051,prsc:2,tex:c21a6f72dcf52d941878ea7edfb1352f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:10,x:33837,y:32405,varname:node_10,prsc:2|VAL-9-R,EXP-11-OUT;n:type:ShaderForge.SFN_Vector1,id:11,x:33628,y:32513,varname:node_11,prsc:2,v1:3.5;n:type:ShaderForge.SFN_Power,id:16,x:33777,y:32689,varname:node_16,prsc:2|VAL-5-OUT,EXP-17-OUT;n:type:ShaderForge.SFN_Vector1,id:17,x:33587,y:32871,varname:node_17,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:18,x:34140,y:32734,varname:node_18,prsc:2|A-483-OUT,B-167-OUT;n:type:ShaderForge.SFN_Multiply,id:76,x:34393,y:32749,varname:node_76,prsc:2|A-241-OUT,B-18-OUT;n:type:ShaderForge.SFN_Multiply,id:167,x:33960,y:32784,varname:node_167,prsc:2|A-16-OUT,B-168-OUT;n:type:ShaderForge.SFN_Vector1,id:168,x:33800,y:32871,varname:node_168,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector3,id:241,x:33960,y:32338,varname:node_241,prsc:2,v1:1,v2:0.3058823,v3:0.1470588;n:type:ShaderForge.SFN_Multiply,id:483,x:34016,y:32463,varname:node_483,prsc:2|A-10-OUT,B-484-OUT;n:type:ShaderForge.SFN_Vector1,id:484,x:33817,y:32589,varname:node_484,prsc:2,v1:2;n:type:ShaderForge.SFN_TexCoord,id:3952,x:32792,y:32582,varname:node_3952,prsc:2,uv:0;proporder:2-3-4-9;pass:END;sub:END;*/

Shader "Custom/boss_liuguang" {
    Properties {
        _node_2 ("node_2", 2D) = "white" {}
        _node_3 ("node_3", 2D) = "white" {}
        _node_4 ("node_4", 2D) = "white" {}
        _node_9 ("node_9", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform sampler2D _node_3; uniform float4 _node_3_ST;
            uniform sampler2D _node_4; uniform float4 _node_4_ST;
            uniform sampler2D _node_9; uniform float4 _node_9_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _node_9_var = tex2D(_node_9,TRANSFORM_TEX(i.uv0, _node_9));
                float4 node_1368 = _Time + _TimeEditor;
                float2 node_6 = (i.uv0+node_1368.g*float2(0,0.1));
                float4 _node_3_var = tex2D(_node_3,TRANSFORM_TEX(node_6, _node_3));
                float2 node_7 = (i.uv0+node_1368.g*float2(0,0.1));
                float4 _node_2_var = tex2D(_node_2,TRANSFORM_TEX(node_7, _node_2));
                float2 node_8 = (i.uv0+node_1368.g*float2(0,0.2));
                float4 _node_4_var = tex2D(_node_4,TRANSFORM_TEX(node_8, _node_4));
                float3 emissive = (float3(1,0.3058823,0.1470588)*((pow(_node_9_var.r,3.5)*2.0)*(pow((_node_3_var.r+_node_2_var.r+_node_4_var.r),3.0)*1.0)));
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
