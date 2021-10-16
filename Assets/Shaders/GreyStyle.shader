Shader "Custom/GreyStyle"
{
    Properties
    {
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Brightness("Brightness", Float) = 1
		_Saturation("Saturation", Float) = 1
		_Contrast("Contrast", Float) = 1
		_forward("forwarddir",vector) = (0,0,1,1)
		_angle("RangeAngle",Range(0,180)) = 60
    }
    SubShader
    {
		//用于避免Dynamic Patching
		Pass
		{
			ZTest Off
			ZWrite Off
			ColorMask 0
		}

        Pass
        {
			Tags { "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

			uniform float4 _circlePos;
			uniform float _insidecircleRadius;
			uniform float _outercircleRadius;


			sampler2D _MainTex;
			half _Brightness;
			half _Saturation;
			half _Contrast;

			float _angle;
			float4 _forward;
			
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float4 worldpos : TEXCOORD0;//世界空间坐标
				float2 uv:TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				float4 worldpos = mul(unity_ObjectToWorld, v.vertex);
				o.worldpos = worldpos;
				o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 insidecirclepos = float2(_circlePos.x,_circlePos.z);
				float2 worldpos = float2(i.worldpos.x, i.worldpos.z);
				float2 forwarddir = float2(_forward.x, _forward.z);
				float2 insidedisvec = insidecirclepos - worldpos;
				float insidedis = length(insidedisvec);
				float ang = acos(dot(normalize(-insidedisvec), normalize(forwarddir))) * 180 / 3.1415926;

				//视野范围外为灰度图
				fixed4 renderTex = tex2D(_MainTex, i.uv);

				//(rgb->Y)
				//fixed r = renderTex.r;
				//fixed g = renderTex.g;
				//fixed b = renderTex.b;
				//fixed y= (r * 0.299f) + (g * 0.587f) + (b * 0.114f);


				//// Apply brightness
				fixed3 finalColor = renderTex.rgb * _Brightness;

				// Apply saturation
				fixed luminance = 0.2125 * renderTex.r + 0.7154 * renderTex.g + 0.0721 * renderTex.b;
				fixed3 luminanceColor = fixed3(luminance, luminance, luminance);
				finalColor = lerp(luminanceColor, finalColor, _Saturation);

				// Apply contrast
				fixed3 avgColor = fixed3(0.5, 0.5, 0.5);
				finalColor = lerp(avgColor, finalColor, _Contrast);



				//视野范围内保持原来色彩
				if (insidedis < _insidecircleRadius)
				{
					return 0;
				}
				renderTex.a = saturate((insidedis - _insidecircleRadius) / (_outercircleRadius - _insidecircleRadius))*0.8f;
                return fixed4(finalColor, renderTex.a);
            }
            ENDCG
        }
    }
}
