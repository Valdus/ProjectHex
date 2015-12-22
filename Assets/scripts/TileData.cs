using UnityEngine;

namespace Assets.scripts
{
    public class TileData
    {
        readonly int _xLocation;
        readonly int _yLocation;
        readonly int _zLocation;
        readonly Material _material;

        public TileData(int xLocation, int yLocation, int zLocation, Material material)
        {
            _xLocation = xLocation;
            _yLocation = yLocation;
            _zLocation = zLocation;
            _material = material;
        }

        public int GetX()
        {
            return _xLocation;
        }

        public int GetZ()
        {
            return _zLocation;
        }

        public int GetY()
        {
            return _yLocation;
        }

        public Material GetMaterial()
        {
            return _material;
        }
    }
}