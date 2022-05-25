// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:6049,x:32719,y:32712,varname:node_6049,prsc:2|emission-8420-OUT,alpha-4731-OUT;n:type:ShaderForge.SFN_Rotator,id:7624,x:31544,y:32625,varname:node_7624,prsc:2|UVIN-4432-UVOUT,ANG-1523-OUT;n:type:ShaderForge.SFN_TexCoord,id:4432,x:31313,y:32526,varname:node_4432,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:8050,x:30818,y:32622,ptovrint:False,ptlb:suiji,ptin:_suiji,varname:node_8050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Abs,id:5852,x:30999,y:32610,varname:node_5852,prsc:2|IN-8050-OUT;n:type:ShaderForge.SFN_Multiply,id:1523,x:31303,y:32756,varname:node_1523,prsc:2|A-5852-OUT,B-9163-R;n:type:ShaderForge.SFN_VertexColor,id:9163,x:30873,y:32831,varname:node_9163,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:3103,x:31411,y:33159,ptovrint:False,ptlb:node_3103,ptin:_node_3103,varname:node_3103,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bc160c3ba3372c847ad1fb35c71abae1,ntxv:0,isnm:False|UVIN-4186-UVOUT;n:type:ShaderForge.SFN_Rotator,id:4186,x:31096,y:33203,varname:node_4186,prsc:2|UVIN-1781-UVOUT,ANG-8051-OUT;n:type:ShaderForge.SFN_TexCoord,id:1781,x:30820,y:33174,varname:node_1781,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:8051,x:30826,y:33390,ptovrint:False,ptlb:fangxiang,ptin:_fangxiang,varname:node_8051,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Power,id:1414,x:31675,y:33214,varname:node_1414,prsc:2|VAL-3103-R,EXP-7365-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7365,x:31354,y:33407,ptovrint:False,ptlb:power,ptin:_power,varname:node_7365,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:9015,x:31736,y:32549,ptovrint:False,ptlb:node_9015,ptin:_node_9015,varname:node_9015,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e03f55b7c0484d64280dc5f53d20f8cb,ntxv:0,isnm:False|UVIN-7624-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4731,x:32368,y:32992,varname:node_4731,prsc:2|A-9015-A,B-9163-A,C-9845-A;n:type:ShaderForge.SFN_Multiply,id:8420,x:32283,y:32657,varname:node_8420,prsc:2|A-9015-RGB,B-1414-OUT,C-9845-RGB;n:type:ShaderForge.SFN_Color,id:9845,x:32010,y:33092,ptovrint:False,ptlb:node_9845,ptin:_node_9845,varname:node_9845,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:8050-9015-3103-8051-7365-9845;pass:END;sub:END;*/

Shader "Unlit/xieshi_yanwu" {
    Properties {
        _suiji ("suiji", Float ) = 1
        _node_9015 ("node_9015", 2D) = "white" {}
        _node_3103 ("node_3103", 2D) = "white" {}
        _fangxiang ("fangxiang", Float ) = 0
        _power ("power", Float ) = 0.5
        _node_9845 ("node_9845", Color) = (0.5,0.5,0.5,1)
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform float _suiji;
            uniform sampler2D _node_3103; uniform float4 _node_3103_ST;
            uniform float _fangxiang;
            uniform float _power;
            uniform sampler2D _node_9015; uniform float4 _node_9015_ST;
            uniform float4 _node_9845;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_7624_ang = (abs(_suiji)*i.vertexColor.r);
                float node_7624_spd = 1.0;
                float node_7624_cos = cos(node_7624_spd*node_7624_ang);
                float node_7624_sin = sin(node_7624_spd*node_7624_ang);
                float2 node_7624_piv = float2(0.5,0.5);
                float2 node_7624 = (mul(i.uv0-node_7624_piv,float2x2( node_7624_cos, -node_7624_sin, node_7624_sin, node_7624_cos))+node_7624_piv);
                float4 _node_9015_var = tex2D(_node_9015,TRANSFORM_TEX(node_7624, _node_9015));
                float node_4186_ang = _fangxiang;
                float node_4186_spd = 1.0;
                float node_4186_cos = cos(node_4186_spd*node_4186_ang);
                float node_4186_sin = sin(node_4186_spd*node_4186_ang);
                float2 node_4186_piv = float2(0.5,0.5);
                float2 node_4186 = (mul(i.uv0-node_4186_piv,float2x2( node_4186_cos, -node_4186_sin, node_4186_sin, node_4186_cos))+node_4186_piv);
                float4 _node_3103_var = tex2D(_node_3103,TRANSFORM_TEX(node_4186, _node_3103));
                float3 emissive = (_node_9015_var.rgb*pow(_node_3103_var.r,_power)*_node_9845.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_node_9015_var.a*i.vertexColor.a*_node_9845.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
