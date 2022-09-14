// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1665,x:32719,y:32712,varname:node_1665,prsc:2|emission-4658-OUT;n:type:ShaderForge.SFN_Tex2d,id:507,x:31954,y:32846,ptovrint:False,ptlb:node_507,ptin:_node_507,varname:_node_507,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e8b27fcce11e8044d83542565f5b9bbc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:5334,x:31341,y:32697,varname:node_5334,prsc:2,spu:0.1,spv:0.6|UVIN-4336-UVOUT;n:type:ShaderForge.SFN_Add,id:371,x:31861,y:32659,varname:node_371,prsc:2|A-75-R,B-7801-R;n:type:ShaderForge.SFN_Multiply,id:8010,x:32459,y:32775,varname:node_8010,prsc:2|A-7622-OUT,B-3323-OUT;n:type:ShaderForge.SFN_Power,id:3323,x:32178,y:32891,varname:node_3323,prsc:2|VAL-507-R,EXP-2185-OUT;n:type:ShaderForge.SFN_Vector1,id:2185,x:31954,y:33029,varname:node_2185,prsc:2,v1:3;n:type:ShaderForge.SFN_Tex2d,id:7801,x:31589,y:32913,ptovrint:False,ptlb:node_7801,ptin:_node_7801,varname:_node_7801,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:753675c49623e3d4a91e06b1d39b3083,ntxv:0,isnm:False|UVIN-6931-UVOUT;n:type:ShaderForge.SFN_Panner,id:6931,x:31341,y:32913,varname:node_6931,prsc:2,spu:-0.2,spv:0.2|UVIN-1777-UVOUT;n:type:ShaderForge.SFN_Color,id:8760,x:32486,y:32454,ptovrint:False,ptlb:node_8760,ptin:_node_8760,varname:_node_8760,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:4658,x:32648,y:32607,varname:node_4658,prsc:2|A-8760-RGB,B-8010-OUT;n:type:ShaderForge.SFN_Tex2d,id:75,x:31612,y:32659,ptovrint:False,ptlb:node_75,ptin:_node_75,varname:_node_75,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0a83aff5ff0939c48a72aaa58dc21b64,ntxv:0,isnm:False|UVIN-5334-UVOUT;n:type:ShaderForge.SFN_Slider,id:6230,x:31669,y:32450,ptovrint:False,ptlb:node_6230,ptin:_node_6230,varname:_node_6230,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:3;n:type:ShaderForge.SFN_Multiply,id:7622,x:32107,y:32502,varname:node_7622,prsc:2|A-6230-OUT,B-371-OUT;n:type:ShaderForge.SFN_TexCoord,id:4336,x:31005,y:32758,varname:node_4336,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:1777,x:31023,y:32935,varname:node_1777,prsc:2,uv:0;proporder:507-7801-8760-75-6230;pass:END;sub:END;*/

Shader "Unlit/boss03_dahuoqiu" {
    Properties {
        _node_507 ("node_507", 2D) = "white" {}
        _node_7801 ("node_7801", 2D) = "white" {}
        _node_8760 ("node_8760", Color) = (1,1,1,1)
        _node_75 ("node_75", 2D) = "white" {}
        _node_6230 ("node_6230", Range(0, 3)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_507; uniform float4 _node_507_ST;
            uniform sampler2D _node_7801; uniform float4 _node_7801_ST;
            uniform float4 _node_8760;
            uniform sampler2D _node_75; uniform float4 _node_75_ST;
            uniform float _node_6230;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_1951 = _Time + _TimeEditor;
                float2 node_5334 = (i.uv0+node_1951.g*float2(0.1,0.6));
                float4 _node_75_var = tex2D(_node_75,TRANSFORM_TEX(node_5334, _node_75));
                float2 node_6931 = (i.uv0+node_1951.g*float2(-0.2,0.2));
                float4 _node_7801_var = tex2D(_node_7801,TRANSFORM_TEX(node_6931, _node_7801));
                float4 _node_507_var = tex2D(_node_507,TRANSFORM_TEX(i.uv0, _node_507));
                float3 emissive = (_node_8760.rgb*((_node_6230*(_node_75_var.r+_node_7801_var.r))*pow(_node_507_var.r,3.0)));
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
