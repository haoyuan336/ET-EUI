// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33515,y:33015,varname:node_4013,prsc:2|emission-7007-OUT,alpha-5313-OUT;n:type:ShaderForge.SFN_Tex2d,id:6832,x:32557,y:32957,ptovrint:False,ptlb:node_6832,ptin:_node_6832,varname:node_6832,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e48ccff9922b7aa40800d93a002b05fd,ntxv:0,isnm:False|UVIN-4114-OUT;n:type:ShaderForge.SFN_Tex2d,id:4931,x:32667,y:33276,ptovrint:False,ptlb:node_4931,ptin:_node_4931,varname:node_4931,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd83b1e31ffedb049a517309ff43febf,ntxv:0,isnm:False|UVIN-4736-OUT;n:type:ShaderForge.SFN_Tex2d,id:9825,x:32599,y:32755,ptovrint:False,ptlb:node_9825,ptin:_node_9825,varname:node_9825,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dfda528dec2411047ac92cb6d160a901,ntxv:0,isnm:False|UVIN-7543-OUT;n:type:ShaderForge.SFN_Tex2d,id:9309,x:31724,y:32634,ptovrint:False,ptlb:node_9309,ptin:_node_9309,varname:node_9309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:adb4da47264141d43a63ed36fa798648,ntxv:0,isnm:False|UVIN-1242-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4666,x:31711,y:32875,ptovrint:False,ptlb:node_4666,ptin:_node_4666,varname:node_4666,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:022611aac36306d43aa55604ca6be6f0,ntxv:0,isnm:False|UVIN-1866-UVOUT;n:type:ShaderForge.SFN_Panner,id:1242,x:31470,y:32637,varname:node_1242,prsc:2,spu:0,spv:-0.1|UVIN-8902-UVOUT;n:type:ShaderForge.SFN_Panner,id:1866,x:31500,y:32862,varname:node_1866,prsc:2,spu:0,spv:-0.3|UVIN-7940-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8902,x:31306,y:32637,varname:node_8902,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:7940,x:31317,y:32862,varname:node_7940,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7663,x:32214,y:32794,varname:node_7663,prsc:2|A-9309-R,B-4666-R,C-7254-OUT;n:type:ShaderForge.SFN_Add,id:7543,x:32401,y:32766,varname:node_7543,prsc:2|A-3659-UVOUT,B-7663-OUT;n:type:ShaderForge.SFN_TexCoord,id:3659,x:32229,y:32644,varname:node_3659,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:7254,x:31724,y:32797,varname:node_7254,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:6161,x:32726,y:32993,varname:node_6161,prsc:2|A-6832-R,B-2384-RGB,C-4626-OUT;n:type:ShaderForge.SFN_Color,id:2384,x:32391,y:33174,ptovrint:False,ptlb:node_2384,ptin:_node_2384,varname:node_2384,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1586208,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Add,id:824,x:33125,y:32930,varname:node_824,prsc:2|A-6845-OUT,B-8830-OUT;n:type:ShaderForge.SFN_Multiply,id:87,x:32214,y:33065,varname:node_87,prsc:2|A-7563-R,B-6959-R,C-598-OUT;n:type:ShaderForge.SFN_Add,id:4114,x:32391,y:32974,varname:node_4114,prsc:2|A-7345-UVOUT,B-87-OUT;n:type:ShaderForge.SFN_TexCoord,id:7345,x:32214,y:32911,varname:node_7345,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:598,x:32052,y:33200,varname:node_598,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:8910,x:32073,y:33370,varname:node_8910,prsc:2|A-9309-R,B-4666-R,C-7856-OUT;n:type:ShaderForge.SFN_Add,id:4736,x:32232,y:33340,varname:node_4736,prsc:2|A-1965-UVOUT,B-8910-OUT;n:type:ShaderForge.SFN_TexCoord,id:1965,x:32073,y:33266,varname:node_1965,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:7856,x:31939,y:33345,varname:node_7856,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Multiply,id:6845,x:32820,y:32772,varname:node_6845,prsc:2|A-3569-OUT,B-9825-R;n:type:ShaderForge.SFN_ValueProperty,id:3569,x:32727,y:32541,ptovrint:False,ptlb:node_3569,ptin:_node_3569,varname:node_3569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_ValueProperty,id:4626,x:32577,y:33153,ptovrint:False,ptlb:node_4626,ptin:_node_4626,varname:node_4626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Tex2d,id:7563,x:31607,y:33029,ptovrint:False,ptlb:node_9309_copy,ptin:_node_9309_copy,varname:_node_9309_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:adb4da47264141d43a63ed36fa798648,ntxv:0,isnm:False|UVIN-852-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6959,x:31594,y:33270,ptovrint:False,ptlb:node_4666_copy,ptin:_node_4666_copy,varname:_node_4666_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:022611aac36306d43aa55604ca6be6f0,ntxv:0,isnm:False|UVIN-818-UVOUT;n:type:ShaderForge.SFN_Panner,id:852,x:31353,y:33032,varname:node_852,prsc:2,spu:-0.1,spv:-0.1|UVIN-8619-UVOUT;n:type:ShaderForge.SFN_Panner,id:818,x:31383,y:33257,varname:node_818,prsc:2,spu:-0.1,spv:0.2|UVIN-7151-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8619,x:31189,y:33032,varname:node_8619,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:7151,x:31200,y:33257,varname:node_7151,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:3140,x:31607,y:33192,varname:node_3140,prsc:2,v1:0.1;n:type:ShaderForge.SFN_If,id:7209,x:32363,y:33708,varname:node_7209,prsc:2|A-2643-A,B-8716-R,GT-114-OUT,EQ-114-OUT,LT-6865-OUT;n:type:ShaderForge.SFN_VertexColor,id:2643,x:31870,y:33559,varname:node_2643,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:8716,x:31842,y:33792,ptovrint:False,ptlb:node_8716,ptin:_node_8716,varname:node_8716,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5fa67499fe277b84299e152b2983784a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:6151,x:31733,y:33699,ptovrint:False,ptlb:node_6151,ptin:_node_6151,varname:node_6151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3890608,max:1;n:type:ShaderForge.SFN_Vector1,id:6865,x:32147,y:33904,varname:node_6865,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:114,x:32153,y:33837,varname:node_114,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5313,x:33013,y:33363,varname:node_5313,prsc:2|A-4931-A,B-7209-OUT;n:type:ShaderForge.SFN_Multiply,id:8830,x:33043,y:33097,varname:node_8830,prsc:2|A-6161-OUT,B-7209-OUT;n:type:ShaderForge.SFN_Multiply,id:7007,x:33236,y:32817,varname:node_7007,prsc:2|A-3381-RGB,B-824-OUT;n:type:ShaderForge.SFN_Color,id:3381,x:32993,y:32612,ptovrint:False,ptlb:node_3381,ptin:_node_3381,varname:node_3381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4044118,c2:0.5563894,c3:1,c4:1;proporder:9309-6832-9825-4666-4931-2384-3569-4626-7563-6959-8716-6151-3381;pass:END;sub:END;*/

Shader "Shader Forge/tui" {
    Properties {
        _node_9309 ("node_9309", 2D) = "white" {}
        _node_6832 ("node_6832", 2D) = "white" {}
        _node_9825 ("node_9825", 2D) = "white" {}
        _node_4666 ("node_4666", 2D) = "white" {}
        _node_4931 ("node_4931", 2D) = "white" {}
        _node_2384 ("node_2384", Color) = (0.1586208,0,1,1)
        _node_3569 ("node_3569", Float ) = 5
        _node_4626 ("node_4626", Float ) = 6
        _node_9309_copy ("node_9309_copy", 2D) = "white" {}
        _node_4666_copy ("node_4666_copy", 2D) = "white" {}
        _node_8716 ("node_8716", 2D) = "white" {}
        _node_6151 ("node_6151", Range(0, 1)) = 0.3890608
        _node_3381 ("node_3381", Color) = (0.4044118,0.5563894,1,1)
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform sampler2D _node_6832; uniform float4 _node_6832_ST;
            uniform sampler2D _node_4931; uniform float4 _node_4931_ST;
            uniform sampler2D _node_9825; uniform float4 _node_9825_ST;
            uniform sampler2D _node_9309; uniform float4 _node_9309_ST;
            uniform sampler2D _node_4666; uniform float4 _node_4666_ST;
            uniform float4 _node_2384;
            uniform float _node_3569;
            uniform float _node_4626;
            uniform sampler2D _node_9309_copy; uniform float4 _node_9309_copy_ST;
            uniform sampler2D _node_4666_copy; uniform float4 _node_4666_copy_ST;
            uniform sampler2D _node_8716; uniform float4 _node_8716_ST;
            uniform float4 _node_3381;
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
                float4 node_9644 = _Time + _TimeEditor;
                float2 node_1242 = (i.uv0+node_9644.g*float2(0,-0.1));
                float4 _node_9309_var = tex2D(_node_9309,TRANSFORM_TEX(node_1242, _node_9309));
                float2 node_1866 = (i.uv0+node_9644.g*float2(0,-0.3));
                float4 _node_4666_var = tex2D(_node_4666,TRANSFORM_TEX(node_1866, _node_4666));
                float2 node_7543 = (i.uv0+(_node_9309_var.r*_node_4666_var.r*0.1));
                float4 _node_9825_var = tex2D(_node_9825,TRANSFORM_TEX(node_7543, _node_9825));
                float2 node_852 = (i.uv0+node_9644.g*float2(-0.1,-0.1));
                float4 _node_9309_copy_var = tex2D(_node_9309_copy,TRANSFORM_TEX(node_852, _node_9309_copy));
                float2 node_818 = (i.uv0+node_9644.g*float2(-0.1,0.2));
                float4 _node_4666_copy_var = tex2D(_node_4666_copy,TRANSFORM_TEX(node_818, _node_4666_copy));
                float2 node_4114 = (i.uv0+(_node_9309_copy_var.r*_node_4666_copy_var.r*0.2));
                float4 _node_6832_var = tex2D(_node_6832,TRANSFORM_TEX(node_4114, _node_6832));
                float4 _node_8716_var = tex2D(_node_8716,TRANSFORM_TEX(i.uv0, _node_8716));
                float node_7209_if_leA = step(i.vertexColor.a,_node_8716_var.r);
                float node_7209_if_leB = step(_node_8716_var.r,i.vertexColor.a);
                float node_114 = 1.0;
                float node_7209 = lerp((node_7209_if_leA*0.0)+(node_7209_if_leB*node_114),node_114,node_7209_if_leA*node_7209_if_leB);
                float3 emissive = (_node_3381.rgb*((_node_3569*_node_9825_var.r)+((_node_6832_var.r*_node_2384.rgb*_node_4626)*node_7209)));
                float3 finalColor = emissive;
                float2 node_4736 = (i.uv0+(_node_9309_var.r*_node_4666_var.r*0.3));
                float4 _node_4931_var = tex2D(_node_4931,TRANSFORM_TEX(node_4736, _node_4931));
                float node_5313 = (_node_4931_var.a*node_7209);
                fixed4 finalRGBA = fixed4(finalColor,node_5313);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
