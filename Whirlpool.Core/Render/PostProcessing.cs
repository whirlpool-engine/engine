﻿#define POSTPROCESSING
using OpenTK.Graphics.OpenGL4;
using Whirlpool.Core.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using OpenTK;

namespace Whirlpool.Core.Render
{
    public class PostProcessing : Singleton<PostProcessing>
    {
#if POSTPROCESSING
        int framebuffer, rbo;
        Texture textureBufferTexture;

        int width = 0, height = 0;
#endif
		public void Init(Vector2 windowSize)
        {
            width = (int)windowSize.X;
            height = (int)windowSize.Y;
#if POSTPROCESSING
            //GL.DrawBuffer(DrawBufferMode.None);
#endif
            //GL.Enable(EnableCap.Multisample);            
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DepthClamp);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Fastest);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);
#if POSTPROCESSING
            textureBufferTexture = new Texture()
            {
                name = "fboTexture",
                width = width,
                height = height,
                animated = false
            };
            GL.GenFramebuffers(1, out framebuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out textureBufferTexture.glTexture);
            GL.BindTexture(TextureTarget.Texture2D, textureBufferTexture.glTexture);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.Byte, IntPtr.Zero);

            //GL.GenRenderbuffers(1, out rbo);
            //GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rbo);
            //GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, width, height);
            //GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, rbo);
            // depth buffer doesnt like me :(

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, textureBufferTexture.glTexture, 0);
            DrawBuffersEnum[] drawBuffers = new[] { DrawBuffersEnum.ColorAttachment0 };
            GL.DrawBuffers(1, drawBuffers);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                Console.WriteLine("\n\n\tFRAMEBUFFER ERROR: " + GL.GetError());
#endif
        }

        public void PreRender()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
            GL.Viewport(0, 0, width, height);
        }

        public void PostRender()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, width, height);
            BaseRenderer.RenderFramebuffer(new Vector2(0, 0), new Vector2(width, height), textureBufferTexture);
        }
    }
}
