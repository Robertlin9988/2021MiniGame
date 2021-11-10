// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ShadowRange" {
		Properties{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Width("RoundWidth", float) = 0.03
		}
		
		//用于避免Dynamic Patching
		SubShader{
		Pass {
		ZTest Off
		ZWrite Off
		ColorMask 0
		}

		Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		struct v2f {
			float4 pos : SV_POSITION;
			float4 oPos : TEXCOORD1;
		};
		fixed4 _Color;
		int _Width;



		float4 _MainTex_ST;

		v2f vert(appdata_base v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.oPos = v.vertex;
			return o;
		}

		fixed4 frag(v2f i) : COLOR
		{
			float dis = sqrt(i.oPos.x * i.oPos.x + i.oPos.y * i.oPos.y);
			//超过0.5(一半)的不渲染
			if (dis > 0.5) 
			{
				discard;
			}
			else 
			{
			   _Color.a = 0.5f;
			}
			return _Color;
		}
		ENDCG
		}
	}
		FallBack "Diffuse"
}
