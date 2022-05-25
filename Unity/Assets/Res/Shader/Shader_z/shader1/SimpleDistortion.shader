// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:0,limd:0,uamb:False,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:0,x:35135,y:32442,varname:node_0,prsc:2|alpha-478-OUT,refract-14-OUT;n:type:ShaderForge.SFN_Multiply,id:14,x:34895,y:32726,varname:node_14,prsc:2|A-16-OUT,B-6401-A,C-509-OUT;n:type:ShaderForge.SFN_ComponentMask,id:16,x:34702,y:32651,varname:node_16,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-25-RGB;n:type:ShaderForge.SFN_Tex2d,id:25,x:34526,y:32625,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_5891,prsc:2,ntxv:0,isnm:False|UVIN-614-OUT;n:type:ShaderForge.SFN_Vector1,id:478,x:34895,y:32651,varname:node_478,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:509,x:34652,y:32912,varname:node_509,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Time,id:536,x:33562,y:32668,varname:node_536,prsc:2;n:type:ShaderForge.SFN_Add,id:595,x:34196,y:32686,varname:node_595,prsc:2|A-605-UVOUT,B-596-OUT;n:type:ShaderForge.SFN_Append,id:596,x:34015,y:32772,varname:node_596,prsc:2|A-601-OUT,B-603-OUT;n:type:ShaderForge.SFN_Slider,id:599,x:33436,y:32814,ptovrint:False,ptlb:SpeedX,ptin:_SpeedX,varname:node_7882,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:600,x:33436,y:32921,ptovrint:False,ptlb:SpeedY,ptin:_SpeedY,varname:node_4563,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:601,x:33821,y:32703,varname:node_601,prsc:2|A-536-T,B-599-OUT;n:type:ShaderForge.SFN_Multiply,id:603,x:33821,y:32849,varname:node_603,prsc:2|A-536-T,B-600-OUT;n:type:ShaderForge.SFN_TexCoord,id:605,x:34015,y:32595,varname:node_605,prsc:2,uv:0;n:type:ShaderForge.SFN_SwitchProperty,id:614,x:34356,y:32536,ptovrint:False,ptlb:Movement,ptin:_Movement,varname:node_9378,prsc:2,on:False|A-605-UVOUT,B-595-OUT;n:type:ShaderForge.SFN_VertexColor,id:6401,x:34475,y:32802,varname:node_6401,prsc:2;proporder:25-614-599-600;pass:END;sub:END;*/

Shader "Rhino/A3_SimpleDistortion" {
    Properties {
        _Refraction ("Refraction", 2D) = "white" {}
        [MaterialToggle] _Movement ("Movement", Float ) = 0
        _SpeedX ("SpeedX", Range(0, 1)) = 0
        _SpeedY ("SpeedY", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
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
            #pragma target 2.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float _SpeedX;
            uniform float _SpeedY;
            uniform fixed _Movement;
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
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD3;
                #else
                    float3 shLight : TEXCOORD3;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float4 node_536 = _Time + _TimeEditor;
                float2 _Movement_var = lerp( i.uv0, (i.uv0+float2((node_536.g*_SpeedX),(node_536.g*_SpeedY))), _Movement );
                float4 _Refraction_var = tex2D(_Refraction,TRANSFORM_TEX(_Movement_var, _Refraction));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction_var.rgb.rg*i.vertexColor.a*0.1);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
/////// Vectors:
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
