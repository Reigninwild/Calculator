Shader "plants" {
 Properties {
     _Color ("Main Color", Color) = (1,1,1,1)
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
     _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
 }
 
 SubShader {
     Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
     LOD 200
     Cull Off
     
 CGPROGRAM
 #pragma multi_compile _ LOD_FADE_CROSSFADE
 #pragma surface surf Lambert alphatest:_Cutoff
 
 sampler2D _MainTex;
 fixed4 _Color;
 
 struct Input {
     float2 uv_MainTex;
 };
 
 void surf (Input IN, inout SurfaceOutput o) {
     fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
     o.Albedo = c.rgb;
	 
	 #ifdef LOD_FADE_CROSSFADE
            o.Alpha = c.a * unity_LODFade.x;
	 #else

     o.Alpha = c.a;
	#endif
 }
 ENDCG
 }
 

FallBack "Transparent/Cutout/VertexLit"
}