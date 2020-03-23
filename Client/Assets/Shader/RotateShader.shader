// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/RotateShader" {

Properties{

			_Color("Color", Color) = (1, 1, 1, 1)

			_MainTex("Main Texture", 2D) = "white"{}

			_RSpeed("Rotate Speed", Range(1, 100)) = 10

	}

SubShader{
			tags{"Queue" = "Transparent"
			"RenderType" = "Transparent" 
			"IgnoreProjector" = "True"}

		Blend SrcAlpha OneMinusSrcAlpha

		Pass{

		Name "RotateShader"

		Cull off

		CGPROGRAM

		#pragma vertex vert

		#pragma fragment frag

		#include "UnityCG.cginc"

		float4 _Color;

		sampler2D _MainTex;

		float _RSpeed;

		struct v2f{

		float4 pos : POSITION;

		float4 uv : TEXCOORD0;

		};

v2f vert (appdata_base v){

v2f o;

o.pos = UnityObjectToClipPos(v.vertex);

o.uv = v.texcoord;

return o;

}

half4 frag(v2f i) : COLOR{

float2 uv = i.uv.xy - float2(0.5, 0.5);

uv = float2(uv.x * cos(_RSpeed * _Time.x) - uv.y * sin(_RSpeed * _Time.x), 

uv.x * sin(_RSpeed * _Time.x) + uv.y * cos(_RSpeed * _Time.x));

uv += float2(0.5, 0.5);

half4 c = tex2D(_MainTex, uv) * _Color;

return c;

}

ENDCG

}

}

}