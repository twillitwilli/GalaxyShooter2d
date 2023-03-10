// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31474,y:32324,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:290a6beb3ff16444581b671bf40ab446,ntxv:0,isnm:False|UVIN-5912-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-2400-OUT,B-2053-RGB,C-8175-OUT,D-2053-A;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32129,y:32859,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8175,x:32208,y:33022,ptovrint:False,ptlb:emissivePower,ptin:_emissivePower,varname:node_8175,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:320,x:31826,y:32382,varname:node_320,prsc:2|VAL-6074-RGB,EXP-102-OUT;n:type:ShaderForge.SFN_ValueProperty,id:102,x:31569,y:32539,ptovrint:False,ptlb:texDensity,ptin:_texDensity,varname:node_102,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Power,id:1821,x:32031,y:32583,varname:node_1821,prsc:2|VAL-6074-A,EXP-596-OUT;n:type:ShaderForge.SFN_ValueProperty,id:596,x:31715,y:32675,ptovrint:False,ptlb:alphaDensity,ptin:_alphaDensity,varname:node_596,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:2400,x:32216,y:32475,varname:node_2400,prsc:2|A-7192-OUT,B-1821-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:7192,x:31993,y:32279,ptovrint:False,ptlb:useTexColor,ptin:_useTexColor,varname:node_7192,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-3177-OUT,B-320-OUT;n:type:ShaderForge.SFN_Vector1,id:3177,x:31736,y:32274,varname:node_3177,prsc:2,v1:1;n:type:ShaderForge.SFN_TexCoord,id:1272,x:30979,y:32270,varname:node_1272,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:5912,x:31283,y:32310,varname:node_5912,prsc:2|A-1272-UVOUT,B-1685-OUT;n:type:ShaderForge.SFN_ValueProperty,id:254,x:30741,y:32464,ptovrint:False,ptlb:texSpdX,ptin:_texSpdX,varname:node_254,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:9361,x:30757,y:32577,ptovrint:False,ptlb:texSpdY,ptin:_texSpdY,varname:node_9361,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:1987,x:30990,y:32476,varname:node_1987,prsc:2|A-254-OUT,B-9361-OUT;n:type:ShaderForge.SFN_Multiply,id:1685,x:31170,y:32476,varname:node_1685,prsc:2|A-1987-OUT,B-8763-T;n:type:ShaderForge.SFN_Time,id:8763,x:30948,y:32674,varname:node_8763,prsc:2;proporder:6074-8175-102-596-7192-254-9361;pass:END;sub:END;*/

Shader "KY/add_alpha_two" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _emissivePower ("emissivePower", Float ) = 1
        _texDensity ("texDensity", Float ) = 2
        _alphaDensity ("alphaDensity", Float ) = 2
        [MaterialToggle] _useTexColor ("useTexColor", Float ) = 0
        _texSpdX ("texSpdX", Float ) = 0
        _texSpdY ("texSpdY", Float ) = 0
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _emissivePower;
            uniform float _texDensity;
            uniform float _alphaDensity;
            uniform fixed _useTexColor;
            uniform float _texSpdX;
            uniform float _texSpdY;
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
                float4 node_8763 = _Time + _TimeEditor;
                float2 node_5912 = (i.uv0+(float2(_texSpdX,_texSpdY)*node_8763.g));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_5912, _MainTex));
                float3 emissive = ((lerp( 1.0, pow(_MainTex_var.rgb,_texDensity), _useTexColor )*pow(_MainTex_var.a,_alphaDensity))*i.vertexColor.rgb*_emissivePower*i.vertexColor.a);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
