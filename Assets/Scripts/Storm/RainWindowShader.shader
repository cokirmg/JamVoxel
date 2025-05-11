Shader "Custom/RainWindowEffect"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _NormalMap ("Rain Normal Map", 2D) = "bump" {}
        _RainIntensity ("Rain Intensity", Range(0.0, 2.0)) = 1.0
        _TimeOffset ("Time Offset", Float) = 0.0
        _RainTint ("Rain Tint", Color) = (0.8,0.8,1,1)
        _DistortionStrength ("Distortion Strength", Range(0.001, 0.1)) = 0.02
        _RainSpeed ("Rain Speed", Range(0.1, 5.0)) = 1.0
    }
    
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        
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
                float4 color : COLOR;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            
            sampler2D _MainTex;
            sampler2D _NormalMap;
            float4 _MainTex_ST;
            float _RainIntensity;
            float _TimeOffset;
            float4 _RainTint;
            float _DistortionStrength;
            float _RainSpeed;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                // Calculate flowing rain UV coordinates
                float2 rainUV = i.uv;
                rainUV.y += _TimeOffset * _RainSpeed * 0.1;
                
                // Add some variation in the X direction as well
                rainUV.x += sin(_TimeOffset * 0.2 + rainUV.y * 10) * 0.01;
                
                // Sample the normal map for rain effect
                float3 normalMap = UnpackNormal(tex2D(_NormalMap, rainUV));
                
                // Calculate distortion based on normal map and intensity
                float2 distortion = normalMap.xy * _DistortionStrength * _RainIntensity;
                
                // Sample the sprite with distortion
                float4 col = tex2D(_MainTex, i.uv + distortion);
                
                // Apply rain tint to create a water-droplet effect
                col.rgb = lerp(col.rgb, col.rgb * _RainTint.rgb, _RainIntensity * 0.3);
                
                // Keep original alpha
                col.a *= i.color.a;
                
                return col;
            }
            ENDCG
        }
    }
    
    Fallback "Sprites/Default"
}