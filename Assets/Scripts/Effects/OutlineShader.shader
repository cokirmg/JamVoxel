Shader "Sprites/Outline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Float) = 0.1
    }
    
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        
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
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineWidth;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Sample the texture in 4 directions
                fixed leftPixel = tex2D(_MainTex, i.uv + float2(-_OutlineWidth * _MainTex_TexelSize.x, 0)).a;
                fixed upPixel = tex2D(_MainTex, i.uv + float2(0, _OutlineWidth * _MainTex_TexelSize.y)).a;
                fixed rightPixel = tex2D(_MainTex, i.uv + float2(_OutlineWidth * _MainTex_TexelSize.x, 0)).a;
                fixed bottomPixel = tex2D(_MainTex, i.uv + float2(0, -_OutlineWidth * _MainTex_TexelSize.y)).a;
                
                // If this pixel is transparent but at least one neighbor is not, draw outline
                if (col.a < 0.1 && (leftPixel > 0.1 || upPixel > 0.1 || rightPixel > 0.1 || bottomPixel > 0.1))
                {
                    return _OutlineColor;
                }
                
                return col;
            }
            ENDCG
        }
    }
    
    Fallback "Sprites/Default"
}
