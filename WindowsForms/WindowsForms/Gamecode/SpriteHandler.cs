using System.Drawing;
using System.Drawing.Imaging;

namespace WindowsForms.Gamecode
{
    internal class SpriteHandler
    {
        private int currentAnimationSprite = 0;
        private int nextAnimationStep = 0;
        private Image[] sprites;

        public SpriteHandler(Image gif)
        {
            sprites = getFrames(gif);
        }
        
        internal Image CurrentSprite
        {
            get
            {
                return sprites[currentAnimationSprite];
            }
        }

        internal void updateSpriteEvery5thTimeCalled()
        {
            if (nextAnimationStep % 5 == 0)
            {
                currentAnimationSprite = (currentAnimationSprite + 1) % sprites.Length;
                nextAnimationStep = 0;
            }
            nextAnimationStep++;
        }

        internal void updateSpriteEvery3thTimeCalled()
        {
            if (nextAnimationStep % 3 == 0)
            {
                currentAnimationSprite = (currentAnimationSprite + 1) % sprites.Length;
                nextAnimationStep = 0;
            }
            nextAnimationStep++;
        }

        internal void updateSpriteEveryTimeCalled()
        {
            currentAnimationSprite = (currentAnimationSprite + 1) % sprites.Length;
        }

        /// <summary>
        /// returns every single image of a gif animation
        /// </summary>
        /// <param name="originalImg"></param>
        /// <returns></returns>
        internal static Image[] getFrames(Image originalImg)
        {
            int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);
            Image[] frames = new Image[numberOfFrames];

            for (int i = 0; i < numberOfFrames; i++)
            {
                originalImg.SelectActiveFrame(FrameDimension.Time, i);
                frames[i] = ((Image)originalImg.Clone());
            }

            return frames;
        }
    }
}
