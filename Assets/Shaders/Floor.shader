Shader "Custom/Floor"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
		[HDR] _Color2("SecondColor",Color)=(1,1,1,1)
		[HDR] _EmissionColor("Emission Color",Color)=(1,1,1,1)
		[HDR] _EmissionColor2("Seccond Emission Color",Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map",2D) = "bump"{}
		_Emission("Emission Mask",2D) = "white"{}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_CenterX("CenterX",Float) = 0
		_CenterY("CenterY",Float) = 0
		_BurnRadius("Burn Radius",Range(0,65)) = 0
		_LineWidth("Burn Line Width",Range(0,2)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _Emission;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_Emission;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
		float _BurnRadius;
		float _LineWidth;
		half _CenterX;
		half _CenterY;
        half4 _Color;
		half4 _Color2;
		half4 _EmissionColor;
		half4 _EmissionColor2;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            half4 c1 = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			half4 c2 = tex2D(_MainTex, IN.uv_MainTex) * _Color2;
			half4 emission1 = tex2D(_Emission, IN.uv_Emission)*_EmissionColor;
			half4 emission2 = tex2D(_Emission, IN.uv_Emission)*_EmissionColor2;

			float2 centerpos = float2(_CenterX, _CenterY);
			float2 worldpos = float2(IN.worldPos.x, IN.worldPos.z);
			float2 disvec = centerpos - worldpos;
			float dis = length(disvec);

			half4 c = lerp(c1, c2, smoothstep(max(0,dis- _LineWidth/2), dis + _LineWidth / 2,_BurnRadius));
			half4 emission = lerp(emission1, emission2, smoothstep(max(0,dis - _LineWidth / 2), dis + _LineWidth / 2, _BurnRadius));

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Emission = emission;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
