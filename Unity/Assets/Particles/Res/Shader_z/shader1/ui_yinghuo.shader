// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// 注意：手动更改此数据可能会妨碍您在 Shader Forge 中打开它
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2925,x:32719,y:32712,varname:node_2925,prsc:2|emission-9648-OUT,alpha-8703-OUT;n:type:ShaderForge.SFN_Tex2d,id:5982,x:31310,y:32711,ptovrint:False,ptlb:node_5982,ptin:_node_5982,varname:node_5982,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:64e671d7ee7b78649a5fa4bbe0d828d0,ntxv:0,isnm:False|UVIN-1187-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3997,x:31310,y:32527,ptovrint:False,ptlb:node_3997,ptin:_node_3997,varname:node_3997,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b84e6b6dbcf58614198d545865e8ab57,ntxv:0,isnm:False|UVIN-3867-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6722,x:30944,y:32527,varname:node_6722,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:3867,x:31129,y:32527,varname:node_3867,prsc:2,spu:0,spv:-0.15|UVIN-6722-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6694,x:31507,y:32617,varname:node_6694,prsc:2|A-3997-R,B-5982-R;n:type:ShaderForge.SFN_Color,id:7868,x:32265,y:32805,ptovrint:False,ptlb:node_7868,ptin:_node_7868,varname:node_7868,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9648,x:32508,y:32805,varname:node_9648,prsc:2|A-141-OUT,B-7868-RGB;n:type:ShaderForge.SFN_Multiply,id:141,x:32322,y:32657,varname:node_141,prsc:2|A-3228-OUT,B-1979-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3228,x:32076,y:32619,ptovrint:False,ptlb:liangdu,ptin:_liangdu,varname:node_3228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:959,x:31498,y:33092,ptovrint:False,ptlb:node_959,ptin:_node_959,varname:node_959,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a3ef47411cf8d7545b58b884472d3a58,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1979,x:31943,y:32668,varname:node_1979,prsc:2|A-6893-OUT,B-959-A;n:type:ShaderForge.SFN_Multiply,id:8703,x:32052,y:33076,varname:node_8703,prsc:2|A-6694-OUT,B-959-A;n:type:ShaderForge.SFN_TexCoord,id:838,x:30945,y:32682,varname:node_838,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1187,x:31153,y:32682,varname:node_1187,prsc:2,spu:0,spv:-0.07|UVIN-838-UVOUT;n:type:ShaderForge.SFN_Power,id:6893,x:31720,y:32541,varname:node_6893,prsc:2|VAL-6694-OUT,EXP-8883-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8883,x:31472,y:32803,ptovrint:False,ptlb:power,ptin:_power,varname:node_8883,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:5982-3997-7868-3228-959-8883;pass:END;sub:END;*/

Shader "Unlit/ui_yinghuo" {
    Properties {
        _node_5982 ("node_5982", 2D) = "white" {}
        _node_3997 ("node_3997", 2D) = "white" {}
        _node_7868 ("node_7868", Color) = (0.5,0.5,0.5,1)
        _liangdu ("liangdu", Float ) = 1
        _node_959 ("node_959", 2D) = "bump" {}
        _power ("power", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
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
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _node_5982; uniform float4 _node_5982_ST;
            uniform sampler2D _node_3997; uniform float4 _node_3997_ST;
            uniform float4 _node_7868;
            uniform float _liangdu;
            uniform sampler2D _node_959; uniform float4 _node_959_ST;
            uniform float _power;
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
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7903 = _Time;
                float2 node_3867 = (i.uv0+node_7903.g*float2(0,-0.15));
                float4 _node_3997_var = tex2D(_node_3997,TRANSFORM_TEX(node_3867, _node_3997));
                float2 node_1187 = (i.uv0+node_7903.g*float2(0,-0.07));
                float4 _node_5982_var = tex2D(_node_5982,TRANSFORM_TEX(node_1187, _node_5982));
                float node_6694 = (_node_3997_var.r*_node_5982_var.r);
                float4 _node_959_var = tex2D(_node_959,TRANSFORM_TEX(i.uv0, _node_959));
                float3 emissive = ((_liangdu*(pow(node_6694,_power)*_node_959_var.a))*_node_7868.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_6694*_node_959_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
