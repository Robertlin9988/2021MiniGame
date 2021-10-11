Shader "Custom/AttackRanges"
{
	Properties
	{
		//_DataTexture("DataTexture",2D) = "black"{}
		_pointnum("pointnum",Int) = 0
		_Color("CircleColor", Color) = (1,1,1,1)
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


			//d3d11 只有HLSL支持数组
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			
			int _pointnum;
			float4 _circlePos[3];
			float _circleRadius[3];
			float4 _Color;
			float _angle[3];
			float4 _forward[3];
			//sampler2D _DataTexture;


			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;//剪裁空间坐标
				float4 worldpos : TEXCOORD0;//世界空间坐标
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float4 worldpos = mul(unity_ObjectToWorld, v.vertex);
				o.worldpos = worldpos;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 worldpos = float2(i.worldpos.x, i.worldpos.z);
				for (int i = 0; i < _pointnum; i++)
				{
					float2 circlepos = float2(_circlePos[i].x, _circlePos[i].z);
					float2 forwarddir = float2(_forward[i].x, _forward[i].z);
					float2 disvec = circlepos - worldpos;
					float dis = length(disvec);
					float ang = acos(dot(normalize(-disvec), normalize(forwarddir))) * 180 / 3.1415926;
					if (_circleRadius[i] == 0 || ang > _angle[i] / 2 || dis > _circleRadius[i])
					{
						continue;
					}
					_Color.a = dis / _circleRadius[i] * 0.5;
					return _Color;

					////必须用lod否则循环无法展开报错
					//float4 tmp0= tex2Dlod(_DataTexture, float4(0.25, i*1.5f/ _pointnum,  0,0));
					//float4 tmp1= tex2Dlod(_DataTexture, float4(0.75, i*1.5f / _pointnum,  0,0));
					//float radius = tmp0.x;
					//float2 circlepos = tmp0.yz;
					//float rangeang = tmp0.w;
					//float2 forwarddir = tmp1.xy;
					//float2 disvec = circlepos - worldpos;
					//float dis = length(disvec);
					//float ang = acos(dot(normalize(-disvec), normalize(forwarddir))) * 180 / 3.1415926;
					//if (radius == 0 || ang > rangeang / 2 || dis > radius)
					//{
					//	continue;
					//}
					//_Color.a = dis / radius * 0.5;
					//return _Color;
				}
				return 0;
			}
			ENDHLSL
		}
	}
}