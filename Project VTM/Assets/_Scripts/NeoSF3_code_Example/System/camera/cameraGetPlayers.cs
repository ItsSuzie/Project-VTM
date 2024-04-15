/*using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class cameraGetPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ProCamera2D cam = GetComponent<ProCamera2D>();
        cam.RemoveAllCameraTargets();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; ++i)
        {
            cam.AddCameraTarget(players[i].transform);
        }
    }
}
*/