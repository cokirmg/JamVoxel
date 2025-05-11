Shader "Custom/TopToBottomLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RevealProgress ("Reveal Progress", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };
            
            sampler2D _MainTex;
            float _RevealProgress;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Get the light texture color
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Calculate the y-position in normalized space for top-to-bottom effect
                // Assuming top is at max y value and bottom is at min y value
                float normalizedY = i.worldPos.y / unity_ObjectToWorld[1][1];
                
                // Apply the top-to-bottom mask based on the reveal progress
                float revealMask = (normalizedY > 1.0 - _RevealProgress) ? 1.0 : 0.0;
                
                // Apply the mask to the alpha
                col.a *= revealMask;
                
                return col;
            }
            ENDCG
        }
    }
}