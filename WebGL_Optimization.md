# WebGL Optimization Tips

- Keep draw calls low; batch mesh renderers when possible.
- Use compressed textures and disable unnecessary mipmaps.
- Limit real-time lights and prefer baked lighting.
- Enable WebGL multithreaded rendering if supported.
- Use a fixed canvas resolution to avoid costly resizes.
- Avoid heavy physics calculations on the main thread.
