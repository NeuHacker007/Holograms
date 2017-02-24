Shader "Custom/Post Outline"
{
	Properties
	{
		//Graphics.Blit() sets the "_MainTex" property to the texture passed in
		_MainTex("Main Texture",2D) = "black"{}
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM

		sampler2D _MainTex;
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uvs : TEXCOORD0;
	};

	v2f vert(appdata_base v)
	{
		v2f o;

		//Despite the fact that we are only drawing a quad to the screen, Unity requires us to multiply vertices by our MVP matrix, presumably to keep things working when inexperienced people try copying code from other shaders.
		o.pos = mul(UNITY_MATRIX_MVP,v.vertex);

		//Also, we need to fix the UVs to match our screen space coordinates. There is a Unity define for this that should normally be used.
		o.uvs = o.pos.xy / 2 + 0.5;

		return o;
	}


	half4 frag(v2f i) : COLOR
	{
		//return the texture we just looked up
		return tex2D(_MainTex,i.uvs.xy);
	}

		ENDCG

	}
		//end pass        
	}
		//end subshader
}
//end shader