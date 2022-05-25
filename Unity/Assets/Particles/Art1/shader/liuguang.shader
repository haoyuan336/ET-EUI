// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7003,x:32719,y:32712,varname:node_7003,prsc:2|emission-2018-OUT;n:type:ShaderForge.SFN_Tex2d,id:9100,x:31852,y:32366,ptovrint:False,ptlb:zhuwenli,ptin:_zhuwenli,varname:_zhuwenli,prsc:2,tex:515c02dc24751ab4681041424cda3b59,ntxv:0,isnm:False|UVIN-7084-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:897,x:30656,y:32247,ptovrint:False,ptlb:raodong,ptin:_raodong,varname:_raodong,prsc:2,tex:3fde17512687abd40bfc575fa8ed1b6e,ntxv:0,isnm:False|UVIN-6929-UVOUT;n:type:ShaderForge.SFN_Panner,id:6929,x:30454,y:32177,varname:node_6929,prsc:2,spu:0.1,spv:0.1|UVIN-5096-UVOUT,DIST-9160-OUT;n:type:ShaderForge.SFN_Multiply,id:9160,x:30204,y:32328,varname:node_9160,prsc:2|A-8376-OUT,B-4748-T;n:type:ShaderForge.SFN_TexCoord,id:5096,x:30145,y:32047,varname:node_5096,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:8376,x:30045,y:32268,varname:node_8376,prsc:2|A-1875-OUT,B-1716-OUT;n:type:ShaderForge.SFN_Time,id:4748,x:30015,y:32441,varname:node_4748,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:1875,x:29845,y:32213,ptovrint:False,ptlb:uspeed,ptin:_uspeed,varname:_uspeed,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:1716,x:29859,y:32364,ptovrint:False,ptlb:vspeed,ptin:_vspeed,varname:_vspeed,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:1353,x:30913,y:32254,varname:node_1353,prsc:2|A-897-R,B-3470-OUT;n:type:ShaderForge.SFN_Slider,id:3470,x:30706,y:32523,ptovrint:False,ptlb:raodongqiangdu,ptin:_raodongqiangdu,varname:_raodongqiangdu,prsc:2,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Add,id:6353,x:31217,y:32187,varname:node_6353,prsc:2|A-6024-UVOUT,B-1353-OUT;n:type:ShaderForge.SFN_Panner,id:6024,x:30937,y:32020,varname:node_6024,prsc:2,spu:0,spv:0;n:type:ShaderForge.SFN_Panner,id:7084,x:31615,y:32113,varname:node_7084,prsc:2,spu:1,spv:1|UVIN-6353-OUT,DIST-1720-OUT;n:type:ShaderForge.SFN_Multiply,id:1720,x:31464,y:32537,varname:node_1720,prsc:2|A-1813-OUT,B-5529-T;n:type:ShaderForge.SFN_Append,id:1813,x:31305,y:32477,varname:node_1813,prsc:2|A-6507-OUT,B-8313-OUT;n:type:ShaderForge.SFN_Time,id:5529,x:31275,y:32650,varname:node_5529,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:6507,x:31105,y:32422,ptovrint:False,ptlb:uspeed_zhuwenli,ptin:_uspeed_zhuwenli,varname:_uspeed_zhuwenli,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:8313,x:31119,y:32573,ptovrint:False,ptlb:vspeed_zhuwenli,ptin:_vspeed_zhuwenli,varname:_vspeed_zhuwenli,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:9053,x:32328,y:32622,varname:node_9053,prsc:2|A-9100-RGB,B-9789-RGB,C-952-RGB,D-9789-A,E-4568-OUT;n:type:ShaderForge.SFN_VertexColor,id:952,x:31959,y:32738,varname:node_952,prsc:2;n:type:ShaderForge.SFN_Color,id:9789,x:32012,y:32893,ptovrint:False,ptlb:yanse,ptin:_yanse,varname:_yanse,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4568,x:32145,y:32412,varname:node_4568,prsc:2|A-9100-RGB,B-8641-OUT;n:type:ShaderForge.SFN_Slider,id:8641,x:31753,y:32617,ptovrint:False,ptlb:liangdu,ptin:_liangdu,varname:_liangdu,prsc:2,min:0,cur:1,max:20;n:type:ShaderForge.SFN_Multiply,id:2018,x:32522,y:32770,varname:node_2018,prsc:2|A-9053-OUT,B-952-A;proporder:9100-897-1875-1716-3470-6507-8313-9789-8641;pass:END;sub:END;*/

Shader "Shader Forge/liuguang" {
    Properties {
        _zhuwenli ("zhuwenli", 2D) = "white" {}
        _raodong ("raodong", 2D) = "white" {}
        _uspeed ("uspeed", Float ) = 0.1
        _vspeed ("vspeed", Float ) = 0.1
        _raodongqiangdu ("raodongqiangdu", Range(0, 5)) = 1
        _uspeed_zhuwenli ("uspeed_zhuwenli", Float ) = 0.1
        _vspeed_zhuwenli ("vspeed_zhuwenli", Float ) = 0.1
        _yanse ("yanse", Color) = (0.5,0.5,0.5,1)
        _liangdu ("liangdu", Range(0, 20)) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _zhuwenli; uniform float4 _zhuwenli_ST;
            uniform sampler2D _raodong; uniform float4 _raodong_ST;
            uniform float _uspeed;
            uniform float _vspeed;
            uniform float _raodongqiangdu;
            uniform float _uspeed_zhuwenli;
            uniform float _vspeed_zhuwenli;
            uniform float4 _yanse;
            uniform float _liangdu;
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
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_5529 = _Time + _TimeEditor;
                float4 node_2873 = _Time + _TimeEditor;
                float4 node_4748 = _Time + _TimeEditor;
                float2 node_6929 = (i.uv0+(float2(_uspeed,_vspeed)*node_4748.g)*float2(0.1,0.1));
                float4 _raodong_var = tex2D(_raodong,TRANSFORM_TEX(node_6929, _raodong));
                float node_1353 = (_raodong_var.r*_raodongqiangdu);
                float2 node_7084 = (((i.uv0+node_2873.g*float2(0,0))+node_1353)+(float2(_uspeed_zhuwenli,_vspeed_zhuwenli)*node_5529.g)*float2(1,1));
                float4 _zhuwenli_var = tex2D(_zhuwenli,TRANSFORM_TEX(node_7084, _zhuwenli));
                float3 emissive = ((_zhuwenli_var.rgb*_yanse.rgb*i.vertexColor.rgb*_yanse.a*(_zhuwenli_var.rgb*_liangdu))*i.vertexColor.a);
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
