// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/DistortionMirror"
{
    Properties
    {
		[HDR]_TintColor("Tint Color", Color) = (1,1,1,1)

		//Texture
		_MainTex("Base (RGB) Gloss (A)", 2D) = "black" {}
		_MirrorTex("MirrorTexture", 2D) = "white" {}
		_InsideTex("InsideTexture",2D) = "white"{}
		_BumpMap("Normalmap", 2D) = "bump" {}

		//Ť���̶�
		_BumpAmt("Distortion", Float) = 10

		//�Ӵ���Ե
		_InvFade("Soft Particles Factor", Range(0,10)) = 1.0

		//�ٶ�
		_WaveSpeed("speed",Range(0,5)) = 0.5

		//uv��ת�Ƕ�
		_RotateAng("RotateAng", Range(-4,4))=4

		//�Ƿ�������
		_reflecton("reflecton",Int)=1


		//������ɫ
		_BurnAmount("Burn Amount",Range(0,1))=0
		_LineWidth("Burn Line Width",Range(0,2))=0.1
		_BurnMap("Burn Map",2D) = "white"{}
		[HDR]_BurnFirstColor("Burn First Color",Color)=(1,1,1,1)
		[HDR]_BurnSecondColor("Burn Second Color",Color) = (1,1,1,1)
    }
    SubShader
    {
		Tags { "Queue" = "Transparent"  "IgnoreProjector" = "True"  "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZWrite Off
		Lighting Off

		GrabPass 
		{
			//�˴���Ϊ������ӹ���Ҫÿ�����»�ȡ֡����ͼ
			//"_GrabTexture"
		}


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
			#define Pi 3.1415926535897932384626
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
				float3 normal : NORMAL;
            };

            struct v2f
            {
				float4 uvgrab : TEXCOORD0;
				float2 uvbump : TEXCOORD1;
				float2 uvmain : TEXCOORD2;
				float2 uvmirror : TEXCOORD3;
				float2 uvBurnMap : TEXCOORD4;
				float4 projPos : TEXCOORD5;
				float3 worldnormal : TEXCOORD6;
				float3 worldpos : TEXCOORD7;
				fixed4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _BumpMap;
			float4 _BumpMap_ST;
			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			sampler2D _MirrorTex;
			float4 _MirrorTex_ST;
			sampler2D _BurnMap;
			float4 _BurnMap_ST;
			sampler2D _InsideTex;

			float _BurnAmount;
			float _LineWidth;
			fixed4 _BurnFirstColor;
			fixed4 _BurnSecondColor;


			float _BumpAmt;
			fixed4 _TintColor;

			float _WaveSpeed;

			sampler2D _CameraDepthTexture;
			float _InvFade;

			float _RotateAng;
			int _reflecton;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				//���㶥���ڵ�ǰ��Ļ�е�λ��
				o.projPos = ComputeScreenPos(o.vertex);//�ü��ռ����굽��Ļ�ռ� �����w
				//��Ϊ_CameraDepthTexture�в���¼͸���������ȣ�������COMPUTE_EYEDEPTH(o.projPos.z)��������Ļλ�ö�Ӧ����ȡ�
				//��UnityCG.cginc ��   #define COMPUTE_EYEDEPTH(o) o = -UnityObjectToViewPos( v.vertex ).z
				COMPUTE_EYEDEPTH(o.projPos.z);


				o.color = v.color;
				o.uvbump = TRANSFORM_TEX(v.uv, _BumpMap);
				o.uvmain = TRANSFORM_TEX(v.uv, _MainTex);
				o.uvmirror = TRANSFORM_TEX(v.uv, _MirrorTex);
				o.uvmirror.x = 1 - o.uvmirror.x;
				o.uvBurnMap= TRANSFORM_TEX(v.uv, _BurnMap);

				o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldnormal = UnityObjectToWorldNormal(v.normal);


				//���㶥����ץȡ���������е�λ��
				//o.uvgrab = ComputeGrabScreenPos(o.vertex);


				//�޸�ComputeGrabScreenPos����ʹ�þ��Ӻ����Ť��Ч��
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
				o.uvgrab.zw = o.vertex.w;
				#if UNITY_SINGLE_PASS_STEREO
				o.uvgrab.xy = TransformStereoScreenSpaceTex(o.uvgrab.xy, o.uvgrab.w);
				#endif
				o.uvgrab.z /= distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex)) / 10;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				//��Ե��������
				//sceneZ�ǻ�ȡ����������е���ȣ����������Ȳ�����͸������ġ�partZ���Ǹ�͸�����壨���������֣���ÿһ�������ȡ�
				//SAMPLE_DEPTH_TEXTURE_PROJ�Ƕ����������в��� �ڶ�������ͨ��ʹ�ö�����ɫ�������ֵ���õ���Ļ����
				//UNITY_PROJ_COORD��given a 4-component vector, return a texture coordinate suitable for projected texture reads. On most platforms this returns the given value directly.
				//LinearEyeDepth�����ֵ���Ի�
				float sceneZ = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				float partZ = i.projPos.z;
				float fade = 1 - saturate(_InvFade * (sceneZ - partZ));
				if (_InvFade < 0.001) fade = 0;


				float2 speed = _Time.xy*_WaveSpeed;


				//���䲿��
				half2 bump = UnpackNormal(tex2D(_BumpMap, i.uvbump+speed)).rg;
				float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
				i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;//sahder ���ž�Ҫ15��ˮ�� ƫ������z���ģ�����Խ������Խ��
				fixed4 refractcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab))* i.color;


				//��������
				i.uvmain += offset;
				fixed4 tex = tex2D(_MainTex, i.uvmain) * i.color;


				//���䲿��
				//��ת45����ת���� UVԭ���ƶ���UV���ĵ�
				i.uvmirror -= float2(0.5, 0.5);
				i.uvmirror = float2(i.uvmirror.x*cos(Pi / _RotateAng) - i.uvmirror.y*sin(Pi / _RotateAng),
					i.uvmirror.y*cos(Pi / _RotateAng) + i.uvmirror.x*sin(Pi / _RotateAng));
				i.uvmirror += float2(0.5, 0.5);
				i.uvmirror += offset;
				fixed4 reflectcol = tex2D(_MirrorTex, i.uvmirror)* i.color;

				//������
				fixed3 worldnormal = normalize(-i.worldnormal);
				fixed3 viewdir = normalize(UnityWorldSpaceViewDir(i.worldpos));
				fixed fresnel = 1 - saturate(dot(viewdir, worldnormal));

				//����Ч��
				fixed burn = tex2D(_BurnMap, i.uvBurnMap).r;

				fixed t = 1 - smoothstep(0.0, _LineWidth, burn - _BurnAmount);
				fixed4 burncolor = lerp(_BurnFirstColor, _BurnSecondColor, t);
				burncolor = pow(burncolor, 5);

				fixed4 insidecolor=tex2D(_InsideTex, i.uvmirror);
				reflectcol = lerp(insidecolor, lerp(reflectcol, burncolor, t*step(0.0001, _BurnAmount)), step(_BurnAmount,burn));
				
				if (reflectcol.a <= 0.8f || _reflecton == 0) fresnel = 1;//������ͼΪ��͸��ʱ����ϵ��Ϊ0

				fixed4 emission = refractcol* fresnel + reflectcol * (1 - fresnel) + tex * _TintColor + fade * _TintColor ;
				emission.a = _TintColor.a * i.color.a;
				return emission;
            }
            ENDCG
        }
    }
}
