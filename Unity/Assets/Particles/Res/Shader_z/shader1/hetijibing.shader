// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:34639,y:32712,varname:node_1,prsc:2|emission-45-OUT,alpha-2-A;n:type:ShaderForge.SFN_Tex2d,id:2,x:33647,y:32594,ptovrint:False,ptlb:node_2,ptin:_node_2,varname:node_5174,prsc:2,tex:164ee5985fe03034d81749c4b978c907,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:8,x:33558,y:33070,ptovrint:False,ptlb:node_8,ptin:_node_8,varname:node_2386,prsc:2,min:-0.6,cur:-0.6,max:0.5;n:type:ShaderForge.SFN_Panner,id:9,x:33698,y:32820,varname:node_9,prsc:2,spu:0,spv:1|DIST-8-OUT;n:type:ShaderForge.SFN_Tex2d,id:11,x:33941,y:32735,ptovrint:False,ptlb:node_11,ptin:_node_11,varname:node_1380,prsc:2,tex:f8e1f737538f47f4a91ebf1ee3ab61ef,ntxv:0,isnm:False|UVIN-9-UVOUT;n:type:ShaderForge.SFN_Multiply,id:12,x:34193,y:32600,varname:node_12,prsc:2|A-93-OUT,B-11-R;n:type:ShaderForge.SFN_Color,id:39,x:34174,y:32986,ptovrint:False,ptlb:node_39,ptin:_node_39,varname:node_3412,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:45,x:34439,y:32879,varname:node_45,prsc:2|A-50-OUT,B-39-RGB;n:type:ShaderForge.SFN_Multiply,id:50,x:34357,y:32672,varname:node_50,prsc:2|A-12-OUT,B-57-OUT;n:type:ShaderForge.SFN_Vector1,id:57,x:34128,y:32786,varname:node_57,prsc:2,v1:2;n:type:ShaderForge.SFN_TexCoord,id:64,x:32990,y:32371,varname:node_64,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:70,x:33188,y:32385,varname:node_70,prsc:2,spu:0.15,spv:0.15|UVIN-64-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:76,x:33363,y:32385,ptovrint:False,ptlb:node_76,ptin:_node_76,varname:node_3839,prsc:2,tex:2fb6a9e62bad6e14383e2e7d547f3ba6,ntxv:0,isnm:False|UVIN-70-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:77,x:33204,y:32612,ptovrint:False,ptlb:node_77,ptin:_node_77,varname:node_2132,prsc:2,tex:3044061a74a49704ab35852bdc2e1982,ntxv:0,isnm:False|UVIN-78-UVOUT;n:type:ShaderForge.SFN_Panner,id:78,x:33040,y:32563,varname:node_78,prsc:2,spu:0,spv:0.3|UVIN-80-UVOUT;n:type:ShaderForge.SFN_Multiply,id:79,x:33465,y:32561,varname:node_79,prsc:2|A-76-R,B-77-R;n:type:ShaderForge.SFN_TexCoord,id:80,x:32865,y:32507,varname:node_80,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:93,x:33847,y:32417,varname:node_93,prsc:2|A-2-R,B-79-OUT;proporder:2-8-11-39-76-77;pass:END;sub:END;*/

Shader "Shader Forge/hetijibing" {
    Properties {
        _node_2 ("node_2", 2D) = "white" {}
        _node_8 ("node_8", Range(-0.6, 0.5)) = -0.6
        _node_11 ("node_11", 2D) = "white" {}
        _node_39 ("node_39", Color) = (0.5,0.5,0.5,1)
        _node_76 ("node_76", 2D) = "white" {}
        _node_77 ("node_77", 2D) = "white" {}
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform float _node_8;
            uniform sampler2D _node_11; uniform float4 _node_11_ST;
            uniform float4 _node_39;
            uniform sampler2D _node_76; uniform float4 _node_76_ST;
            uniform sampler2D _node_77; uniform float4 _node_77_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD1;
                #else
                    float3 shLight : TEXCOORD1;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _node_2_var = tex2D(_node_2,TRANSFORM_TEX(i.uv0, _node_2));
                float4 node_5057 = _Time + _TimeEditor;
                float2 node_70 = (i.uv0+node_5057.g*float2(0.15,0.15));
                float4 _node_76_var = tex2D(_node_76,TRANSFORM_TEX(node_70, _node_76));
                float2 node_78 = (i.uv0+node_5057.g*float2(0,0.3));
                float4 _node_77_var = tex2D(_node_77,TRANSFORM_TEX(node_78, _node_77));
                float2 node_9 = (i.uv0+_node_8*float2(0,1));
                float4 _node_11_var = tex2D(_node_11,TRANSFORM_TEX(node_9, _node_11));
                float3 emissive = ((((_node_2_var.r+(_node_76_var.r*_node_77_var.r))*_node_11_var.r)*2.0)*_node_39.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_node_2_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
