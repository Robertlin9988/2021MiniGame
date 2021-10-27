Shader "Unlit/cubemapTest"
{
    Properties
    {
         _CubeMap("Cubmap",CUBE) =""{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertexL:TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            samplerCUBE _CubeMap;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertexL = v.vertex;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = texCUBE(_CubeMap, normalize(i.vertexL.xyz));

                return col;
            }
            ENDCG
        }
    }
}
