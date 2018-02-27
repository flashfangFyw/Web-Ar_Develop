using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using Mapbox.Unity.Map;
using Mapbox.Map;


/* 
    Author:     fyw 
    CreateDate: 2018-02-24 10:40:02 
    Desc:       自定义RangeTileProvider 添加动态加载新的地图区域块的方法
*/


public class MyRangeTileProvider : RangeTileProvider
{
    //[SerializeField]
    //private int _west;
    //[SerializeField]
    //private int _north;
    //[SerializeField]
    //private int _east;
    //[SerializeField]
    //private int _south;
    //private 
    public override void OnInitialized()
    {
        var centerTile = TileCover.CoordinateToTileId(_map.CenterLatitudeLongitude, _map.AbsoluteZoom);
        AddTile(new UnwrappedTileId(_map.AbsoluteZoom, centerTile.X, centerTile.Y));
        for (int x = (int)(centerTile.X - _west); x <= (centerTile.X + _east); x++)
        {
            for (int y = (int)(centerTile.Y - _north); y <= (centerTile.Y + _south); y++)
            {
                AddTile(new UnwrappedTileId(_map.AbsoluteZoom, x, y));
            }
        }
    }
    public void UpdateNewTile()
    {
        
    }
}
