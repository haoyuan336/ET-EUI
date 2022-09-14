// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33738,y:32753,varname:node_1,prsc:2|emission-198-OUT,alpha-171-R;n:type:ShaderForge.SFN_Panner,id:17,x:33052,y:32766,varname:node_17,prsc:2,spu:0,spv:0.5|DIST-544-OUT;n:type:ShaderForge.SFN_Tex2d,id:171,x:33098,y:33051,ptovrint:False,ptlb:node_171,ptin:_node_171,varname:node_7358,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector3,id:197,x:33363,y:32928,varname:node_197,prsc:2,v1:1,v2:0.6283767,v3:0.2909818;n:type:ShaderForge.SFN_Multiply,id:198,x:33562,y:32874,varname:node_198,prsc:2|A-215-OUT,B-197-OUT;n:type:ShaderForge.SFN_Multiply,id:215,x:33422,y:32666,varname:node_215,prsc:2|A-216-OUT,B-399-R;n:type:ShaderForge.SFN_Vector1,id:216,x:33272,y:32621,varname:node_216,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:399,x:33260,y:32755,ptovrint:False,ptlb:node_399,ptin:_node_399,varname:node_378,prsc:2,tex:8e82a8daecc0a9349862b6f020bbcc50,ntxv:0,isnm:False|UVIN-17-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:544,x:32753,y:32942,ptovrint:False,ptlb:node_544,ptin:_node_544,varname:node_7524,prsc:2,glob:False,v1:0.3;proporder:171-399-544;pass:END;sub:END;*/

Shader "Custom/6_18qilang" {
    Properties {
        _node_171 ("node_171", 2D) = "white" {}
        _node_399 ("node_399", 2D) = "white" {}
        _node_544 ("node_544", Float ) = 0.3
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
            uniform sampler2D _node_171; uniform float4 _node_171_ST;
            uniform sampler2D _node_399; uniform float4 _node_399_ST;
            uniform float _node_544;
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
                float2 node_17 = (i.uv0+_node_544*float2(0,0.5));
                float4 _node_399_var = tex2D(_node_399,TRANSFORM_TEX(node_17, _node_399));
                float3 emissive = ((2.0*_node_399_var.r)*float3(1,0.6283767,0.2909818));
                float3 finalColor = emissive;
                float4 _node_171_var = tex2D(_node_171,TRANSFORM_TEX(i.uv0, _node_171));
                return fixed4(finalColor,_node_171_var.r);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
