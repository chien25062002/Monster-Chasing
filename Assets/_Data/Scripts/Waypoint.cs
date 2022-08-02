
public class Waypoint
{
    public string currentMapName;
    public int currentMapId;
    public string nextMapName;
    public int nextMapId;

    public float x;
    public float y;
    public float z;

    public Waypoint(string[] data) {
        currentMapName = data[0];
        currentMapId = int.Parse(data[1]);
        nextMapName = data[2];
        nextMapId = int.Parse(data[3]);
        x = float.Parse(data[4]);
        y = float.Parse(data[5]);
        z = float.Parse(data[6]);
    }
}
