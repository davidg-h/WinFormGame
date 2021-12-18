using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms.Gamecode
{
    class SpriteHandler
    {
        Image[] sprites;
        int currentAnimationSprite = 0;
        int nextAnimationStep = 0;
        public Image CurrentSprite {
            get{
                return sprites[currentAnimationSprite];
            }
        }

        public SpriteHandler(Image gif)
        {
            sprites = getFrames(gif);
        }

        public void updateSpriteEvery5thTimeCalled()
        {
            if(nextAnimationStep % 5 == 0)
            {
                currentAnimationSprite = (currentAnimationSprite + 1) % sprites.Length;
                nextAnimationStep = 0;
            }
            nextAnimationStep++;
        }
        public void updateSpriteEvery3thTimeCalled()
        {
            if (nextAnimationStep % 3 == 0)
            {
                currentAnimationSprite = (currentAnimationSprite + 1) % sprites.Length;
                nextAnimationStep = 0;
            }
            nextAnimationStep++;
        }
        public void updateSpriteEveryTimeCalled()
        {
            currentAnimationSprite = (currentAnimationSprite + 1) % sprites.Length;
        }

        public static Image[] getFrames(Image originalImg)
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
