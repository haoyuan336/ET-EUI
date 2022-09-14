// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// 注意：手动更改此数据可能会妨碍您在 Shader Forge 中打开它
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2021,x:32719,y:32712,varname:node_2021,prsc:2|emission-5908-OUT,alpha-6618-OUT;n:type:ShaderForge.SFN_Tex2d,id:2256,x:31313,y:32943,ptovrint:False,ptlb:node_2256,ptin:_node_2256,varname:_node_2256,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:22e1ca65e420e0b408bbe167bf8cfb03,ntxv:0,isnm:False|UVIN-9526-UVOUT;n:type:ShaderForge.SFN_Color,id:7946,x:32023,y:33424,ptovrint:False,ptlb:node_7946,ptin:_node_7946,varname:_node_7946,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5908,x:32409,y:33188,varname:node_5908,prsc:2|A-4068-OUT,B-7946-RGB;n:type:ShaderForge.SFN_TexCoord,id:1038,x:30938,y:32943,varname:node_1038,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:9526,x:31122,y:32943,varname:node_9526,prsc:2,spu:-0.1,spv:-0.15|UVIN-1038-UVOUT;n:type:ShaderForge.SFN_Power,id:7979,x:31835,y:33245,varname:node_7979,prsc:2|VAL-9001-OUT,EXP-6325-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6325,x:31644,y:33337,ptovrint:False,ptlb:power,ptin:_power,varname:node_6325,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:8478,x:31825,y:33436,ptovrint:False,ptlb:beishu,ptin:_beishu,varname:node_8478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4156,x:32006,y:33257,varname:node_4156,prsc:2|A-7979-OUT,B-8478-OUT;n:type:ShaderForge.SFN_Tex2d,id:5577,x:31343,y:33143,ptovrint:False,ptlb:node_5577,ptin:_node_5577,varname:node_5577,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:68972efc2d9b2e949a4e254d90d9bab9,ntxv:0,isnm:False|UVIN-3555-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2392,x:30926,y:33126,varname:node_2392,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:3555,x:31122,y:33126,varname:node_3555,prsc:2,spu:0.05,spv:-0.05|UVIN-2392-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9001,x:31644,y:33149,varname:node_9001,prsc:2|A-2256-R,B-5577-R;n:type:ShaderForge.SFN_Multiply,id:4068,x:32173,y:33155,varname:node_4068,prsc:2|A-9331-R,B-4156-OUT;n:type:ShaderForge.SFN_Tex2d,id:9331,x:31913,y:32954,ptovrint:False,ptlb:node_9331,ptin:_node_9331,varname:node_9331,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6618,x:32328,y:32805,varname:node_6618,prsc:2|A-2256-A,B-9331-A,C-5577-A;proporder:2256-7946-6325-8478-5577-9331;pass:END;sub:END;*/

Shader "Unlit/q_chengjiu_01" {
    Properties {
        _node_2256 ("node_2256", 2D) = "white" {}
        _node_7946 ("node_7946", Color) = (1,1,1,1)
        _power ("power", Float ) = 1
        _beishu ("beishu", Float ) = 1
        _node_5577 ("node_5577", 2D) = "white" {}
        _node_9331 ("node_9331", 2D) = "white" {}
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
            Blend SrcAlpha One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_2256; uniform float4 _node_2256_ST;
            uniform float4 _node_7946;
            uniform float _power;
            uniform float _beishu;
            uniform sampler2D _node_5577; uniform float4 _node_5577_ST;
            uniform sampler2D _node_9331; uniform float4 _node_9331_ST;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _node_9331_var = tex2D(_node_9331,TRANSFORM_TEX(i.uv0, _node_9331));
                float4 node_1374 = _Time;
                float2 node_9526 = (i.uv0+node_1374.g*float2(-0.1,-0.15));
                float4 _node_2256_var = tex2D(_node_2256,TRANSFORM_TEX(node_9526, _node_2256));
                float2 node_3555 = (i.uv0+node_1374.g*float2(0.05,-0.05));
                float4 _node_5577_var = tex2D(_node_5577,TRANSFORM_TEX(node_3555, _node_5577));
                float3 emissive = ((_node_9331_var.r*(pow((_node_2256_var.r*_node_5577_var.r),_power)*_beishu))*_node_7946.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_node_2256_var.a*_node_9331_var.a*_node_5577_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
