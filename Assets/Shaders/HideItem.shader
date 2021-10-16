Shader "Custom/HideItem"
{
    Properties
    {
		_Color("Color Tint", Color) = (1, 1, 1, 1)
		_MainTex("Main Tex", 2D) = "white" {}
		_Ramp("Ramp Texture", 2D) = "white" {}
		_Outline("Outline", Range(0, 1)) = 0.1
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_Specular("Specular", Color) = (1, 1, 1, 1)
		_SpecularScale("Specular Scale", Range(0, 0.1)) = 0.01
    }
    SubShader
    {
        Pass
        {
			Tags { "RenderType" = "Opque" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"


			uniform float4 _circlePos;
			uniform float _insidecircleRadius;
			uniform float _outercircleRadius;

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _Ramp;
			fixed4 _Specular;
			fixed _SpecularScale;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
            };

            struct v2f
            {
				float4 worldpos : TEXCOORD0;//世界空间坐标
                float2 uv : TEXCOORD1;
                float4 vertex : SV_POSITION;
				float3 worldNormal : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				float4 worldpos = mul(unity_ObjectToWorld, v.vertex);
				o.worldpos = worldpos;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				//Deside alpha
				float2 insidecirclepos = float2(_circlePos.x,_circlePos.z);
				float2 worldpos = float2(i.worldpos.x, i.worldpos.z);
				float2 insidedisvec = insidecirclepos - worldpos;
				float insidedis = length(insidedisvec);

				float alpha = step(insidedis / _insidecircleRadius,1);

				if (alpha == 0)
				{
					discard;
				}

				//Toon Shading
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldpos));
				fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldpos));
				fixed3 worldHalfDir = normalize(worldLightDir + worldViewDir);

				fixed4 c = tex2D(_MainTex, i.uv);
				fixed3 albedo = c.rgb * _Color.rgb;

				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;

				UNITY_LIGHT_ATTENUATION(atten, i, i.worldpos);

				fixed diff = dot(worldNormal, worldLightDir);
				diff = (diff * 0.5 + 0.5) * atten;

				fixed3 diffuse = _LightColor0.rgb * albedo * tex2D(_Ramp, float2(diff, diff)).rgb;

				fixed spec = dot(worldNormal, worldHalfDir);
				fixed w = fwidth(spec) * 2.0;
				fixed3 specular = _Specular.rgb * lerp(0, 1, smoothstep(-w, w, spec + _SpecularScale - 1)) * step(0.0001, _SpecularScale);

				fixed3 finalcolor = ambient + diffuse + specular;

				return fixed4(finalcolor, 1);
            }
            ENDCG
        }
    }
}
