// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// 注意：手动更改此数据可能会妨碍您在 Shader Forge 中打开它
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2021,x:32719,y:32712,varname:node_2021,prsc:2|emission-7857-OUT;n:type:ShaderForge.SFN_Tex2d,id:709,x:32159,y:32723,ptovrint:False,ptlb:node_709,ptin:_node_709,varname:_node_709,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3d858289703948e439d29cf3b433e286,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2256,x:31708,y:32940,ptovrint:False,ptlb:node_2256,ptin:_node_2256,varname:_node_2256,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:22e1ca65e420e0b408bbe167bf8cfb03,ntxv:0,isnm:False|UVIN-9526-UVOUT;n:type:ShaderForge.SFN_Add,id:7857,x:32407,y:32826,varname:node_7857,prsc:2|A-709-RGB,B-5908-OUT;n:type:ShaderForge.SFN_Color,id:7946,x:32063,y:33314,ptovrint:False,ptlb:node_7946,ptin:_node_7946,varname:_node_7946,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5908,x:32216,y:33119,varname:node_5908,prsc:2|A-4156-OUT,B-7946-RGB;n:type:ShaderForge.SFN_TexCoord,id:1038,x:31353,y:32990,varname:node_1038,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:9526,x:31542,y:32990,varname:node_9526,prsc:2,spu:0.23,spv:-0.1|UVIN-1038-UVOUT;n:type:ShaderForge.SFN_Power,id:7979,x:31885,y:32986,varname:node_7979,prsc:2|VAL-2256-R,EXP-6325-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6325,x:31698,y:33156,ptovrint:False,ptlb:power,ptin:_power,varname:node_6325,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:8478,x:31767,y:33321,ptovrint:False,ptlb:beishu,ptin:_beishu,varname:node_8478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4156,x:32041,y:33119,varname:node_4156,prsc:2|A-7979-OUT,B-8478-OUT;proporder:709-2256-7946-6325-8478;pass:END;sub:END;*/

Shader "Unlit/ui_lianhuashouzhang" {
    Properties {
        _node_709 ("node_709", 2D) = "white" {}
        _node_2256 ("node_2256", 2D) = "white" {}
        _node_7946 ("node_7946", Color) = (1,1,1,1)
        _power ("power", Float ) = 1
        _beishu ("beishu", Float ) = 1
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_709; uniform float4 _node_709_ST;
            uniform sampler2D _node_2256; uniform float4 _node_2256_ST;
            uniform float4 _node_7946;
            uniform float _power;
            uniform float _beishu;
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
                float4 _node_709_var = tex2D(_node_709,TRANSFORM_TEX(i.uv0, _node_709));
                float4 node_5343 = _Time;
                float2 node_9526 = (i.uv0+node_5343.g*float2(0.23,-0.1));
                float4 _node_2256_var = tex2D(_node_2256,TRANSFORM_TEX(node_9526, _node_2256));
                float3 emissive = (_node_709_var.rgb+((pow(_node_2256_var.r,_power)*_beishu)*_node_7946.rgb));
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
