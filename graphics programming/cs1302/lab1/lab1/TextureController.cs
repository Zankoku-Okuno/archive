using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace lab1
{
    public class TextureController
    {
        private Dictionary<string, Texture2D> sprite_textures = new Dictionary<string, Texture2D>();

        public Texture2D this[string key]
        {
            get { return sprite_textures[key]; }
            set { sprite_textures[key] = value; }
        }

    }
}
