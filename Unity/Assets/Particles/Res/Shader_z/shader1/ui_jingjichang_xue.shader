// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4963,x:32719,y:32712,varname:node_4963,prsc:2|emission-752-OUT;n:type:ShaderForge.SFN_Tex2d,id:4097,x:32278,y:32717,ptovrint:False,ptlb:node_4097,ptin:_node_4097,varname:node_4097,prsc:2,tex:c494dc447c3e2b042a62e86936aaa449,ntxv:0,isnm:False|UVIN-3509-UVOUT;n:type:ShaderForge.SFN_Panner,id:3509,x:32009,y:32706,varname:node_3509,prsc:2,spu:-1,spv:0|DIST-9215-OUT;n:type:ShaderForge.SFN_Multiply,id:752,x:32523,y:32584,varname:node_752,prsc:2|A-3756-OUT,B-4097-R;n:type:ShaderForge.SFN_Slider,id:9215,x:31654,y:32723,ptovrint:False,ptlb:node_9215,ptin:_node_9215,varname:node_9215,prsc:2,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:6107,x:32179,y:32432,ptovrint:False,ptlb:node_6107,ptin:_node_6107,varname:node_6107,prsc:2,tex:d8459f766578f294a8fe09ee0e22e37a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3756,x:32556,y:32404,varname:node_3756,prsc:2|A-9787-OUT,B-6107-RGB,C-9600-RGB;n:type:ShaderForge.SFN_Vector1,id:9787,x:32275,y:32307,varname:node_9787,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:9600,x:32505,y:32236,ptovrint:False,ptlb:node_9600,ptin:_node_9600,varname:node_9600,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:4097-9215-6107-9600;pass:END;sub:END;*/

Shader "Unlit/ui_jingjichang_xue" {
    Properties {
        _node_4097 ("node_4097", 2D) = "white" {}
        _node_9215 ("node_9215", Range(-1, 1)) = 1
        _node_6107 ("node_6107", 2D) = "white" {}
        _node_9600 ("node_9600", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
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
            uniform sampler2D _node_4097; uniform float4 _node_4097_ST;
            uniform float _node_9215;
            uniform sampler2D _node_6107; uniform float4 _node_6107_ST;
            uniform float4 _node_9600;
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
                float4 _node_6107_var = tex2D(_node_6107,TRANSFORM_TEX(i.uv0, _node_6107));
                float2 node_3509 = (i.uv0+_node_9215*float2(-1,0));
                float4 _node_4097_var = tex2D(_node_4097,TRANSFORM_TEX(node_3509, _node_4097));
                float3 emissive = ((2.0*_node_6107_var.rgb*_node_9600.rgb)*_node_4097_var.r);
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
