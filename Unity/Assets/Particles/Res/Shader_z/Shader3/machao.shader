// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3258,x:33322,y:32661,varname:node_3258,prsc:2|emission-473-OUT,alpha-2400-A;n:type:ShaderForge.SFN_Tex2d,id:5250,x:32473,y:32660,ptovrint:False,ptlb:node_5250,ptin:_node_5250,varname:node_5250,prsc:2,tex:b7e20bf099ca485448f261df478c7e07,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6978,x:32434,y:32861,ptovrint:False,ptlb:node_6978,ptin:_node_6978,varname:node_6978,prsc:2,tex:de6362364a2290b47b448cac19455986,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:1487,x:32658,y:32779,varname:node_1487,prsc:2|A-5250-R,B-6978-R;n:type:ShaderForge.SFN_Multiply,id:9160,x:32988,y:32816,varname:node_9160,prsc:2|A-1487-OUT,B-962-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8960,x:32502,y:33187,ptovrint:False,ptlb:node_8960,ptin:_node_8960,varname:node_8960,prsc:2,glob:False,v1:0.3;n:type:ShaderForge.SFN_Slider,id:2915,x:32502,y:33068,ptovrint:False,ptlb:node_2915,ptin:_node_2915,varname:node_2915,prsc:2,min:0,cur:4.700855,max:50;n:type:ShaderForge.SFN_Power,id:962,x:32788,y:32946,varname:node_962,prsc:2|VAL-1487-OUT,EXP-2915-OUT;n:type:ShaderForge.SFN_Color,id:3771,x:32852,y:32635,ptovrint:False,ptlb:node_3771,ptin:_node_3771,varname:node_3771,prsc:2,glob:False,c1:0.3529412,c2:0.464503,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:473,x:33118,y:32697,varname:node_473,prsc:2|A-3771-RGB,B-9160-OUT;n:type:ShaderForge.SFN_VertexColor,id:2400,x:32915,y:33060,varname:node_2400,prsc:2;proporder:5250-6978-8960-2915-3771;pass:END;sub:END;*/

Shader "Shader Forge/machao" {
    Properties {
        _node_5250 ("node_5250", 2D) = "white" {}
        _node_6978 ("node_6978", 2D) = "white" {}
        _node_8960 ("node_8960", Float ) = 0.3
        _node_2915 ("node_2915", Range(0, 50)) = 4.700855
        _node_3771 ("node_3771", Color) = (0.3529412,0.464503,1,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
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
            uniform sampler2D _node_5250; uniform float4 _node_5250_ST;
            uniform sampler2D _node_6978; uniform float4 _node_6978_ST;
            uniform float _node_2915;
            uniform float4 _node_3771;
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
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _node_5250_var = tex2D(_node_5250,TRANSFORM_TEX(i.uv0, _node_5250));
                float4 _node_6978_var = tex2D(_node_6978,TRANSFORM_TEX(i.uv0, _node_6978));
                float node_1487 = (_node_5250_var.r+_node_6978_var.r);
                float3 emissive = (_node_3771.rgb*(node_1487*pow(node_1487,_node_2915)));
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
