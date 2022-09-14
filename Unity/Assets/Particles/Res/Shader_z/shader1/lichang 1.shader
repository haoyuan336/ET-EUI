// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33724,y:32711,varname:node_4013,prsc:2|emission-9795-OUT,alpha-943-A;n:type:ShaderForge.SFN_Tex2d,id:9042,x:32086,y:32953,ptovrint:False,ptlb:node_9042,ptin:_node_9042,varname:node_9042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:adb4da47264141d43a63ed36fa798648,ntxv:0,isnm:False|UVIN-4990-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5765,x:32086,y:32788,ptovrint:False,ptlb:node_5765,ptin:_node_5765,varname:node_5765,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:16d9bc1e54f575544b319cf3fdb179d3,ntxv:0,isnm:False|UVIN-4773-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6367,x:32716,y:32853,ptovrint:False,ptlb:node_6367,ptin:_node_6367,varname:node_6367,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:66a9496c3e27a544e929037726be50de,ntxv:0,isnm:False|UVIN-3041-OUT;n:type:ShaderForge.SFN_TexCoord,id:8317,x:31750,y:32788,varname:node_8317,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4773,x:31920,y:32788,varname:node_4773,prsc:2,spu:0.2,spv:-0.1|UVIN-8317-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6209,x:31750,y:32953,varname:node_6209,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4990,x:31920,y:32953,varname:node_4990,prsc:2,spu:-0.1,spv:0.2|UVIN-6209-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6763,x:32379,y:32873,varname:node_6763,prsc:2|A-5765-R,B-9042-R,C-6889-OUT;n:type:ShaderForge.SFN_Multiply,id:4876,x:32274,y:33059,varname:node_4876,prsc:2|A-5765-R,B-9042-R;n:type:ShaderForge.SFN_Add,id:3041,x:32547,y:32853,varname:node_3041,prsc:2|A-9695-UVOUT,B-6763-OUT;n:type:ShaderForge.SFN_Tex2d,id:256,x:33111,y:33087,ptovrint:False,ptlb:node_6367_copy,ptin:_node_6367_copy,varname:_node_6367_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:66a9496c3e27a544e929037726be50de,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6124,x:32951,y:32982,varname:node_6124,prsc:2|A-6367-R,B-4876-OUT,C-3196-RGB,D-5280-OUT;n:type:ShaderForge.SFN_Panner,id:9695,x:32259,y:32712,varname:node_9695,prsc:2,spu:0.1,spv:0|UVIN-5379-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5379,x:32056,y:32620,varname:node_5379,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9795,x:33301,y:32822,varname:node_9795,prsc:2|A-6124-OUT,B-256-R,C-622-OUT;n:type:ShaderForge.SFN_OneMinus,id:622,x:32984,y:32710,varname:node_622,prsc:2|IN-2611-R;n:type:ShaderForge.SFN_Tex2d,id:2611,x:32726,y:32679,ptovrint:False,ptlb:node_2611,ptin:_node_2611,varname:node_2611,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ba3b026dc3be6154798d242f69c475b8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:6889,x:32214,y:32930,varname:node_6889,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Color,id:3196,x:32613,y:33089,ptovrint:False,ptlb:node_3196,ptin:_node_3196,varname:node_3196,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.2689655,c3:0,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:5280,x:32746,y:33234,ptovrint:False,ptlb:node_5280,ptin:_node_5280,varname:node_5280,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_VertexColor,id:943,x:33275,y:33008,varname:node_943,prsc:2;proporder:9042-5765-6367-256-2611-3196-5280;pass:END;sub:END;*/

Shader "Shader Forge/lichang" {
    Properties {
        _node_9042 ("node_9042", 2D) = "white" {}
        _node_5765 ("node_5765", 2D) = "white" {}
        _node_6367 ("node_6367", 2D) = "white" {}
        _node_6367_copy ("node_6367_copy", 2D) = "white" {}
        _node_2611 ("node_2611", 2D) = "white" {}
        _node_3196 ("node_3196", Color) = (1,0.2689655,0,1)
        _node_5280 ("node_5280", Float ) = 5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha One
            Cull Off
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
            uniform sampler2D _node_9042; uniform float4 _node_9042_ST;
            uniform sampler2D _node_5765; uniform float4 _node_5765_ST;
            uniform sampler2D _node_6367; uniform float4 _node_6367_ST;
            uniform sampler2D _node_6367_copy; uniform float4 _node_6367_copy_ST;
            uniform sampler2D _node_2611; uniform float4 _node_2611_ST;
            uniform float4 _node_3196;
            uniform float _node_5280;
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
                float4 node_284 = _Time + _TimeEditor;
                float2 node_4773 = (i.uv0+node_284.g*float2(0.2,-0.1));
                float4 _node_5765_var = tex2D(_node_5765,TRANSFORM_TEX(node_4773, _node_5765));
                float2 node_4990 = (i.uv0+node_284.g*float2(-0.1,0.2));
                float4 _node_9042_var = tex2D(_node_9042,TRANSFORM_TEX(node_4990, _node_9042));
                float2 node_3041 = ((i.uv0+node_284.g*float2(0.1,0))+(_node_5765_var.r*_node_9042_var.r*0.3));
                float4 _node_6367_var = tex2D(_node_6367,TRANSFORM_TEX(node_3041, _node_6367));
                float4 _node_6367_copy_var = tex2D(_node_6367_copy,TRANSFORM_TEX(i.uv0, _node_6367_copy));
                float4 _node_2611_var = tex2D(_node_2611,TRANSFORM_TEX(i.uv0, _node_2611));
                float3 emissive = ((_node_6367_var.r*(_node_5765_var.r*_node_9042_var.r)*_node_3196.rgb*_node_5280)*_node_6367_copy_var.r*(1.0 - _node_2611_var.r));
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
