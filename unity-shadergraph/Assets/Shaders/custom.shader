Shader "Custom/URPCompatibleShader" {
    Properties {
        // ...existing properties...
    }
    SubShader {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 200

        Pass {
            // ...existing pass code...
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // ...existing code...
            ENDHLSL
        }
    }
    // ...existing code...
}
