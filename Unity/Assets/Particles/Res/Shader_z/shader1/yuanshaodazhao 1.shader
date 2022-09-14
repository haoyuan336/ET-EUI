// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:8997,x:32697,y:32721,varname:node_8997,prsc:2|emission-936-OUT,alpha-2768-A;n:type:ShaderForge.SFN_Tex2d,id:6237,x:31373,y:32787,ptovrint:False,ptlb:node_6237,ptin:_node_6237,varname:node_6237,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:753675c49623e3d4a91e06b1d39b3083,ntxv:0,isnm:False|UVIN-9775-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:310,x:31853,y:32944,ptovrint:False,ptlb:node_310,ptin:_node_310,varname:node_310,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a349495ee72010641947bc2a082ebb8c,ntxv:0,isnm:False|UVIN-6361-OUT;n:type:ShaderForge.SFN_Tex2d,id:2015,x:31352,y:32999,ptovrint:False,ptlb:node_2015,ptin:_node_2015,varname:node_2015,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2fb6a9e62bad6e14383e2e7d547f3ba6,ntxv:0,isnm:False|UVIN-8993-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2421,x:32019,y:33223,ptovrint:False,ptlb:node_2421,ptin:_node_2421,varname:node_2421,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:90c31ac011e0bed429c9b7347f019b9c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:9775,x:31150,y:32760,varname:node_9775,prsc:2,spu:0,spv:2|UVIN-3155-UVOUT;n:type:ShaderForge.SFN_Panner,id:8993,x:31092,y:32943,varname:node_8993,prsc:2,spu:0,spv:1|UVIN-766-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2510,x:31326,y:32582,varname:node_2510,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:6361,x:31616,y:32944,varname:node_6361,prsc:2|A-6237-R,B-2015-R,C-2510-UVOUT;n:type:ShaderForge.SFN_Multiply,id:538,x:32353,y:32948,varname:node_538,prsc:2|A-2712-OUT,B-668-OUT;n:type:ShaderForge.SFN_VertexColor,id:2768,x:32077,y:32720,varname:node_2768,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:3467,x:32212,y:32878,ptovrint:False,ptlb:node_3467,ptin:_node_3467,varname:node_3467,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:2712,x:32074,y:33012,varname:node_2712,prsc:2|VAL-310-R,EXP-3250-OUT;n:type:ShaderForge.SFN_Vector1,id:3250,x:31853,y:33201,varname:node_3250,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Power,id:668,x:32211,y:33263,varname:node_668,prsc:2|VAL-2421-R,EXP-8378-OUT;n:type:ShaderForge.SFN_Multiply,id:936,x:32463,y:32750,varname:node_936,prsc:2|A-2768-RGB,B-538-OUT,C-3467-OUT,D-6783-RGB;n:type:ShaderForge.SFN_Color,id:6783,x:31901,y:32720,ptovrint:False,ptlb:node_6783,ptin:_node_6783,varname:node_6783,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:8378,x:31804,y:33424,ptovrint:False,ptlb:node_8378,ptin:_node_8378,varname:node_8378,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:4;n:type:ShaderForge.SFN_TexCoord,id:3155,x:30863,y:32772,varname:node_3155,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:766,x:30859,y:32944,varname:node_766,prsc:2,uv:0;proporder:6237-310-2015-2421-3467-6783-8378;pass:END;sub:END;*/

Shader "Custom/yuanshaodazhao" {
    Properties {
        _node_6237 ("node_6237", 2D) = "white" {}
        _node_310 ("node_310", 2D) = "white" {}
        _node_2015 ("node_2015", 2D) = "white" {}
        _node_2421 ("node_2421", 2D) = "white" {}
        _node_3467 ("node_3467", Float ) = 1
        _node_6783 ("node_6783", Color) = (0.5,0.5,0.5,1)
        _node_8378 ("node_8378", Range(0, 4)) = 0
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
            Blend One One
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
            uniform sampler2D _node_6237; uniform float4 _node_6237_ST;
            uniform sampler2D _node_310; uniform float4 _node_310_ST;
            uniform sampler2D _node_2015; uniform float4 _node_2015_ST;
            uniform sampler2D _node_2421; uniform float4 _node_2421_ST;
            uniform float _node_3467;
            uniform float4 _node_6783;
            uniform float _node_8378;
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
                float4 node_3381 = _Time + _TimeEditor;
                float2 node_9775 = (i.uv0+node_3381.g*float2(0,2));
                float4 _node_6237_var = tex2D(_node_6237,TRANSFORM_TEX(node_9775, _node_6237));
                float2 node_8993 = (i.uv0+node_3381.g*float2(0,1));
                float4 _node_2015_var = tex2D(_node_2015,TRANSFORM_TEX(node_8993, _node_2015));
                float2 node_6361 = (_node_6237_var.r+_node_2015_var.r+i.uv0);
                float4 _node_310_var = tex2D(_node_310,TRANSFORM_TEX(node_6361, _node_310));
                float4 _node_2421_var = tex2D(_node_2421,TRANSFORM_TEX(i.uv0, _node_2421));
                float3 emissive = (i.vertexColor.rgb*(pow(_node_310_var.r,1.5)*pow(_node_2421_var.r,_node_8378))*_node_3467*_node_6783.rgb);
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
