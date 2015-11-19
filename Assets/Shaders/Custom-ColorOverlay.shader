Shader "Custom/Custom-ColorOverlay" {
	Properties {
		_Color ("Color", Color) = (0.094,0.8980,0.4470,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			struct vertexOutput
			{
				float4 pos : POSITION;
			};

			uniform float4 _Color;

			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			fixed4 frag(vertexOutput i) : COLOR{
				return _Color;
				//return float4(0.094,0.8980,0.4470,1);
			}
			ENDCG
		}
		
	} 
	FallBack "Diffuse"
}
