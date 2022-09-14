// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-8895-OUT;n:type:ShaderForge.SFN_Tex2d,id:323,x:32295,y:32715,ptovrint:False,ptlb:node_323,ptin:_node_323,varname:node_323,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:443d674bb0d948a40a3cee8b879a7dd4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5950,x:32313,y:32493,ptovrint:False,ptlb:node_5950,ptin:_node_5950,varname:node_5950,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f1e6c83f1b040c346b67c76cc4ec21df,ntxv:0,isnm:False|UVIN-3048-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1814,x:31801,y:32503,varname:node_1814,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:3048,x:32010,y:32528,varname:node_3048,prsc:2,spu:-0.15,spv:-0.4|UVIN-1814-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8895,x:32585,y:32730,varname:node_8895,prsc:2|A-5950-R,B-323-RGB,C-4942-OUT,D-5945-RGB;n:type:ShaderForge.SFN_Slider,id:4942,x:32181,y:33004,ptovrint:False,ptlb:node_4942,ptin:_node_4942,varname:node_4942,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Color,id:5945,x:32327,y:33147,ptovrint:False,ptlb:node_5945,ptin:_node_5945,varname:node_5945,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:323-5950-4942-5945;pass:END;sub:END;*/

Shader "Shader Forge/yuji_shenqi" {
    Properties {
        _node_323 ("node_323", 2D) = "white" {}
        _node_5950 ("node_5950", 2D) = "white" {}
        _node_4942 ("node_4942", Range(0, 5)) = 0
        _node_5945 ("node_5945", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_323; uniform float4 _node_323_ST;
            uniform sampler2D _node_5950; uniform float4 _node_5950_ST;
            uniform float _node_4942;
            uniform float4 _node_5945;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 node_7634 = _Time + _TimeEditor;
                float2 node_3048 = (i.uv0+node_7634.g*float2(-0.15,-0.4));
                float4 _node_5950_var = tex2D(_node_5950,TRANSFORM_TEX(node_3048, _node_5950));
                float4 _node_323_var = tex2D(_node_323,TRANSFORM_TEX(i.uv0, _node_323));
                float3 finalColor = (_node_5950_var.r*_node_323_var.rgb*_node_4942*_node_5945.rgb);
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
