// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7873,x:33068,y:32696,varname:node_7873,prsc:2|emission-9074-OUT,alpha-3871-OUT;n:type:ShaderForge.SFN_Tex2d,id:890,x:31555,y:32982,ptovrint:False,ptlb:node_890,ptin:_node_890,varname:node_890,prsc:2,ntxv:0,isnm:False|UVIN-5951-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1241,x:31555,y:32791,ptovrint:False,ptlb:node_1241,ptin:_node_1241,varname:node_1241,prsc:2,ntxv:0,isnm:False|UVIN-1094-UVOUT;n:type:ShaderForge.SFN_Panner,id:1094,x:31312,y:32757,varname:node_1094,prsc:2,spu:0.2,spv:0.5|UVIN-6114-UVOUT;n:type:ShaderForge.SFN_Panner,id:5951,x:31321,y:33017,varname:node_5951,prsc:2,spu:0.1,spv:0.3|UVIN-8992-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8992,x:31121,y:32993,varname:node_8992,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:6114,x:31085,y:32783,varname:node_6114,prsc:2,uv:0;n:type:ShaderForge.SFN_Fresnel,id:3871,x:32498,y:33122,varname:node_3871,prsc:2|EXP-5535-OUT;n:type:ShaderForge.SFN_Multiply,id:8078,x:32614,y:32689,varname:node_8078,prsc:2|A-3083-R,B-5295-OUT,C-2413-OUT;n:type:ShaderForge.SFN_Add,id:3039,x:31748,y:32953,varname:node_3039,prsc:2|A-1241-R,B-890-R;n:type:ShaderForge.SFN_Tex2d,id:6091,x:32304,y:32941,ptovrint:False,ptlb:node_6091,ptin:_node_6091,varname:node_6091,prsc:2,ntxv:0,isnm:False|UVIN-5352-OUT;n:type:ShaderForge.SFN_TexCoord,id:8191,x:31497,y:33237,varname:node_8191,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:26,x:31681,y:33210,varname:node_26,prsc:2,spu:0,spv:0.2|UVIN-8191-UVOUT;n:type:ShaderForge.SFN_Add,id:5352,x:32128,y:32958,varname:node_5352,prsc:2|A-8953-OUT,B-26-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:8953,x:31920,y:32905,varname:node_8953,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3039-OUT;n:type:ShaderForge.SFN_Color,id:3417,x:32273,y:32502,ptovrint:False,ptlb:node_3417,ptin:_node_3417,varname:node_3417,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:2413,x:32147,y:32762,ptovrint:False,ptlb:node_2413,ptin:_node_2413,varname:node_2413,prsc:2,min:0,cur:0,max:6;n:type:ShaderForge.SFN_Slider,id:5535,x:32075,y:33295,ptovrint:False,ptlb:node_5535,ptin:_node_5535,varname:node_5535,prsc:2,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Tex2d,id:8757,x:31318,y:32338,ptovrint:False,ptlb:node_890_copy,ptin:_node_890_copy,varname:_node_890_copy,prsc:2,ntxv:0,isnm:False|UVIN-6071-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7893,x:31318,y:32147,ptovrint:False,ptlb:node_1241_copy,ptin:_node_1241_copy,varname:_node_1241_copy,prsc:2,ntxv:0,isnm:False|UVIN-5815-UVOUT;n:type:ShaderForge.SFN_Panner,id:5815,x:31075,y:32113,varname:node_5815,prsc:2,spu:0.2,spv:-0.1|UVIN-3516-UVOUT;n:type:ShaderForge.SFN_Panner,id:6071,x:31084,y:32373,varname:node_6071,prsc:2,spu:0.1,spv:-0.2|UVIN-1226-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1226,x:30884,y:32349,varname:node_1226,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:3516,x:30848,y:32139,varname:node_3516,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:3193,x:31511,y:32309,varname:node_3193,prsc:2|A-7893-R,B-8757-R;n:type:ShaderForge.SFN_TexCoord,id:3509,x:31260,y:32593,varname:node_3509,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:3690,x:31444,y:32566,varname:node_3690,prsc:2,spu:0,spv:0.2|UVIN-3509-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:9586,x:31683,y:32261,varname:node_9586,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3193-OUT;n:type:ShaderForge.SFN_Add,id:8197,x:31891,y:32314,varname:node_8197,prsc:2|A-9586-OUT,B-3690-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5962,x:32121,y:32325,ptovrint:False,ptlb:node_6091_copy,ptin:_node_6091_copy,varname:_node_6091_copy,prsc:2,ntxv:0,isnm:False|UVIN-8197-OUT;n:type:ShaderForge.SFN_Multiply,id:5295,x:32027,y:32679,varname:node_5295,prsc:2|A-6845-OUT,B-1353-OUT;n:type:ShaderForge.SFN_Slider,id:7303,x:31901,y:32111,ptovrint:False,ptlb:node_7303,ptin:_node_7303,varname:node_7303,prsc:2,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Multiply,id:6845,x:32364,y:32226,varname:node_6845,prsc:2|A-7303-OUT,B-5962-R;n:type:ShaderForge.SFN_Slider,id:9430,x:31688,y:32835,ptovrint:False,ptlb:node_9430,ptin:_node_9430,varname:node_9430,prsc:2,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Multiply,id:1353,x:32516,y:32854,varname:node_1353,prsc:2|A-9430-OUT,B-6091-R;n:type:ShaderForge.SFN_Tex2d,id:3083,x:32121,y:32556,ptovrint:False,ptlb:node_3083,ptin:_node_3083,varname:node_3083,prsc:2,tex:66371ec7c7947d14286785d76b728bcc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9074,x:32813,y:32629,varname:node_9074,prsc:2|A-3417-RGB,B-8078-OUT;proporder:1241-890-6091-3417-2413-5535-8757-7893-5962-7303-9430-3083;pass:END;sub:END;*/

Shader "Custom/lusu_guangjicangsheng" {
    Properties {
        _node_1241 ("node_1241", 2D) = "white" {}
        _node_890 ("node_890", 2D) = "white" {}
        _node_6091 ("node_6091", 2D) = "white" {}
        _node_3417 ("node_3417", Color) = (0.5,0.5,0.5,1)
        _node_2413 ("node_2413", Range(0, 6)) = 0
        _node_5535 ("node_5535", Range(0, 3)) = 0
        _node_890_copy ("node_890_copy", 2D) = "white" {}
        _node_1241_copy ("node_1241_copy", 2D) = "white" {}
        _node_6091_copy ("node_6091_copy", 2D) = "white" {}
        _node_7303 ("node_7303", Range(0, 3)) = 0
        _node_9430 ("node_9430", Range(0, 3)) = 0
        _node_3083 ("node_3083", 2D) = "white" {}
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
            Name "ForwardBase"
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_890; uniform float4 _node_890_ST;
            uniform sampler2D _node_1241; uniform float4 _node_1241_ST;
            uniform sampler2D _node_6091; uniform float4 _node_6091_ST;
            uniform float4 _node_3417;
            uniform float _node_2413;
            uniform float _node_5535;
            uniform sampler2D _node_890_copy; uniform float4 _node_890_copy_ST;
            uniform sampler2D _node_1241_copy; uniform float4 _node_1241_copy_ST;
            uniform sampler2D _node_6091_copy; uniform float4 _node_6091_copy_ST;
            uniform float _node_7303;
            uniform float _node_9430;
            uniform sampler2D _node_3083; uniform float4 _node_3083_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD3;
                #else
                    float3 shLight : TEXCOORD3;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
////// Lighting:
////// Emissive:
                float4 _node_3083_var = tex2D(_node_3083,TRANSFORM_TEX(i.uv0, _node_3083));
                float4 node_5033 = _Time + _TimeEditor;
                float2 node_5815 = (i.uv0+node_5033.g*float2(0.2,-0.1));
                float4 _node_1241_copy_var = tex2D(_node_1241_copy,TRANSFORM_TEX(node_5815, _node_1241_copy));
                float2 node_6071 = (i.uv0+node_5033.g*float2(0.1,-0.2));
                float4 _node_890_copy_var = tex2D(_node_890_copy,TRANSFORM_TEX(node_6071, _node_890_copy));
                float2 node_8197 = ((_node_1241_copy_var.r+_node_890_copy_var.r).r+(i.uv0+node_5033.g*float2(0,0.2)));
                float4 _node_6091_copy_var = tex2D(_node_6091_copy,TRANSFORM_TEX(node_8197, _node_6091_copy));
                float2 node_1094 = (i.uv0+node_5033.g*float2(0.2,0.5));
                float4 _node_1241_var = tex2D(_node_1241,TRANSFORM_TEX(node_1094, _node_1241));
                float2 node_5951 = (i.uv0+node_5033.g*float2(0.1,0.3));
                float4 _node_890_var = tex2D(_node_890,TRANSFORM_TEX(node_5951, _node_890));
                float2 node_5352 = ((_node_1241_var.r+_node_890_var.r).r+(i.uv0+node_5033.g*float2(0,0.2)));
                float4 _node_6091_var = tex2D(_node_6091,TRANSFORM_TEX(node_5352, _node_6091));
                float3 emissive = (_node_3417.rgb*(_node_3083_var.r*((_node_7303*_node_6091_copy_var.r)*(_node_9430*_node_6091_var.r))*_node_2413));
                float3 finalColor = emissive;
                return fixed4(finalColor,pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_5535));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
