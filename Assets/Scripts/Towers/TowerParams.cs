using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerParams
{
    //Arrow
    public static float[] ARROW_TOWER_RADIUS = new float[] { 15, 17, 19, 17, 22 };
    public static float[] ARROW_TOWER_DAMEGE = new float[] { 10, 20, 40, 30, 50 };
    //Cannon
    public static float[]  CANNON_TOWER_RADIUS = new float[] { 16, 18, 20, 25, 22 };
    public static float[] CANNON_PROJESCTILE_DAMEGE = new float[] { 20, 35, 60, 80, 150 };
    public static float[] CANN0N_EXPLOSION_RADIUS = new float[] { 10, 12, 14, 20, 24 };
    //Magic
    public static float[] MAGIC_TOWER_RADIUS = new float[] {15,17,19,22,17 };
    public static float[] MAGIC_TOWER_DAMAGE = new float[] {1,2,4,10,4 };
    public static float[] MAGIC_TOWER_SLOWNESS = new float[] {0.3F,0.5F,0.65F,0.9F,0.6F };
}
