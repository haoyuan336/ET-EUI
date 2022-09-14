// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-32-OUT,alpha-37-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33683,y:32717,ptlb:node_2,ptin:_node_2,tex:729b851d556df6748ab1609d97ee09b8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:4,x:33751,y:33011,spu:0.2,spv:0|DIST-25-OUT;n:type:ShaderForge.SFN_Slider,id:25,x:33820,y:32922,ptlb:node_25,ptin:_node_25,min:-2.5,cur:-2.5,max:2.5;n:type:ShaderForge.SFN_Multiply,id:26,x:33266,y:32833|A-68-OUT,B-52-R;n:type:ShaderForge.SFN_Vector3,id:31,x:33248,y:32662,v1:1,v2:0.1241379,v3:0;n:type:ShaderForge.SFN_Multiply,id:32,x:33047,y:32703|A-31-OUT,B-26-OUT;n:type:ShaderForge.SFN_Multiply,id:37,x:33073,y:32985|A-2-A,B-52-R;n:type:ShaderForge.SFN_Tex2d,id:52,x:33522,y:33011,ptlb:node_52,ptin:_node_52,tex:c494dc447c3e2b042a62e86936aaa449,ntxv:0,isnm:False|UVIN-4-UVOUT;n:type:ShaderForge.SFN_Multiply,id:68,x:33440,y:32644|A-69-OUT,B-2-R;n:type:ShaderForge.SFN_Vector1,id:69,x:33537,y:32522,v1:1;proporder:2-25-52;pass:END;sub:END;*/

Shader "Custom/zhurong_dazhao" {
    Properties {
        _node_2 ("node_2", 2D) = "white" {}
        _node_25 ("node_25", Range(-2.5, 2.5)) = -2.5
        _node_52 ("node_52", 2D) = "white" {}
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform float _node_25;
            uniform sampler2D _node_52; uniform float4 _node_52_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_119 = i.uv0;
                float4 node_2 = tex2D(_node_2,TRANSFORM_TEX(node_119.rg, _node_2));
                float2 node_4 = (node_119.rg+_node_25*float2(0.2,0));
                float4 node_52 = tex2D(_node_52,TRANSFORM_TEX(node_4, _node_52));
                float3 emissive = (float3(1,0.1241379,0)*((1.0*node_2.r)*node_52.r));
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,(node_2.a*node_52.r));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
