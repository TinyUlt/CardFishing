// - Unlit


Shader "SelfShader/FishWithWave" {
Properties {
	_MainTex ("Base layer (RGB)", 2D) = "white" {}
	_DetailTex ("layer (RGB)", 2D) = "white" {}
	_DetailColor ("layer Color", Color) = (1,1,1,1)
	_ModelUpVector("model Up vector",vector) = (0,1,0)

	_ScrollX ("layer Scroll speed X", Float) = 1
	_ScrollY ("layer Scroll speed Y", Float) = 0.0

	_DetailScale("Detail Scale", Float) = 0.5

	
	_MMultiplier ("Layer Multiplier", Float) = 1.0

	_RimColor("Rim Color",Color) = (0,0,0,0)
}

	
SubShader {
	Tags { "IgnoreProjector"="True" "RenderType"="Opaque" }
	
	//Blend SrcAlpha OneMinusSrcAlpha
	//Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	LOD 100
	
	
	
	CGINCLUDE
	#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
	#pragma exclude_renderers molehill
	#include "UnityCG.cginc"
	sampler2D _MainTex;
	sampler2D _DetailTex;

	float4 _MainTex_ST;
	float4 _DetailTex_ST;

	half _ScrollX;
	half _ScrollY;
	half _MMultiplier;
	half _DetailScale;

	half3 _ModelUpVector;
	fixed4 _DetailColor;

	fixed4 _RimColor;
	
	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0;
		fixed4 color : TEXCOORD1;
		fixed percent : TEXCOORD2;
	};

	
	v2f vert (appdata_full v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv.xy = TRANSFORM_TEX(v.texcoord.xy,_MainTex);
		half4 worldPos = mul(unity_ObjectToWorld, v.vertex);
		o.uv.zw = (worldPos.xz * _DetailScale + half2(_ScrollX, _ScrollY) * _Time);//frac(worldPos.xz );
		
		o.color = _MMultiplier.xxxx;
		o.percent = clamp(dot(_ModelUpVector,UnityObjectToWorldNormal(v.normal) - 0.1f),0,1);
		//o.percent = clamp(dot(_ModelUpVector,v.normal),0,1);
		return o;
	}
	ENDCG


	Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest		
		fixed4 frag (v2f i) : COLOR
		{
			fixed4 o;
			fixed4 tex = tex2D (_MainTex, i.uv.xy);
			fixed4 tex2 = tex2D (_DetailTex, i.uv.zw);
			o = tex + (tex2 * _DetailColor * i.percent * _MMultiplier) + _RimColor;

			o.a = 1;
			return o;
		}
		ENDCG 
	}
  }
}
