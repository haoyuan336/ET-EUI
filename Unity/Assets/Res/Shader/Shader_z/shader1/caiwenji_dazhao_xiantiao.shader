// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// 注意：手动更改此数据可能会妨碍您在 Shader Forge 中打开它
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2274,x:32719,y:32712,varname:node_2274,prsc:2|emission-9179-OUT,alpha-8525-OUT;n:type:ShaderForge.SFN_Tex2d,id:6562,x:31869,y:33056,ptovrint:False,ptlb:node_6562,ptin:_node_6562,varname:node_6562,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1665b34f6c4ea05449c312f415d367cd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:857,x:31194,y:32676,varname:node_857,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:8293,x:31545,y:32441,varname:node_8293,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8190,x:31850,y:32469,varname:node_8190,prsc:2,spu:0,spv:1|UVIN-8293-UVOUT,DIST-6832-OUT;n:type:ShaderForge.SFN_Multiply,id:1304,x:32322,y:32718,varname:node_1304,prsc:2|A-4928-OUT,B-857-RGB;n:type:ShaderForge.SFN_Tex2d,id:3526,x:32098,y:32415,ptovrint:False,ptlb:node_3526,ptin:_node_3526,varname:node_3526,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8bf46f1f9832b374dab55ce86fb8dde8,ntxv:0,isnm:False|UVIN-8190-UVOUT;n:type:ShaderForge.SFN_Power,id:4928,x:32308,y:32432,varname:node_4928,prsc:2|VAL-3526-RGB,EXP-4754-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4754,x:32111,y:32599,ptovrint:False,ptlb:power,ptin:_power,varname:node_4754,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:9268,x:32184,y:32913,ptovrint:False,ptlb:beishu,ptin:_beishu,varname:node_9268,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:6832,x:31657,y:32769,varname:node_6832,prsc:2|A-8894-OUT,B-6937-OUT;n:type:ShaderForge.SFN_Vector1,id:6937,x:31496,y:32940,varname:node_6937,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:8670,x:31260,y:32915,varname:node_8670,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:8894,x:31432,y:32801,varname:node_8894,prsc:2|A-857-A,B-8670-OUT;n:type:ShaderForge.SFN_Multiply,id:8525,x:32453,y:32988,varname:node_8525,prsc:2|A-3526-A,B-6562-R;n:type:ShaderForge.SFN_Multiply,id:9179,x:32534,y:32826,varname:node_9179,prsc:2|A-1304-OUT,B-9268-OUT;proporder:6562-3526-4754-9268;pass:END;sub:END;*/

Shader "Custom/caiwenji_dazhao_xiantiao" {
    Properties {
        _node_6562 ("node_6562", 2D) = "white" {}
        _node_3526 ("node_3526", 2D) = "white" {}
        _power ("power", Float ) = 1
        _beishu ("beishu", Float ) = 1
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
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _node_6562; uniform float4 _node_6562_ST;
            uniform sampler2D _node_3526; uniform float4 _node_3526_ST;
            uniform float _power;
            uniform float _beishu;
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
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float2 node_8190 = (i.uv0+((i.vertexColor.a*2.0)+(-1.0))*float2(0,1));
                float4 _node_3526_var = tex2D(_node_3526,TRANSFORM_TEX(node_8190, _node_3526));
                float3 emissive = ((pow(_node_3526_var.rgb,_power)*i.vertexColor.rgb)*_beishu);
                float3 finalColor = emissive;
                float4 _node_6562_var = tex2D(_node_6562,TRANSFORM_TEX(i.uv0, _node_6562));
                fixed4 finalRGBA = fixed4(finalColor,(_node_3526_var.a*_node_6562_var.r));
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
