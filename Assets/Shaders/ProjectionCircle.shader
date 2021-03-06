// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/ProjectionCircle"
{
	Properties
	{
		_circleRadius("circleRadius",float) = 0
		_selfcirclePos("circlePos",vector) = (0,0,0,1)
		_Color("CircleColor", Color) = (1,1,1,1)
		_Width("CircleWidth", Range(3,10)) = 5
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

			float4 _selfcirclePos;
			float _circleRadius;
			float4 _Color;
			float _Width;

			//uniform float4 _circlePos;
			//uniform float _insidecircleRadius;
			//uniform float _outercircleRadius;


            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;//剪裁空间坐标
				float4 worldpos : TEXCOORD0;//世界空间坐标
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				float4 worldpos = mul(unity_ObjectToWorld, v.vertex);
				o.worldpos = worldpos;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 circlepos = float2(_selfcirclePos.x,_selfcirclePos.z);
				float2 worldpos = float2(i.worldpos.x, i.worldpos.z);
				float2 disvec = circlepos - worldpos;
				float dis = length(disvec);
				float mindistance = _circleRadius - _circleRadius / _Width;

				//float2 insidecirclepos = float2(_circlePos.x, _circlePos.z);
				//float2 insidedisvec = insidecirclepos - worldpos;
				//float insidedis = length(insidedisvec);

				//float alpha = step(insidedis / _insidecircleRadius, 1);

				if (_circleRadius==0 || dis > _circleRadius || dis < mindistance)
				{
					return 0;
				}
				_Color.a = (dis - mindistance) / (_circleRadius - mindistance) * 0.8 ;
				return _Color;
            }
            ENDCG
        }
    }
}
