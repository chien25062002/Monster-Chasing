
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
        x = mSystem.ParseFloat(data[4]);
        y = mSystem.ParseFloat(data[5]);
        z = mSystem.ParseFloat(data[6]);
        align = int.Parse(data[7]);
        charX = mSystem.ParseFloat(data[8]);
        charY = mSystem.ParseFloat(data[9]);
        charZ = mSystem.ParseFloat(data[10]);
    }
}
