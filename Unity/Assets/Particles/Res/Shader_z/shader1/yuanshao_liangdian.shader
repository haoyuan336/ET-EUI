// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3195,x:32719,y:32712,varname:node_3195,prsc:2|emission-5827-OUT;n:type:ShaderForge.SFN_Tex2d,id:4581,x:32265,y:32611,ptovrint:False,ptlb:node_4581,ptin:_node_4581,varname:node_4581,prsc:2,tex:7c79398f89ebab944ace9c32a18f8207,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5467,x:31784,y:32618,ptovrint:False,ptlb:node_5467,ptin:_node_5467,varname:node_5467,prsc:2,tex:f6392bcb65970f94eb288f66ec6a7400,ntxv:0,isnm:False|UVIN-2149-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9366,x:31768,y:32832,ptovrint:False,ptlb:node_9366,ptin:_node_9366,varname:node_9366,prsc:2,tex:2fc431a899521674db6ac1d307bd29b3,ntxv:0,isnm:False|UVIN-2019-UVOUT;n:type:ShaderForge.SFN_Panner,id:2149,x:31583,y:32627,varname:node_2149,prsc:2,spu:0,spv:-0.3|UVIN-6152-UVOUT;n:type:ShaderForge.SFN_Panner,id:2019,x:31559,y:32832,varname:node_2019,prsc:2,spu:-0.2,spv:0|UVIN-8921-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6152,x:31295,y:32625,varname:node_6152,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:8921,x:31327,y:32832,varname:node_8921,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7925,x:31968,y:32926,varname:node_7925,prsc:2|A-5467-R,B-9366-R;n:type:ShaderForge.SFN_Add,id:5827,x:32466,y:32811,varname:node_5827,prsc:2|A-4581-RGB,B-1843-OUT;n:type:ShaderForge.SFN_Tex2d,id:1325,x:32062,y:32731,ptovrint:False,ptlb:node_1325,ptin:_node_1325,varname:node_1325,prsc:2,tex:ad538eaa2c3c0c049a3664e2259658e3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1843,x:32281,y:32879,varname:node_1843,prsc:2|A-1325-R,B-8940-OUT;n:type:ShaderForge.SFN_Slider,id:2242,x:31782,y:33263,ptovrint:False,ptlb:node_2242,ptin:_node_2242,varname:node_2242,prsc:2,min:0,cur:3,max:4;n:type:ShaderForge.SFN_Multiply,id:5669,x:32096,y:33106,varname:node_5669,prsc:2|A-7925-OUT,B-2242-OUT;n:type:ShaderForge.SFN_Color,id:7032,x:32036,y:33382,ptovrint:False,ptlb:node_7032,ptin:_node_7032,varname:node_7032,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:8940,x:32329,y:33115,varname:node_8940,prsc:2|A-5669-OUT,B-7032-RGB;proporder:4581-1325-5467-9366-2242-7032;pass:END;sub:END;*/

Shader "Unlit/yuanshao_liangdian" {
    Properties {
        _node_4581 ("node_4581", 2D) = "white" {}
        _node_1325 ("node_1325", 2D) = "white" {}
        _node_5467 ("node_5467", 2D) = "white" {}
        _node_9366 ("node_9366", 2D) = "white" {}
        _node_2242 ("node_2242", Range(0, 4)) = 3
        _node_7032 ("node_7032", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_4581; uniform float4 _node_4581_ST;
            uniform sampler2D _node_5467; uniform float4 _node_5467_ST;
            uniform sampler2D _node_9366; uniform float4 _node_9366_ST;
            uniform sampler2D _node_1325; uniform float4 _node_1325_ST;
            uniform float _node_2242;
            uniform float4 _node_7032;
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
                float4 _node_4581_var = tex2D(_node_4581,TRANSFORM_TEX(i.uv0, _node_4581));
                float4 _node_1325_var = tex2D(_node_1325,TRANSFORM_TEX(i.uv0, _node_1325));
                float4 node_7824 = _Time + _TimeEditor;
                float2 node_2149 = (i.uv0+node_7824.g*float2(0,-0.3));
                float4 _node_5467_var = tex2D(_node_5467,TRANSFORM_TEX(node_2149, _node_5467));
                float2 node_2019 = (i.uv0+node_7824.g*float2(-0.2,0));
                float4 _node_9366_var = tex2D(_node_9366,TRANSFORM_TEX(node_2019, _node_9366));
                float3 emissive = (_node_4581_var.rgb+(_node_1325_var.r*(((_node_5467_var.r*_node_9366_var.r)*_node_2242)*_node_7032.rgb)));
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
