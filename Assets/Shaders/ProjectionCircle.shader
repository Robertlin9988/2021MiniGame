// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/ProjectionCircle"
{
	Properties
	{
		_circleRadius("circleRadius",float) = 0
		_circlePos("circlePos",vector) = (0,0,0,1)
		_Color("CircleColor", Color) = (1,1,1,1)
		_Width("CircleWidth", Range(3,10)) = 5
	}

	SubShader
	{
		//���ڱ���Dynamic Patching
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

			float4 _circlePos;
			float _circleRadius;
			float4 _Color;
			float _Width;


            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;//���ÿռ�����
				float4 worldpos : TEXCOORD0;//����ռ�����
				float3 disdir : TEXCOORD2;//���㵽�⻷���ľ���
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				float4 worldpos = mul(unity_ObjectToWorld, v.vertex);
				o.worldpos = worldpos;
				o.disdir = (_circlePos - worldpos).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 circlepos = float2(_circlePos.x,_circlePos.z);
				float2 worldpos = float2(i.worldpos.x, i.worldpos.z);
				float2 disvec = circlepos - worldpos;
				float dis = length(disvec);
				float mindistance = _circleRadius - _circleRadius / _Width;
				if (_circleRadius==0 || dis > _circleRadius || dis < mindistance)
				{
					return 0;
				}
				_Color.a = (dis - mindistance) / (_circleRadius - mindistance) * 0.8;
				return _Color;
            }
            ENDCG
        }
    }
}