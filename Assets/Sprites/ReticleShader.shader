Shader "Unlit/ReticleShader"
{
    Properties
    {
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Center ("Center", Vector) = (0.5, 0.5, 0.0, 0.0)
        _Radius ("Radius", Range(0.0, 1.0)) = 0.4
        _Thickness ("Thickness", Range(0.0, 0.5)) = 0.1
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vertex_shader
            #pragma fragment fragment_shader

            struct vertex_in
            {
                float4 position : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct vertex_out
            {
                float4 position : SV_Position;
                float4 uv : TEXCOORD0;
            };

            fixed4 _Color;
            float4 _Center;
            float _Radius;
            float _Thickness;

            vertex_out vertex_shader(vertex_in vin)
            {
                vertex_out vout;
                vout.position = UnityObjectToClipPos(vin.position);
                vout.uv = vin.uv;
                return vout;
            }

            float4 fragment_shader(vertex_out vertex) : SV_Target
            {
                float intensity = 1.0 - abs(distance(_Center.xy, vertex.uv.xy) - _Radius) / _Thickness;
                return fixed4(_Color.rgb, _Color.a * intensity);
            }
            ENDCG
        }
    }
    FallBack "UI/Default"
}
