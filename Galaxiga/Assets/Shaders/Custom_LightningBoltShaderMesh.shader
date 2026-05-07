Shader "Custom/LightningBoltShaderMesh"
{
  Properties
  {
    _MainTex ("Main Texture (RGBA)", 2D) = "white" {}
    _GlowTex ("Glow Texture (RGBA)", 2D) = "blue" {}
    _TintColor ("Tint Color (RGB)", Color) = (1,1,1,1)
    _GlowTintColor ("Glow Tint Color (RGB)", Color) = (1,1,1,1)
    _InvFade ("Soft Particles Factor", Range(0.01, 100)) = 1
    _JitterMultiplier ("Jitter Multiplier (Float)", float) = 0
    _Turbulence ("Turbulence (Float)", float) = 0
    _TurbulenceVelocity ("Turbulence Velocity (Vector)", Vector) = (0,0,0,0)
    _SrcBlendMode ("SrcBlendMode (Source Blend Mode)", float) = 5
    _DstBlendMode ("DstBlendMode (Destination Blend Mode)", float) = 1
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "LIGHTMODE" = "ALWAYS"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent+10"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: GlowPass
    {
      Name "GlowPass"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "ALWAYS"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent+10"
        "RenderType" = "Transparent"
      }
      LOD 400
      ZWrite Off
      Cull Off
      Blend Zero Zero
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightningTime;
      uniform float _JitterMultiplier;
      uniform float _Turbulence;
      uniform float4 _TurbulenceVelocity;
      uniform float4 _GlowTintColor;
      uniform sampler2D _GlowTex;
      struct appdata_t
      {
          float4 tangent :TANGENT;
          float4 vertex :POSITION;
          float4 color :COLOR;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float2 tangent_1;
          float4 tmpvar_2;
          float tmpvar_3;
          tmpvar_3 = abs(in_v.tangent.w);
          float tmpvar_4;
          float tmpvar_5;
          tmpvar_5 = (_LightningTime.y - in_v.texcoord1.x);
          tmpvar_4 = (tmpvar_5 / (in_v.texcoord1.w - in_v.texcoord1.x));
          float tmpvar_6;
          tmpvar_6 = (in_v.texcoord.z * (tmpvar_3 + tmpvar_3));
          float2 tmpvar_7;
          tmpvar_7 = (((_TurbulenceVelocity * float4(tmpvar_4, tmpvar_4, tmpvar_4, tmpvar_4)).xz + normalize(in_v.tangent.xz)) * ((_Turbulence / max(0.5, tmpvar_3)) * tmpvar_4));
          float4 tmpvar_8;
          tmpvar_8.yw = float2(0, 0);
          tmpvar_8.x = tmpvar_7.x;
          tmpvar_8.z = tmpvar_7.y;
          float2 tmpvar_9;
          float tmpvar_10;
          tmpvar_10 = (in_v.texcoord.x - 0.5);
          tmpvar_9 = (((normalize(in_v.normal.xz) * (tmpvar_10 + tmpvar_10)) * tmpvar_6) * 1.5);
          float4 tmpvar_11;
          tmpvar_11.yw = float2(0, 0);
          tmpvar_11.x = tmpvar_9.x;
          tmpvar_11.z = tmpvar_9.y;
          float2 tmpvar_12;
          tmpvar_12.x = (-in_v.normal.z);
          tmpvar_12.y = in_v.normal.x;
          tangent_1 = (((normalize(tmpvar_12) * tmpvar_6) * (in_v.tangent.w / tmpvar_3)) * (1 + ((frac((sin(dot((_LightningTime.xyz * in_v.vertex.xyz), float3(12.9898, 78.233, 45.5432))) * 43758.55)) * _JitterMultiplier) * 0.05)));
          float4 tmpvar_13;
          tmpvar_13.yw = float2(0, 0);
          tmpvar_13.x = tangent_1.x;
          tmpvar_13.z = tangent_1.y;
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          tmpvar_14.xyz = ((in_v.vertex + tmpvar_11) + (tmpvar_13 + tmpvar_8)).xyz;
          float tmpvar_15;
          tmpvar_15 = float((_LightningTime.y<in_v.texcoord1.y));
          float tmpvar_16;
          tmpvar_16 = clamp(((tmpvar_15 * clamp((tmpvar_5 / max(1E-05, ((1E-05 + in_v.texcoord1.y) - in_v.texcoord1.x))), 0, 1)) + ((1 - tmpvar_15) * (1 - clamp(((_LightningTime.y - in_v.texcoord1.z) / max(1E-05, (in_v.texcoord1.w - in_v.texcoord1.z))), 0, 1)))), 0, 1);
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = in_v.color.xyz;
          tmpvar_2 = ((tmpvar_16 * _GlowTintColor) * tmpvar_17);
          tmpvar_2.w = (tmpvar_2.w * in_v.texcoord.w);
          out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
          out_v.xlv_COLOR0 = tmpvar_2;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_14));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          tmpvar_1 = (tex2D(_GlowTex, in_f.xlv_TEXCOORD0) * in_f.xlv_COLOR0);
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: LinePass
    {
      Name "LinePass"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "ALWAYS"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent+10"
        "RenderType" = "Transparent"
      }
      LOD 100
      ZWrite Off
      Cull Off
      Blend Zero Zero
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightningTime;
      uniform float _JitterMultiplier;
      uniform float _Turbulence;
      uniform float4 _TurbulenceVelocity;
      uniform float4 _TintColor;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 tangent :TANGENT;
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float2 tangent_1;
          float4 tmpvar_2;
          float tmpvar_3;
          float tmpvar_4;
          tmpvar_4 = (_LightningTime.y - in_v.texcoord1.x);
          tmpvar_3 = (tmpvar_4 / (in_v.texcoord1.w - in_v.texcoord1.x));
          float2 tmpvar_5;
          tmpvar_5 = (((_TurbulenceVelocity * float4(tmpvar_3, tmpvar_3, tmpvar_3, tmpvar_3)).xz + normalize(in_v.tangent.xz)) * ((_Turbulence / max(0.5, abs(in_v.tangent.w))) * tmpvar_3));
          float4 tmpvar_6;
          tmpvar_6.yw = float2(0, 0);
          tmpvar_6.x = tmpvar_5.x;
          tmpvar_6.z = tmpvar_5.y;
          float2 tmpvar_7;
          tmpvar_7.x = (-in_v.tangent.z);
          tmpvar_7.y = in_v.tangent.x;
          tangent_1 = (normalize(tmpvar_7) * in_v.tangent.w);
          float4 tmpvar_8;
          tmpvar_8.yw = float2(0, 0);
          tmpvar_8.x = tangent_1.x;
          tmpvar_8.z = tangent_1.y;
          float4 tmpvar_9;
          tmpvar_9.w = 1;
          tmpvar_9.xyz = ((in_v.vertex + (tmpvar_8 * (1 + (frac((sin(dot((_LightningTime.xyz * in_v.vertex.xyz), float3(12.9898, 78.233, 45.5432))) * 43758.55)) * _JitterMultiplier)))) + tmpvar_6).xyz;
          float tmpvar_10;
          tmpvar_10 = float((_LightningTime.y<in_v.texcoord1.y));
          float tmpvar_11;
          tmpvar_11 = clamp(((tmpvar_10 * clamp((tmpvar_4 / max(1E-05, ((1E-05 + in_v.texcoord1.y) - in_v.texcoord1.x))), 0, 1)) + ((1 - tmpvar_10) * (1 - clamp(((_LightningTime.y - in_v.texcoord1.z) / max(1E-05, (in_v.texcoord1.w - in_v.texcoord1.z))), 0, 1)))), 0, 1);
          float4 tmpvar_12;
          tmpvar_12.w = 1;
          tmpvar_12.xyz = in_v.color.xyz;
          tmpvar_2 = ((tmpvar_11 * _TintColor) * tmpvar_12);
          tmpvar_2.w = (tmpvar_2.w * (in_v.color.w * 10));
          out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
          out_v.xlv_COLOR0 = tmpvar_2;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_9));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          tmpvar_1 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) * in_f.xlv_COLOR0);
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
