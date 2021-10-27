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

		//扭曲程度
		_BumpAmt("Distortion", Float) = 10

		//接触边缘
		_InvFade("Soft Particles Factor", Range(0,10)) = 1.0

		//速度
		_WaveSpeed("speed",Range(0,5)) = 0.5

		//uv旋转角度
		_RotateAng("RotateAng", Range(-4,4))=4

		//是否开启反射
		_reflecton("reflecton",Int)=1


		//消融颜色
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
			//此处因为两层叠加故需要每次重新获取帧缓冲图
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
				//计算顶点在当前屏幕中的位置
				o.projPos = ComputeScreenPos(o.vertex);//裁剪空间坐标到屏幕空间 需除以w
				//因为_CameraDepthTexture中不记录透明物体的深度，所以用COMPUTE_EYEDEPTH(o.projPos.z)来计算屏幕位置对应的深度。
				//在UnityCG.cginc 里   #define COMPUTE_EYEDEPTH(o) o = -UnityObjectToViewPos( v.vertex ).z
				COMPUTE_EYEDEPTH(o.projPos.z);


				o.color = v.color;
				o.uvbump = TRANSFORM_TEX(v.uv, _BumpMap);
				o.uvmain = TRANSFORM_TEX(v.uv, _MainTex);
				o.uvmirror = TRANSFORM_TEX(v.uv, _MirrorTex);
				o.uvmirror.x = 1 - o.uvmirror.x;
				o.uvBurnMap= TRANSFORM_TEX(v.uv, _BurnMap);

				o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldnormal = UnityObjectToWorldNormal(v.normal);


				//计算顶点在抓取到的纹理中的位置
				//o.uvgrab = ComputeGrabScreenPos(o.vertex);


				//修改ComputeGrabScreenPos函数使得镜子后呈现扭曲效果
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
				//边缘高亮部分
				//sceneZ是获取到深度纹理中的深度，但是这个深度不包括透明物体的。partZ才是该透明物体（就是能量罩）上每一个点的深度。
				//SAMPLE_DEPTH_TEXTURE_PROJ是对深度纹理进行采样 第二个参数通常使用顶点着色器输出插值而得的屏幕坐标
				//UNITY_PROJ_COORD：given a 4-component vector, return a texture coordinate suitable for projected texture reads. On most platforms this returns the given value directly.
				//LinearEyeDepth让深度值线性化
				float sceneZ = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				float partZ = i.projPos.z;
				float fade = 1 - saturate(_InvFade * (sceneZ - partZ));
				if (_InvFade < 0.001) fade = 0;


				float2 speed = _Time.xy*_WaveSpeed;


				//折射部分
				half2 bump = UnpackNormal(tex2D(_BumpMap, i.uvbump+speed)).rg;
				float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
				i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;//sahder 入门精要15章水波 偏移量和z相乘模拟深度越大，折射越大
				fixed4 refractcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab))* i.color;


				//基础纹理
				i.uvmain += offset;
				fixed4 tex = tex2D(_MainTex, i.uvmain) * i.color;


				//反射部分
				//旋转45度旋转矩阵 UV原点移动到UV中心点
				i.uvmirror -= float2(0.5, 0.5);
				i.uvmirror = float2(i.uvmirror.x*cos(Pi / _RotateAng) - i.uvmirror.y*sin(Pi / _RotateAng),
					i.uvmirror.y*cos(Pi / _RotateAng) + i.uvmirror.x*sin(Pi / _RotateAng));
				i.uvmirror += float2(0.5, 0.5);
				i.uvmirror += offset;
				fixed4 reflectcol = tex2D(_MirrorTex, i.uvmirror)* i.color;

				//菲涅尔
				fixed3 worldnormal = normalize(-i.worldnormal);
				fixed3 viewdir = normalize(UnityWorldSpaceViewDir(i.worldpos));
				fixed fresnel = 1 - saturate(dot(viewdir, worldnormal));

				//消融效果
				fixed burn = tex2D(_BurnMap, i.uvBurnMap).r;

				fixed t = 1 - smoothstep(0.0, _LineWidth, burn - _BurnAmount);
				fixed4 burncolor = lerp(_BurnFirstColor, _BurnSecondColor, t);
				burncolor = pow(burncolor, 5);

				fixed4 insidecolor=tex2D(_InsideTex, i.uvmirror);
				reflectcol = lerp(insidecolor, lerp(reflectcol, burncolor, t*step(0.0001, _BurnAmount)), step(_BurnAmount,burn));
				
				if (reflectcol.a <= 0.8f || _reflecton == 0) fresnel = 1;//反射贴图为纯透明时反射系数为0

				fixed4 emission = refractcol* fresnel + reflectcol * (1 - fresnel) + tex * _TintColor + fade * _TintColor ;
				emission.a = _TintColor.a * i.color.a;
				return emission;
            }
            ENDCG
        }
    }
}
