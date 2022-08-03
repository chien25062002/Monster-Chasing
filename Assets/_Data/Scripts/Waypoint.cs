
public class Waypoint
{
    public const int ALIGN_LEFT = 0;
    public const int ALIGN_CENTER = 1;
    public const int ALIGN_RIGHT = 2;
    public string currentMapName;
    public int currentMapId;
    public string nextMapName;
    public int nextMapId;

    public float x;
    public float y;
    public float z;
    public int align;

    public float charX;
    public float charY;
    public float charZ;

    public Waypoint(string[] data) {
        currentMapName = data[0];
        currentMapId = int.Parse(data[1]);
        nextMapName = data[2];
        nextMapId = int.Parse(data[3]);
        x = float.Parse(data[4]);
        y = float.Parse(data[5]);
        z = float.Parse(data[6]);
        align = int.Parse(data[7]);
        charX = float.Parse(data[8]);
        charY = float.Parse(data[9]);
        charZ = float.Parse(data[10]);
    }
}
