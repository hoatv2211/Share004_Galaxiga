Shader "Spine/Straight Alpha/Skeleton Fill"
{
  Properties
  {
    _FillColor ("FillColor", Color) = (1,1,1,1)
    _FillPhase ("FillPhase", Range(0, 1)) = 0
    [NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      uniform float4 _FillColor;
      uniform float _FillPhase;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR :COLOR;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR :COLOR;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1.w = 1;
          tmpvar_1.xyz = in_v.vertex.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_1));
          out_v.xlv_TEXCOORD0 = in_v.texcoord.xy;
          out_v.xlv_COLOR = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 rawColor_1;
          float4 tmpvar_2;
          tmpvar_2 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          rawColor_1 = tmpvar_2;
          float tmpvar_3;
          tmpvar_3 = (rawColor_1.w * in_f.xlv_COLOR.w);
          float4 tmpvar_4;
          tmpvar_4.xyz = lerp(((rawColor_1.xyz * in_f.xlv_COLOR.xyz) * rawColor_1.w), (_FillColor.xyz * tmpvar_3), float3(_FillPhase, _FillPhase, _FillPhase));
          tmpvar_4.w = tmpvar_3;
          out_f.color = tmpvar_4;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Diffuse"
}
